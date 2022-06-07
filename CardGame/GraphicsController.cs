using CardGame.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CardGame
{
    public static class GraphicsController
    {
        //private static  Button[] buttons = new Button[4];
        public static Label heroHealthScore = new Label();
        public static Stats stats = new Stats();
        public static Label villainHealthScore = new Label();
        public static Label timeLabel = new Label();
        public static Label turnCounter = new Label();
        static FontFamily fontFamily = new FontFamily("Arial");
        public static Button[] buttons = new Button[4];
        public static Label heroCurseLabel = new Label();
        public static Label villainCurseLabel = new Label();

        public static void InitButtons(Form current, Timer turnTimer)
        {
            
            for (int i = 0; i < 3; i++)
            {
                Button button = new Button();              
                button.Size = new Size(280, 275);
                //button.Location = new Point(300 * i + 300- button.Width / 2, 500);
                button.Location = new Point(300 * i + 300 - button.Width / 2, 800);
                button.FlatAppearance.BorderSize = 0;
                button.FlatStyle = FlatStyle.Flat;
                current.Controls.Add(button);
                buttons[i] = button;
                buttons[i].Font = new Font(fontFamily, 10, FontStyle.Italic, GraphicsUnit.Point);
            }
        }

        public static void InitHero(Form current)
        {
            PictureBox pic = new PictureBox();
            pic.Image = Resources.Mage;
            pic.Size = new Size(300, 450);
            pic.Location = new Point(50, 27);
            pic.BackColor = Color.Transparent;
            pic.SizeMode = PictureBoxSizeMode.CenterImage;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            current.Controls.Add(pic);
        }

        public static void InitStats(Form current)
        {
            heroHealthScore.Size = new Size(150, 20);
            heroHealthScore.Location = new Point(100, 10);
            heroHealthScore.BackColor = Color.IndianRed;
            heroHealthScore.Text = "Здоровье " + stats.HeroHealth.ToString();
            current.Controls.Add(heroHealthScore);

            villainHealthScore.Size = new Size(150, 20);
            villainHealthScore.Location = new Point(870, 10);
            villainHealthScore.BackColor = Color.IndianRed;
            villainHealthScore.Text = "Здоровье " + stats.VillainHealth.ToString();
            current.Controls.Add(villainHealthScore);

            timeLabel.Size = new Size(150, 20);
            timeLabel.Location = new Point(450, 10);
            timeLabel.BackColor = Color.Goldenrod;
            timeLabel.Text = stats.TurnTime.ToString();
            current.Controls.Add(timeLabel);

            turnCounter.Size = new Size(200,50);
            turnCounter.Location = new Point(1070, 720);
            turnCounter.BackColor = Color.Goldenrod;
            turnCounter.Font = new Font(fontFamily, 10, FontStyle.Italic, GraphicsUnit.Point);
            turnCounter.Text = "Текущий ход: " + stats.CurrentTurn.ToString();
            current.Controls.Add(turnCounter);

            heroCurseLabel.Size = new Size(110, 20);
            heroCurseLabel.Location =  new Point(350, 400);
            heroCurseLabel.BackColor = Color.MediumPurple;
            heroCurseLabel.Font = new Font(fontFamily, 10, FontStyle.Italic, GraphicsUnit.Point);
            heroCurseLabel.Text = "Проклятие: " + stats.HeroCurse.ToString();
            current.Controls.Add(heroCurseLabel);

            villainCurseLabel.Size = new Size(110, 20);
            villainCurseLabel.Location = new Point(670, 400);
            villainCurseLabel.BackColor = Color.Purple;
            villainCurseLabel.Font = new Font(fontFamily, 10, FontStyle.Italic, GraphicsUnit.Point);
            villainCurseLabel.Text = "Проклятие: " + stats.VillainCurse.ToString();
            current.Controls.Add(villainCurseLabel);
        }
        public static void InitVillain(Form current)
        {
            PictureBox pic = new PictureBox();
            pic.Image = Resources.EvilMage;
            pic.Size = new Size(400, 470);
            pic.Location = new Point(720 , 27);
            pic.BackColor = Color.Transparent;
            pic.SizeMode = PictureBoxSizeMode.CenterImage;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            current.Controls.Add(pic);
        }

        public static void SetBackGroundImage(Form current, int n)
        {
            if (n == 1)
                current.BackgroundImage = Resources.MainMenu;
            if (n == 2)
                current.BackgroundImage = Resources.BGMap;
            current.BackgroundImageLayout = ImageLayout.Stretch;
        }

        public static void NewCards(Form current,Timer turnTimer)
        {
            turnTimer.Stop();           
            Timer timer = new Timer();
            InitButtons(current, turnTimer);
            for (int i = 0; i < 3; i++)
            {
                var button = buttons[i];
                if (stats.CurrentTurn % 2 == 0)
                    button.BackgroundImage = Resources.BlackCard;
                else
                    button.BackgroundImage = Resources.WhiteCard;
                button.BackgroundImageLayout = ImageLayout.Stretch;
                button.Enabled = false;
                button.Text = "";
                button.Location = new Point(300 * i + 300 - button.Width / 2, 800);
            }  
            timer.Interval = 500;
            timer.Start();
            timer.Tick += (sender, EventArgs) =>
            {
                GraphicsController.MoveCards(sender, EventArgs, timer,turnTimer,current);
            };           
        }

        public static void MoveCards(object sender, EventArgs e, Timer timer,Timer turnTimer, Form current)
        {
            buttons[0].Location = new Point(buttons[0].Location.X, buttons[0].Location.Y - 50);
            
            buttons[1].Location = new Point(buttons[1].Location.X, buttons[1].Location.Y - 50);
            
            buttons[2].Location = new Point(buttons[2].Location.X, buttons[2].Location.Y - 50);

            if (buttons[2].Top <=  500)
            {
                TurnOverCards(current,turnTimer);
                timer.Stop();
            }
        }

        public static void InitCards(Form current, Timer turnTimer)
        {
            var cardIndex = GameController.RandomizeCards();
            for (int i = 0; i <3;i++)
            {
                AssignCardAbility(current, turnTimer, cardIndex[i],buttons[i]);
            }
        }

        public static void AssignCardAbility(Form current, Timer turnTimer,int cardIndex,Button button)
        {
            if(cardIndex == 1)
            {
                button.Text = "Огненный шар\r\n Наносит 15 урона \r\n Плюс одну пятую здоровья оппонента";
                button.Click += (sender, EventArgs) => { GameController.FireBall(sender, EventArgs, current, turnTimer); };
            }

            if (cardIndex == 2)
            {
                button.Text = "Ледяная глыба\r\n Выпускает ледяную глыбу\r\n Наносит 30 урона";
                button.Click += (sender, EventArgs) => { GameController.IceShard(sender, EventArgs, current, turnTimer); };
            }

            if (cardIndex == 3)
            {
                button.Text = "Лечение\r\n Восполняет 25 здоровья\r\n Увеличивется на 15% умноженные \r\nна текущее число ходов";
                button.Click += (sender, EventArgs) => { GameController.Heal(sender, EventArgs, current, turnTimer); };
            }

            if (cardIndex == 4)
            {
                button.Text = "Похищение жизни\r\n Восстанавливает 15 здоровья и наносит 20 урона";
                button.Click += (sender, EventArgs) => { GameController.LifeSteal(sender, EventArgs, current, turnTimer); };
            }

            if (cardIndex == 5)
            {               
                button.Text = "Солидарность\r\n Восстанавливает вам 30 здоровья,\r\n а врагу 50\r\nВы делаете ещё один ход";
                button.Click += (sender, EventArgs) => { GameController.HealBoth(sender, EventArgs, current, turnTimer); };               
            }

            if (cardIndex == 6)
            {
                button.Text = "Каменная кожа\r\nУменьшает получаемый на следующем ходу урон на 20 \r\nВосстанавливает 15 здоровья \r\nУвеличивется на 10 %\r\n умноженные на число ходов";
                button.Click += (sender, EventArgs) => { GameController.ReduceDamage(sender, EventArgs, current, turnTimer); };
            }

            if (cardIndex == 7)
            {
                button.Text = "Перенаправление\r\nНаносит противнику в 1.5 раза\r\nбольше урона, чем он нанес\r\n вам на предыдущем ходу\r\n Не работает с обменом";
                button.Click += (sender, EventArgs) => { GameController.Crit(sender, EventArgs, current, turnTimer); };
            }

            if (cardIndex == 8)
            {
                button.Text = "Обмен\r\n Вы меняетесь здоровьем";
                button.Click += (sender, EventArgs) => { GameController.SwapHP(sender, EventArgs, current, turnTimer); };
            }

            if (cardIndex == 9)
            {
                button.Text = "Проклятие\r\nКаждый ход противник \r\nполучает 10 урона.\r\n Суммируется";
                button.Click += (sender, EventArgs) => { GameController.Curse(sender, EventArgs, current, turnTimer); };
            }

            if (cardIndex == 10)
            {
                button.Text = "Очищение\r\nВосстанавливает 20 здоровья\r\nУменьшает Проклятие на 15";
                button.Click += (sender, EventArgs) => { GameController.Purify(sender, EventArgs, current, turnTimer); };
            }

            if (cardIndex == 11)
            {
                button.Text = "Проклятый смерч\r\nНаносит 15 урона\r\nТакже каждый ход противник \r\nполучает 5 урона.\r\n Суммируется ";
                button.Click += (sender, EventArgs) => { GameController.CurseStrike(sender, EventArgs, current, turnTimer); };
            }

            if (cardIndex == 12)
            {
                button.Text = "Магия крови\r\nНаносит вам 10 урона\r\nОчищает от 5 ед. проклятия\r\nНаносит 40 урона";
                button.Click += (sender, EventArgs) => { GameController.BloodyStrike(sender, EventArgs, current, turnTimer); };
            }

            if (cardIndex == 13)
            {
                button.Text = "Удар молнии\r\n \r\n Наносит 25 урона и 10 на следующий ход";
                button.Click += (sender, EventArgs) => { GameController.LightningStrike(sender, EventArgs, current, turnTimer); };
            }
        }

        public static void TurnOverCards(Form current,Timer turnTimer)
        {
            for (int i = 0; i < 3; i++)
            {
                var button = buttons[i];
                if (stats.CurrentTurn % 2 == 0)
                {
                    button.BackgroundImage = Resources.BlackButton;
                    button.ForeColor = Color.White;
                }
                else
                {
                    button.BackgroundImage = Resources.WhiteButton;
                    button.ForeColor = Color.Black;
                }
                button.BackgroundImageLayout = ImageLayout.Stretch;
                button.Enabled = true;
            }
            InitCards(current, turnTimer);
            stats.TurnTime = 0;
            timeLabel.Text = stats.TurnTime.ToString();
            turnTimer.Start();
        }
        public static void DeleteCards()
        {

        }
    }
}
