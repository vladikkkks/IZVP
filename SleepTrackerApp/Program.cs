using System;
using System.Drawing;
using System.Windows.Forms;

namespace SleepTrackerApp
{
    public class MainForm : Form
    {
        private DateTimePicker dtpSleepTime, dtpWakeTime;
        private ComboBox cbAgeCategory;
        private Button btnCalculate;
        private Label lblResult;

        public MainForm()
        {
            Text = "Трекер сну";
            Size = new Size(400, 400);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 10);

            // Час засинання
            Controls.Add(new Label { Text = "Час засинання:", Location = new Point(20, 20), AutoSize = true });
            dtpSleepTime = new DateTimePicker 
            { 
                Location = new Point(20, 45), 
                Width = 150, 
                Format = DateTimePickerFormat.Time, 
                ShowUpDown = true // Показує стрілочки замість календаря
            };
            Controls.Add(dtpSleepTime);

            // Час пробудження
            Controls.Add(new Label { Text = "Час пробудження:", Location = new Point(200, 20), AutoSize = true });
            dtpWakeTime = new DateTimePicker 
            { 
                Location = new Point(200, 45), 
                Width = 150, 
                Format = DateTimePickerFormat.Time, 
                ShowUpDown = true 
            };
            Controls.Add(dtpWakeTime);

            // Вікова категорія
            Controls.Add(new Label { Text = "Ваша вікова категорія:", Location = new Point(20, 95), AutoSize = true });
            cbAgeCategory = new ComboBox 
            { 
                Location = new Point(20, 120), 
                Width = 330, 
                DropDownStyle = ComboBoxStyle.DropDownList 
            };
            cbAgeCategory.Items.AddRange(new string[] 
            { 
                "Діти та підлітки (до 17 років)", 
                "Дорослі (18-64 роки)", 
                "Літні люди (65+ років)" 
            });
            cbAgeCategory.SelectedIndex = 1; // За замовчуванням "Дорослі", оскільки вам 18
            Controls.Add(cbAgeCategory);

            // Кнопка розрахунку
            btnCalculate = new Button 
            { 
                Text = "Аналізувати сон", 
                Location = new Point(20, 170), 
                Size = new Size(330, 45), 
                BackColor = Color.MediumPurple, 
                ForeColor = Color.White,
                Cursor = Cursors.Hand 
            };
            btnCalculate.Click += BtnCalculate_Click;
            Controls.Add(btnCalculate);

            // Результат
            lblResult = new Label 
            { 
                Text = "Введіть дані для аналізу...", 
                Location = new Point(20, 240), 
                Size = new Size(330, 100) 
            };
            Controls.Add(lblResult);
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            DateTime sleep = dtpSleepTime.Value;
            DateTime wake = dtpWakeTime.Value;

            // Якщо час пробудження менший за час засинання, значить людина прокинулась наступного дня
            if (wake <= sleep)
            {
                wake = wake.AddDays(1);
            }

            TimeSpan duration = wake - sleep;
            double hours = duration.TotalHours;

            // Визначаємо норму залежно від віку
            int minHours = 7;
            int maxHours = 9;

            if (cbAgeCategory.SelectedIndex == 0) // Діти/підлітки
            {
                minHours = 8;
                maxHours = 10;
            }
            else if (cbAgeCategory.SelectedIndex == 2) // Літні люди
            {
                minHours = 7;
                maxHours = 8;
            }

            // Формуємо рекомендацію
            string status = "";
            if (hours < minHours)
            {
                status = "Недосип! 🔴\nВам потрібно спати більше для відновлення сил.";
                lblResult.ForeColor = Color.Crimson;
            }
            else if (hours > maxHours)
            {
                status = "Пересип! 🟡\nЗанадто довгий сон може викликати млявість.";
                lblResult.ForeColor = Color.DarkGoldenrod;
            }
            else
            {
                status = "Норма! 🟢\nЧудовий графік, продовжуйте в тому ж дусі.";
                lblResult.ForeColor = Color.DarkGreen;
            }

            lblResult.Text = $"Тривалість вашого сну: {hours:F1} год.\n\nВердикт: {status}";
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