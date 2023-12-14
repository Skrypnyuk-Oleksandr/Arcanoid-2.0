using System;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace Arcanoid
{
    public class Physics2DController
    {
        Random rnd = new Random();
        public bool IsCollide(Player player, MapController map, Label scoreLabel, Label livesLabel, Label livesDedLabel)
        {
            bool isColliding = false;
            if (player.ballX / 20 + player.dirX > MapController.mapWidth - 1 || player.ballX / 20 + player.dirX < 0)
            {
                Thread myThread1 = new Thread(Print);
                myThread1.Start();
                void Print()
                {
                    Console.Beep(1500, 50);
                }
                player.dirX *= -1;
                isColliding = true;
                livesDedLabel.Text = "";
            }
            if (player.ballY / 20 + player.dirY < 0)
            {
                Thread myThread1 = new Thread(Print);
                myThread1.Start();
                void Print()
                {
                    Console.Beep(1500, 50);
                }
                player.dirY *= -1;
                isColliding = true;
                livesDedLabel.Text = "";
            }
            if (map.map[player.ballY / 20 + player.dirY, player.ballX / 20] != 0)
            {
                Thread myThread1 = new Thread(Print);
                myThread1.Start();
                void Print()
                {
                    Console.Beep(1500, 50);
                    Console.WriteLine(player.spidY);
                }
                livesDedLabel.Text = "";
                bool addScore = false;
                isColliding = true;
                if (map.map[player.ballY / 20 + player.dirY, player.ballX / 20] > 10 && map.map[player.ballY / 20 + player.dirY, player.ballX / 20] < 99)
                {
                    map.map[player.ballY / 20 + player.dirY, player.ballX / 20] = 0;
                    map.map[player.ballY / 20 + player.dirY, player.ballX / 20 - 1] = 0;
                    addScore = true;
                }
                else if (map.map[player.ballY / 20 + player.dirY, player.ballX / 20] < 9)
                {
                    map.map[player.ballY / 20 + player.dirY, player.ballX / 20] = 0;
                    map.map[player.ballY / 20 + player.dirY, player.ballX / 20 + 1] = 0;
                    addScore = true;
                }
                if (addScore)
                {
                    int r = rnd.Next(1, 10);
                    Thread myThread2 = new Thread(Print2);
                    myThread2.Start();
                    void Print2()
                    {
                        Console.Beep(1500, 200);
                    }
                    if (r > 1)
                    {
                        
                        SystemSounds.Question.Play();
                        player.platformWidth = rnd.Next(1, 3);

                    }
                    if(r == 20)
                    {
                        Random rand = new Random();
                        var bonus = new[] { 2, 6 };
                        int XY = bonus[rand.Next(bonus.Length)];
                        player.spidX = XY;
                        Console.WriteLine(player.spidX);
                        player.spidY = XY;
                        if (player.spidX > 4)
                        {
                            Arcanoid.RandomBonus.Text = "Random Bonus:" + "\n" + " ✚ speed ";
                        }
                        if (player.spidX < 4)
                        {
                           Arcanoid.RandomBonus.Text = "Random Bonus:" + "\n" + " ᠆ speed ";
                        }
                    }
                    else;
                    player.score += 100;
                    livesDedLabel.Text = "❂ + 100 Score";
                    if (player.score % 1000 == 0 && player.score > 0)
                    {
                        player.lives++;
                        livesLabel.Text = "Lives: " + player.lives;
                        Thread myThread3 = new Thread(Print3);
                        myThread3.Start();
                        void Print3()
                        {
                            Console.Beep(2500, 100);
                        }
                        livesDedLabel.Text = "❤ + 1 Lives ";
                        map.AddLine();
                    }
                }
                player.dirY *= -1;
            }
            scoreLabel.Text = "Score: " + player.score;
            return isColliding;
        }
    }
}

