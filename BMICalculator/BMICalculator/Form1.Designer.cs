namespace BMICalculator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            textBoxWeight = new TextBox();
            label2 = new Label();
            textBoxHeight = new TextBox();
            buttonCalculate = new Button();
            labelResult = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(298, 260);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 0;
            label1.Text = "Вага (кг):";
            // 
            // textBoxWeight
            // 
            textBoxWeight.Location = new Point(288, 291);
            textBoxWeight.Name = "textBoxWeight";
            textBoxWeight.Size = new Size(100, 23);
            textBoxWeight.TabIndex = 1;
            textBoxWeight.Text = "textBoxWeight";
            textBoxWeight.TextChanged += textBox1_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(466, 260);
            label2.Name = "label2";
            label2.Size = new Size(64, 15);
            label2.TabIndex = 2;
            label2.Text = "Зріст (см):";
            // 
            // textBoxHeight
            // 
            textBoxHeight.Location = new Point(452, 291);
            textBoxHeight.Name = "textBoxHeight";
            textBoxHeight.Size = new Size(100, 23);
            textBoxHeight.TabIndex = 3;
            textBoxHeight.Text = "textBoxHeight";
            textBoxHeight.TextChanged += textBox2_TextChanged;
            // 
            // buttonCalculate
            // 
            buttonCalculate.Location = new Point(379, 338);
            buttonCalculate.Name = "buttonCalculate";
            buttonCalculate.Size = new Size(81, 23);
            buttonCalculate.TabIndex = 4;
            buttonCalculate.Text = "Обчислити";
            buttonCalculate.UseVisualStyleBackColor = true;
            buttonCalculate.Click += buttonCalculate_Click;
            // 
            // labelResult
            // 
            labelResult.AutoSize = true;
            labelResult.Location = new Point(391, 385);
            labelResult.Name = "labelResult";
            labelResult.Size = new Size(60, 15);
            labelResult.TabIndex = 5;
            labelResult.Text = "Результат";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelResult);
            Controls.Add(buttonCalculate);
            Controls.Add(textBoxHeight);
            Controls.Add(label2);
            Controls.Add(textBoxWeight);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBoxWeight;
        private Label label2;
        private TextBox textBoxHeight;
        private Button buttonCalculate;
        private Label labelResult;
    }
}
