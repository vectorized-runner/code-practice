namespace CodePractice
{
    public unsafe interface IAllocator
    {
        public void* Alloc(int size);
        public void Free();
        public void Resize(int newSize);
    }
}