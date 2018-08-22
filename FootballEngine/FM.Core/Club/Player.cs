using System;
using System.Collections.Generic;
using System.Text;
using FM.Core.Match;

namespace FM.Core.Club
{
    public class Player
        : IPlayer
    {

        /// <inheritdoc />
        public Guid Id { get; set; }
        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public PlayerPosition Position { get; set; }


        /// <inheritdoc />
        public int Tackling { get; set; }

        /// <inheritdoc />
        public int Passing { get; set; }

        /// <inheritdoc />
        public int Shooting { get; set; }

    }
}
