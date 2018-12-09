using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Digger
{
    public class DiggerWindow : Form
    {
        private readonly Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();
        private readonly GameState gameState;
        private readonly HashSet<Keys> pressedKeys = new HashSet<Keys>();
        private int tickCount;

        
        public DiggerWindow(DirectoryInfo imagesDirectory = null)
        {
            gameState = new GameState();
            ClientSize = new Size(
                GameState.ElementSize * Game.MapWidth,
                GameState.ElementSize * Game.MapHeight + GameState.ElementSize);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            if (imagesDirectory == null)
                imagesDirectory = new DirectoryInfo("Images");
            foreach (var e in imagesDirectory.GetFiles("*.*"))
            {
                if (e.ToString() == "*.gif") // я пытался ("отрисовка" анимаций)
                    ImageAnimator.Animate(Image.FromFile(e.FullName), OnFrameChanged);
                else
                    bitmaps[e.Name] = (Bitmap)Image.FromFile(e.FullName);
            }
            var timer = new Timer();
            var timer1 = new Timer();
            timer.Interval = 15; //таймер тиков 
            timer.Tick += TimerTick;
            timer.Start();

            timer1.Interval = 1000 / 60; //таймер внутриигрового времени 
            timer1.Tick += GetTime;
            timer1.Start();
        }

        private void OnFrameChanged(object o, EventArgs e)
        {
            Invalidate();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "Digger";
            DoubleBuffered = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            pressedKeys.Add(e.KeyCode);
            Game.KeyPressed = e.KeyCode;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            pressedKeys.Remove(e.KeyCode);
            Game.KeyPressed = pressedKeys.Any() ? pressedKeys.Min() : Keys.None;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(0, GameState.ElementSize);
            e.Graphics.FillRectangle(
                Brushes.Black, 0, 0, GameState.ElementSize * Game.MapWidth,
                GameState.ElementSize * Game.MapHeight);
            foreach (var a in gameState.Animations)
                e.Graphics.DrawImage(bitmaps[a.Creature.GetImageFileName()], a.Location);
            e.Graphics.ResetTransform();
            e.Graphics.DrawString("Score " + Game.Scores.ToString(), new Font("Arial", 16), Brushes.Blue, 0, 0);
            e.Graphics.DrawString("Time " + Game.GameTime, new Font("Arial", 16), Brushes.Red, 150, 0);
            if (Game.IsOver)
                e.Graphics.DrawString("Game Over", new Font("Georgia", 32), Brushes.SteelBlue, Width / 2 - 125, Height / 2 - 70);
        }

        private void TimerTick(object sender, EventArgs args)
        {
            if (tickCount == 0) gameState.BeginAct(); 
            foreach (var e in gameState.Animations)
                e.Location = new Point(e.Location.X + 4 * e.Command.DeltaX, e.Location.Y + 4 * e.Command.DeltaY);
            if (tickCount == 7)
                gameState.EndAct();
            tickCount++;
            if (tickCount == 8) tickCount = 0;
            Invalidate();
        }

        private void GetTime(object sender, EventArgs args)
        {
            if (!Game.IsOver)
            {
                Game.time++;
                Game.GameTime = String.Format("{0:d2}:{1:d2}:{2:d2}", Game.time / 3600, (Game.time / 60) % 60, Game.time % 60);
            }
            else Game.time = -1;

        }

    }
}