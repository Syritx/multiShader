using System;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;

namespace shaderTest {
    class Program {

        static void Main(string[] args) {

            GameWindowSettings gameWindowSettings = new GameWindowSettings();
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(1000, 720),
                Title = "Hello",
                APIVersion = new Version(3, 2),
                Flags = ContextFlags.ForwardCompatible,
                Profile = ContextProfile.Core,
                StartVisible = true,
                StartFocused = true
            };

            new MainWindow(gameWindowSettings, nativeWindowSettings);
        }
    }
}