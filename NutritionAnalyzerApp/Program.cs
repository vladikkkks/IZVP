using System;
using System.Drawing;
using System.Windows.Forms;

namespace NutritionAnalyzerApp
{
    public class MainForm : Form
    {
        private NumericUpDown numDailyNorm, numCalories;
        private TextBox txtFoodName;
        private Button btnAddFood;
        private ListBox lstMeals;
        private ProgressBar pbCalories;
        private Label lblStatus;
        private int totalCalories = 0;

        public MainForm()
        {
            Text = "Аналізатор здорового харчування";
            Size = new Size(450, 500);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 10);

            // Дневная норма
            Controls.Add(new Label { Text = "Денна норма калорій:", Location = new Point(20, 20), AutoSize = true });
            numDailyNorm = new NumericUpDown { Location = new Point(180, 18), Width = 100, Maximum = 10000, Value = 2000 };
            numDailyNorm.ValueChanged += (s, e) => UpdateProgress();
            Controls.Add(numDailyNorm);

            // Ввод продукта
            GroupBox gbAdd = new GroupBox { Text = "Додати прийом їжі", Location = new Point(20, 60), Size = new Size(390, 120) };

            gbAdd.Controls.Add(new Label { Text = "Продукт (напр. МакМеню):", Location = new Point(15, 30), AutoSize = true });
            txtFoodName = new TextBox { Location = new Point(15, 55), Width = 200 };
            gbAdd.Controls.Add(txtFoodName);

            gbAdd.Controls.Add(new Label { Text = "Ккал:", Location = new Point(230, 30), AutoSize = true });
            numCalories = new NumericUpDown { Location = new Point(230, 55), Width = 70, Maximum = 5000 };
            gbAdd.Controls.Add(numCalories);

            btnAddFood = new Button { Text = "Додати", Location = new Point(310, 53), Width = 70, BackColor = Color.LightGreen };
            btnAddFood.Click += BtnAddFood_Click;
            gbAdd.Controls.Add(btnAddFood);

            Controls.Add(gbAdd);

            // Список
            Controls.Add(new Label { Text = "Список спожитого:", Location = new Point(20, 195), AutoSize = true });
            lstMeals = new ListBox { Location = new Point(20, 220), Size = new Size(390, 130) };
            Controls.Add(lstMeals);

            // ProgressBar
            pbCalories = new ProgressBar { Location = new Point(20, 370), Size = new Size(390, 30), Style = ProgressBarStyle.Continuous };
            Controls.Add(pbCalories);

            // Статус
            lblStatus = new Label { Text = "Спожито: 0 / 2000 ккал", Location = new Point(20, 410), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            Controls.Add(lblStatus);
        }

        private void BtnAddFood_Click(object sender, EventArgs e)
        {
            string food = txtFoodName.Text;
            int calories = (int)numCalories.Value;

            if (string.IsNullOrWhiteSpace(food) || calories <= 0)
            {
                MessageBox.Show("Введіть назву продукту та калорійність.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            totalCalories += calories;
            lstMeals.Items.Add($"{food} — {calories} ккал");

            txtFoodName.Clear();
            numCalories.Value = 0;

            UpdateProgress();
        }

        private void UpdateProgress()
        {
            int norm = (int)numDailyNorm.Value;

            // Предотвращение ошибки ProgressBar (значение не может быть больше максимума)
            pbCalories.Maximum = norm;
            pbCalories.Value = totalCalories > norm ? norm : totalCalories;

            lblStatus.Text = $"Спожито: {totalCalories} / {norm} ккал";

            if (totalCalories > norm)
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text += " (Перевищення норми!)";
            }
            else
            {
                lblStatus.ForeColor = Color.DarkGreen;
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