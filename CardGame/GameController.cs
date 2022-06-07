using CardGame.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace CardGame
{
    public static class GameController
    {
        public static Stats stats = new Stats();
        

        public static  void Init(Form current)
        {
            Timer turnTimer = new Timer();
            turnTimer.Interval = 1000;
            turnTimer.Tick += (sender, EventArgs) => {
                stats.TurnTime += 1;
                GraphicsController.timeLabel.Text = stats.TurnTime.ToString();
                if (stats.TurnTime == 10)
                {
                    stats.TurnTime = 0;
                    NextTurn(current, turnTimer);
                }
                
            };
                GraphicsController.InitStats(current);
            GraphicsController.InitButtons(current, turnTimer);
            GraphicsController.NewCards(current,turnTimer);
        }

        public static void NextTurn(Form current, Timer turnTimer)
        {
            stats.CurrentTurn++;
            stats.TurnTime = 0;

            UpdateStats(current, turnTimer);
            for (int i = 0; i < 3; i++)
            {
                current.Controls.Remove(GraphicsController.buttons[i]);
            }
            GraphicsController.NewCards(current, turnTimer);
        }

        public static void FireBall(object sender, EventArgs e, Form current, Timer turnTimer)
        {
            if (stats.CurrentTurn % 2 == 1)
            {
                stats.LastTurnDamage = (int)(15 + stats.VillainHealth / 5 - stats.DamageReduction);
                stats.VillainHealth -= (int)(15 + stats.VillainHealth / 5 - stats.DamageReduction);               
            }
            else
            {
                stats.LastTurnDamage = (int)(15 + stats.HeroHealth / 5 - stats.DamageReduction);
                stats.HeroHealth -= (int)(15 + stats.HeroHealth / 5 - stats.DamageReduction);               
            }
            
            NextTurn(current, turnTimer);
        }

        public static void IceShard(object sender, EventArgs e, Form current, Timer turnTimer)
        {
            if (stats.CurrentTurn % 2 == 1)
                stats.VillainHealth -= 30 - stats.DamageReduction;
            else
                stats.HeroHealth -= 30 - stats.DamageReduction;
            stats.LastTurnDamage = 30 - stats.DamageReduction;
            NextTurn(current, turnTimer);
        }

        public static void Heal(object sender, EventArgs e, Form current, Timer turnTimer)
        {
            stats.LastTurnDamage = 0;
            if (stats.CurrentTurn % 2 == 1)
                stats.HeroHealth += (int)(25 * (stats.CurrentTurn * 0.15 + 1));
            else
                stats.VillainHealth += (int)(25 * (stats.CurrentTurn * 0.15 + 1));
            NextTurn(current, turnTimer);
        }
        

        public static void LifeSteal(object sender, EventArgs e, Form current, Timer turnTimer)
        {
            if (stats.CurrentTurn % 2 == 1)
            {
                stats.HeroHealth += (int)(15);
                stats.VillainHealth -= (int)(20 - stats.DamageReduction);
            }
            else
            {
                stats.VillainHealth += (int)(15);
                stats.HeroHealth -= (int)(20 - stats.DamageReduction);
            }
            stats.LastTurnDamage = (int)(20 - stats.DamageReduction);
            NextTurn(current, turnTimer);
        }

        public static void HealBoth(object sender, EventArgs e, Form current, Timer turnTimer)
        {
            stats.LastTurnDamage = 0;
            stats.TurnTime = 0;
            if (stats.CurrentTurn % 2 == 1)
            {
                stats.VillainHealth += 50;
                stats.HeroHealth += 30;
            }
            else
            {
                stats.VillainHealth += 30;
                stats.HeroHealth += 50;
            }

            UpdateStats(current, turnTimer);
            for (int i = 0; i < 3; i++)
            {
                current.Controls.Remove(GraphicsController.buttons[i]);
            }
            GraphicsController.NewCards(current, turnTimer);
        }

        public static void ReduceDamage(object sender, EventArgs e, Form current, Timer turnTimer)
        {
            if (stats.CurrentTurn % 2 == 1)
                stats.HeroHealth += (int)(15 * (stats.CurrentTurn * 0.1 + 1));
            else
                stats.VillainHealth += (int)(15 * (stats.CurrentTurn * 0.1 + 1));
            stats.LastTurnDamage = 0;
            NextTurn(current, turnTimer);
            stats.DamageReduction = 20;
        }

        public static void Crit(object sender, EventArgs e, Form current, Timer turnTimer)
        {
            if (stats.LastTurnDamage != 0)
            {
                if (stats.CurrentTurn % 2 == 1)
                {
                    stats.VillainHealth -= (int)(stats.LastTurnDamage * 1.5 - stats.DamageReduction);
                }
                else
                {
                    stats.HeroHealth -= (int)(stats.LastTurnDamage * 1.5 - stats.DamageReduction);
                }
            }
            NextTurn(current, turnTimer);
        }

        public static void SwapHP(object sender, EventArgs e, Form current, Timer turnTimer)
        {
            stats.LastTurnDamage = 0;
            var health = stats.VillainHealth;
            stats.VillainHealth = stats.HeroHealth;
            stats.HeroHealth = health;
            NextTurn(current, turnTimer);
        }

        public static void Curse(object sender, EventArgs e, Form current, Timer turnTimer)
        {
            stats.LastTurnDamage = 0;
            if (stats.CurrentTurn % 2 == 1)
            {
                stats.VillainCurse += 10;
            }
            else
            {
                stats.HeroCurse += 10;
            }
            NextTurn(current,turnTimer);
        }

        public static void Purify(object sender, EventArgs e, Form current, Timer turnTimer)
        {
            if (stats.CurrentTurn % 2 == 1)
            {
                stats.HeroHealth += 20;
                stats.HeroCurse -= 15;
            }
            else
            {
                stats.VillainHealth += 20;
                stats.VillainCurse -= 15;             
            }
            stats.LastTurnDamage = 0;
            NextTurn(current, turnTimer);
        }

        public static void CurseStrike(object sender, EventArgs e, Form current, Timer turnTimer)
        {

            if (stats.CurrentTurn % 2 == 1)
            {
                stats.VillainCurse += 5;
                stats.VillainHealth -= 15 - (int)(stats.DamageReduction * 0.75);
            }
            else
            {
                stats.HeroCurse += 5;
                stats.HeroHealth -= 15 - (int)(stats.DamageReduction * 0.75);
            }
            if(stats.DamageReduction == 0)
                stats.LastTurnDamage = 15;
            NextTurn(current, turnTimer);
        }

        public static void BloodyStrike(object sender, EventArgs e, Form current, Timer turnTimer)
        {

            if (stats.CurrentTurn % 2 == 1)
            {
                stats.HeroCurse -= 5;
                stats.HeroHealth -= 10;
                stats.VillainHealth -= 40 - stats.DamageReduction;
            }
            else
            {
                stats.VillainCurse -= 5;
                stats.VillainHealth -= 10;
                stats.HeroHealth -= 40 - stats.DamageReduction;               
            }
            stats.LastTurnDamage = 40;
            NextTurn(current, turnTimer);
        }

        public static void LightningStrike(object sender, EventArgs e, Form current, Timer turnTimer)
        {
            if (stats.CurrentTurn % 2 == 1)
            {
                stats.VillainHealth -= 25 - stats.DamageReduction;              
            }
            else
            {
                stats.HeroHealth -= 25 - stats.DamageReduction;
            }
            stats.LightningDamage = 14;
            stats.LastTurnDamage = 30 - stats.DamageReduction;
            NextTurn(current, turnTimer);
        }

        public static int[] RandomizeCards()
        {
            Random rndCard = new Random();
            var cardsArray = new int[3];
            cardsArray[0] = rndCard.Next(1, 14);
            cardsArray[1] = rndCard.Next(1, 14);
            while(cardsArray[0] == cardsArray[1])
            {
                cardsArray[1] = rndCard.Next(1, 14);
            }
            cardsArray[2] = rndCard.Next(1, 14);
            while (cardsArray[0] == cardsArray[2] || cardsArray[1] == cardsArray[2])
            {
                cardsArray[2] = rndCard.Next(1, 14);
            }
            return cardsArray;
        }
        private static void UpdateStats(Form current, Timer turnTimer)
        {
            stats.HeroHealth -= stats.HeroCurse;
            stats.VillainHealth -= stats.VillainCurse;
            GraphicsController.heroHealthScore.Text = "Здоровье " + stats.HeroHealth.ToString();
            GraphicsController.villainHealthScore.Text = "Здоровье " + stats.VillainHealth.ToString();
            GraphicsController.turnCounter.Text = "Текущий ход: " + stats.CurrentTurn.ToString();
            GraphicsController.villainCurseLabel.Text = "Проклятие: " + stats.VillainCurse.ToString();
            GraphicsController.heroCurseLabel.Text = "Проклятие: " + stats.HeroCurse.ToString();
            stats.DamageReduction = 0;
            if (stats.LightningDamage == 15)
            {
                stats.HeroHealth -= 15;
                stats.LightningDamage = 0;
            }
            else
                if (stats.LightningDamage > 0)
                stats.LightningDamage = 15;
            if (stats.HeroHealth <= 0)
            {
                turnTimer.Stop();
                if (MessageBox.Show("Игра окончена", "Второй игрок победил", MessageBoxButtons.OK, MessageBoxIcon.Asterisk) == DialogResult.OK)
                    Form1.ActiveForm.Close();
            }

            if (stats.VillainHealth <= 0)
            {
                turnTimer.Stop();
                if (MessageBox.Show("Игра окончена", "Первый игрок победил", MessageBoxButtons.OK, MessageBoxIcon.Asterisk) == DialogResult.OK)
                    Form1.ActiveForm.Close();
            }
        }
    }
}
