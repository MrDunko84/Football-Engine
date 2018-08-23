using System;
using System.Collections.Generic;
using FM.Core.Match;
using FM.Core.Randomise;
using FM.Core.Tests.Team;
using NUnit.Framework;

namespace FM.Core.Tests.Match
{

    [TestFixture]
    public class MatchSimulatorTests
    {

        [OneTimeSetUp]
        public void SetupTest()
        {

            _randomise = new SystemRandom();

            _options = new MatchSimulatorOptions
            {
                ChanceLikelyhood = 0.25d,
                ChanceBoost = 0.07d
            };

        }

        private IRandomise _randomise;
        private MatchSimulatorOptions _options;


        private void RunMatches(ITeam homeTeam, ITeam awayTeam, int runs)
        {
            var match = new MatchSimulator(_randomise, homeTeam, awayTeam, _options);
            var results = new List<MatchResult>();

            for (var i = 0; i < runs; i++)
            {
                match.KickOff();
                results.Add(match.Result());
                Console.WriteLine(match.ToString());

            }

            Console.WriteLine(new string('-', 50));
            Console.WriteLine();
        }


        private static readonly object[] WeakTeamTestCases =
        {
            new object[] {TestTeamFactory.WeakTeam(), TestTeamFactory.WeakTeam()},
            new object[] {TestTeamFactory.WeakTeam(), TestTeamFactory.AverageTeam()},
            new object[] {TestTeamFactory.WeakTeam(), TestTeamFactory.StrongTeam()},

            new object[] {TestTeamFactory.WeakTeam(), TestTeamFactory.WeakPassing()},
            new object[] {TestTeamFactory.WeakTeam(), TestTeamFactory.WeakDefence()},
            new object[] {TestTeamFactory.WeakTeam(), TestTeamFactory.WeakAttack()},
            new object[] {TestTeamFactory.WeakTeam(), TestTeamFactory.StrongPassing()},
            new object[] {TestTeamFactory.WeakTeam(), TestTeamFactory.StrongDefence()},
            new object[] {TestTeamFactory.WeakTeam(), TestTeamFactory.StrongAttack()}
        };


        private static readonly object[] AverageTeamTestCases =
        {
            new object[] {TestTeamFactory.AverageTeam(), TestTeamFactory.WeakTeam()},
            new object[] {TestTeamFactory.AverageTeam(), TestTeamFactory.AverageTeam()},
            new object[] {TestTeamFactory.AverageTeam(), TestTeamFactory.StrongTeam()},

            new object[] {TestTeamFactory.AverageTeam(), TestTeamFactory.WeakPassing()},
            new object[] {TestTeamFactory.AverageTeam(), TestTeamFactory.WeakDefence()},
            new object[] {TestTeamFactory.AverageTeam(), TestTeamFactory.WeakAttack()},
            new object[] {TestTeamFactory.AverageTeam(), TestTeamFactory.StrongPassing()},
            new object[] {TestTeamFactory.AverageTeam(), TestTeamFactory.StrongDefence()},
            new object[] {TestTeamFactory.AverageTeam(), TestTeamFactory.StrongAttack()}
        };


        private static readonly object[] StrongTeamTestCases =
        {
            new object[] {TestTeamFactory.StrongTeam(), TestTeamFactory.WeakTeam()},
            new object[] {TestTeamFactory.StrongTeam(), TestTeamFactory.AverageTeam()},
            new object[] {TestTeamFactory.StrongTeam(), TestTeamFactory.StrongTeam()},

            new object[] {TestTeamFactory.StrongTeam(), TestTeamFactory.WeakPassing()},
            new object[] {TestTeamFactory.StrongTeam(), TestTeamFactory.WeakDefence()},
            new object[] {TestTeamFactory.StrongTeam(), TestTeamFactory.WeakAttack()},
            new object[] {TestTeamFactory.StrongTeam(), TestTeamFactory.StrongPassing()},
            new object[] {TestTeamFactory.StrongTeam(), TestTeamFactory.StrongDefence()},
            new object[] {TestTeamFactory.StrongTeam(), TestTeamFactory.StrongAttack()}
        };

        [Test]
        [TestCaseSource(nameof(AverageTeamTestCases))]
        public void AverageTeamTest(ITeam homeTeam, ITeam awayTeam)
        {

            RunMatches(homeTeam, awayTeam, 3);
            Assert.Pass();

        }

        [Test]
        [TestCaseSource(nameof(StrongTeamTestCases))]
        public void StrongTeamTest(ITeam homeTeam, ITeam awayTeam)
        {

            RunMatches(homeTeam, awayTeam, 3);
            Assert.Pass();

        }


        [Test]
        [TestCaseSource(nameof(WeakTeamTestCases))]
        public void WeakTeamTest(ITeam homeTeam, ITeam awayTeam)
        {

            RunMatches(homeTeam, awayTeam, 3);
            Assert.Pass();

        }
    }


}
