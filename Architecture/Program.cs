using System;
using System.Windows.Forms;

namespace Digger.Architecture
{
    internal static class Program
    {
        [STAThread]
        public static void Start()
        {
            Game.CreateMap();
            Application.Run(new DiggerWindow());
        }
        private static void Main()
        {
                Start();
        }
    }
}