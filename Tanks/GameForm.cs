using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Tanks.Controller;

namespace Tanks
{
    delegate void KeyDownHandler(KeyEventArgs e);
    public partial class GameForm : Form
    {
        private readonly PackmanController controller;
        private Form informationForm;
        private int width;
        private int height;



        public GameForm(int width, int height, int tanksValue, int appleValue)
        {
            this.width = width - width % 15;
            this.height = height - height % 15;
            InitializeComponent();
            controller = new PackmanController();
            ClientSize = new Size(this.width, this.height + 30);
            MinimumSize = new Size(this.width + 16, this.height + 69);
            GameMapPictureBox.Size = new Size(this.width, this.height);
            GameMapPictureBox.Location = new Point(0, 30);

            controller.Initial(tanksValue, appleValue, GameMapPictureBox.Size);
            GameMapPictureBox.Image = controller.GetNextBitmap(GameMapPictureBox.Size);
        }


        private void StartGame_Click(object sender, EventArgs e)
        {
            GameResultLabel.Visible = false;
            mock.Select();
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
                    ScoresLabel.Text = controller.GetScores();

                }
                else
                {
                    timer.Stop();
                    if (controller.GetGameResult())
                    {
                        GameResultLabel.Text = "Ты победил!";
                    }
                    else
                    {
                        GameResultLabel.Text = "Ты проиграл!";
                    }
                    GameResultLabel.Visible = true;
                    controller.Initial();
                    GameMapPictureBox.Image = controller.GetNextBitmap(GameMapPictureBox.Size);
                    ScoresLabel.Text = controller.GetScores();
                    MenuImage.Show();
                    StartButton.Show();
                    StartButton.Select();

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

        private void OpenInfoPanel_Click(object sender, EventArgs e)
        {
            informationForm = new InformationForm(controller.GetInformationSource());
            informationForm.Show();
        }
    }
}
