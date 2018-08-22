using System;
using System.Collections.Generic;
using FM.Core.Match;
using FM.Core.Tests.Match;
using FM.Core.Tests.Players;

namespace FM.Core.Tests.Team
{

    public class TestTeamFactory
    {
        public static ITeam WeakTeam() { return BuildTeam("Weak Team", TestPlayers.BuildWeak()); }

        public static ITeam StrongTeam() { return BuildTeam("Strong Team", TestPlayers.BuildStrong()); }

        public static ITeam AverageTeam() { return BuildTeam("Average Team", TestPlayers.BuildAverage()); }

        public static ITeam WeakDefence() { return BuildTeam("Weak Defence", TestPlayers.BuildWeakDefence()); }

        public static ITeam WeakPassing() { return BuildTeam("Weak Passing", TestPlayers.BuildWeakPassing()); }

        public static ITeam WeakAttack() { return BuildTeam("Weak Attack", TestPlayers.BuildWeakShooting()); }

        public static ITeam StrongDefence() { return BuildTeam("Strong Defence", TestPlayers.BuildStrongDefence()); }

        public static ITeam StrongPassing() { return BuildTeam("Strong Passing", TestPlayers.BuildStrongPassing()); }

        public static ITeam StrongAttack() { return BuildTeam("Strong Attack", TestPlayers.BuildStrongShooting()); }



        private static ITeam BuildTeam(string name, IEnumerable<IPlayer> players) { return new Club.Team(Guid.NewGuid(), name, players); }

    }

}