using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;
using System.Xml.Linq;
using mark;
using Newtonsoft.Json;
using System.Text;

namespace Tests
{
    public partial class FinalForm : Form
    {
        double Mark;
        // Конструктор FinalForm з обробкою результатів
        public FinalForm(string PersonName, string Theme, int NumbersOfQwest, int RightAnswers)
        {
            InitializeComponent();

            // Відображення результатів на формі
            NameLabel.Text += PersonName;
            ThemeLabel.Text = Theme;
            NumbersLabel.Text += NumbersOfQwest.ToString();
            RightLabel.Text += RightAnswers.ToString();

            // Розрахунок оцінки
            Mark = mark.MarkClass.Mark(NumbersOfQwest, RightAnswers);

            MarkLabel.Text += Mark.ToString();

            // Автоматичне збереження результатів на сервері
            SaveResultToServer(PersonName, Theme, RightAnswers, Mark);
        }

        // Метод асинхронного збереження результатів на сервері
        private async void SaveResultToServer(string name, string testTheme, int correctAnswers, double averageScore)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Створення JSON-об'єкта з даними результатів
                var data = new
                {
                    name,
                    testTheme,
                    correctAnswers,
                    averageScore
                };

                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await httpClient.PostAsync("http://localhost/Tests/Result/save_result.php", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        
                    }
                    else
                    {
                        MessageBox.Show($"Error saving result on server: {response.ReasonPhrase}", "Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving result on server: {ex.Message}", "Error");
                }
            }
        }

        #region Exit
        // Обробка події закриття форми
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Обробка події натискання кнопки "Вихід"
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        // Обробка події натискання кнопки "Назад"
        private void button1_Click(object sender, EventArgs e)
        {
            LoadForm loadForm = new LoadForm();
            loadForm.Show();
            this.Hide();
        }
    }
}