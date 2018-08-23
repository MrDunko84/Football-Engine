using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FM.Core.AI
{

    public interface IMovement
    {
        PointF Location { get; }
        PointF Destination { get; }

        void SetStartLocation(PointF location);

        void SetSpeed(float speed);
        void SetDestination(PointF destination);
        void Update();

    }

    public class MovementHelper
    {
        public const int Scale = 4;

        public static readonly Random Rnd = new Random();

        public static PointF PlotPath(PointF start, PointF finish, double speed)
        {
            if (Math.Abs(start.X - finish.X) < 1 && Math.Abs(start.Y - finish.Y) < 1) return finish;

            var deltaX = (finish.X - start.X);
            var deltaY = (finish.Y - start.Y);

            var delta = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));

            var lengthRatio = (float)(speed / delta);

            var extendedDeltaX = deltaX * lengthRatio;
            var extendedDeltaY = deltaY * lengthRatio;

            return new PointF(start.X + extendedDeltaX,
                             start.Y + extendedDeltaY);

        }

    }
}
