using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FM.Core.AI
{
    public class Player
    {
        public Player(int number)
        {
            Number = number;
            Location = new Point(0, 0);
            _finish = new Point(0, 0);
            _speed = 1;
        }

        public int Number { get; }
        public Point Location { get; private set; }

        private Point _finish;
        private double _speed;

        /// <inheritdoc />
        public override string ToString()
        {
            return Number.ToString();
        }

        public void SetDestination(Point destination)
        {
            _finish = new Point(destination.X, destination.Y);
        }

        public void SetSpeed(double speed)
        {
            _speed = speed;
        }


        public void Update()
        {
            Location = PlotPath(Location, _finish, _speed);
        }

        private static Point PlotPath(Point start, Point finish, double speed)
        {
            if (start.X == finish.X && start.Y == finish.Y) return finish;

            var deltaX = (finish.X - start.X);
            var deltaY = (finish.Y - start.Y);

            var delta = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));

            var lengthRatio = speed / delta;

            var extendedDeltaX = deltaX * lengthRatio;
            var extendedDeltaY = deltaY * lengthRatio;

            Console.WriteLine(extendedDeltaX.ToString() + "-" + extendedDeltaY.ToString());

            return new Point(start.X + (int)Math.Round(extendedDeltaX),
                             start.Y + (int)Math.Round(extendedDeltaY));

        }

    }
}
