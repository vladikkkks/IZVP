using System;
using System.Drawing;
using System.Windows.Forms;

namespace MobilePlanSelector
{
    public class MainForm : Form
    {
        private NumericUpDown numInternet, numMinutes, numSMS;
        private Button btnSelectPlan;
        private Label lblResult;

        public MainForm()
        {
            Text = "Підбір тарифу мобільного зв'язку";
            Size = new Size(400, 400);
            StartPosition = FormStartPosition.CenterScreen;

            // --- Інтернет ---
            Controls.Add(new Label { Text = "Необхідний обсяг інтернету (ГБ):", Location = new Point(20, 20), AutoSize = true });
            numInternet = new NumericUpDown { Location = new Point(20, 45), Width = 340, Maximum = 1000, Minimum = 0 };
            Controls.Add(numInternet);

            // --- Хвилини ---
            Controls.Add(new Label { Text = "Хвилини на інші мережі:", Location = new Point(20, 85), AutoSize = true });
            numMinutes = new NumericUpDown { Location = new Point(20, 110), Width = 340, Maximum = 5000, Minimum = 0 };
            Controls.Add(numMinutes);

            // --- SMS ---
            Controls.Add(new Label { Text = "Кількість SMS:", Location = new Point(20, 150), AutoSize = true });
            numSMS = new NumericUpDown { Location = new Point(20, 175), Width = 340, Maximum = 1000, Minimum = 0 };
            Controls.Add(numSMS);

            // --- Кнопка ---
            btnSelectPlan = new Button { Text = "Підібрати тариф", Location = new Point(20, 220), Size = new Size(340, 40) };
            btnSelectPlan.Click += BtnSelectPlan_Click;
            Controls.Add(btnSelectPlan);

            // --- Результат ---
            lblResult = new Label { Text = "Введіть ваші потреби та натисніть кнопку...", Location = new Point(20, 280), Size = new Size(340, 60), Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.Blue };
            Controls.Add(lblResult);
        }

        private void BtnSelectPlan_Click(object sender, EventArgs e)
        {
            // Зчитуємо значення з NumericUpDown
            int gb = (int)numInternet.Value;
            int minutes = (int)numMinutes.Value;
            int sms = (int)numSMS.Value;

            string recommendedPlan = "";
            int price = 0;

            // Логіка підбору тарифу (if/else)
            if (gb <= 5 && minutes <= 100 && sms <= 50)
            {
                recommendedPlan = "Тариф 'Лайт' (5 ГБ, 100 хв, 50 SMS)";
                price = 100;
            }
            else if (gb <= 15 && minutes <= 300 && sms <= 100)
            {
                recommendedPlan = "Тариф 'Смарт' (15 ГБ, 300 хв, 100 SMS)";
                price = 150;
            }
            else if (gb <= 30 && minutes <= 500 && sms <= 300)
            {
                recommendedPlan = "Тариф 'Про' (30 ГБ, 500 хв, 300 SMS)";
                price = 250;
            }
            else
            {
                // Якщо потреби перевищують ліміти попередніх тарифів
                recommendedPlan = "Тариф 'Безлім' (Безліміт інтернет, 1000+ хв, 500+ SMS)";
                price = 350;
            }

            // Виводимо результат
            lblResult.Text = $"Найвигідніший для вас:\n{recommendedPlan}\nВартість: {price} грн/місяць";
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}