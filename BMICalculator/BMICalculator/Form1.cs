namespace BMICalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            // Зчитуємо вагу та зріст з текстових полів. 
            // double.TryParse перевіряє, чи дійсно користувач ввів числа.
            if (double.TryParse(textBoxWeight.Text, out double weight) &&
                double.TryParse(textBoxHeight.Text, out double heightCm))
            {
                // Формула ІМТ вимагає зріст у метрах, тому ділимо сантиметри на 100
                double heightM = heightCm / 100;

                // Обчислюємо ІМТ: вага поділена на зріст у квадраті
                double bmi = weight / (heightM * heightM);
                string category = "";

                // Визначаємо текстову оцінку за стандартами ВООЗ
                if (bmi < 18.5)
                {
                    category = "Недостатня вага";
                }
                else if (bmi >= 18.5 && bmi < 25)
                {
                    category = "Норма";
                }
                else if (bmi >= 25 && bmi < 30)
                {
                    category = "Надлишкова вага";
                }
                else
                {
                    category = "Ожиріння";
                }

                // Виводимо результат у labelResult, округливши ІМТ до 1 знака після коми
                labelResult.Text = $"Ваш ІМТ: {Math.Round(bmi, 1)}\nОцінка: {category}";
            }
            else
            {
                // Якщо користувач ввів щось неправильно (наприклад, літери або залишив поле порожнім)
                MessageBox.Show("Будь ласка, введіть коректні числові значення.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
