namespace FM.Core.Randomise
{

    public interface IRandomise
    {
        int Next(int minValue, int maxValue);
        double NextDouble();
    }


}
