using System;
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

    public class MatchScorer
    {
        public IPlayer Player { get; set; }
        public int Minute { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Player.Name} ({Minute})";
        }
    }

    public class MatchStats
    {
        public List<MatchScorer> Goals = new List<MatchScorer>();
        public int MinutesInPossession;
        public int Chances;
        public int Fouls;
    }

    public class MatchReport
    {
        public MatchStats Home = new MatchStats();
        public MatchStats Away = new MatchStats();

        public int MinutesPlayed;

        public int GetHomePossession()
        {
            return (int)Math.Ceiling(GetPercentage(MinutesPlayed, Home.MinutesInPossession));
        }

        public int GetAwayPossession()
        {
            return (int)Math.Floor(GetPercentage(MinutesPlayed, Away.MinutesInPossession));
        }

        private static double GetPercentage(int minutesPlayed, int minutesInPosession)
        {
            return ((double) 100 / minutesPlayed) * minutesInPosession;
        }

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

        public MatchReport Report { get; private set; }

        public MatchResult Result()
        {
            if (Report.Home.Goals.Count() > Report.Away.Goals.Count()) return MatchResult.HomeWin;
            if (Report.Away.Goals.Count() > Report.Home.Goals.Count()) return MatchResult.AwayWin;

            return MatchResult.Draw;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var homeGoals = string.Join(", ", Report.Home.Goals.Select((x) => x.ToString()));
            var awayGoals = string.Join(", ", Report.Away.Goals.Select((x) => x.ToString()));

            var sb = new StringBuilder();

            sb.AppendLine($"Teams  : {HomeTeam.Name} - {AwayTeam.Name}");
            sb.AppendLine($"Goals  : {Report.Home.Goals.Count().ToString()} - {Report.Away.Goals.Count().ToString()}");
            sb.AppendLine($"Scorers : {homeGoals} | {awayGoals}");
            sb.AppendLine($"Result : {Result().ToString()}");
            sb.AppendLine($"Possession : {Report.GetHomePossession().ToString()}% - {Report.GetAwayPossession().ToString()}%");
            sb.AppendLine($"Chances: {Report.Home.Chances.ToString()} - {Report.Away.Chances.ToString()}");
            sb.AppendLine($"Fouls: {Report.Home.Fouls.ToString()} - {Report.Away.Fouls.ToString()}");

            return sb.ToString();
        }

        private void ResetMatch()
        {
            HomeTeam.SetupTeam();
            AwayTeam.SetupTeam();

            Report = new MatchReport();

        }

        /// <summary>
        ///     Kick the match off
        /// </summary>
        public void KickOff()
        {

            ResetMatch();

            while (Report.MinutesPlayed < 90)
            {

                Report.MinutesPlayed++;

                var homeTeamInPossession = HomeTeamInPossession();

                var outPossessionStats = !homeTeamInPossession ? Report.Home : Report.Away;
                var inPossessionStats = homeTeamInPossession ? Report.Home : Report.Away;

                inPossessionStats.MinutesInPossession++;

                if (IsFoul(homeTeamInPossession)) { outPossessionStats.Fouls++; }

                if (!IsChance(homeTeamInPossession)) continue;
                inPossessionStats.Chances++;

                if (!IsGoal(homeTeamInPossession)) continue;

                var scorer = DetermineScorer(homeTeamInPossession);
                
                inPossessionStats.Goals.Add(new MatchScorer() {Minute = Report.MinutesPlayed, Player = scorer});

            }

        }

        /// <summary>
        ///     Determine if the home team have possession
        /// </summary>
        private bool HomeTeamInPossession()
        {

            var tacklingTotal = (HomeTeam.Tackling + HomeTeam.Passing) + (AwayTeam.Tackling + AwayTeam.Passing);
            var result = _random.Next(0, tacklingTotal);

            return result <= (HomeTeam.Tackling + HomeTeam.Passing);

        }

        private bool IsFoul(bool homeTeamInPossession)
        {
            return IsEvent(!homeTeamInPossession,
                           HomeTeam.Tackling,
                           HomeTeam.Passing,
                           AwayTeam.Tackling,
                           AwayTeam.Passing,
                           0.1d,
                           0.0d);
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
                            shooting = (shooting * 6);
                        } else if (x.Position == PlayerPosition.Midfield)
                        {
                            shooting = (shooting * 2);
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
