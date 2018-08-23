using System;
using System.Collections.Generic;
using System.Text;
using FM.Core.Club;
using FM.Core.Match;

namespace FM.Core.Tests.Players
{
    public class TestPlayers
    {

        //public static IEnumerable<IPlayer> BuildPlayers()
        //{
        //    return new IPlayer[]
        //    {
        //        BuildPlayer("David Berry", 50, 30, 0, PlayerPosition.Goalkeeper),
                
        //        BuildPlayer("Clive Higgins", 62, 31, 13, PlayerPosition.Defence),
        //        BuildPlayer("Sean Stanway", 65, 30, 33, PlayerPosition.Defence),
        //        BuildPlayer("Mark Salt", 71, 50, 22, PlayerPosition.Defence),
        //        BuildPlayer("Alex Bingham", 81, 33, 10, PlayerPosition.Defence),

        //        BuildPlayer("Danny Lowson", 30, 60, 67, PlayerPosition.Midfield),
        //        BuildPlayer("Doug Watson", 45, 50, 51, PlayerPosition.Midfield),
        //        BuildPlayer("Carl Dunkley", 56, 80, 82, PlayerPosition.Midfield),
        //        BuildPlayer("Richard Clark", 51, 72, 42, PlayerPosition.Midfield),

        //        BuildPlayer("Sir Tom", 10, 30, 90, PlayerPosition.Attack),
        //        BuildPlayer("Paul Calvert", 20, 20, 70, PlayerPosition.Attack),

        //    };

        //}


        public static IEnumerable<IPlayer> BuildWeak() { return BuildTeam(BuildWeakPlayer); }
        public static IEnumerable<IPlayer> BuildStrong() { return BuildTeam(BuildStrongPlayer); }
        public static IEnumerable<IPlayer> BuildAverage() { return BuildTeam(BuildAveragePlayer); }

        public static IEnumerable<IPlayer> BuildWeakDefence() { return BuildTeam(BuildWeakTackling); }
        public static IEnumerable<IPlayer> BuildWeakPassing() { return BuildTeam(BuildWeakPassing); }
        public static IEnumerable<IPlayer> BuildWeakShooting() { return BuildTeam(BuildWeakShooting); }

        public static IEnumerable<IPlayer> BuildStrongDefence() { return BuildTeam(BuildStrongTackling); }
        public static IEnumerable<IPlayer> BuildStrongPassing() { return BuildTeam(BuildStrongPassing); }
        public static IEnumerable<IPlayer> BuildStrongShooting() { return BuildTeam(BuildStrongShooting); }




        private static IEnumerable<IPlayer> BuildTeam(Func<string, PlayerPosition, IPlayer> playerBuilder)
        {
            return new IPlayer[]
            {
                playerBuilder.Invoke("David Berry", PlayerPosition.Goalkeeper),
                
                playerBuilder.Invoke("Clive Higgins",  PlayerPosition.Defence),
                playerBuilder.Invoke("Sean Stanway",PlayerPosition.Defence),
                playerBuilder.Invoke("Mark Salt", PlayerPosition.Defence),
                playerBuilder.Invoke("Alex Bingham", PlayerPosition.Defence),

                playerBuilder.Invoke("Danny Lowson", PlayerPosition.Midfield),
                playerBuilder.Invoke("Doug Watson", PlayerPosition.Midfield),
                playerBuilder.Invoke("Carl Dunkley", PlayerPosition.Midfield),
                playerBuilder.Invoke("Richard Clark", PlayerPosition.Midfield),

                playerBuilder.Invoke("Sir Tom", PlayerPosition.Attack),
                playerBuilder.Invoke("Paul Calvert", PlayerPosition.Attack)
            };

        }




        private static IPlayer BuildWeakPlayer(string name, PlayerPosition position) { return BuildPlayer(name, 20, 20, 20, position); }
        private static IPlayer BuildStrongPlayer(string name, PlayerPosition position) { return BuildPlayer(name, 80, 80, 80, position); }
        private static IPlayer BuildAveragePlayer(string name, PlayerPosition position) { return BuildPlayer(name, 50, 50, 50, position); }


        private static IPlayer BuildWeakTackling(string name, PlayerPosition position) { return BuildPlayer(name, 20, 50, 50, position); }
        private static IPlayer BuildWeakPassing(string name, PlayerPosition position) { return BuildPlayer(name, 50, 20, 50, position); }
        private static IPlayer BuildWeakShooting(string name, PlayerPosition position) { return BuildPlayer(name, 50, 50, 20, position); }


        private static IPlayer BuildStrongTackling(string name, PlayerPosition position) { return BuildPlayer(name, 80, 50, 50, position); }
        private static IPlayer BuildStrongPassing(string name, PlayerPosition position) { return BuildPlayer(name, 50, 80, 50, position); }
        private static IPlayer BuildStrongShooting(string name, PlayerPosition position) { return BuildPlayer(name, 50, 50, 80, position); }



        private static IPlayer BuildPlayer(string name, int tackling, int passing, int shooting, PlayerPosition position)
        {
            return new Player()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Tackling =  tackling,
                Passing = passing,
                Shooting = shooting,
                Position = position
            };
        }

    }
}
