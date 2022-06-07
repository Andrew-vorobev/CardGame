using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame
{
    public class Stats
    {
        private static int health = 100;        

        public int HeroHealth
        {
            get { return health; }
            set 
            {
                if (value > 100)
                    health = 100;
                else
                    health = value;
            }
        }

        private static int villainHealth = 100;

        public int VillainHealth
        {
            get { return villainHealth; }
            set {
                    if (value >= 100)
                         villainHealth = 100;
                    else
                    villainHealth = value;
            }
        }

        private static int turn = 1;

        public int CurrentTurn
        {
            get { return turn; }
            set { turn = value; }
        }

        private int reduceDamage = 0;

        public int DamageReduction
        {
            get { return reduceDamage; }
            set { reduceDamage = value; }
        }

        private int turnTime = 0;

        public int TurnTime
        {
            get { return turnTime; }
            set
            {
                if (value > 10)
                    turnTime = 1;
                else
                    turnTime = value;                               
            }
        }

        private int lastTurnDamage;

        public int LastTurnDamage
        {
            get { return lastTurnDamage; }
            set { lastTurnDamage = value; }
        }

        private int heroCurse;

        public int HeroCurse
        {
            get { return heroCurse; }
            set 
            {
                if (value < 0)
                    heroCurse = 0;
                else
                    heroCurse = value;
            }
        }

        private int villainCurse;

        public int VillainCurse
        {
            get { return villainCurse; }
            set 
            { 
                if(value < 0)
                    villainCurse = 0;
                else
                    villainCurse = value;
            }
        }

        private int lightningDamage;

        public int LightningDamage
        {
            get { return lightningDamage; }
            set { lightningDamage = value; }
        }

    }
}
