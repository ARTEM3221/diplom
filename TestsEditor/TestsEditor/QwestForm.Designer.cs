namespace TestsEditor
{
    partial class QwestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QwestForm));
            this.label1 = new System.Windows.Forms.Label();
            this.QwestBox = new System.Windows.Forms.TextBox();
            this.answ1 = new System.Windows.Forms.TextBox();
            this.answ2 = new System.Windows.Forms.TextBox();
            this.answ3 = new System.Windows.Forms.TextBox();
            this.answ4 = new System.Windows.Forms.TextBox();
            this.NextButton = new System.Windows.Forms.Button();
            this.RightAnswersBox = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 195);
            this.label1.TabIndex = 0;
            this.label1.Text = "Питання:\r\n\r\n\r\n\r\n\r\nВідповідь №1:\r\n\r\n\r\nВідповідь №2:\r\n\r\n\r\nВідповідь №3:\r\n\r\n\r\nВідпов" +
    "ідь №4:";
            // 
            // QwestBox
            // 
            this.QwestBox.Location = new System.Drawing.Point(15, 25);
            this.QwestBox.Multiline = true;
            this.QwestBox.Name = "QwestBox";
            this.QwestBox.Size = new System.Drawing.Size(567, 39);
            this.QwestBox.TabIndex = 1;
            // 
            // answ1
            // 
            this.answ1.Location = new System.Drawing.Point(102, 70);
            this.answ1.Name = "answ1";
            this.answ1.Size = new System.Drawing.Size(480, 20);
            this.answ1.TabIndex = 2;
            // 
            // answ2
            // 
            this.answ2.Location = new System.Drawing.Point(102, 110);
            this.answ2.Name = "answ2";
            this.answ2.Size = new System.Drawing.Size(480, 20);
            this.answ2.TabIndex = 3;
            // 
            // answ3
            // 
            this.answ3.Location = new System.Drawing.Point(102, 149);
            this.answ3.Name = "answ3";
            this.answ3.Size = new System.Drawing.Size(480, 20);
            this.answ3.TabIndex = 4;
            // 
            // answ4
            // 
            this.answ4.Location = new System.Drawing.Point(102, 187);
            this.answ4.Name = "answ4";
            this.answ4.Size = new System.Drawing.Size(480, 20);
            this.answ4.TabIndex = 5;
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(588, 25);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(120, 39);
            this.NextButton.TabIndex = 7;
            this.NextButton.Text = "Далі";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // RightAnswersBox
            // 
            this.RightAnswersBox.FormattingEnabled = true;
            this.RightAnswersBox.Location = new System.Drawing.Point(588, 70);
            this.RightAnswersBox.Name = "RightAnswersBox";
            this.RightAnswersBox.Size = new System.Drawing.Size(120, 139);
            this.RightAnswersBox.TabIndex = 8;
            // 
            // QwestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(724, 217);
            this.Controls.Add(this.RightAnswersBox);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.answ4);
            this.Controls.Add(this.answ3);
            this.Controls.Add(this.answ2);
            this.Controls.Add(this.answ1);
            this.Controls.Add(this.QwestBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QwestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редагування питання";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QwestForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox QwestBox;
        private System.Windows.Forms.TextBox answ1;
        private System.Windows.Forms.TextBox answ2;
        private System.Windows.Forms.TextBox answ3;
        private System.Windows.Forms.TextBox answ4;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.CheckedListBox RightAnswersBox;
    }
}