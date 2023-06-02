namespace laba1scoi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
            trackBar1 = new TrackBar();
            label1 = new Label();
            comboBox1 = new ComboBox();
            groupBox1 = new GroupBox();
            button2 = new Button();
            vScrollBar1 = new VScrollBar();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            pictureBox3 = new PictureBox();
            comboBox2 = new ComboBox();
            trackBar2 = new TrackBar();
            label2 = new Label();
            checkBox4 = new CheckBox();
            checkBox5 = new CheckBox();
            checkBox6 = new CheckBox();
            button1 = new Button();
            pictureBox4 = new PictureBox();
            myCanvas1 = new MyCanvas();
            richTextBox1 = new RichTextBox();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(-2, -2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(426, 488);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(6, 26);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(138, 148);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(172, 35);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(40, 24);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "R";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Checked = true;
            checkBox2.CheckState = CheckState.Checked;
            checkBox2.Location = new Point(172, 71);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(41, 24);
            checkBox2.TabIndex = 3;
            checkBox2.Text = "G";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Checked = true;
            checkBox3.CheckState = CheckState.Checked;
            checkBox3.Location = new Point(171, 109);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(40, 24);
            checkBox3.TabIndex = 4;
            checkBox3.Text = "B";
            checkBox3.UseVisualStyleBackColor = true;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(244, 52);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(130, 56);
            trackBar1.TabIndex = 6;
            trackBar1.Value = 10;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(254, 23);
            label1.Name = "label1";
            label1.Size = new Size(60, 20);
            label1.TabIndex = 7;
            label1.Text = "Opacity";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Ничего", "Сумма", "Умножение", "Вычитание", "Максимум", "Геом", "Ср", "Маска круг", "Маска квадрат", "Маска прямоугольник", "Критерий Гаврилова", "Критерий Отсу", "Критерий Ниблека", "Линейная фильтрация", "Медианная фильтрация" });
            comboBox1.Location = new Point(235, 107);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 8;
            comboBox1.Text = "Ничего";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(pictureBox2);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(trackBar1);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(checkBox2);
            groupBox1.Controls.Add(checkBox3);
            groupBox1.Location = new Point(6, 14);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(414, 185);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "img1";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // button2
            // 
            button2.Location = new Point(254, 145);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 9;
            button2.Text = "Фурье образ";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // vScrollBar1
            // 
            vScrollBar1.Location = new Point(516, 13);
            vScrollBar1.Name = "vScrollBar1";
            vScrollBar1.Size = new Size(26, 427);
            vScrollBar1.TabIndex = 10;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(groupBox3);
            groupBox2.Controls.Add(groupBox1);
            groupBox2.Controls.Add(vScrollBar1);
            groupBox2.Location = new Point(424, -15);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(434, 438);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(pictureBox3);
            groupBox3.Controls.Add(comboBox2);
            groupBox3.Controls.Add(trackBar2);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(checkBox4);
            groupBox3.Controls.Add(checkBox5);
            groupBox3.Controls.Add(checkBox6);
            groupBox3.Location = new Point(6, 224);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(414, 185);
            groupBox3.TabIndex = 11;
            groupBox3.TabStop = false;
            groupBox3.Text = "img2";
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(6, 26);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(138, 148);
            pictureBox3.TabIndex = 1;
            pictureBox3.TabStop = false;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Ничего", "Сумма", "Умножение", "Вычитание", "Максимум", "Геом", "Ср" });
            comboBox2.Location = new Point(235, 107);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(151, 28);
            comboBox2.TabIndex = 8;
            comboBox2.Text = "Ничего";
            // 
            // trackBar2
            // 
            trackBar2.Location = new Point(244, 52);
            trackBar2.Name = "trackBar2";
            trackBar2.Size = new Size(130, 56);
            trackBar2.TabIndex = 6;
            trackBar2.Value = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(254, 23);
            label2.Name = "label2";
            label2.Size = new Size(60, 20);
            label2.TabIndex = 7;
            label2.Text = "Opacity";
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(172, 35);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(40, 24);
            checkBox4.TabIndex = 2;
            checkBox4.Text = "R";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(172, 71);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(41, 24);
            checkBox5.TabIndex = 3;
            checkBox5.Text = "G";
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            checkBox6.AutoSize = true;
            checkBox6.Location = new Point(171, 109);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new Size(40, 24);
            checkBox6.TabIndex = 4;
            checkBox6.Text = "B";
            checkBox6.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(602, 445);
            button1.Name = "button1";
            button1.Size = new Size(112, 29);
            button1.TabIndex = 12;
            button1.Text = "Сохранить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.Location = new Point(-2, 492);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(765, 204);
            pictureBox4.TabIndex = 13;
            pictureBox4.TabStop = false;
            // 
            // myCanvas1
            // 
            myCanvas1.BackColor = SystemColors.Window;
            myCanvas1.img = null;
            myCanvas1.Location = new Point(781, 492);
            myCanvas1.Name = "myCanvas1";
            myCanvas1.Size = new Size(228, 204);
            myCanvas1.TabIndex = 14;
            myCanvas1.Text = "myCanvas1";
            myCanvas1.MouseUp += myCanvas1_MouseUp;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(864, 34);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(445, 389);
            richTextBox1.TabIndex = 15;
            richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(927, 4);
            label3.Name = "label3";
            label3.Size = new Size(225, 20);
            label3.TabIndex = 16;
            label3.Text = "Коэф. Гаусса для задания на 40";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1321, 708);
            Controls.Add(label3);
            Controls.Add(richTextBox1);
            Controls.Add(myCanvas1);
            Controls.Add(pictureBox4);
            Controls.Add(button1);
            Controls.Add(groupBox2);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private TrackBar trackBar1;
        private Label label1;
        private ComboBox comboBox1;
        private GroupBox groupBox1;
        private VScrollBar vScrollBar1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private PictureBox pictureBox3;
        private ComboBox comboBox2;
        private TrackBar trackBar2;
        private Label label2;
        private CheckBox checkBox4;
        private CheckBox checkBox5;
        private CheckBox checkBox6;
        private Button button1;
        private PictureBox pictureBox4;
        private MyCanvas myCanvas1;
        private Button button2;
        private RichTextBox richTextBox1;
        private Label label3;
    }
}