using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExpenseTrackerApp
{
    public class MainForm : Form
    {
        private TextBox txtSalary, txtItemName, txtItemPrice;
        private Button btnAdd;
        private ListBox lstExpenses;
        private Label lblTotal, lblRemaining;
        private double totalExpenses = 0;

        public MainForm()
        {
            Text = "Трекер витрат та підписок";
            Size = new Size(400, 500);
            StartPosition = FormStartPosition.CenterScreen;

            // Зарплата
            Controls.Add(new Label { Text = "Місячний бюджет/зарплата:", Location = new Point(20, 20), AutoSize = true });
            txtSalary = new TextBox { Location = new Point(200, 20), Width = 150 };
            Controls.Add(txtSalary);

            // Назва витрати
            Controls.Add(new Label { Text = "Назва підписки (напр. Netflix):", Location = new Point(20, 60), AutoSize = true });
            txtItemName = new TextBox { Location = new Point(200, 60), Width = 150 };
            Controls.Add(txtItemName);

            // Ціна витрати
            Controls.Add(new Label { Text = "Сума (грн):", Location = new Point(20, 100), AutoSize = true });
            txtItemPrice = new TextBox { Location = new Point(200, 100), Width = 150 };
            Controls.Add(txtItemPrice);

            // Кнопка додавання
            btnAdd = new Button { Text = "Додати витрату", Location = new Point(20, 140), Size = new Size(330, 35) };
            btnAdd.Click += BtnAdd_Click;
            Controls.Add(btnAdd);

            // Список витрат
            Controls.Add(new Label { Text = "Список ваших витрат:", Location = new Point(20, 190), AutoSize = true });
            lstExpenses = new ListBox { Location = new Point(20, 215), Size = new Size(330, 150) };
            Controls.Add(lstExpenses);

            // Підсумки
            lblTotal = new Label { Text = "Всього витрат: 0 грн", Location = new Point(20, 380), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            Controls.Add(lblTotal);

            lblRemaining = new Label { Text = "Залишок: 0 грн", Location = new Point(20, 410), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.Green };
            Controls.Add(lblRemaining);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // Перевірка, чи правильно введена зарплата
            if (!double.TryParse(txtSalary.Text, out double salary) || salary < 0)
            {
                MessageBox.Show("Будь ласка, введіть коректну суму зарплати.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Перевірка назви витрати
            if (string.IsNullOrWhiteSpace(txtItemName.Text))
            {
                MessageBox.Show("Будь ласка, введіть назву витрати.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Перевірка суми витрати
            if (!double.TryParse(txtItemPrice.Text, out double price) || price <= 0)
            {
                MessageBox.Show("Будь ласка, введіть коректну ціну витрати.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Додаємо витрату до загальної суми та відображаємо у списку
            totalExpenses += price;
            lstExpenses.Items.Add($"{txtItemName.Text} — {price} грн");

            // Очищаємо поля вводу для наступної витрати
            txtItemName.Clear();
            txtItemPrice.Clear();

            // Оновлюємо підсумки
            double remaining = salary - totalExpenses;
            lblTotal.Text = $"Всього витрат: {totalExpenses} грн";
            lblRemaining.Text = $"Залишок: {remaining} грн";
            
            // Якщо витрати перевищили бюджет, робимо залишок червоним
            if (remaining < 0)
                lblRemaining.ForeColor = Color.Red;
            else
                lblRemaining.ForeColor = Color.Green;
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