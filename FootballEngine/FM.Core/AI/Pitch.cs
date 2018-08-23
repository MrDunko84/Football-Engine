using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FM.Core.AI
{

    ///           (0,0)                           
    ///             -----------|---------|-----------
    ///             |          |         |          |
    ///             |          |---------|          |
    ///             |                               |
    ///             |                               |
    ///             |                               |
    ///             |                               |
    ///             |             NORTH             |
    ///             |                               |
    ///             |                               |
    ///             |                               |
    ///             |                               |
    ///             |                               |
    ///  [LENGTH]   |------------(25,50)------------|
    ///  100yards   |                               |
    ///             | WEST        CENTER       EAST |
    ///             |                               |
    ///             |                               |
    ///             |                               |
    ///             |             SOUTH             |
    ///             |                               |
    ///             |                               |
    ///             |                               |
    ///             |                               |
    ///             |          |---------|          |
    ///             |          |         |          |
    ///             -----------|---------|-----------
    ///                          [WIDTH]         (50,100)
    ///                          50yards
    ///
    

    public enum PitchArea
    {
        North = 1,
        South,
        Full
    }

    public enum PitchSide
    {
        East = 1,
        Center,
        West,
        Full
    }

    public enum PitchParts
    {
        Pitch = 1,
        Half,
        Central,
        Channel,
        PenaltyBox,
        SixYardBox
    }

    public enum PitchSpots
    {
        Center,

    }


    public class Pitch
    {

        public PitchRegion Region { get; }

        public Pitch(int width, int length)
        {

            // build the pitch regions

            Region = PitchRegion.BuildRegion(PitchArea.Full, PitchSide.Full, PitchParts.Pitch, 0, 0, width, length);

            var northHalf = PitchRegion.BuildRegion(PitchArea.South, PitchSide.Full, PitchParts.Half, 0, 0, width, (length / 2));

            var southHalf = PitchRegion.BuildRegion(PitchArea.North, PitchSide.Full, PitchParts.Half, 0, (length / 2), width, (length / 2));

            ConstructNorthHalf(northHalf);
            ConstructSouthHalf(southHalf);

            Region.AddInnerRegion(northHalf);
            Region.AddInnerRegion(southHalf);
            
        }

        private static void ConstructNorthHalf(PitchRegion northHalf)
        {
            var penaltyBox = PitchRegion.BuildRegion(PitchArea.North,
                                                     PitchSide.Center,
                                                     PitchParts.PenaltyBox,
                                                     (northHalf.Width / 2) - 22,
                                                     0,
                                                     (22 * 2),
                                                     18);

            northHalf.AddInnerRegion(penaltyBox);
            northHalf.AddInnerRegion(PitchSide.West, PitchParts.Channel, 0,0, penaltyBox.X, northHalf.Length);
            northHalf.AddInnerRegion(PitchSide.East, PitchParts.Channel, (penaltyBox.X + penaltyBox.Width), 0, penaltyBox.X, northHalf.Length);
            northHalf.AddInnerRegion(PitchSide.Center, PitchParts.Central, penaltyBox.X, penaltyBox.Length, penaltyBox.Width, (northHalf.Length - penaltyBox.Length));

            penaltyBox.AddInnerRegion(PitchParts.SixYardBox, penaltyBox.X + 12, 0, 20, 6);
        }

        private static void ConstructSouthHalf(PitchRegion southHalf)
        {
            var penaltyBox = PitchRegion.BuildRegion(PitchArea.South,
                                                     PitchSide.Center,
                                                     PitchParts.PenaltyBox,
                                                     (southHalf.Width / 2) - 22,
                                                     (southHalf.Y + southHalf.Length) - 18,
                                                     (22 * 2),
                                                     18);

            southHalf.AddInnerRegion(penaltyBox);
            southHalf.AddInnerRegion(PitchSide.West, PitchParts.Channel, 0,southHalf.Y, penaltyBox.X, southHalf.Length);
            southHalf.AddInnerRegion(PitchSide.East, PitchParts.Channel, (penaltyBox.X + penaltyBox.Width), southHalf.Y, penaltyBox.X, southHalf.Length);
            southHalf.AddInnerRegion(PitchSide.Center, PitchParts.Central, penaltyBox.X, southHalf.Y, penaltyBox.Width, (southHalf.Length - penaltyBox.Length));

        }

    }


    public class PitchRegion
    {
        private readonly List<PitchRegion> _innerRegions;
        private readonly Rectangle _region;
        
        private PitchRegion(PitchArea area,
                            PitchSide side,
                            PitchParts part,
                            Rectangle region)
        {
            Area = area;
            Side = side;
            Part = part;

            _region = region;
            _innerRegions = new List<PitchRegion>();
        }

        public PitchArea Area { get; }
        public PitchSide Side { get; }
        public PitchParts Part { get; }

        public int X => _region.X;
        public int Y => _region.Y;

        public int Width => _region.Width;
        public int Length => _region.Height;

        public Rectangle ToRectangle()
        {
            return new Rectangle(X, Y, Width, Length);
        }


        public static PitchRegion BuildRegion(PitchArea area,
                                              PitchSide side,
                                              PitchParts part,
                                              int x,
                                              int y,
                                              int width,
                                              int length)
        {

            var region = new Rectangle(x, y, width, length);

            return new PitchRegion(area, side, part, region);

        }


        public void AddInnerRegion(PitchRegion region)
        {
            _innerRegions.Add(region);
        }

        public void AddInnerRegion(PitchSide side,
                                   PitchParts part,
                                   int x,
                                   int y,
                                   int width,
                                   int length)
        {
            AddInnerRegion(BuildRegion(Area, side, part, x, y, width, length));
        }

        public List<PitchRegion> GetRegions() { return _innerRegions.ToList(); }

        public void AddInnerRegion(PitchParts part,
                                   int x,
                                   int y,
                                   int width,
                                   int length)
        {
            AddInnerRegion(BuildRegion(Area, Side, part, x, y, width, length));
        }

        private PitchRegion IsInRegion(Point location) { return _region.Contains(location) ? this : null; }

        public PitchRegion GetRegion(Point location)
        {
            if (IsInRegion(location) == null) return null;

            return _innerRegions == null ?
                this :
                _innerRegions.LastOrDefault(x => x.GetRegion(location) != null);

        }


    }

}
