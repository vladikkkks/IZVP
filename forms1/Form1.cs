namespace forms1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double num1 = Convert.ToDouble(textBox1.Text);
                double num2 = Convert.ToDouble(textBox2.Text);
                double num3 = Convert.ToDouble(textBox3.Text);

                double sum = num1 + num2 + num3;
                double diff = num1 - num2 - num3;
                double prod = num1 * num2 * num3;

                label1.Text = $"Сума: {sum}\nРізниця: {diff}\nДобуток: {prod}";
            }
            catch (FormatException)
            {
                MessageBox.Show("Будь ласка, введіть тільки цифри у всі поля.", "Помилка введення");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
