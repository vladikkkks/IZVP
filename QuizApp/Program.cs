using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuizApp
{
    public class MainForm : Form
    {
        // Змінні для збереження елементів
        private RadioButton rbQ1A, rbQ1B, rbQ1C;
        private RadioButton rbQ2A, rbQ2B, rbQ2C;
        private CheckBox cbQ3A, cbQ3B, cbQ3C;
        private Button btnSubmit;

        public MainForm()
        {
            Text = "Тестова анкета";
            Size = new Size(450, 500);
            StartPosition = FormStartPosition.CenterScreen;

            // Питання 1 (RadioButton - одна правильна відповідь)
            GroupBox gb1 = new GroupBox { Text = "1. Який тип даних використовується для цілих чисел?", Location = new Point(20, 20), Size = new Size(390, 100) };
            rbQ1A = new RadioButton { Text = "string", Location = new Point(20, 25), AutoSize = true };
            rbQ1B = new RadioButton { Text = "int", Location = new Point(20, 45), AutoSize = true }; // Правильна
            rbQ1C = new RadioButton { Text = "bool", Location = new Point(20, 65), AutoSize = true };
            gb1.Controls.Add(rbQ1A); gb1.Controls.Add(rbQ1B); gb1.Controls.Add(rbQ1C);
            Controls.Add(gb1);

            // Питання 2 (RadioButton - одна правильна відповідь)
            GroupBox gb2 = new GroupBox { Text = "2. Яка мова використовується для розробки Windows Forms?", Location = new Point(20, 130), Size = new Size(390, 100) };
            rbQ2A = new RadioButton { Text = "C#", Location = new Point(20, 25), AutoSize = true }; // Правильна
            rbQ2B = new RadioButton { Text = "Python", Location = new Point(20, 45), AutoSize = true };
            rbQ2C = new RadioButton { Text = "HTML", Location = new Point(20, 65), AutoSize = true };
            gb2.Controls.Add(rbQ2A); gb2.Controls.Add(rbQ2B); gb2.Controls.Add(rbQ2C);
            Controls.Add(gb2);

            // Питання 3 (CheckBox - кілька правильних відповідей)
            GroupBox gb3 = new GroupBox { Text = "3. Які з цих програм підходять для написання коду? (кілька варіантів)", Location = new Point(20, 240), Size = new Size(390, 110) };
            cbQ3A = new CheckBox { Text = "Visual Studio", Location = new Point(20, 25), AutoSize = true }; // Правильна
            cbQ3B = new CheckBox { Text = "Visual Studio Code", Location = new Point(20, 50), AutoSize = true }; // Правильна
            cbQ3C = new CheckBox { Text = "Adobe Photoshop", Location = new Point(20, 75), AutoSize = true };
            gb3.Controls.Add(cbQ3A); gb3.Controls.Add(cbQ3B); gb3.Controls.Add(cbQ3C);
            Controls.Add(gb3);

            // Кнопка завершення
            btnSubmit = new Button { Text = "Завершити тест та показати результат", Location = new Point(20, 370), Size = new Size(390, 40) };
            btnSubmit.Click += BtnSubmit_Click;
            Controls.Add(btnSubmit);
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            int score = 0;
            int maxScore = 3;

            // Перевірка 1 питання
            if (rbQ1B.Checked) score++;

            // Перевірка 2 питання
            if (rbQ2A.Checked) score++;

            // Перевірка 3 питання (даємо бал, якщо вибрані дві правильні і НЕ вибрана неправильна)
            if (cbQ3A.Checked && cbQ3B.Checked && !cbQ3C.Checked)
            {
                score++;
            }

            // Формування текстового результату
            string resultText = $"Ваш результат: {score} з {maxScore} балів.\n\n";
            
            if (score == maxScore)
                resultText += "Відмінно! Ви відповіли правильно на всі запитання.";
            else if (score > 0)
                resultText += "Непогано, але є помилки.";
            else
                resultText += "На жаль, всі відповіді неправильні. Спробуйте ще раз.";

            // Виведення результату
            MessageBox.Show(resultText, "Результат тесту", MessageBoxButtons.OK, MessageBoxIcon.Information);
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