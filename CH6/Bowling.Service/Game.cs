﻿using System;

namespace Bowling.Service
{
    public class Game
    {
        private int score;
        private int currentFrame = 1;
        private bool isFirstThrow = true;
        private Scorer scorer = new Scorer();

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
            scorer.AddThrow(pins);

            AdjustCurrentFrame(pins);
        }

        private void AdjustCurrentFrame(int pins)
        {
            if (isFirstThrow)
            {
                if (AdjustFrameForStrike(pins) == false)
                {
                    isFirstThrow = false;
                }               
            }
            else
            {
                isFirstThrow = true;
                AdvanceFrame();
            }
        }

        private bool AdjustFrameForStrike(int pins)
        {
            if (pins == 10)
            {
                AdvanceFrame();
                return true;
            }

            return false;
        }

        private void AdvanceFrame()
        {
            currentFrame++;

            if (currentFrame > 11)
            {
                currentFrame = 11;
            }
        }

        public int ScoreForFrame(int theFrame)
        {
            return scorer.ScoreForFrame(theFrame);
        }

    }
}
