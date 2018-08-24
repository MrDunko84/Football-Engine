using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FM.Core.AI;

namespace FM.UI.Test
{
    public partial class Form1 : Form
    {

        private readonly Random _rnd = new Random();
        private readonly List<Player> _players = new List<Player> { 
            new Player(1), 
            new Player(2), 
            new Player(3), 
            new Player(4), 
            new Player(5), 
            new Player(6), 
            new Player(7), 
            new Player(8), 
            new Player(9), 
            new Player(10), 
        };

        

        
        public Form1()
        {
            InitializeComponent();

            _players.ForEach((x) => {
                                x.SetStartLocation(new PointF(MovementHelper.Rnd.Next(0, 80 * MovementHelper.Scale),
                                          MovementHelper.Rnd.Next(0, 120 * MovementHelper.Scale)));

                                x.SetDestination(new PointF(MovementHelper.Rnd.Next(0, 80 * MovementHelper.Scale),
                                          MovementHelper.Rnd.Next(0, 120 * MovementHelper.Scale)));
                                                          });


        }

        private void Form1_Load(object sender, EventArgs e) { this.DoubleBuffered = true; }


        private Color GetRandomColor()
        {
            return Color.FromArgb(_rnd.Next(256), _rnd.Next(256), _rnd.Next(256));
        }


        private void DrawRegion(PitchRegion region, Pen pen,  Graphics g)
        {

            var rect = region.ToRectangle();
            
            
            rect.X = rect.X * MovementHelper.Scale;
            rect.Y = rect.Y * MovementHelper.Scale;
            rect.Width = rect.Width * MovementHelper.Scale;
            rect.Height = rect.Height * MovementHelper.Scale;

            

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

            _players.ForEach((p) => UpdatePlayer(e.Graphics, p));


        }

        private void UpdatePlayer(Graphics g, Player player)
        {
          g.DrawString(player.ToString(),
                new Font("Arial", 8),
                new SolidBrush(Color.Red),
                player.Location);

            player.Update();
        }

        private void timerPaint_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

    }
}
