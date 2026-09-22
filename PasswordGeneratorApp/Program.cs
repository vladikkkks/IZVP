using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PasswordGeneratorApp
{
    public class MainForm : Form
    {
        private NumericUpDown numLength;
        private CheckBox chkUpper, chkNumbers, chkSymbols;
        private Button btnGenerate;
        private TextBox txtResult;

        public MainForm()
        {
            Text = "Генератор паролів";
            Size = new Size(360, 340);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 10);

            // Довжина пароля
            Controls.Add(new Label { Text = "Довжина пароля:", Location = new Point(20, 20), AutoSize = true });
            numLength = new NumericUpDown { Location = new Point(170, 18), Width = 130, Minimum = 4, Maximum = 128, Value = 12 };
            Controls.Add(numLength);

            // Налаштування символів
            chkUpper = new CheckBox { Text = "Великі літери (A-Z)", Location = new Point(20, 60), AutoSize = true, Checked = true };
            Controls.Add(chkUpper);

            chkNumbers = new CheckBox { Text = "Цифри (0-9)", Location = new Point(20, 90), AutoSize = true, Checked = true };
            Controls.Add(chkNumbers);

            chkSymbols = new CheckBox { Text = "Спецсимволи (!@#$%)", Location = new Point(20, 120), AutoSize = true };
            Controls.Add(chkSymbols);

            // Кнопка генерації
            btnGenerate = new Button { Text = "Згенерувати пароль", Location = new Point(20, 165), Size = new Size(280, 40), BackColor = Color.LightGreen, Cursor = Cursors.Hand };
            btnGenerate.Click += BtnGenerate_Click;
            Controls.Add(btnGenerate);

            // Поле для виведення результату
            txtResult = new TextBox { Location = new Point(20, 225), Size = new Size(280, 30), ReadOnly = true, TextAlign = HorizontalAlignment.Center, Font = new Font("Consolas", 14, FontStyle.Bold) };
            Controls.Add(txtResult);
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            // Базовий набір (завжди включений, щоб пароль не був порожнім)
            string validChars = "abcdefghijklmnopqrstuvwxyz";
            
            // Додаємо символи залежно від вибраних CheckBox
            if (chkUpper.Checked) validChars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (chkNumbers.Checked) validChars += "0123456789";
            if (chkSymbols.Checked) validChars += "!@#$%^&*()_+-=[]{}|;:,.<>?";

            int length = (int)numLength.Value;
            StringBuilder result = new StringBuilder();
            Random random = new Random();

            // Генерація випадкових символів із дозволеного набору
            for (int i = 0; i < length; i++)
            {
                result.Append(validChars[random.Next(validChars.Length)]);
            }

            txtResult.Text = result.ToString();
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