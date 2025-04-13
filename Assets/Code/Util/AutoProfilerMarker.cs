using Unity.Profiling;

namespace CodePractice
{
    public ref struct AutoProfilerMarker
    {
        public ProfilerMarker Marker;

        public AutoProfilerMarker(string name)
        {
            Marker = new ProfilerMarker(name);
            Marker.Begin();
        }

        public void Dispose()
        {
            Marker.End();
        }
    }
}