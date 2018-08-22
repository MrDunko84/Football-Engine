namespace FM.Core.Match
{

    public enum PlayerPosition
    {
        Goalkeeper,
        Defence,
        Midfield,
        Attack
    }


    // TODO: Expand skills to player skills that we build up core skills
    public interface IPlayer
        : IIdentifier, 
          ISkills
    {

        string Name { get; set; }

        PlayerPosition Position { get; set; }

        

    }

}