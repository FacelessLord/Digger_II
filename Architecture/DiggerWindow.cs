using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Digger.Architecture
{
    public class DiggerWindow : Form
    {
        private readonly Dictionary<string, Bitmap> _bitmaps = new Dictionary<string, Bitmap>();
        public GameState _gameState;
        public readonly HashSet<Keys> _pressedKeys = new HashSet<Keys>();
        private int _tickCount;

        public int _dx = 0;
        public int _dy = 0;
        
        public int _maxWindowWidth = 21;
        public int _maxWindowHeight = 21;



        public DiggerWindow(DirectoryInfo imagesDirectory = null)
        {
            _gameState = new GameState();
            Game._state = _gameState;
            Game._window = this;
            ClientSize = new Size(
                GameState.ElementSize * Math.Min(Game.MapWidth, _maxWindowWidth),
                GameState.ElementSize * Math.Min(Game.MapHeight, _maxWindowHeight) + GameState.ElementSize);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            if (imagesDirectory == null)
                imagesDirectory = new DirectoryInfo("Images");
            foreach (var e in imagesDirectory.GetFiles("*.png"))
            {
                //MessageBox.Show(e.FullName);
                if (e.ToString() == "*.gif") // я пытался ("отрисовка" анимаций)
                    ImageAnimator.Animate(Image.FromFile(e.FullName), OnFrameChanged);
                else
                    _bitmaps[e.Name] = (Bitmap) Image.FromFile(e.FullName);
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
            _pressedKeys.Add(e.KeyCode);
            Game._keyPressed = e.KeyCode;
            if (!Game._isOver && Game._player != null)
            {
                Game._player.OnKeyPressed(e);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            _pressedKeys.Remove(e.KeyCode);
            Game._keyPressed = _pressedKeys.Any() ? _pressedKeys.Min() : Keys.None;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //Console.Out.WriteLine($"tick {_tickCount} ddx {ddx}");
            int size = GameState.ElementSize;
            e.Graphics.TranslateTransform(0, size);
            e.Graphics.TranslateTransform(-_dx * size, -_dy * size);
            e.Graphics.FillRectangle(
                Brushes.Black, 0, 0, GameState.ElementSize * Game.MapWidth,
                GameState.ElementSize * Game.MapHeight);
            if (_tickCount == 0)
            {
                _dx = Math.Min(Game._locX - 10, Game.MapWidth - _maxWindowWidth);
                _dx = _dx < 0 ? 0 : _dx;
                _dy = Math.Min(Game._locY - 10, Game.MapHeight - _maxWindowHeight);
                _dy = _dy < 0 ? 0 : _dy;
            }
            foreach (var a in _gameState._animations)
            {
                if (a._creature.GetImageFileName() != null)
                {
                    e.Graphics.DrawImage(_bitmaps[a._creature.GetImageFileName()], a._location);
                }
            }


            e.Graphics.ResetTransform();
            e.Graphics.DrawString("Score " + Game._scores.ToString(), new Font("Arial", 16), Brushes.Blue, 0, 0);
            e.Graphics.DrawString("Time " + Game._gameTime, new Font("Arial", 16), Brushes.Red, 150, 0);
            if (Game._isOver)
                e.Graphics.DrawString("Game Over", new Font("Georgia", 32), Brushes.SteelBlue, Width / 2 - 125,
                    Height / 2 - 70);
        }

        private void TimerTick(object sender, EventArgs args)
        {

            if (_tickCount == 0)
            {
                _gameState.BeginAct();
            }

            foreach (var e in _gameState._animations)
                e._location = new Point(e._location.X + 4 * e._command._deltaX, e._location.Y + 4 * e._command._deltaY);

            if (_tickCount == 7)
                _gameState.EndAct();
            _tickCount++;
            if (_tickCount == 8) _tickCount = 0;
            Invalidate();
        }

        private void GetTime(object sender, EventArgs args)
        {
            if (!Game._isOver)
            {
                Game._time++;
                Game._gameTime = $"{Game._time / 3600:d2}:{(Game._time / 60) % 60:d2}:{Game._time % 60:d2}";
            }
            else
                Game._time = -1;
        }
    }
}