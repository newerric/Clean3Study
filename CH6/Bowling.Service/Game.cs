using System;

namespace Bowling.Service
{
    public class Game
    {
        private int ball;
        private int currentFrame = 1;
        private bool isFirstThrow = true;
        private int score;
        private int[] throws = new int[21];
        private int currentThrow;

        public int Score
        {
            get
            {
                return ScoreForFrame(currentFrame - 1);
            }
        }

        public int CurrentFrame
        {
            get { return currentFrame; }
        }

        public void Add(int pins)
        {
            throws[currentThrow++] = pins;
            score += pins;

            AdjustCurrentFrame(pins);
        }

        private void AdjustCurrentFrame(int pins)
        {
            if (isFirstThrow)
            {
                if (pins == 10) // Strike
                {
                    currentFrame++;
                }
                else
                {
                    isFirstThrow = false;
                }
            }
            else
            {
                isFirstThrow = true;
                currentFrame++;
            }

            if (currentFrame > 11)
            {
                currentFrame = 11;
            }
        }

        public int ScoreForFrame(int theFrame)
        {
            ball = 0;
            int score = 0;

            for (int currentFrame = 0; currentFrame < theFrame; currentFrame++)
            {
                if (Strike())   // Strike
                {
                    score += 10 + NextTwoBallsForStrike;
                    ball++;
                }
                else if (Spare())
                {
                    score += 10 + NextBallForSpare;
                    ball += 2;
                }
                else
                {
                    score += HandleSecondThrow();
                }
            }

            return score;
        }

        private int HandleSecondThrow()
        {
            int score = 0;

            // spare
            if (Spare())
            {
                ball += 2;
                score += 10 + NextBallForSpare;
            }
            else
            {
                score += TwoBallsInFrame;
                ball += 2;
            }

            return score;
        }

        private bool Strike()
        {
            return throws[ball] == 10;
        }

        private int NextTwoBallsForStrike
        {
            get { return (throws[ball + 1] + throws[ball + 2]); }
        }

        private bool Spare()
        {
            return throws[ball] + throws[ball + 1] == 10;
        }

        private int NextBallForSpare
        {
            get { return throws[ball + 2]; }
        }

        private int TwoBallsInFrame
        {
            get { return throws[ball] + throws[ball + 1]; }
        }

    }
}
