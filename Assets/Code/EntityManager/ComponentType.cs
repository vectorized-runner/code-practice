namespace CodePractice
{
    public struct ComponentType
    {
        public int TypeIndex;

        public static ComponentType Get<T>() where T : unmanaged, IComponent
        {
            return new ComponentType
            {
                TypeIndex = TypeManager.GetTypeIndex<T>()
            };
        }
    }
}