namespace FM.Core.Match
{

    /// <summary>
    ///     Represents the skills for the team within a Match
    /// </summary>
    public interface ISkills
    {
        int Tackling { get; set; }
        int Passing { get; set; }
        int Shooting { get; set; }
    }


}
