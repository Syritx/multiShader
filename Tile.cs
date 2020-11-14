using System;
using System.IO;
using System.Text;

using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;

using OpenTK.Graphics.OpenGL;

namespace shaderTest {
    class Tile {
        Shader shader;

        float[] vertices;

        int vertexArrayObject,
            vertexBufferObject;

        string vertexShaderSource;
        string fragmentShaderSource; 

        public Tile(float x, float y, Vector2 size) {
            vertices = new float[] {
                -size.X+x, -size.Y+y, 0, 
                -size.X+x,  size.Y+y, 0, 
                 size.X+x,  size.Y+y, 0,
                 size.X+x, -size.Y+y, 0  
            };

            vertexShaderSource = GetShaderSource("vertexShader.glsl");
            fragmentShaderSource = GetShaderSource("fragmentShader.glsl");

            shader = new Shader(vertexShaderSource, fragmentShaderSource);
            vertexBufferObject = GL.GenBuffer();
        }

        public void Render() {
            vertexArrayObject = GL.GenVertexArray();

            GL.VertexAttribPointer(0,3,VertexAttribPointerType.Float,false,3 * sizeof(float),0);
            GL.EnableVertexAttribArray(0);

            GL.BindVertexArray(vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            try { 
                shader.UseShader();
            } catch(Exception e1) {}
            GL.BindVertexArray(vertexArrayObject);
            GL.DrawArrays(PrimitiveType.TriangleFan, 0, vertices.Length/3);
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