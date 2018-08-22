using System.Collections.Generic;

namespace FM.Core.Match
{

    public interface IMatchEvent
    {
        int Id { get; }
        bool Continue { get; }
        IEnumerable<string> Commentary { get; }
    }

}