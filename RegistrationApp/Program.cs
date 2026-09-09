using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LoanCalculatorApp
{
    public class MainForm : Form
    {
        private TextBox txtAmount, txtRate, txtTerm;
        private Button btnCalculate;
        private DataGridView dgvSchedule;

        public MainForm()
        {
            Text = "Калькулятор кредиту";
            Size = new Size(600, 500);
            StartPosition = FormStartPosition.CenterScreen;

            // Сума
            Controls.Add(new Label { Text = "Сума кредиту (грн):", Location = new Point(20, 20), AutoSize = true });
            txtAmount = new TextBox { Location = new Point(180, 20), Width = 150 };
            Controls.Add(txtAmount);

            // Ставка
            Controls.Add(new Label { Text = "Річна ставка (%):", Location = new Point(20, 60), AutoSize = true });
            txtRate = new TextBox { Location = new Point(180, 60), Width = 150 };
            Controls.Add(txtRate);

            // Термін
            Controls.Add(new Label { Text = "Термін (місяців):", Location = new Point(20, 100), AutoSize = true });
            txtTerm = new TextBox { Location = new Point(180, 100), Width = 150 };
            Controls.Add(txtTerm);

            // Кнопка
            btnCalculate = new Button { Text = "Обчислити графік", Location = new Point(20, 140), Size = new Size(310, 35) };
            btnCalculate.Click += BtnCalculate_Click;
            Controls.Add(btnCalculate);

            // Таблиця DataGridView для виведення графіку
            dgvSchedule = new DataGridView 
            { 
                Location = new Point(20, 190), 
                Size = new Size(540, 250),
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };
            Controls.Add(dgvSchedule);
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            // Валідація введених даних
            if (double.TryParse(txtAmount.Text, out double amount) &&
                double.TryParse(txtRate.Text, out double annualRate) &&
                int.TryParse(txtTerm.Text, out int months) && 
                amount > 0 && annualRate > 0 && months > 0)
            {
                // Створення структури таблиці
                DataTable table = new DataTable();
                table.Columns.Add("Місяць", typeof(int));
                table.Columns.Add("Платіж (грн)", typeof(string));
                table.Columns.Add("Відсотки", typeof(string));
                table.Columns.Add("Тіло кредиту", typeof(string));
                table.Columns.Add("Залишок", typeof(string));

                // Обчислення ануїтетного платежу
                double monthlyRate = annualRate / 100 / 12;
                double monthlyPayment = amount * (monthlyRate / (1 - Math.Pow(1 + monthlyRate, -months)));
                double balance = amount;

                // Заповнення графіку по місяцях
                for (int i = 1; i <= months; i++)
                {
                    double interestPayment = balance * monthlyRate;
                    double principalPayment = monthlyPayment - interestPayment;
                    balance -= principalPayment;
                    
                    if (balance < 0) balance = 0; // Коригування похибки округлення на останньому місяці

                    table.Rows.Add(
                        i, 
                        Math.Round(monthlyPayment, 2).ToString("F2"), 
                        Math.Round(interestPayment, 2).ToString("F2"), 
                        Math.Round(principalPayment, 2).ToString("F2"), 
                        Math.Round(balance, 2).ToString("F2")
                    );
                }

                // Прив'язка даних до DataGridView
                dgvSchedule.DataSource = table;
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть коректні додатні числа.", "Помилка вводу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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