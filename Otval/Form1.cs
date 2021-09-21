using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otval
{
    public partial class Form1 : Form
    {
        private Bitmap _screenshot;
        private Graphics _canvas;
        private Random _rnd;

        public Form1()
        {
            InitializeComponent();

            Start();
        }

        private void Start()
        {
            _rnd = new Random();

            _canvas = Picture.CreateGraphics();

            Bitmap myBitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Picture.Image = myBitmap;
            _canvas = Graphics.FromImage(Picture.Image);

            this.WindowState = FormWindowState.Minimized;
            _canvas.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
            _screenshot = new Bitmap(Picture.Image);
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.J)
            {
                Application.Exit();
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            Picture.Invalidate();
        }

        private void Picture_Paint(object sender, PaintEventArgs e)
        {
            _canvas.Clear(Color.Black);

            int SpanX, SpanY, SpanHeight;


            if (_rnd.Next(0, 31) == 0)
            {
                _canvas.Clear(Color.Fuchsia);
            }
            else
            {
                _canvas.DrawImage(_screenshot, _rnd.Next(-5, 5), 0);
            }

            for (int i = 0; i < 11; i++)
            {
                SpanHeight = _rnd.Next(4, 123);
                if (_rnd.Next(0, 5) == 0)
                {
                    SpanX = _rnd.Next(-60, 60);
                }
                else
                {
                    SpanX = _rnd.Next(-15, 15);
                }
                SpanY = _rnd.Next(0, _screenshot.Height);

                _canvas.DrawImage(_screenshot, new RectangleF(SpanX, SpanY, _screenshot.Width, SpanHeight), new RectangleF(0, SpanY, _screenshot.Width, SpanHeight), GraphicsUnit.Pixel);
            }

            for (int j = 0; j < _rnd.Next(0, 7); j++)
            {
                Pen Pen = new Pen(Color.White);

                SpanY = _rnd.Next(_screenshot.Height);

                if (_rnd.Next(0, 5) == 0)
                {
                    Pen.Color = Color.Fuchsia;
                }
                else
                {
                    Pen.Color = Color.Lime;
                }

                for (int i = 0; i < _rnd.Next(0, 40); i++)
                {
                    if (_rnd.Next(0, 3) == 0)
                    {
                        _canvas.DrawLine(Pen, 0, SpanY + i, _screenshot.Width, SpanY + i);
                    }
                }
            }
        }
    }
}
