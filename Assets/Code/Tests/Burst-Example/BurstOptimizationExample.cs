using System.Runtime.CompilerServices;
using NUnit.Framework;
using Unity.Burst;
using Unity.PerformanceTesting;
using Random = Unity.Mathematics.Random;

public unsafe class BurstOptimizationExample
{
	private const int arrayCount = 1_000_000;

	[Test, Performance]
	public void BurstAdd()
	{
		int[] first = null;
		int[] second = null;
		Random random;

		Measure.Method(() =>
			{
				fixed (int* firstPtr = first)
				fixed (int* secondPtr = second)
				{
					BurstAdd_Impl(firstPtr, secondPtr, arrayCount);
				}
			})
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

	[Test, Performance]
	public void DefaultAdd()
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

	[MethodImpl(MethodImplOptions.NoInlining)]
	private static void BurstAdd_Impl([NoAlias] int* a, [NoAlias] int* b, int count)
	{
		for (var i = 0; i < count; i++)
		{
			a[i] += b[i];
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private static void Add_Impl(int[] a, int[] b, int count)
	{
		for (var i = 0; i < count; i++)
		{
			a[i] += b[i];
		}
	}
}