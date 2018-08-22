namespace FM.Core.Match
{

    public class MatchSimulatorOptions
    {
        public double ChanceLikelyhood { get; set; } = 0.25d;
        public double ChanceBoost { get; set; } = 0.08d;

        public double GoalLikelyhood { get; set; } = 0.11d;
        public double GoalBoost { get; set; } = 0.05d;
    }

}
