namespace FM.Core.Match
{

    public enum PlayerPosition
    {
        Goalkeeper,
        Defence,
        Midfield,
        Attack
    }

    public interface IPlayer
        : IIdentifier, 
          ISkills
    {

        string Name { get; set; }

        PlayerPosition Position { get; set; }

        

    }

}