using System;
using System.Collections.Generic;
using Code;
using Unity.Mathematics;
using UnityEngine;

namespace SuperMetalSoldier
{
	public static class Extensions
	{
		// ReSharper disable once InconsistentNaming
		public static float3 x0y(this float2 f)
		{
			return new float3(f.x, 0f, f.y);
		}
	}

	public enum AnimationState
	{
		Idle,
		Walk,
		Run,
	}

	[Serializable]
	public struct PlayerData
	{
		public float3 Position;
		public float3 Velocity;
		public quaternion Rotation;
		public float JumpDuration;
		public float JumpLandedTime;
		public bool IsGrounded;
		public bool IsApplyingGravity;
		public float LastGroundedTime;
		public AnimationState AnimationState;
	}

	[Serializable]
	public struct EnemyData
	{
		public float3 Position;
		public quaternion Rotation;
		public float3 Velocity;
	}

	public struct EnemyManagedData
	{
		public GameObject Go;
		public Rigidbody Rb;
	}

	public struct PlayerManagedData
	{
		public GameObject Go;
		public Rigidbody Rb;
		public Animator Animator;
	}

	public class SuperMetalGame : MonoBehaviour
	{
		// To be assigned
		public EnemyAuthoring[] EnemyAuthorings;
		public SuperMetalConfig Config;
		public GameObject PlayerRender;
		//
		
		public PlayerData Player;
		public EntityGroup<EnemyData> Enemies;
		public Dictionary<Id, EnemyManagedData> EnemyIdToManaged = new();
		public IdManager IdManager;
		
		public static SuperMetalGame Instance { get; private set; }

		private PlayerManagedData _playerManaged;
		
		private static readonly int _animatorSpeedId = Animator.StringToHash("Speed");
		private float _animSpeedParameter;
		private Transform _cameraTransform;
		private float _playerRotationCurrentVelocity;
		
		private void Awake()
		{
			Instance = this;
		}

		private void Start()
		{
			_cameraTransform = CameraController.Instance.CameraRender.transform;
			
			// Init Player
			{
				Player.Position = Config.PlayerInitialPos;
				Player.AnimationState = AnimationState.Idle;

				_playerManaged = new PlayerManagedData
				{
					Go = PlayerRender,
					Animator = PlayerRender.GetComponentInChildren<Animator>(),
					Rb = PlayerRender.GetComponent<Rigidbody>(),
				};
			}

			// Init enemies
			{
				IdManager = IdManager.Create();
				
				Enemies = new EntityGroup<EnemyData>(32);
				
				// For simplicity, for now
				EnemyAuthorings = FindObjectsByType<EnemyAuthoring>(FindObjectsSortMode.None);

				foreach (var authoring in EnemyAuthorings)
				{
					var id = IdManager.CreateId();
					var initialPos = authoring.transform.position;
					var initialRot = authoring.transform.rotation;
					
					Enemies.Add(id, new EnemyData
					{
						Position = initialPos,
						Rotation = initialRot,
						Velocity = float3.zero,
					});
					
					var enemyGo = Instantiate(Config.EnemyPrefab, initialPos, initialRot);
					var enemyRb = enemyGo.GetComponent<Rigidbody>();
					EnemyIdToManaged.Add(id, new EnemyManagedData
					{
						Go = enemyGo,
						Rb = enemyRb,
					});
					
					// Authoring isn't required at Runtime
					Destroy(authoring.gameObject);
				}
			}
		}

		private void OnDestroy()
		{
			Enemies.Dispose();
			IdManager.Dispose();
		}
		
		private float2 GetPlayerMoveInput()
		{
			var result = float2.zero;

			if (Input.GetKey(KeyCode.W))
			{
				result += new float2(0, 1);
			}
			else if (Input.GetKey(KeyCode.S))
			{
				result += new float2(0, -1);
			}

			if (Input.GetKey(KeyCode.A))
			{
				result += new float2(-1, 0);
			}
			else if (Input.GetKey(KeyCode.D))
			{
				result += new float2(1, 0);
			}

			return result;
		}

		private void Update()
		{
			var dt = Time.deltaTime;
			var time = Time.time;

			// Player Sync back from physics engine
			{
				var playerRb = _playerManaged.Rb;
				Player.Position = playerRb.position;
				Player.Rotation = playerRb.rotation;
				Player.Velocity = playerRb.linearVelocity;
			}

			// Enemy sync back from Physics Engine
			{
				foreach (var (id, managed) in EnemyIdToManaged)
				{
					ref var enemy = ref Enemies.ElementAt(id);
					var rb = managed.Rb;
					
					enemy.Position = rb.position;
					enemy.Rotation = rb.rotation;
					enemy.Velocity = rb.linearVelocity;
				}
			}

			// This is to track movement over time
			Debug.DrawRay(Player.Position, -math.up(), Color.red, Config.PlayerMovementTrailDuration);

			// IsGrounded logic
			{
				const float upOffset = 0.1f;
				var raycastPos = Player.Position + math.up() * (upOffset - Config.PlayerBodyLength);
				var isGrounded = Physics.Raycast(raycastPos, -math.up(), Config.GroundedDistanceCheck, -1,
					QueryTriggerInteraction.Ignore);

				Debug.DrawRay(raycastPos, -math.up() * Config.GroundedDistanceCheck, Color.yellow, 0.1f);

				if (!Player.IsGrounded && isGrounded)
				{
					Player.LastGroundedTime = time;
				}

				Player.IsGrounded = isGrounded;
			}

			// Player Jump
			{
				var isJumpOutOfCooldown = time - Player.LastGroundedTime > Config.PlayerJumpCooldownAfterGrounded;
				var canJump = Player.IsGrounded && isJumpOutOfCooldown;
				if (canJump && Input.GetKeyDown(KeyCode.Space))
				{
					Player.Velocity += math.up() * Config.JumpPushAmount;
				}
			}

			// Player Gravity
			{
				if (!Player.IsGrounded)
				{
					Player.Velocity += -math.up() * Config.Gravity * dt;
					Player.IsApplyingGravity = true;
				}
				else
				{
					Player.IsApplyingGravity = false;
				}
			}

			var runInput = Input.GetKey(KeyCode.LeftShift);

			// Player Update Pos
			{
				var moveInput = GetPlayerMoveInput();
				var normalizedInputDir = math.normalize(moveInput);
				var hasMoveInput = math.lengthsq(moveInput) > 0.01f;

				if (hasMoveInput)
				{
					var targetRotation = math.degrees(math.atan2(normalizedInputDir.x, normalizedInputDir.y)) + _cameraTransform.eulerAngles.y;
					var currentTransformY = math.degrees(math.Euler(Player.Rotation)).y;
					var rotation = Mathf.SmoothDampAngle(currentTransformY, targetRotation, ref _playerRotationCurrentVelocity, Config.PlayerRotationSmoothTime);
					Player.Rotation = Quaternion.Euler(0f, rotation, 0f);
					
					// My original implementation -- I guess both could be used
					// var lookPosition = Player.Position + moveInput.x0y();
					// var wantedRotation = quaternion.LookRotation(moveInput.x0y(), math.up());
					// var rotateDegrees = Config.PlayerTurnAnglePerSec * dt;
					// var rotation = Quaternion.RotateTowards(Player.Rotation, wantedRotation, rotateDegrees);
					// Debug.DrawRay(lookPosition, math.up(), Color.cyan, 10f);
					// Player.Rotation = rotation;
				}
				
				var acceleration = runInput ? Config.PlayerRunAcceleration : (hasMoveInput ? Config.PlayerWalkAcceleration : Config.PlayerDeceleration);
				var targetSpeed = runInput ? Config.PlayerRunMaxHorizontalSpeed : (hasMoveInput ? Config.PlayerWalkMaxHorizontalSpeed : 0f);
				var currentSpeed = math.length(Player.Velocity.xz);
				var moveDir = math.forward(Player.Rotation).xz;
				var newSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * dt);
					
				Player.Velocity.xz = moveDir * newSpeed;
			}

			// Player Update Anim
			{
				var previousState = Player.AnimationState;

				if (runInput)
				{
					Player.AnimationState = AnimationState.Run;
				}
				else
				{
					// Check velocity
					const float walkVelocityThreshold = 0.1f;
					var horizontalVelocity = math.length(Player.Velocity.xz);
					if (horizontalVelocity < walkVelocityThreshold)
					{
						Player.AnimationState = AnimationState.Idle;
					}
					else
					{
						Player.AnimationState = AnimationState.Walk;
					}
				}

				// Sync to Animator
				var newState = Player.AnimationState;
				if (previousState != newState)
				{
					_playerManaged.Animator.SetTrigger(AnimationStateToStr(newState));
				}
			}

			// Player Sync to Physics
			{
				_playerManaged.Rb.linearVelocity = Player.Velocity;
				_playerManaged.Rb.rotation = Player.Rotation;
			}

			// Enemy Sync to Physics
			{
				foreach (var (id, managed) in EnemyIdToManaged)
				{
					ref var enemy = ref Enemies.ElementAt(id);
					var rb = managed.Rb;
					rb.position = enemy.Position;
					rb.rotation = enemy.Rotation;
					rb.linearVelocity = enemy.Velocity;
				}
			}

			// Enemy Sync Render Position
			{
				for (var index = 0; index < Enemies.Entities.Length; index++)
				{
					ref var enemy = ref Enemies.Entities.ElementAt(index);
					var id = Enemies.Ids[index];
					var enemyTf = EnemyIdToManaged[id].Go.transform;
					enemyTf.transform.position = enemy.Position;
					enemyTf.transform.rotation = enemy.Rotation;
				}
			}
			
			// Player Animation Speed update
			_animSpeedParameter = math.length(Player.Velocity.xz);
			_playerManaged.Animator.SetFloat(_animatorSpeedId, _animSpeedParameter);
		}

		private string AnimationStateToStr(AnimationState state)
		{
			switch (state)
			{
				case AnimationState.Idle:
					return "Idle";
				case AnimationState.Walk:
					return "Walk";
				case AnimationState.Run:
					return "Run";
				default:
					throw new ArgumentOutOfRangeException(nameof(state), state, null);
			}
		}
	}
}