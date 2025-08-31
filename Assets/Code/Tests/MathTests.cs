using System;
using System.Runtime.InteropServices;
using NUnit.Framework;
using Unity.Mathematics;

namespace Code.Tests
{
	public static class MathTests
	{
		[Test]
		public static void SignEqual()
		{
			Assert.IsTrue(math.all(math.abs(math.sign(new int2(-1, -1)) - new float2(-1, -1)) < 0.01f));
		}

		[Test]
		public static void SignEqual_Float()
		{
			Assert.IsTrue(math.all(math.abs(math.sign(new float2(-1, -1)) - new float2(-1, -1)) < 0.01f));
		}

		[Test]
		public static void Angle()
		{
			var angleBase = quaternion.AxisAngle(new float3(1, 0, 0), math.radians(0f));
			var angleRotated = quaternion.AxisAngle(new float3(1, 0, 0), math.radians(90f));
			var angleBetweenDeg = math.degrees(math.angle(angleBase, angleRotated));

			Assert.IsTrue(math.abs(angleBetweenDeg - 90f) < 0.01f);
		}
		
		[StructLayout(LayoutKind.Explicit)]
		struct Union
		{
			[FieldOffset(0)]
			public int x;
			
			[FieldOffset(0)]
			public float y;
		}
		
		[Test]
		public static void TestAsFloat()
		{
			Union u;
			u.x = 0;
			u.y = 1.0f;
			
			Assert.AreEqual(math.asfloat(u.x), u.y);
		}
		
		[Test]
		public static void TestCSum()
		{ 
			Assert.AreEqual(6, math.csum(new int3(1, 2, 3)));
		}
		
		[Test]
		public static void TestFMod()
		{
			Assert.IsTrue(math.abs(math.fmod(2.54f, 2.0f) - 0.54f) < 0.001f);
		}
		
		[Test]
		public static void TestFrac()
		{
			Assert.IsTrue(math.abs(math.frac(6.248f) - 0.248f) < 0.001f);
		}
		
		[Test]
		public static void TestHash()
		{
			Assert.AreNotEqual(math.hash(new float3(1.0f, 2.0f, 3.0f)), math.hash(new float3(1.00000001f, 2.0f, 3.0f)));
		}
		
		[Test]
		public static void TestMadd()
		{
			Assert.AreEqual(146, math.mad(3, 47, 5));
		}
		
		[Test]
		public static void TestRemap()
		{
			Assert.IsTrue(math.abs(math.remap(0f, 1f, 2.0f, 5.0f, 0.5f) - 3.5f) < 0.01f);
		}
	}
}