namespace CodePractice
{
    public unsafe interface IAllocator
    {
        public void* Alloc(int size);
        public void Clear();
        public void Free();
        public void Resize(int newSize);
    }
}