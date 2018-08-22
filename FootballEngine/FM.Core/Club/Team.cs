using System;
using System.Collections.Generic;
using System.Linq;
using FM.Core.Match;

namespace FM.Core.Club
{

    public class Team
        : ITeam
    {

        public Team(Guid id, 
                    string name, 
                    IEnumerable<IPlayer> players)
        {
            Id = id;
            Name = name;
            Players = players.ToList();
        }

        private int DetermineTackling()
        {
            return (int)(Players.Sum((x) =>
            {
                switch (x.Position)
                {
                    case PlayerPosition.Attack:
                        return x.Tackling * 0.6d;

                    case PlayerPosition.Midfield:

                        return x.Tackling * 0.9d;

                    case PlayerPosition.Defence:
                    case PlayerPosition.Goalkeeper:

                        return x.Tackling;

                    default:

                        return x.Tackling;
                }

            }) / Players.Count());
        }

        private int DeterminePassing()
        {
            return (int)(Players.Sum((x) =>
            {
                switch (x.Position)
                {

                    case PlayerPosition.Attack:
                        return x.Passing * 0.95d;

                    case PlayerPosition.Midfield:

                        return x.Passing;

                    case PlayerPosition.Defence:
                    case PlayerPosition.Goalkeeper:

                        return x.Passing * 0.85d;

                    default:

                        return x.Passing;
                }

            }) / Players.Count());
        }

        private int DetermineShooting()
        {
            return (int)(Players.Sum((x) =>
            {
                switch (x.Position)
                {

                    case PlayerPosition.Attack:
                        return x.Shooting;

                    case PlayerPosition.Midfield:

                        return x.Shooting;

                    case PlayerPosition.Defence:
                    case PlayerPosition.Goalkeeper:

                        return x.Shooting * 0.65d;

                    default:

                        return x.Shooting;
                }

            }) / Players.Count());
        }

        /// <inheritdoc />
        public Guid Id { get; set; }
        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public int Tackling { get; set; }

        /// <inheritdoc />
        public int Passing { get; set; }

        /// <inheritdoc />
        public int Shooting { get; set; }

        /// <inheritdoc />
        public List<IPlayer> Players { get; set; }

        /// <inheritdoc />
        public void SetupTeam()
        {
            Tackling = DetermineTackling();
            Passing = DeterminePassing();
            Shooting = DetermineShooting();
        }
    }

}
