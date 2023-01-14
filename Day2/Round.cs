using System;
using System.Collections.Generic;

namespace Day2
{
    internal partial class Program
    {
        public enum Thing
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3,
        }

        public enum Outcome
        {
            Draw,
            OpponentWins,
            SelfWins
        }

        public class Round
        {
            private const int OutcomeScoreWin = 6;
            private const int OutcomeScoreDraw = 3;
            private const int OutcomeScoreLose = 0;

            public Round(Thing opponent, Thing self)
            {
                Opponent = opponent;
                Self = self;
            }

            public Thing Opponent { get; private set; }
            public Thing Self { get; private set; }

            public Outcome Winner
            {
                get
                {
                    if (Opponent == Self)
                    {
                        // scores equal, so draw
                        return Outcome.Draw;
                    }

                    // Then, a winner for that round is selected:
                    // Rock defeats Scissors
                    // Scissors defeats Paper
                    // Paper defeats Rock
                    // If both players choose the same shape, the round instead ends in a draw
                    if (Opponent == Thing.Rock && Self == Thing.Scissors) 
                    {
                        return Outcome.OpponentWins;
                    }

                    if (Opponent == Thing.Scissors && Self == Thing.Paper)
                    {
                        return Outcome.OpponentWins;
                    }

                    if (Opponent == Thing.Paper && Self == Thing.Rock)
                    {
                        return Outcome.OpponentWins;
                    }

                    return Outcome.SelfWins;
                }
            }


            public int Score
            {
                get
                {
                    if (Winner == Outcome.OpponentWins)
                    {
                        return OpponentScore;
                    }
                    else if (Winner == Outcome.SelfWins)
                    {
                        return SelfScore;
                    }
                    else
                    {
                        // Outcome.Draw
                        return OpponentScore;
                    }
                }
            }

            public int OpponentScore
            {
                get { return GetScore(Opponent); }
            }

            public int SelfScore
            {
                get { return GetScore(Self); }
            }

            public override string ToString()
            {
                var mapOpponent = new Dictionary<Thing, string>()
                {
                    { Thing.Rock, "A" },
                    { Thing.Paper, "B" },
                    { Thing.Scissors, "C" }
                };

                var mapSelf = new Dictionary<Thing, string>()
                {
                    { Thing.Rock, "X" },
                    { Thing.Paper, "Y" },
                    { Thing.Scissors, "Z" }
                };

                return $"{mapOpponent[Opponent]} {mapSelf[Self]} ({Opponent} {Self}); Winner {Winner}, My Score {SelfScore}";
            }

            private int GetScore(Thing player)
            {
                var mapScores = new Dictionary<Thing, int>()
                {
                    { Thing.Rock, 1 },
                    { Thing.Paper, 2 },
                    { Thing.Scissors, 3 }
                };

                int score = 0;

                var winner = Winner;

                if (winner == Outcome.OpponentWins)
                {
                    score = OutcomeScoreLose + mapScores[player];
                }
                else if (winner == Outcome.Draw)
                {
                    score = OutcomeScoreDraw + mapScores[player];
                }
                else if (winner == Outcome.SelfWins)
                {
                    score = OutcomeScoreWin + mapScores[player];
                }

                return score;
            }
        }

        public class RoundPartTwo
        {
            private const int OutcomeScoreWin = 6;
            private const int OutcomeScoreDraw = 3;
            private const int OutcomeScoreLose = 0;

            public RoundPartTwo(Thing opponent, Outcome outcome)
            {
                Opponent = opponent;
                Outcome = outcome;
            }


            public Thing Opponent { get; private set; }
            public Outcome Outcome { get; private set; }

            public Thing MyMove
            {
                get { return GetMoveForOutcome(Opponent, Outcome); }
            }

            public int SelfScore
            {
                get { return GetScore(MyMove, Outcome); }
            }

            public override string ToString()
            {
                var mapOpponent = new Dictionary<Thing, string>()
                {
                    { Thing.Rock, "A" },
                    { Thing.Paper, "B" },
                    { Thing.Scissors, "C" }
                };

                return $"{mapOpponent[Opponent]} {Outcome}, My Move {MyMove}, My Score {SelfScore}";
            }

            private static int GetScore(Thing player, Outcome outcome)
            {
                var mapScores = new Dictionary<Thing, int>()
                {
                    { Thing.Rock, 1 },
                    { Thing.Paper, 2 },
                    { Thing.Scissors, 3 }
                };

                int score = 0;

                if (outcome == Outcome.OpponentWins)
                {
                    score = OutcomeScoreLose + mapScores[player];
                }
                else if (outcome == Outcome.Draw)
                {
                    score = OutcomeScoreDraw + mapScores[player];
                }
                else if (outcome == Outcome.SelfWins)
                {
                    score = OutcomeScoreWin + mapScores[player];
                }

                return score;
            }

            private static Thing GetMoveForOutcome(Thing opponent, Outcome outcome)
            {
                // TODO
                if (outcome == Outcome.Draw)
                {
                    return opponent;
                }

                // Rock defeats Scissors
                // Scissors defeats Paper
                // Paper defeats Rock
                if (outcome == Outcome.OpponentWins)
                {
                    if (opponent == Thing.Scissors)
                    {
                        return Thing.Paper;
                    }
                    if (opponent == Thing.Rock)
                    {
                        return Thing.Scissors;
                    }
                    if (opponent == Thing.Paper)
                    {
                        return Thing.Rock;
                    }

                    throw new InvalidOperationException();
                }
                else // Outcome.SelfWins
                {
                    if (opponent == Thing.Scissors)
                    {
                        return Thing.Rock;
                    }
                    if (opponent == Thing.Paper)
                    {
                        return Thing.Scissors;
                    }
                    if (opponent == Thing.Rock)
                    {
                        return Thing.Paper;
                    }

                    throw new InvalidOperationException();
                }

            }
        }
    }
}
