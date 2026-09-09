using System;
using System.Drawing;
using System.Windows.Forms;

namespace BookingSystemApp
{
    public class MainForm : Form
    {
        private Label lblStatus;
        private Button btnConfirm;
        private int ticketPrice = 150; // Ціна одного квитка (грн)
        private int selectedSeats = 0;

        public MainForm()
        {
            Text = "Бронювання місць у кінотеатрі";
            Size = new Size(500, 520);
            StartPosition = FormStartPosition.CenterScreen;

            // Візуальний елемент "Екран"
            Label lblScreen = new Label 
            { 
                Text = "Е К Р А Н", 
                BackColor = Color.DarkGray, 
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(50, 20), 
                Size = new Size(380, 30),
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            Controls.Add(lblScreen);

            // Генерація місць: 5 рядів по 8 місць
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Button btnSeat = new Button
                    {
                        Text = $"{row + 1}-{col + 1}", // Текст: Ряд-Місце
                        Size = new Size(42, 42),
                        Location = new Point(50 + col * 48, 80 + row * 48),
                        BackColor = Color.LightGreen, // Початковий колір - вільне місце
                        Cursor = Cursors.Hand,
                        FlatStyle = FlatStyle.Flat
                    };
                    // Прив'язуємо всі кнопки до одного методу
                    btnSeat.Click += Seat_Click;
                    Controls.Add(btnSeat);
                }
            }

            // Текст зі статусом
            lblStatus = new Label 
            { 
                Text = "Обрано місць: 0\nДо сплати: 0 грн", 
                Location = new Point(50, 360), 
                AutoSize = true, 
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            Controls.Add(lblStatus);

            // Кнопка підтвердження
            btnConfirm = new Button 
            { 
                Text = "Підтвердити бронювання", 
                Location = new Point(230, 360), 
                Size = new Size(200, 40),
                BackColor = Color.LightBlue,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnConfirm.Click += BtnConfirm_Click;
            Controls.Add(btnConfirm);
        }

        // Загальний метод обробки кліку по БУДЬ-ЯКОМУ місцю
        private void Seat_Click(object sender, EventArgs e)
        {
            Button clickedSeat = sender as Button;

            // Якщо місце вже куплене/заброньоване (червоне) - нічого не робимо
            if (clickedSeat.BackColor == Color.Tomato)
            {
                MessageBox.Show("Це місце вже зайняте!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Якщо місце вільне (зелене) -> обираємо його (жовте)
            if (clickedSeat.BackColor == Color.LightGreen)
            {
                clickedSeat.BackColor = Color.Gold;
                selectedSeats++;
            }
            // Якщо місце вже було обране нами (жовте) -> скасовуємо вибір (зелене)
            else if (clickedSeat.BackColor == Color.Gold)
            {
                clickedSeat.BackColor = Color.LightGreen;
                selectedSeats--;
            }

            UpdateStatus();
        }

        // Оновлення тексту з ціною
        private void UpdateStatus()
        {
            int totalCost = selectedSeats * ticketPrice;
            lblStatus.Text = $"Обрано місць: {selectedSeats}\nДо сплати: {totalCost} грн";
        }

        // Кліпка "Підтвердити"
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (selectedSeats == 0)
            {
                MessageBox.Show("Будь ласка, оберіть хоча б одне місце.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Перетворюємо всі обрані (жовті) місця на заброньовані (червоні)
            foreach (Control ctrl in Controls)
            {
                if (ctrl is Button btn && btn.BackColor == Color.Gold)
                {
                    btn.BackColor = Color.Tomato;
                    btn.Cursor = Cursors.No; // Змінюємо курсор, щоб показати, що недоступно
                }
            }

            MessageBox.Show($"Ви успішно заброньювали {selectedSeats} квитків.\nДо сплати: {selectedSeats * ticketPrice} грн.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            // Скидаємо лічильник для наступного клієнта
            selectedSeats = 0;
            UpdateStatus();
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