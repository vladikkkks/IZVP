using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BudgetTrackerApp
{
    public class MainForm : Form
    {
        private ComboBox cbType, cbCategory, cbFilter;
        private TextBox txtAmount;
        private DateTimePicker dtpDate;
        private Button btnAdd, btnSave, btnLoad, btnFilter, btnClearFilter;
        private DataGridView dgvTransactions;
        private Label lblBalance;
        
        private DataTable table;
        private string filePath = "transactions.txt";

        public MainForm()
        {
            Text = "Домашня бухгалтерія";
            Size = new Size(650, 600);
            StartPosition = FormStartPosition.CenterScreen;

            // --- БЛОК ВВЕДЕННЯ ДАНИХ ---
            Controls.Add(new Label { Text = "Тип:", Location = new Point(20, 20), AutoSize = true });
            cbType = new ComboBox { Location = new Point(20, 40), Width = 100, DropDownStyle = ComboBoxStyle.DropDownList };
            cbType.Items.AddRange(new string[] { "Дохід", "Витрата" });
            cbType.SelectedIndex = 1; // За замовчуванням "Витрата"
            Controls.Add(cbType);

            Controls.Add(new Label { Text = "Категорія:", Location = new Point(130, 20), AutoSize = true });
            cbCategory = new ComboBox { Location = new Point(130, 40), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cbCategory.Items.AddRange(new string[] { "Продукти", "Транспорт", "Розваги", "Зарплата", "Стипендія", "Інше" });
            Controls.Add(cbCategory);

            Controls.Add(new Label { Text = "Сума (грн):", Location = new Point(290, 20), AutoSize = true });
            txtAmount = new TextBox { Location = new Point(290, 40), Width = 100 };
            Controls.Add(txtAmount);

            Controls.Add(new Label { Text = "Дата:", Location = new Point(400, 20), AutoSize = true });
            dtpDate = new DateTimePicker { Location = new Point(400, 40), Width = 120, Format = DateTimePickerFormat.Short };
            Controls.Add(dtpDate);

            btnAdd = new Button { Text = "Додати", Location = new Point(530, 39), Width = 80, Height = 23 };
            btnAdd.Click += BtnAdd_Click;
            Controls.Add(btnAdd);

            // --- БЛОК ФІЛЬТРАЦІЇ ---
            Controls.Add(new Label { Text = "Фільтр за категорією:", Location = new Point(20, 80), AutoSize = true });
            cbFilter = new ComboBox { Location = new Point(160, 77), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cbFilter.Items.AddRange(new string[] { "Продукти", "Транспорт", "Розваги", "Зарплата", "Стипендія", "Інше" });
            Controls.Add(cbFilter);

            btnFilter = new Button { Text = "Застосувати", Location = new Point(320, 75), Width = 90 };
            btnFilter.Click += BtnFilter_Click;
            Controls.Add(btnFilter);

            btnClearFilter = new Button { Text = "Скинути фільтр", Location = new Point(420, 75), Width = 100 };
            btnClearFilter.Click += BtnClearFilter_Click;
            Controls.Add(btnClearFilter);

            // --- БЛОК ТАБЛИЦІ ---
            SetupDataTable();
            dgvTransactions = new DataGridView 
            { 
                Location = new Point(20, 110), 
                Size = new Size(590, 350),
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = table,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            Controls.Add(dgvTransactions);

            // --- БЛОК ПІДСУМКІВ ТА ФАЙЛІВ ---
            lblBalance = new Label { Text = "Поточний баланс: 0 грн", Location = new Point(20, 480), AutoSize = true, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            Controls.Add(lblBalance);

            btnSave = new Button { Text = "Зберегти у файл", Location = new Point(350, 480), Width = 120, Height = 35 };
            btnSave.Click += BtnSave_Click;
            Controls.Add(btnSave);

            btnLoad = new Button { Text = "Завантажити", Location = new Point(490, 480), Width = 120, Height = 35 };
            btnLoad.Click += BtnLoad_Click;
            Controls.Add(btnLoad);
        }

        private void SetupDataTable()
        {
            table = new DataTable();
            table.Columns.Add("Дата", typeof(string));
            table.Columns.Add("Тип", typeof(string));
            table.Columns.Add("Категорія", typeof(string));
            table.Columns.Add("Сума", typeof(double));
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (cbCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Оберіть категорію.", "Помилка"); return;
            }
            
            if (double.TryParse(txtAmount.Text, out double amount) && amount > 0)
            {
                table.Rows.Add(dtpDate.Value.ToShortDateString(), cbType.SelectedItem, cbCategory.SelectedItem, amount);
                txtAmount.Clear();
                CalculateBalance();
            }
            else
            {
                MessageBox.Show("Введіть коректну суму.", "Помилка");
            }
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex != -1)
            {
                // Застосовуємо фільтр до таблиці (синтаксис схожий на SQL)
                table.DefaultView.RowFilter = $"Категорія = '{cbFilter.SelectedItem}'";
            }
        }

        private void BtnClearFilter_Click(object sender, EventArgs e)
        {
            table.DefaultView.RowFilter = string.Empty; // Знімаємо фільтр
            cbFilter.SelectedIndex = -1;
        }

        private void CalculateBalance()
        {
            double totalIncome = 0;
            double totalExpense = 0;

            foreach (DataRow row in table.Rows)
            {
                double val = Convert.ToDouble(row["Сума"]);
                if (row["Тип"].ToString() == "Дохід") totalIncome += val;
                else totalExpense += val;
            }

            double balance = totalIncome - totalExpense;
            lblBalance.Text = $"Поточний баланс: {balance} грн";
            lblBalance.ForeColor = balance >= 0 ? Color.Green : Color.Red;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (DataRow row in table.Rows)
                    {
                        // Зберігаємо дані через розділювач '|'
                        sw.WriteLine($"{row["Дата"]}|{row["Тип"]}|{row["Категорія"]}|{row["Сума"]}");
                    }
                }
                MessageBox.Show("Дані успішно збережено у файл!", "Збереження", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка збереження: " + ex.Message);
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            if (File.Exists(filePath))
            {
                table.Rows.Clear(); // Очищаємо таблицю перед завантаженням
                try
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] parts = line.Split('|');
                            if (parts.Length == 4)
                            {
                                table.Rows.Add(parts[0], parts[1], parts[2], Convert.ToDouble(parts[3]));
                            }
                        }
                    }
                    CalculateBalance();
                    MessageBox.Show("Дані успішно завантажено!", "Завантаження", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка завантаження: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Файл збережень ще не існує. Збережіть дані спочатку.", "Інформація");
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