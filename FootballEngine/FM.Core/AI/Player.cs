using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FM.Core.AI
{
    public class Player
        : IMovement
    {
        public Player(int number)
        {
            Number = number;
            Location = new PointF(0, 0);
            Destination = new PointF(0, 0);
            _speed = 1.2f;
        }

        public int Number { get; }
        public PointF Location { get; private set; }

        /// <inheritdoc />
        public PointF Destination { get; private set; }

        private float _speed;

        /// <inheritdoc />
        public override string ToString()
        {
            return Number.ToString();
        }

        /// <inheritdoc />
        public void SetStartLocation(PointF location)
        {
            Location = new PointF(location.X, location.Y);
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        /// <inheritdoc />
        public void SetDestination(PointF destination)
        {
            Destination = new PointF(destination.X, destination.Y);
        }


        public void Update()
        {
            Location = MovementHelper.PlotPath(Location, Destination, _speed);

            if (Math.Abs(Location.X - Destination.X) <= 0 && Math.Abs(Location.Y - Destination.Y) <= 0)
            {
                // change detination
                SetDestination(new PointF(MovementHelper.Rnd.Next(0, 80 * MovementHelper.Scale),
                                          MovementHelper.Rnd.Next(0, 120 * MovementHelper.Scale)));

                SetSpeed((float)MovementHelper.Rnd.Next(6, 20) / 10);

            }

        }

    }
}
