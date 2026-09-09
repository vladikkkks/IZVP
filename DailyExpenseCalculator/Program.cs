using System;
using System.Drawing;
using System.Windows.Forms;

namespace DailyExpenseCalculator
{
    public class MainForm : Form
    {
        private TextBox txtExpenseName, txtDailyCost;
        private Button btnCalculate;
        private Label lblResults, lblComparison;

        public MainForm()
        {
            Text = "Калькулятор щоденних звичок";
            Size = new Size(450, 450);
            StartPosition = FormStartPosition.CenterScreen;

            // Назва витрати
            Controls.Add(new Label { Text = "На що ви витрачаєте гроші? (напр., Кава, Таксі):", Location = new Point(20, 20), AutoSize = true });
            txtExpenseName = new TextBox { Location = new Point(20, 45), Width = 390 };
            Controls.Add(txtExpenseName);

            // Щоденна вартість
            Controls.Add(new Label { Text = "Скільки це коштує в день? (грн):", Location = new Point(20, 80), AutoSize = true });
            txtDailyCost = new TextBox { Location = new Point(20, 105), Width = 390 };
            Controls.Add(txtDailyCost);

            // Кнопка
            btnCalculate = new Button { Text = "Розрахувати", Location = new Point(20, 145), Size = new Size(390, 40) };
            btnCalculate.Click += BtnCalculate_Click;
            Controls.Add(btnCalculate);

            // Виведення сум
            lblResults = new Label { Text = "Тут з'являться ваші витрати...", Location = new Point(20, 200), AutoSize = true, Font = new Font("Segoe UI", 10) };
            Controls.Add(lblResults);

            // Виведення порівняння (що можна купити)
            lblComparison = new Label { Text = "", Location = new Point(20, 300), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DarkBlue };
            Controls.Add(lblComparison);
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            // Перевірка назви
            if (string.IsNullOrWhiteSpace(txtExpenseName.Text))
            {
                MessageBox.Show("Будь ласка, введіть назву витрати.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Перевірка вартості
            if (!double.TryParse(txtDailyCost.Text, out double dailyCost) || dailyCost <= 0)
            {
                MessageBox.Show("Будь ласка, введіть коректну вартість (число більше нуля).", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = txtExpenseName.Text;
            
            // Розрахунки
            double monthCost = dailyCost * 30;
            double yearCost = dailyCost * 365;
            double fiveYearCost = dailyCost * 1825; // 365 * 5

            // Форматування результату (N0 додає пробіли між тисячами, напр. 15 000)
            lblResults.Text = $"Ваші витрати на '{name}':\n\n" +
                              $"За місяць (30 днів): {monthCost:N0} грн\n" +
                              $"За рік (365 днів): {yearCost:N0} грн\n" +
                              $"За 5 років: {fiveYearCost:N0} грн";

            // Логіка порівняння з популярними речами
            string comparison = "За гроші, витрачені за 5 років, ви могли б купити:\n";
            if (fiveYearCost >= 500000) 
                comparison += "🏠 Квартиру або дуже хороший автомобіль!";
            else if (fiveYearCost >= 150000) 
                comparison += "🚗 Непоганий вживаний автомобіль!";
            else if (fiveYearCost >= 50000) 
                comparison += "📱 Топовий флагманський смартфон + круту відпустку!";
            else if (fiveYearCost >= 25000) 
                comparison += "💻 Новий потужний ноутбук для програмування!";
            else if (fiveYearCost >= 10000) 
                comparison += "🚲 Хороший велосипед або бюджетний смартфон.";
            else 
                comparison += "🎧 Хороші бездротові навушники або багато піци.";

            lblComparison.Text = comparison;
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