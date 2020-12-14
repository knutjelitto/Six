using System;

namespace Six.Support
{
    public class Disposable : IDisposable
    {
        public Disposable(Action onDispose)
        {
            OnDispose = onDispose;
        }

        public Action OnDispose { get; }

        public void Dispose()
        {
            OnDispose();
        }
    }
}
