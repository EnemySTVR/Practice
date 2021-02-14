using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tanks.Controller;

namespace Tanks
{
    delegate void KeyDownHandler(KeyEventArgs e);
    public partial class GameForm : Form
    {
        private readonly PackmanController controller;
        private readonly Form informationForm;
        

        public GameForm(int width, int height, int tanksValue, int appleValue)
        {
            InitializeComponent();
            controller = new PackmanController();
            ClientSize = new Size(width, height);
            MinimumSize = new Size(width + 16, height + 39);
            GameMapPictureBox.Size = new Size(width, height);

            controller.Initial(tanksValue, appleValue, GameMapPictureBox.Size);
            GameMapPictureBox.Image = controller.GetNextBitmap(GameMapPictureBox.Size);

            informationForm = new InformationForm(controller.GetInformationSource());
            informationForm.Show();
        }


        private void StartGame_Click(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            MenuImage.Hide();
            StartButton.Hide();
            timer.Interval = 50;
            timer.Tick += delegate(object obj, EventArgs args)
            {
                var nextBitmap = controller.GetNextBitmap(GameMapPictureBox.Size);
                if (nextBitmap != null)
                {
                    GameMapPictureBox.Image = nextBitmap;
                }
                else
                {
                    timer.Stop();
                    controller.Reset();
                    
                    controller.Initial();
                    GameMapPictureBox.Image = controller.GetNextBitmap(GameMapPictureBox.Size);
                    MenuImage.Show();
                    StartButton.Show();

                }
                
            };
            timer.Start();
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case (char)Keys.Left:
                    controller.ChangePlayerDirection(Direction.Left);
                    break;
                case (char)Keys.Up:
                    controller.ChangePlayerDirection(Direction.Up);
                    break;
                case (char)Keys.Right:
                    controller.ChangePlayerDirection(Direction.Right);
                    break;
                case (char)Keys.Down:
                    controller.ChangePlayerDirection(Direction.Down);
                    break;
                case (char)Keys.Space:
                    controller.KolobokShot();
                    break;
            }
        }

    }
}
