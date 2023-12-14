using System;
using System.Drawing;
using System.Windows.Forms;

namespace Arcanoid
{
    public class MapController
    {
        
        public Image arcanoidSet;
        public const int mapWidth = 21;
        public const int mapHeight = 30;
        public int[,] map = new int[mapHeight, mapWidth];
        public MapController()
        {
            arcanoidSet = Image.FromFile("arcanoid.png");
        }
        public void AddLine()
        {
            for (int i = mapHeight - 2; i > 0; i--)
            {
                for (int j = 1; j < MapController.mapWidth; j += 2)
                {
                    map[i, j] = map[i - 1, j];
                }
            }
            Random r = new Random();
            for (int j = 1; j < mapWidth; j += 2)
            {
                int currPlatform = r.Next(1, 8);
                map[0, j] = currPlatform;
                map[0, j + 1] = currPlatform + currPlatform * 10;
            }
        }

        public void DrawMap(Graphics g, Player player, Label RandomBonus)
        {
            if (player.platformWidth == 0)
            {
                RandomBonus.Text = "Random Bonus:";
                g.DrawImage(arcanoidSet, new Rectangle(new Point(player.platformX, player.platformY), new Size(60, 20)), 398 + (170 * player.platformAnnimationFrame), 17, 150, 40, GraphicsUnit.Pixel);
            }
            if (player.platformWidth == 1)
            {
                RandomBonus.Text = "Random Bonus:" + "\n" + " ✚ width ";
                g.DrawImage(arcanoidSet, new Rectangle(new Point(player.platformX, player.platformY), new Size(80, 20)), 398 + (170 * player.platformAnnimationFrame), 17, 150, 40, GraphicsUnit.Pixel);
            }
            if (player.platformWidth == 2)
            {
                RandomBonus.Text = "Random Bonus:" + "\n" + " ᠆ 1 width";
                g.DrawImage(arcanoidSet, new Rectangle(new Point(player.platformX, player.platformY), new Size(40, 20)), 398 + (170 * player.platformAnnimationFrame), 17, 150, 40, GraphicsUnit.Pixel);
            }
            else;

            g.DrawImage(arcanoidSet, new Rectangle(new Point(player.ballX, player.ballY), new Size(20, 20)), 806, 549, 73, 73, GraphicsUnit.Pixel);
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    if (map[i, j] == 1)
                    {
                        g.DrawImage(arcanoidSet, new Rectangle(new Point(j * 20, i * 20), new Size(40, 20)), 20, 17, 169, 56, GraphicsUnit.Pixel);
                    }
                    if (map[i, j] == 2)
                    {
                        g.DrawImage(arcanoidSet, new Rectangle(new Point(j * 20, i * 20), new Size(40, 20)), 20, 93, 169, 56, GraphicsUnit.Pixel);
                    }
                    if (map[i, j] == 3)
                    {
                        g.DrawImage(arcanoidSet, new Rectangle(new Point(j * 20, i * 20), new Size(40, 20)), 20, 168, 169, 56, GraphicsUnit.Pixel);
                    }
                    if (map[i, j] == 4)
                    {
                        g.DrawImage(arcanoidSet, new Rectangle(new Point(j * 20, i * 20), new Size(40, 20)), 20, 244, 169, 56, GraphicsUnit.Pixel);
                    }
                    if (map[i, j] == 5)
                    {
                        g.DrawImage(arcanoidSet, new Rectangle(new Point(j * 20, i * 20), new Size(40, 20)), 20, 319, 169, 56, GraphicsUnit.Pixel);
                    }
                    if (map[i, j] == 6)
                    {
                        g.DrawImage(arcanoidSet, new Rectangle(new Point(j * 20, i * 20), new Size(40, 20)), 20, 394, 169, 56, GraphicsUnit.Pixel);
                    }
                    if (map[i, j] == 7)
                    {
                        g.DrawImage(arcanoidSet, new Rectangle(new Point(j * 20, i * 20), new Size(40, 20)), 20, 470, 169, 56, GraphicsUnit.Pixel);
                    }
                    if (map[i, j] == 8)
                    {

                    }
                }
            }
        }
        public void DrawArea(Graphics g)
        {
            g.DrawRectangle(Pens.White, new Rectangle(20, 0, MapController.mapWidth * 19, MapController.mapHeight * 20));
        }
        public void GeneratePlatforms()
        {
            Random r = new Random();
            for (int i = 0; i < MapController.mapHeight / 3; i++)
            {
                for (int j = 1; j < MapController.mapWidth; j += 2)
                {
                    int currPlatform = r.Next(1, 8);
                    map[i, j] = currPlatform;
                    map[i, j + 1] = currPlatform + currPlatform * 10;
                }
            }
        }
    }
}

