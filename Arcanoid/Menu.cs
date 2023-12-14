using System;
using System.Windows.Forms;

namespace Arcanoid
{
    public partial class Menu : Form
    {
        public Menu()
        {
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeComponent();
            label1.Visible = false;
            label1.AutoSize = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Arcanoid f = new Arcanoid();
            f.ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            label1.Text = "Кнопки керування:\n" + "    Right або Left - старт\n"
                + "    Right - рух на право\n" + "    Left - рух на ліво\n"
                + "    Enter - перезапуск\n" + "    Escape - пауза\n" + "\n"
                + "Рандом бонуси: діють 8 секунд\n" + "  + 1 width - збільшення шерини платформи на 1-ну клітину    \n"
                + "   - 1 width - зменшення шерини платформи на 1-ну клітину\n" + "\n" + "Написав: Скрипнюк Олександр Юрійович";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            label1.Text = "На планеті Земля сталася катастрофа, щити які захищали   " + "\n"
                + "планету багато століть вийшли з ладу і почали падати " + "\n" + "на землю. Людям нічого не залишалося крім, як " +
                "\n" + "знищувати їх поки вони не знищили землю.\n\n\n\n\n\n";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}

