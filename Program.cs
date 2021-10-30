using System;
using ImGuiGL;
using ImGuiNET;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Game
{
    class Program
    {
        static private GameWindow glWindow;
        static private RenderWindow renderWindow;
        static private ContextSettings renderWindowContext;
        static private ImGuiController imgui;

        static private Clock clock;
        static private Time timePerTick = Time.Zero;
        static private Time timeSinceLastUpdate = Time.Zero;
        static private Time frameTime = Time.Zero;

        static void Main(string[] args)
        {
            GameWindowSettings glWindowSettings = new GameWindowSettings();
            NativeWindowSettings glNativeWindowSettings = new NativeWindowSettings();
            glNativeWindowSettings.StartVisible = false;
            glWindow = new GameWindow(glWindowSettings, glNativeWindowSettings);

            renderWindowContext = new ContextSettings();
            renderWindowContext.AttributeFlags = ContextSettings.Attribute.Default;
            renderWindow = new RenderWindow(new VideoMode(800, 600), "C# SFML + OPENGL", Styles.Default, renderWindowContext);

            renderWindow.Resized += RenderWindow_Resized;
            renderWindow.Closed += RenderWindow_Closed;
            renderWindow.TextEntered += RenderWindow_TextEntered;
            renderWindow.KeyPressed += RenderWindow_KeyPressed;
            renderWindow.MouseWheelScrolled += RenderWindow_MouseWheelScrolled;

            glWindow.Size = new OpenTK.Mathematics.Vector2i((int)renderWindow.Size.X, (int)renderWindow.Size.Y);
            glWindow.Location = new OpenTK.Mathematics.Vector2i(renderWindow.Position.X, renderWindow.Position.Y);
            imgui = new ImGuiController((int)renderWindow.Size.X, (int)renderWindow.Size.Y);

            renderWindow.SetActive(false);
            CreateFixedTimeFrame();


            while (renderWindow.IsOpen)
            {
                renderWindow.DispatchEvents();

                frameTime = clock.Restart();
                timeSinceLastUpdate += frameTime;
                imgui.Update(ref renderWindow, frameTime.AsSeconds());

                while (timeSinceLastUpdate > timePerTick)
                {
                    timeSinceLastUpdate -= timePerTick;
                    // fixed update
                }

                // draw
                renderWindow.SetActive(true);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
                ImGui.ShowDemoWindow();
                imgui.Render();
                glWindow.SwapBuffers();
                renderWindow.Display();
            }
        }

        private static void CreateFixedTimeFrame()
        {
            clock = new Clock();
            timePerTick = Time.FromSeconds(1.0f / 60.0f);
            timeSinceLastUpdate = Time.Zero;
            frameTime = Time.Zero;
            timeSinceLastUpdate += clock.Restart();
        }

        private static void RenderWindow_KeyPressed(object sender, KeyEventArgs e)
        {
            // ...
        }
        private static void RenderWindow_TextEntered(object sender, TextEventArgs e)
        {
            imgui.PressChar(e.Unicode[0]);
        }
        private static void RenderWindow_MouseWheelScrolled(object sender, MouseWheelScrollEventArgs e)
        {
            imgui.MouseScroll(e.X, e.Y, e.Delta);
        }
        private static void RenderWindow_Closed(object sender, EventArgs e)
        {
            renderWindow.Close();
        }
        private static void RenderWindow_Resized(object sender, SizeEventArgs e)
        {
            GL.Viewport(0, 0, (int)e.Width, (int)e.Height);
            imgui.WindowResized((int)e.Width, (int)e.Height);
        }
    }
}
