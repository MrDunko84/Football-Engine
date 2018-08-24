using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.Xna.Framework;

namespace FM.Core.AI
{
    public class Player
        : IMovement
    {
        public Player(int number)
        {
            Number = number;
            Location = new Vector2(0, 0);
            Destination = new Vector2(0, 0);
            Speed = 0.2f;
        }

        public int Number { get; }
        public Vector2 Location { get; private set; }

        /// <inheritdoc />
        public Vector2 Destination { get; private set; }

        /// <inheritdoc />
        public float Speed { get; private set; }


        /// <inheritdoc />
        public override string ToString()
        {
            return Number.ToString();
        }

        /// <inheritdoc />
        public void SetStartLocation(Vector2 location)
        {
            Location = new Vector2(location.X, location.Y);
        }

        public void SetSpeed(float speed)
        {
            Speed = speed;
        }

        /// <inheritdoc />
        public void SetDestination(Vector2 destination)
        {
            Destination = new Vector2(destination.X, destination.Y);
        }


        public void Update()
        {
            //Location = MovementHelper.PlotPath(Location, Destination, _speed);

            //if (Math.Abs(Location.X - Destination.X) <= 0 && Math.Abs(Location.Y - Destination.Y) <= 0)
            //{
            //    // change detination
            //    SetDestination(new Vector2(MovementHelper.Rnd.Next(0, 80 * MovementHelper.Scale),
            //                              MovementHelper.Rnd.Next(0, 120 * MovementHelper.Scale)));

            //    SetSpeed((float)MovementHelper.Rnd.Next(6, 20) / 10);

            //}

        }

    }
}
