using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.Xna.Framework;

namespace FM.Core.AI
{

    public interface IMovement
    {
        Vector2 Location { get; }
        Vector2 Destination { get; }

        void SetStartLocation(Vector2 location);

        void SetSpeed(float speed);
        void SetDestination(Vector2 destination);
        void Update();

    }

    public class MovementHelper
    {
        public const int Scale = 3;

        public static readonly Random Rnd = new Random();

        public static Vector2 PlotPath(Vector2 start, Vector2 finish, double speed, GameTime gameTime)
        {
            if (Math.Abs(start.X - finish.X) < 1 && Math.Abs(start.Y - finish.Y) < 1) return finish;

            var deltaX = (finish.X - start.X);
            var deltaY = (finish.Y - start.Y);

            var delta = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));

            var lengthRatio = (float)(speed / delta);

            var extendedDeltaX = deltaX * lengthRatio * gameTime.ElapsedGameTime.Milliseconds;
            var extendedDeltaY = deltaY * lengthRatio * gameTime.ElapsedGameTime.Milliseconds;

            return new Vector2(start.X + extendedDeltaX,
                               start.Y + extendedDeltaY);

        }


        public static Vector2 PlotPath2(Vector2 start, Vector2 finish, double speed, GameTime gameTime)
        {

            var deltaX = (finish.X - start.X);
            var deltaY = (finish.Y - start.Y);

            var distance = (float)Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
            var directionX = ((deltaX) / distance) * gameTime.ElapsedGameTime.Milliseconds;
            var directionY = ((deltaY) / distance) *  gameTime.ElapsedGameTime.Milliseconds;

            return new Vector2(start.X + directionX,
                               start.Y + directionY);

        }

    }
}
