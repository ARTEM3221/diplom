namespace Tests
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnNext = new System.Windows.Forms.Button();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbAnswer4 = new System.Windows.Forms.CheckBox();
            this.cbAnswer3 = new System.Windows.Forms.CheckBox();
            this.cbAnswer2 = new System.Windows.Forms.CheckBox();
            this.cbAnswer1 = new System.Windows.Forms.CheckBox();
            this.backButton = new System.Windows.Forms.Button();
            this.lblTimer = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(93, 241);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "Далі";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Location = new System.Drawing.Point(13, 12);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(35, 13);
            this.lblQuestion.TabIndex = 1;
            this.lblQuestion.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(372, 241);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Вийти";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Lavender;
            this.panel1.Controls.Add(this.lblQuestion);
            this.panel1.Location = new System.Drawing.Point(-1, -3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(462, 60);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Lavender;
            this.panel2.Controls.Add(this.cbAnswer4);
            this.panel2.Controls.Add(this.cbAnswer3);
            this.panel2.Controls.Add(this.cbAnswer2);
            this.panel2.Controls.Add(this.cbAnswer1);
            this.panel2.Location = new System.Drawing.Point(-1, 63);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(462, 160);
            this.panel2.TabIndex = 8;
            // 
            // cbAnswer4
            // 
            this.cbAnswer4.AutoSize = true;
            this.cbAnswer4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAnswer4.Location = new System.Drawing.Point(16, 126);
            this.cbAnswer4.Name = "cbAnswer4";
            this.cbAnswer4.Size = new System.Drawing.Size(77, 17);
            this.cbAnswer4.TabIndex = 9;
            this.cbAnswer4.Text = "checkBox4";
            this.cbAnswer4.UseVisualStyleBackColor = true;
            // 
            // cbAnswer3
            // 
            this.cbAnswer3.AutoSize = true;
            this.cbAnswer3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAnswer3.Location = new System.Drawing.Point(16, 89);
            this.cbAnswer3.Name = "cbAnswer3";
            this.cbAnswer3.Size = new System.Drawing.Size(77, 17);
            this.cbAnswer3.TabIndex = 8;
            this.cbAnswer3.Text = "checkBox3";
            this.cbAnswer3.UseVisualStyleBackColor = true;
            // 
            // cbAnswer2
            // 
            this.cbAnswer2.AutoSize = true;
            this.cbAnswer2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAnswer2.Location = new System.Drawing.Point(16, 49);
            this.cbAnswer2.Name = "cbAnswer2";
            this.cbAnswer2.Size = new System.Drawing.Size(77, 17);
            this.cbAnswer2.TabIndex = 7;
            this.cbAnswer2.Text = "checkBox2";
            this.cbAnswer2.UseVisualStyleBackColor = true;
            // 
            // cbAnswer1
            // 
            this.cbAnswer1.AutoSize = true;
            this.cbAnswer1.BackColor = System.Drawing.Color.Lavender;
            this.cbAnswer1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAnswer1.Location = new System.Drawing.Point(16, 16);
            this.cbAnswer1.Name = "cbAnswer1";
            this.cbAnswer1.Size = new System.Drawing.Size(77, 17);
            this.cbAnswer1.TabIndex = 6;
            this.cbAnswer1.Text = "checkBox1";
            this.cbAnswer1.UseVisualStyleBackColor = false;
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(12, 241);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 9;
            this.backButton.Text = "Назад";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Location = new System.Drawing.Point(174, 246);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(0, 13);
            this.lblTimer.TabIndex = 10;
            // 
            // MainForm
            // 
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(459, 276);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Тестування";
            this.TransparencyKey = System.Drawing.Color.White;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       
        
        
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.CheckBox cbAnswer4;
        private System.Windows.Forms.CheckBox cbAnswer3;
        private System.Windows.Forms.CheckBox cbAnswer2;
        private System.Windows.Forms.CheckBox cbAnswer1;
        private System.Windows.Forms.Label lblTimer;
    }
}