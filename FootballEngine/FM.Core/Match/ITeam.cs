using System.Collections.Generic;

namespace FM.Core.Match
{
    public interface ITeam
        : IIdentifier, ISkills
    {

        string Name { get; }
        List<IPlayer> Players { get; }

        void SetupTeam();

    }

}