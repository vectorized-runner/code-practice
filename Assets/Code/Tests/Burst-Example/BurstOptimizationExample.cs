using System.Runtime.CompilerServices;
using NUnit.Framework;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.PerformanceTesting;
using Random = Unity.Mathematics.Random;

[BurstCompile]
public static unsafe class BurstOptimizationExample
{
	private const int arrayCount = 1_000_000;

	[Test, Performance]
	public static void BurstAdd()
	{
		int[] first = null;
		int[] second = null;
		Random random;
		int* firstPtr = null;
		int* secondPtr = null;

		Measure.Method(() =>
			{
				BurstAdd_Impl(firstPtr, secondPtr, arrayCount);
			})
			.WarmupCount(10)
			.MeasurementCount(10)
			.IterationsPerMeasurement(10)
			.SetUp(() =>
			{
				first = new int[arrayCount];
				second = new int[arrayCount];
				firstPtr = (int*)UnsafeUtility.AddressOf(ref first[0]);
				secondPtr = (int*)UnsafeUtility.AddressOf(ref second[0]);
				random = new Random(123456);

				for (int i = 0; i < arrayCount; i++)
				{
					first[i] = random.NextInt();
					second[i] = random.NextInt();
				}
			})
			.Run();
	}

	[Test, Performance]
	public static void DefaultAdd()
	{
		int[] first = null;
		int[] second = null;
		Random random;

		Measure.Method(() => { Add_Impl(first, second, arrayCount); })
			.WarmupCount(10)
			.MeasurementCount(10)
			.IterationsPerMeasurement(10)
			.SetUp(() =>
			{
				first = new int[arrayCount];
				second = new int[arrayCount];
	
				random = new Random(123456);

				for (int i = 0; i < arrayCount; i++)
				{
					first[i] = random.NextInt();
					second[i] = random.NextInt();
				}
			})
			.Run();
	}

	[BurstCompile]
	private static void BurstAdd_Impl([NoAlias] int* a, [NoAlias] int* b, int count)
	{
		// Unity.Burst.CompilerServices.Aliasing.ExpectNotAliased(a, b);
		
		for (var i = 0; i < count; i++)
		{
			a[i] += b[i];
		}
	}

	private static void Add_Impl(int[] a, int[] b, int count)
	{
		for (var i = 0; i < count; i++)
		{
			a[i] += b[i];
		}
	}
}