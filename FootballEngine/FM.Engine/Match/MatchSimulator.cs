using FM.Engine.Randomise;

namespace FM.Engine.Match
{

    public class MatchSimulator
    {

        private readonly IRandomise _random;

        public MatchSimulator(IRandomise random,
                              ITeamSkills homeTeam,
                              ITeamSkills awayTeam)
        {
            _random = random;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
        }

        public ITeamSkills HomeTeam {get;}
        public ITeamSkills AwayTeam {get;}

        public int HomeGoals {get; private set;}
        public int AwayGoals {get; private set;}


        /// <summary>
        /// Kick the match off
        /// </summary>
        public void KickOff()
        {

            var minutes = 0;

            while (minutes <= 90)
            {
                minutes++;

                var homeTeamInPossession = HomeTeamInPossession();

                if (!IsChance(homeTeamInPossession)) continue;
                if (!IsGoal(homeTeamInPossession)) continue;

                if (homeTeamInPossession)
                {
                    HomeGoals++;
                }
                else
                {
                    AwayGoals++;
                }

            }

        }

        /// <summary>
        /// Determine if the home team have possession
        /// </summary>
        private bool HomeTeamInPossession()
        {

            var tacklingTotal = (HomeTeam.Tackling + AwayTeam.Tackling);
            var result = _random.Next(0, tacklingTotal);

            return (result <= HomeTeam.Tackling);

        }

        /// <summary>
        /// Determine if there is a chance
        /// </summary>
        private bool IsChance(bool homeTeamInPossession)
        {
            return IsEvent(homeTeamInPossession,
                           HomeTeam.Passing,
                           HomeTeam.Tackling,
                           AwayTeam.Passing,
                           AwayTeam.Tackling,
                           0.27d,
                           0.1d);
        }

        /// <summary>
        /// Determine if there is a goal
        /// </summary>
        private bool IsGoal(bool homeTeamInPossession)
        {

            return IsEvent(homeTeamInPossession,
                           HomeTeam.Shooting,
                           HomeTeam.GoalKeeping,
                           AwayTeam.Shooting,
                           AwayTeam.GoalKeeping,
                           0.12d,
                           0.04d);

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

            var forAgainstRange = (forSkill + againstSkill);
            var forBoost = (_random.Next(0, forAgainstRange) <= forSkill);

            var chanceOfEvent = eventChance;
            chanceOfEvent = forBoost ? chanceOfEvent + eventChanceBoost : chanceOfEvent - eventChanceBoost;

            var chance = _random.NextDouble();

            return (chance < chanceOfEvent);

        }


    }

}