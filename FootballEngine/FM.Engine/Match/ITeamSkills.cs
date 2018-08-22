namespace FM.Engine.Match
{

    /// <summary>
    /// Represents the skills for the team within a Match
    /// </summary>
    public interface ITeamSkills
    {
        int GoalKeeping { get; }
        int Tackling { get; }
        int Passing { get; }
        int Shooting { get; }
    }
}
