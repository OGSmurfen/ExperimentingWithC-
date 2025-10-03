using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace ConsoleApp1
{
    internal class Program
    {
        static int x = 100, y = 100;

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        static void Main(string[] args)
        {
            

            Timer timer = new Timer(DrawGraphics, null, 0, 100);
            while (true)
            {
                var key = Console.ReadKey(intercept: true);

                switch (key.Key)
                {
                    case ConsoleKey.RightArrow:
                        x += 10;
                        break;
                    case ConsoleKey.LeftArrow:
                        x -= 10;
                        break;
                    case ConsoleKey.UpArrow:
                        y -= 10;
                        break;
                    case ConsoleKey.DownArrow:
                        y += 10;
                        break;
                }
            }
        }

        [SupportedOSPlatform("windows6.1")]
        private static void DrawGraphics(object? state)
        {
            

            var hdc = GetDC(IntPtr.Zero);

            using (Graphics g = Graphics.FromHdc(hdc))
            {
                g.DrawImage(NoWindowDraw_TimerUsage.Properties.Resources.MyPosBuddy_removebg_preview, new Point(x, y));

                Pen skyBlue = new Pen(Brushes.DeepSkyBlue);
                skyBlue.Width = 5;
                skyBlue.LineJoin = System.Drawing.Drawing2D.LineJoin.Bevel;
                g.DrawRectangle(skyBlue, new Rectangle(500, 500, 150, 150));

                skyBlue.Dispose();


            }
        }
    }
}
