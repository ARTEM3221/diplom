using System;
using System.Xml;
using System.Windows.Forms;
using System.Net.Http;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
    public partial class MainForm : Form
    {
        private XmlDocument xmlDoc;
        private XmlNodeList questions;
        private int currentQuestion;
        private int correctAnswers;
        private string rightAnswer;
        private string xmlPath;
        private List<string> userAnswers;
        private int testDuration;
        private CancellationTokenSource timerCancellationTokenSource;
        public int CorrectAnswers => correctAnswers;

        // Конструктор
        public MainForm(string xmlPath)
        {
            InitializeComponent();
            this.xmlPath = xmlPath;
            LoadTestAsync(xmlPath); // Завантажуємо тест з вказаного xml-шляху
        }

        // Властивості
        public string TestUrl { get; set; }
        public string TestTheme { get; set; }
        public string Theme { get; private set; }
        public int Questions { get; private set; }
        public string PersonName { get; set; }

        // Асинхронний метод завантаження даних тесту з XML
        private async void LoadTestAsync(string xmlPath)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string testXml = await httpClient.GetStringAsync(xmlPath);
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(testXml); // Завантажуємо XML документ з отриманим XML рядком
            }

            // Читаємо тему тесту
            XmlNode themeNode = xmlDoc.SelectSingleNode("/test/Theme");
            string testTheme = themeNode?.InnerText ?? "";
            this.TestTheme = testTheme;

            // Ініціалізація даних для тесту
            questions = xmlDoc.SelectNodes("/test/qw/*");
            userAnswers = new List<string>(new string[questions.Count]);
            currentQuestion = 0;
            correctAnswers = 0;

            // Встановлюємо тему та кількість запитань
            Theme = xmlDoc.SelectSingleNode("/test/Theme")?.InnerText ?? "Unknown";
            Questions = questions.Count;

            // Відображаємо перше питання
            DisplayQuestion(currentQuestion);

            // Встановлюємо таймер
            XmlNode durationNode = xmlDoc.SelectSingleNode("/test/Duration");
            int testDurationInMinutes;
            if (int.TryParse(durationNode?.InnerText, out testDurationInMinutes))
            {
                int hours = testDurationInMinutes / 60;
                int minutes = testDurationInMinutes % 60;
                int seconds = 0;

                TimeSpan testDuration = new TimeSpan(hours, minutes, seconds);

                if (testDuration > TimeSpan.Zero)
                {
                    timerCancellationTokenSource = new CancellationTokenSource();
                    StartTimer(testDuration, timerCancellationTokenSource.Token); // Запускаємо таймер
                }
            }
        }

        // Відображаємо запитання за індексом
        private void DisplayQuestion(int questionIndex)
        {
            // Обробляємо дані для відображення питання та відповідей
            XmlNode questionNode = questions[questionIndex];
            string questionText = questionNode.Attributes["text"].InnerText;
            rightAnswer = questionNode.Attributes["right"].InnerText;

            XmlNode answersNode = questionNode.SelectSingleNode("answers");
            string[] answers = answersNode.InnerText.Split('|');

            lblQuestion.Text = questionText;

            // Встановлюємо текст варіантів відповіді
            cbAnswer1.Text = answers[0];
            cbAnswer2.Text = answers[1];
            cbAnswer3.Text = answers[2];
            cbAnswer4.Text = answers[3];

            // Обнуляємо вибрані відповіді
            cbAnswer1.Checked = false;
            cbAnswer2.Checked = false;
            cbAnswer3.Checked = false;
            cbAnswer4.Checked = false;
        }

        // Метод для перевірки та обрахунку відповідей
        private void CheckAnswer()
        {
            // Створюємо список вибраних відповідей
            List<string> selectedAnswers = new List<string>();
            if (cbAnswer1.Checked) selectedAnswers.Add(cbAnswer1.Text);
            if (cbAnswer2.Checked) selectedAnswers.Add(cbAnswer2.Text);
            if (cbAnswer3.Checked) selectedAnswers.Add(cbAnswer3.Text);
            if (cbAnswer4.Checked) selectedAnswers.Add(cbAnswer4.Text);

            // Перевіряємо правильність відповідей та збільшуємо кількість правильних відповідей, якщо потрібно
            string[] correctAnswersArray = rightAnswer.Split('|');

            bool isAnswerCorrect;
            if (rightAnswer == "")
            {
                isAnswerCorrect = selectedAnswers.Count == 0;
            }
            else
            {
                isAnswerCorrect = selectedAnswers.Count == correctAnswersArray.Length && selectedAnswers.All(answer => correctAnswersArray.Contains(answer));
            }

            if (isAnswerCorrect) correctAnswers++;

            // Зберігаємо відповідь користувача для поточного питання
            userAnswers[currentQuestion] = string.Join("|", selectedAnswers);
        }

        // Метод для відновлення збереженої відповіді користувача для поточного питання
        private void RestoreUserAnswers(int questionIndex)
        {
            string[] selectedAnswers = userAnswers[questionIndex]?.Split('|');
            cbAnswer1.Checked = selectedAnswers?.Contains(cbAnswer1.Text) ?? false;
            cbAnswer2.Checked = selectedAnswers?.Contains(cbAnswer2.Text) ?? false;
            cbAnswer3.Checked = selectedAnswers?.Contains(cbAnswer3.Text) ?? false;
            cbAnswer4.Checked = selectedAnswers?.Contains(cbAnswer4.Text) ?? false;
        }

        // Обробник події натискання кнопки "Далі"
        private void btnNext_Click(object sender, EventArgs e)
        {
            // Перевіряємо та обраховуємо відповідь
            CheckAnswer();

            // Якщо це не останнє питання, переходимо до наступного
            if (currentQuestion < questions.Count - 1)
            {
                currentQuestion++;

                DisplayQuestion(currentQuestion);

                // Відновлюємо відповідь користувача для поточного питання
                RestoreUserAnswers(currentQuestion);
            }
            else
            {
                // Якщо це було останнє питання, то завершуємо тест
                if (currentQuestion >= questions.Count - 1)
                {
                    EndTest();
                }
            }

            // Вмикаємо кнопку "Назад", якщо користувач уже не на першому питанні
            backButton.Enabled = currentQuestion > 0;
        }

        // Обробник події закриття форми
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // При закритті форми закриваємо весь додаток
            Application.Exit();
        }

        // Обробник події натискання кнопки "Вихід"
        private void button1_Click_1(object sender, EventArgs e)
        {
            // При натисканні кнопки "Вихід" закриваємо весь додаток
            Application.Exit();
        }

        // Обробник події натискання кнопки "Назад"
        private void backButton_Click(object sender, EventArgs e)
        {
            // Якщо не перше питання, повертаємося назад
            if (currentQuestion > 0)
            {
                currentQuestion--;
                DisplayQuestion(currentQuestion);

                // Відновлюємо відповідь користувача для поточного питання
                RestoreUserAnswers(currentQuestion);

                // Вимикаємо кнопку "Назад", якщо користувач повернувся на перше питання
                backButton.Enabled = currentQuestion > 0;
            }

            // Вмикаємо кнопку "Далі", якщо користувач уже не на останньому питанні
            btnNext.Enabled = currentQuestion < questions.Count - 1;
        }

        private async void StartTimer(TimeSpan duration, CancellationToken cancellationToken)
        {
            // Запускаємо таймер, який відлік часу до кінця тесту
            try
            {
                for (TimeSpan remainingTime = duration; remainingTime >= TimeSpan.Zero; remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1)))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    Invoke((Action)(() =>
                    {
                        // Оновлюємо відображення часу на формі
                        lblTimer.Text = $"Час, що залишився: {remainingTime:hh\\:mm\\:ss}";
                    }));

                    await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                }

                Invoke((Action)(() =>
                {
                    // Якщо час вийшов, інформуємо користувача та завершуємо тест
                    MessageBox.Show("Час вийшов!");
                    EndTest();
                }));
            }
            catch (OperationCanceledException)
            {
                // Час було зупинено користувачем, не робимо нічого
            }
        }

        // // Завершуємо тест
        private void EndTest()
        {
            btnNext.Enabled = false;

            Form finalForm = new FinalForm(PersonName, TestTheme, questions.Count, correctAnswers);
            finalForm.Show();
            this.Hide();
        }
    }
}