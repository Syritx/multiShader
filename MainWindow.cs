using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;

using OpenTK.Graphics.OpenGL;

namespace shaderTest {
    class MainWindow : GameWindow
    {
        List<Tile> tiles = new List<Tile>();

        NativeWindowSettings nativeWindowSettings;
        public MainWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings) {

            this.nativeWindowSettings = nativeWindowSettings;

            for (int i = -10; i <= 10; i++) {
                float a = (float)i/7;
                tiles.Add(new Tile(a,a,new Vector2(.07f, .07f)));
            }

            Run();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            foreach(Tile t in tiles) {
                t.Render();
            }

            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            int width = nativeWindowSettings.Size.X;
            int height = nativeWindowSettings.Size.Y;

            Console.WriteLine(width + " " + height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            Matrix4 perspectiveMatrix =
                Matrix4.CreatePerspectiveFieldOfView(1, width / height, 1.0f, 2000.0f);

            GL.LoadMatrix(ref perspectiveMatrix);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.End();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0, 0, 0, 0);
            GL.Enable(EnableCap.DepthTest);
        }


        string GetShaderSource(string file) {
            string source = "";

            using (StreamReader reader = new StreamReader(file, Encoding.UTF8)) {
                source = reader.ReadToEnd();
            }
            return source;
        }
    }
}