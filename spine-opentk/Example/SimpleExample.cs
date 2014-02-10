using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using System.Diagnostics;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using Spine;

namespace spine_opentk_example
{
	public class SimpleExample : GameWindow
	{
        const int fps_frames = 50;
        private readonly List<long> ftime;
        private readonly Stopwatch stopwatch;
        private long lastTime;
        private bool altDown = false;

		Avatar Spineboy;

		public SimpleExample() : base(1024, 768)
        {
            ftime = new List<long>(fps_frames);
            stopwatch = new Stopwatch();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        /// <summary>
        /// Occurs when a key is pressed.
        /// </summary>
        /// <param name="sender">The KeyboardDevice which generated this event.</param>
        /// <param name="e">The key that was pressed.</param>
        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == global::OpenTK.Input.Key.Escape)
                Exit();
            else if (e.Key == global::OpenTK.Input.Key.AltLeft)
                altDown = true;
            else if (altDown && e.Key == global::OpenTK.Input.Key.Enter)
                if (WindowState == WindowState.Fullscreen)
                    WindowState = WindowState.Normal;
                else
                    WindowState = WindowState.Fullscreen;
        }

        void Keyboard_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            altDown = false;
        }

        void Mouse_ButtonDown(object sender, MouseButtonEventArgs args)
        {
        }

        void Mouse_ButtonUp(object sender, MouseButtonEventArgs args)
        {
        }

        void Mouse_Move(object sender, MouseMoveEventArgs args)
        {
        }

        void Mouse_Wheel(object sender, MouseWheelEventArgs args)
        {
        }

        /// <summary>
        /// Setup OpenGL and load resources here.
        /// </summary>
        /// <param name="e">Not used.</param>
        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.MidnightBlue);
			GL.Enable(EnableCap.Texture2D);
			
			GL.Enable(EnableCap.Blend);
			GL.BlendEquation(BlendEquationMode.FuncAdd);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.AlphaFunc(AlphaFunction.Always, 0);

			Spineboy = new Avatar(@"data\spineboy");
			Spineboy.state.SetAnimation(0, "walk", true);

            stopwatch.Restart();
            lastTime = 0;
        }

        /// <summary>
        /// Respond to resize events here.
        /// </summary>
        /// <param name="e">Contains information on the new GameWindow size.</param>
        /// <remarks>There is no need to call the base implementation.</remarks>
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, Width, Height, 0, -1, 1);
        }

        /// <summary>
        /// Add your game logic here.
        /// </summary>
        /// <param name="e">Contains timing information.</param>
        /// <remarks>There is no need to call the base implementation.</remarks>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (ftime.Count == fps_frames)
                ftime.RemoveAt(0);

			Spineboy.Update((stopwatch.ElapsedMilliseconds - lastTime) / 1000.0f);

            ftime.Add(stopwatch.ElapsedMilliseconds - lastTime);
            lastTime = stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Add your game rendering code here.
        /// </summary>
        /// <param name="e">Contains timing information.</param>
        /// <remarks>There is no need to call the base implementation.</remarks>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);

			Spineboy.Draw2D(500, 600);

            SwapBuffers();
        }

        /// <summary>
        /// Entry point of this example.
        /// </summary>
        [STAThread]
        public static void Main()
        {
			using (SimpleExample example = new SimpleExample())
            {
                example.Title = "Gwen-DotNet OpenTK test";
                example.VSync = VSyncMode.On;
                example.Run(30, 30);
                example.TargetRenderFrequency = 60;
            }
        }
	}
}
