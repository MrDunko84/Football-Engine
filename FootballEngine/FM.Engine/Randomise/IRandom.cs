using System.Collections.Generic;
using System.Text;

namespace FM.Engine.Randomise
{
    public interface IRandomise
    {
        int Next(int minValue, int maxValue);
        double NextDouble();
    }


}
