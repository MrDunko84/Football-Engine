using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FM.Core.AI;

namespace FM.UI.Test
{
    public partial class Form1 : Form
    {

        private readonly Random _rnd = new Random();
        
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
            
            using (var brush = new SolidBrush(GetRandomColor()))
            {
                g.FillRectangle(brush, rect);
            }
            g.DrawRectangle(pen, rect);

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
        }
    }
}
