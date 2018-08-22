using System.Collections.Generic;
using System.Linq;
using System.Text;
using FM.Core.Randomise;

namespace FM.Core.Match
{

    public enum MatchResult
    {
        HomeWin = 1,
        AwayWin,
        Draw
    }

    public class MatchSimulator
    {
        private readonly MatchSimulatorOptions _options;

        private readonly IRandomise _random;

        public MatchSimulator(IRandomise random,
                              ITeam homeTeam,
                              ITeam awayTeam,
                              MatchSimulatorOptions options)
        {
            _random = random;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            _options = options;
        }

        public ITeam HomeTeam { get; }
        public ITeam AwayTeam { get; }

        public int HomeGoals { get; private set; }
        public int AwayGoals { get; private set; }

        public List<int> HomeGoalMinutes { get; private set; }
        public List<int> AwayGoalMinutes { get; private set; }

        public List<IPlayer> HomeGoalScorers { get; private set; }
        public List<IPlayer> AwayGoalScorers { get; private set; }


        public MatchResult Result()
        {
            if (HomeGoals > AwayGoals) return MatchResult.HomeWin;
            if (AwayGoals > HomeGoals) return MatchResult.AwayWin;

            return MatchResult.Draw;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder();

            var homeGoalMin = string.Join(", ", HomeGoalMinutes);
            var awayGoalMin = string.Join(", ", AwayGoalMinutes);

            var homeGoalScorer = string.Join(", ", HomeGoalScorers.Select((x) => x.Name));
            var awayGoalScorer = string.Join(", ", AwayGoalScorers.Select((x) => x.Name));

            sb.AppendLine($"Teams  : {HomeTeam.Name} - {AwayTeam.Name}");
            sb.AppendLine($"Score  : {HomeGoals.ToString()} - {AwayGoals.ToString()}");
            sb.AppendLine($"Mins  : {homeGoalMin} | {awayGoalMin}");
            sb.AppendLine($"Scorers  : {homeGoalScorer} | {awayGoalScorer}");
            sb.AppendLine($"Result : {Result().ToString()}");

            return sb.ToString();
        }

        private void ResetMatch()
        {
            HomeTeam.SetupTeam();
            AwayTeam.SetupTeam();

            HomeGoals = 0;
            HomeGoalMinutes = new List<int>();
            HomeGoalScorers = new List<IPlayer>();

            AwayGoals = 0;
            AwayGoalMinutes = new List<int>();
            AwayGoalScorers = new List<IPlayer>();


        }

        /// <summary>
        ///     Kick the match off
        /// </summary>
        public void KickOff()
        {

            var minutes = 0;

            ResetMatch();

            while (minutes < 90)
            {
                minutes++;

                var homeTeamInPossession = HomeTeamInPossession();

                if (!IsChance(homeTeamInPossession)) continue;
                if (!IsGoal(homeTeamInPossession)) continue;

                var scorer = DetermineScorer(homeTeamInPossession);

                if (homeTeamInPossession)
                {
                    HomeGoals++;
                    HomeGoalMinutes.Add(minutes);
                    HomeGoalScorers.Add(scorer);
                }
                else
                {
                    AwayGoals++;
                    AwayGoalMinutes.Add(minutes);
                    AwayGoalScorers.Add(scorer);
                }

            }

        }

        /// <summary>
        ///     Determine if the home team have possession
        /// </summary>
        private bool HomeTeamInPossession()
        {

            var tacklingTotal = HomeTeam.Tackling + AwayTeam.Tackling;
            var result = _random.Next(0, tacklingTotal);

            return result <= HomeTeam.Tackling;

        }

        /// <summary>
        ///     Determine if there is a chance
        /// </summary>
        private bool IsChance(bool homeTeamInPossession)
        {
            return IsEvent(homeTeamInPossession,
                           HomeTeam.Passing,
                           HomeTeam.Tackling,
                           AwayTeam.Passing,
                           AwayTeam.Tackling,
                           _options.ChanceLikelyhood,
                           _options.ChanceBoost);
        }

        /// <summary>
        ///     Determine if there is a goal
        /// </summary>
        private bool IsGoal(bool homeTeamInPossession)
        {
            return IsEvent(homeTeamInPossession,
                           HomeTeam.Shooting,
                           HomeTeam.Tackling,
                           AwayTeam.Shooting,
                           AwayTeam.Tackling,
                           _options.GoalLikelyhood,
                           _options.GoalBoost);
        }

        private bool IsEvent(bool homeTeamInPossession,
                             int homeForSkill,
                             int homeAgainstSkill,
                             int awayForSkill,
                             int awayAgainstSkill,
                             double eventChance,
                             double eventChanceBoost)
        {

            var forSkill = homeTeamInPossession ? homeForSkill : awayForSkill;
            var againstSkill = homeTeamInPossession ? awayAgainstSkill : homeAgainstSkill;

            var forAgainstRange = forSkill + againstSkill;
            var forBoost = _random.Next(0, forAgainstRange) <= forSkill;

            var chanceOfEvent = eventChance;
            chanceOfEvent = forBoost ? chanceOfEvent + eventChanceBoost : chanceOfEvent - eventChanceBoost;

            var chance = _random.NextDouble();

            return chance < chanceOfEvent;

        }

        private IPlayer DetermineScorer(bool homeTeamInPossession)
        {

            var playerOdds = new List<IPlayer>();
            var players = homeTeamInPossession ? HomeTeam.Players : AwayTeam.Players;

            players.Where((x) => x.Position != PlayerPosition.Goalkeeper)
                   .ToList()
                   .ForEach((x) =>
                    {
                        var shooting = x.Shooting;
                        if (x.Position == PlayerPosition.Attack) {
                            shooting = (int)(shooting * 1.75d);
                        }

                        for (var i = 0; i < shooting; i++)
                        {
                            playerOdds.Add(x);
                        }

                    });

            return playerOdds[_random.Next(0, playerOdds.Count - 1)];

        }

    }


}
