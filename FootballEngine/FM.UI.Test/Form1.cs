using System;
using System.Drawing;
using System.Windows.Forms;
using FM.Core.AI;

namespace FM.UI.Test
{
    public partial class Form1 : Form
    {

        private readonly Random _rnd = new Random();
        private readonly Player _player = new Player(11);

        const int scale = 4;

        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) { this.DoubleBuffered = true; }


        private Color GetRandomColor()
        {
            return Color.FromArgb(_rnd.Next(256), _rnd.Next(256), _rnd.Next(256));
        }


        private void DrawRegion(PitchRegion region, Pen pen,  Graphics g)
        {

            var rect = region.ToRectangle();
            
            
            rect.X = rect.X * scale;
            rect.Y = rect.Y * scale;
            rect.Width = rect.Width * scale;
            rect.Height = rect.Height * scale;

            

            var solidColor = chkFillRegions.Checked ? GetRandomColor() : Color.White;
            
            using (var brush = new SolidBrush(solidColor))
            {
                g.FillRectangle(brush, rect);
            }

            g.DrawRectangle(pen, rect);
            g.DrawString(region.ToString(), 
                new Font("Arial", 10),
                new SolidBrush(Color.Black),
                new PointF(rect.X, rect.Y));

            var inner = region.GetRegions();

            inner?.ForEach((x) => DrawRegion(x, pen, g));

        }


        private void Form1_OnPaint(object sender, PaintEventArgs e)
        {
            var pitch = new Pitch(80, 120);
            

            e.Graphics.Clear(Color.White);



            using (var pen = new Pen(Color.Black)) 
            {
                DrawRegion(pitch.Region, pen, e.Graphics);
            }

            e.Graphics.DrawString(_player.ToString(),
                new Font("Arial", 10),
                new SolidBrush(Color.Black),
                _player.Location);

            _player.Update();

        }

        private void timerPaint_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void cmdSet_Click(object sender, EventArgs e)
        {
            _player.SetDestination(new Point(Int32.Parse(txtX.Text) * scale, Int32.Parse(txtY.Text) * scale));
        }

        private void cmdSpeed_Click(object sender, EventArgs e)
        {
            _player.SetSpeed(Double.Parse(txtSpeed.Text));
        }
    }
}
