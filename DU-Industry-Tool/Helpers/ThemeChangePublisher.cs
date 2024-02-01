using System;
using System.Collections.Concurrent;
using Krypton.Toolkit;

namespace DU_Helpers
{
    public class ThemeChangePublisher : IDisposable
    {
        private ConcurrentBag<Action<KryptonCustomPaletteBase>> handlers = new ConcurrentBag<Action<KryptonCustomPaletteBase>>();
        private bool disposed = false;

        public void Subscribe(Action<KryptonCustomPaletteBase> handler)
        {
            if (handler != null)
            {
                handlers.Add(handler);
            }
        }

        public void PublishThemeChange(KryptonCustomPaletteBase palette)
        {
            foreach (var handler in handlers)
            {
                handler(palette);
            }
        }

        public void Dispose()
        {
            if (disposed) return;
            // atomically remove all elements from handlers
            handlers = new ConcurrentBag<Action<KryptonCustomPaletteBase>>();
            disposed = true;
        }
    }
}