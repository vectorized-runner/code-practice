using System;

namespace Code
{
	public unsafe struct Array<T> where T : unmanaged, IDisposable
	{
		private T* _ptr;
		private int _count;

		public Array(int count)
		{
			_ptr = Util.Malloc<T>(count);
			_count = count;
		}

		public void Dispose()
		{
			if (_ptr != null)
			{
				Util.Free(_ptr);
				this = default;
			}
		}
	}
}