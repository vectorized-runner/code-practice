using System;

namespace Code
{
	public unsafe struct Array<T> where T : unmanaged, IDisposable
	{
		public T* Ptr;
		public int Count;

		public Array(int count)
		{
			Ptr = Util.Malloc<T>(count);
			Count = count;
		}

		public void Dispose()
		{
			if (Ptr != null)
			{
				Util.Free(Ptr);
				this = default;
			}
		}
	}
}