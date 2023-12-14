using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Arcanoid
{
    public partial class Arcanoid : Form
    {
        MapController map;
        Player player;
        Physics2DController physics;
        public Label scoreLabel;
        public Label livesLabel;
        public Label livesDedLabel;
        static public Label RandomBonus;
        public Arcanoid()
        {
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            InitializeComponent();
            scoreLabel = new Label();
            scoreLabel.Location = new Point((MapController.mapWidth) * 20 + 2, 50);
            scoreLabel.AutoSize = true;

            livesLabel = new Label();
            livesLabel.Location = new Point((MapController.mapWidth) * 20 + 2, 100);
            livesLabel.AutoSize = true;
            livesDedLabel = new Label();

            livesDedLabel.Location = new Point((MapController.mapWidth) * 20 + 2, 150);
            livesDedLabel.AutoSize = true;
            RandomBonus = new Label();

            RandomBonus.Location = new Point((MapController.mapWidth) * 20 + 2, 200);
            RandomBonus.AutoSize = true;

            this.Controls.Add(RandomBonus);
            this.Controls.Add(scoreLabel);
            this.Controls.Add(livesLabel);
            this.Controls.Add(livesDedLabel);
            timer1.Tick += new EventHandler(update);
            timer2.Tick += new EventHandler(animationUpdate);
            timer2.Interval = 100;
            timer3.Tick += Tick;
            timer3.Interval = 1000;
            this.KeyDown += new KeyEventHandler(inputCheck);
            Init();
        }
        private void Tick(object sender, EventArgs e)
        {
            player.platformWidth = 0;
            player.spidX = 4;
            player.spidY = 4;
            timer3.Stop();
        }
        private void animationUpdate(object sender, EventArgs e)
        {
            if (player.platformAnnimationFrame < 2)
                player.platformAnnimationFrame++;
            else
                player.platformAnnimationFrame = 0;
        }
        
        public void shake()
        {
            if (player.platformWidth == 0)
            {
                map.map[player.platformY / 20, player.platformX / 20] = 9;
                map.map[player.platformY / 20, player.platformX / 20 + 1] = 99;
                map.map[player.platformY / 20, player.platformX / 20 + 2] = 999;
            }
            if (player.platformWidth == 1)
            {
                map.map[player.platformY / 20, player.platformX / 20] = 9;
                map.map[player.platformY / 20, player.platformX / 20 + 1] = 99;
                map.map[player.platformY / 20, player.platformX / 20 + 2] = 999;
                map.map[player.platformY / 20, player.platformX / 20 + 3] = 9999;
            }
            if (player.platformWidth == 2)
            {
                map.map[player.platformY / 20, player.platformX / 20] = 9;
                map.map[player.platformY / 20, player.platformX / 20 + 1] = 99;
            }
            else;
        }

        private void erase()
        {
            if (player.platformWidth == 0)
            {
                map.map[player.platformY / 20, player.platformX / 20] = 0;
                map.map[player.platformY / 20, player.platformX / 20 + 1] = 0;
                map.map[player.platformY / 20, player.platformX / 20 + 2] = 0;
            }
            if (player.platformWidth == 1)
            {
                map.map[player.platformY / 20, player.platformX / 20] = 0;
                map.map[player.platformY / 20, player.platformX / 20 + 1] = 0;
                map.map[player.platformY / 20, player.platformX / 20 + 2] = 0;
                map.map[player.platformY / 20, player.platformX / 20 + 3] = 0;
            }
            if (player.platformWidth == 2)
            {
                map.map[player.platformY / 20, player.platformX / 20] = 0;
                map.map[player.platformY / 20, player.platformX / 20 + 1] = 0;
            }
            else;
        }
        private void Escape()
        {
            timer1.Stop();
            DialogResult result = MessageBox.Show("Ваш рахунок: " + Convert.ToString(player.score) + " очків\n" + "\n" + "Вийти в меню?", "Пауза ", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Close();
            }
            else
            {
                timer1.Start();
            }
        }

        private void inputCheck(object sender, KeyEventArgs e)
        {
            erase();
            switch (e.KeyCode)
            {
                case Keys.Right:
                    if (player.platformWidth == 0)
                    {
                        if (player.platformX / 20 + 2 < MapController.mapWidth - 1)
                            player.platformX += 10;
                    }
                    if (player.platformWidth == 1)
                    {
                        if (player.platformX / 20 + 2 < MapController.mapWidth - 2)
                            player.platformX += 10;
                        timer3.Start();
                    }
                    if (player.platformWidth == 2)
                    {
                        if (player.platformX / 20 + 2 < MapController.mapWidth - 0)
                            player.platformX += 10;
                        timer3.Start();
                    }
                    else;
                    timer1.Start();
                    break;
                case Keys.Left:
                    if (player.platformX > 20)
                        player.platformX -= 10;
                    timer1.Start();
                    break;
                case Keys.Escape:
                    Escape();
                    break;
                case Keys.Enter:
                    Init();
                    break;

            }
            shake();
        }
        public void update(object sender, EventArgs e)
        {
            if (player.ballY / 20 + player.dirY > MapController.mapHeight - 1)
            {
                player.lives--;
                livesDedLabel.Text = "💔 - 1 Lives ";
                Thread myThread1 = new Thread(Print);
                myThread1.Start();
                void Print()
                {
                    Console.Beep(400, 100);
                }
                if (player.lives <= 0)
                {
                    livesLabel.Text = "Lives: 0";
                    timer1.Stop();
                    System.Media.SystemSounds.Hand.Play();
                    MessageBox.Show("Ваш рахунок: " + Convert.ToString(player.score) + " очків", "ВИ ПРОГРАЛИ", MessageBoxButtons.OK);
                    Init();
                }
                else Continue();
            }

            map.map[player.ballY / 20, player.ballX / 20] = 0;
            if (!physics.IsCollide(player, map, scoreLabel, livesLabel, livesDedLabel))
                player.ballX += player.dirX * player.spidX;
            if (!physics.IsCollide(player, map, scoreLabel, livesLabel, livesDedLabel))
                player.ballY += player.dirY * player.spidY;
            map.map[player.ballY / 20, player.ballX / 20] = 8;
            shake();
            Invalidate();
        }
        public void Continue()
        {
            timer1.Interval = 1;
            scoreLabel.Text = "Score: " + player.score;
            livesLabel.Text = "Lives: " + player.lives;
            shake();
            map.map[player.ballY / 20, player.ballX / 20] = 0;

            player.ballY = 550;
            player.ballX = 220;
            map.map[player.ballY / 20, player.ballX / 20] = 8;
            player.dirX = 1;
            player.dirY = -1;
        }
        public void Init()
        {
            map = new MapController();
            player = new Player();
            physics = new Physics2DController();
            this.Width = (MapController.mapWidth + 10) * 20;
            this.Height = (MapController.mapHeight + 3) * 20;
            timer1.Interval = 1;
            player.score = 0;
            player.lives = 5;
            scoreLabel.Text = "Score: " + player.score;
            livesLabel.Text = "Lives: " + player.lives;
            for (int i = 0; i < MapController.mapHeight; i++)
            {
                for (int j = 0; j < MapController.mapWidth; j++)
                {
                    map.map[i, j] = 0;
                }
            }
            player.platformX = (MapController.mapWidth - 1) / 2 * 19;
            player.platformY = (MapController.mapHeight - 1) * 20;
            shake();
            player.ballY = 550;
            player.ballX = 210;
            map.map[player.ballY / 20, player.ballX / 20] = 8;
            player.dirX = 1;
            player.dirY = -1;
            map.GeneratePlatforms();
            timer2.Start();
        }
        private void OnPaint(object sender, PaintEventArgs e)
        {
            map.DrawArea(e.Graphics);
            map.DrawMap(e.Graphics, player, RandomBonus);
        }

        private void Arcanoid_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            Menu f = new Menu();
                f.Show();
                Close();  
        }
    }
}
