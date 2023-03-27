namespace TestsEditor
{
    partial class HelloForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelloForm));
            this.NextButton = new System.Windows.Forms.Button();
            this.NumQwBox = new System.Windows.Forms.NumericUpDown();
            this.comboBoxFolders = new System.Windows.Forms.ComboBox();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.ThemeBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DurationBox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NumQwBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DurationBox)).BeginInit();
            this.SuspendLayout();
            // 
            // NextButton
            // 
            this.NextButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.NextButton.Location = new System.Drawing.Point(540, 31);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(96, 23);
            this.NextButton.TabIndex = 16;
            this.NextButton.Text = "ОК";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // NumQwBox
            // 
            this.NumQwBox.Location = new System.Drawing.Point(480, 6);
            this.NumQwBox.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.NumQwBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumQwBox.Name = "NumQwBox";
            this.NumQwBox.Size = new System.Drawing.Size(54, 20);
            this.NumQwBox.TabIndex = 14;
            this.NumQwBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // comboBoxFolders
            // 
            this.comboBoxFolders.FormattingEnabled = true;
            this.comboBoxFolders.Location = new System.Drawing.Point(65, 6);
            this.comboBoxFolders.Name = "comboBoxFolders";
            this.comboBoxFolders.Size = new System.Drawing.Size(124, 21);
            this.comboBoxFolders.TabIndex = 15;
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(224, 6);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(151, 20);
            this.NameBox.TabIndex = 13;
            // 
            // ThemeBox
            // 
            this.ThemeBox.Location = new System.Drawing.Point(42, 33);
            this.ThemeBox.Name = "ThemeBox";
            this.ThemeBox.Size = new System.Drawing.Size(492, 20);
            this.ThemeBox.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(471, 39);
            this.label1.TabIndex = 11;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // DurationBox
            // 
            this.DurationBox.Location = new System.Drawing.Point(540, 7);
            this.DurationBox.Name = "DurationBox";
            this.DurationBox.Size = new System.Drawing.Size(72, 20);
            this.DurationBox.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(618, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "хв";
            // 
            // HelloForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(647, 60);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DurationBox);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.NumQwBox);
            this.Controls.Add(this.comboBoxFolders);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.ThemeBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HelloForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Створення тестування";
            ((System.ComponentModel.ISupportInitialize)(this.NumQwBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DurationBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button NextButton;
        public System.Windows.Forms.NumericUpDown NumQwBox;
        private System.Windows.Forms.ComboBox comboBoxFolders;
        public System.Windows.Forms.TextBox NameBox;
        public System.Windows.Forms.TextBox ThemeBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown DurationBox;
        private System.Windows.Forms.Label label2;
    }
}