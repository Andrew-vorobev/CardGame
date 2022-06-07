using CardGame.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CardGame
{
    public static class MainMenu
    {
        public static void InitMainMenu(Form current)
        {
            current.Controls.Clear();
            GraphicsController.SetBackGroundImage(current, 1);
            AddMenuComponents(current);
        }

        static void StartGame(object sender, EventArgs e, Form current)
        {
            current.Controls.Clear();
            GraphicsController.SetBackGroundImage(current, 2);
            GameController.Init(current);
            GraphicsController.InitHero(current);
            GraphicsController.InitVillain(current);
        }

        static void AddMenuComponents(Form current)
        {
            Button startButton = new Button();
            startButton.Size = new Size(100, 50);
            startButton.Location = new Point(600 - startButton.Width / 2, 600);
            startButton.Click += (sender, EventArgs) => { StartGame(sender, EventArgs, current); };
            startButton.Text = "Начать игру";
            startButton.BackgroundImage = Resources.WhiteButton;
            startButton.BackgroundImageLayout = ImageLayout.Stretch;
            current.Controls.Add(startButton);

            TextBox textBox = new TextBox();
            textBox.Size = new Size(400, 200);
            textBox.Location = new Point(600 - textBox.Width / 2, 300);
            textBox.ReadOnly = true;
            textBox.Multiline = true;
            textBox.Text = "Вот уже век прошел с того момента как этот мир окутал мрак. Темный маг по имени Рафаам наложил проклятие на этот мир и теперь правит его обломками" +
                "Демонические сущности проникли в мир людей и начали уничтожать поселения, пожирать души, обращать людей в бесов. И вот уже сто лет человечество живёт в постоянном страхе перед " +
                "тёмным магом, но один эльф-чародей осмелился бросить ему вызов." +
                "Одному из игроков придётся взять ответственность за спасение мира и играть за эльфа, вотрой игрок будет отстаивать права нежити. На ход вам даётся 10 секунд."
                +"Успевайте разыгрывать карты и попытайтесь одержать победу. Внимательно читайте описание карт. Некоторые карты накладывают проклятие. Игрок получает урон от проклятия каждый ход. Удачи!";
            textBox.BackColor = Color.OrangeRed;
            textBox.ForeColor = Color.Black;
            textBox.BorderStyle = BorderStyle.None;
            current.Controls.Add(textBox);

        }
    }
}
