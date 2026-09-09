using System;
using System.Drawing;
using System.Windows.Forms;

namespace EcoTrackerApp
{
    public class MainForm : Form
    {
        private TextBox txtCarTrips, txtElectricityHours;
        private Button btnCalculate;
        private Label lblResult;

        public MainForm()
        {
            Text = "Калькулятор екосліду";
            Size = new Size(420, 350);
            StartPosition = FormStartPosition.CenterScreen;

            Controls.Add(new Label { Text = "Кількість поїздок на авто (за тиждень):", Location = new Point(20, 20), AutoSize = true });
            txtCarTrips = new TextBox { Location = new Point(20, 45), Width = 360 };
            Controls.Add(txtCarTrips);

            Controls.Add(new Label { Text = "Години роботи електроприладів (в середньому за день):", Location = new Point(20, 85), AutoSize = true });
            txtElectricityHours = new TextBox { Location = new Point(20, 110), Width = 360 };
            Controls.Add(txtElectricityHours);

            btnCalculate = new Button { Text = "Оцінити викиди CO2", Location = new Point(20, 160), Size = new Size(360, 40) };
            btnCalculate.Click += BtnCalculate_Click;
            Controls.Add(btnCalculate);

            lblResult = new Label { Text = "Тут з'явиться ваш результат...", Location = new Point(20, 220), Size = new Size(360, 80), Font = new Font("Segoe UI", 10) };
            Controls.Add(lblResult);
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtCarTrips.Text, out double carTrips) &&
                double.TryParse(txtElectricityHours.Text, out double electricityHours) && 
                carTrips >= 0 && electricityHours >= 0)
            {
                // Прості коефіцієнти для розрахунку
                // 1. Автомобіль: припустимо, 1 поїздка = 10 км. Викиди ~0.2 кг CO2/км -> 2 кг CO2 на поїздку.
                double co2PerCarTrip = 2.0; 
                
                // 2. Електроенергія: припустимо, середнє споживання ~1 кВт-год. Викиди ~0.4 кг CO2/кВт-год.
                // Множимо на 7, щоб отримати показник за тиждень.
                double co2PerElectricityHour = 0.4;

                double totalCarCo2 = carTrips * co2PerCarTrip;
                double totalElectricityCo2 = electricityHours * 7 * co2PerElectricityHour;
                double totalCo2 = totalCarCo2 + totalElectricityCo2;

                string advice = "\n\nПорада: ";
                if (totalCo2 > 50)
                {
                    advice += "Ваш екослід досить високий. Спробуйте частіше користуватися залізницею чи громадським транспортом замість авто!";
                }
                else
                {
                    advice += "Чудовий результат! Ви дбаєте про довкілля та економите ресурси.";
                }

                lblResult.Text = $"Ваші приблизні викиди CO2 за тиждень:\n" +
                                 $"- Від авто: {totalCarCo2:F1} кг\n" +
                                 $"- Від електроенергії: {totalElectricityCo2:F1} кг\n" +
                                 $"Загалом: {totalCo2:F1} кг CO2" + advice;
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть коректні додатні числа.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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