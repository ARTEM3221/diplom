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
            Mark = MarkClass.Mark(NumbersOfQwest, RightAnswers);

            MarkLabel.Text += Mark.ToString();

            // Автоматичне збереження результатів на сервері
            SaveResultToServer(PersonName, Theme, RightAnswers, Mark);
        }

        // Метод асинхронного збереження результатів на сервері
        private async void SaveResultToServer(string name, string testTheme, int correctAnswers, double averageScore)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
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

                    HttpResponseMessage response = await httpClient.PostAsync("http://localhost/Result/save_result.php", content);
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

        // Обробка події натискання кнопки "Назад"
        private void button1_Click(object sender, EventArgs e)
        {
            LoadForm loadForm = new LoadForm();
            loadForm.Show();
            this.Hide();
        }

        private void MarkLabel_Click(object sender, EventArgs e)
        {

        }
    }

    // Клас для розрахунку оцінки
    public static class MarkClass
    {
        public static double Mark(int totalQuestions, int correctAnswers)
        {
            // Розрахунок оцінки на основі загальної кількості питань і правильних відповідей
            double percentage = (double)correctAnswers / totalQuestions * 100;

            if (percentage >= 90)
            {
                return 5.0;
            }
            else if (percentage >= 75)
            {
                return 4.0;
            }
            else if (percentage >= 60)
            {
                return 3.0;
            }
            else if (percentage >= 45)
            {
                return 2.0;
            }
            else
            {
                return 1.0;
            }
        }
    }
}
