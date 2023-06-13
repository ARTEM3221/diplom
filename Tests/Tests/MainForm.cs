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
            LoadTestAsync(xmlPath);
        }

        public string TestUrl { get; set; }
        public string TestTheme { get; set; }
        public string Theme { get; private set; }
        public int Questions { get; private set; }
        public string PersonName { get; set; }

        // Асинхронний метод для завантаження даних тесту з XML
        private async void LoadTestAsync(string xmlPath)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string testXml = await httpClient.GetStringAsync(xmlPath);
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(testXml);
            }

            // Отримання теми тесту
            XmlNode themeNode = xmlDoc.SelectSingleNode("/test/Theme");
            string testTheme = themeNode?.InnerText ?? "";
            this.TestTheme = testTheme;

            questions = xmlDoc.SelectNodes("/test/qw/*");
            userAnswers = new List<string>(new string[questions.Count]);
            currentQuestion = 0;
            correctAnswers = 0;

            Theme = xmlDoc.SelectSingleNode("/test/Theme")?.InnerText ?? "Невідомо";
            Questions = questions.Count;

            DisplayQuestion(currentQuestion);

            XmlNode durationNode = xmlDoc.SelectSingleNode("/test/Duration");
            int testDurationInMinutes;
            if (int.TryParse(durationNode?.InnerText, out testDurationInMinutes))
            {
                // Розрахунок годин, хвилин і секунд на основі хвилин
                int hours = testDurationInMinutes / 60;
                int minutes = testDurationInMinutes % 60;
                int seconds = 0;

                // Створення об'єкту TimeSpan з розрахованими значеннями
                TimeSpan testDuration = new TimeSpan(hours, minutes, seconds);

                if (testDuration > TimeSpan.Zero)
                {
                    timerCancellationTokenSource = new CancellationTokenSource();
                    StartTimer(testDuration, timerCancellationTokenSource.Token);
                }
            }
        }

        // Відображення питання за індексом
        private void DisplayQuestion(int questionIndex)
        {
            XmlNode questionNode = questions[questionIndex];
            string questionText = questionNode.Attributes["text"].InnerText;
            rightAnswer = questionNode.Attributes["right"].InnerText;

            XmlNode answersNode = questionNode.SelectSingleNode("answers");
            string[] answers = answersNode.InnerText.Split('|');

            lblQuestion.Text = questionText;

            cbAnswer1.Text = answers[0];
            cbAnswer2.Text = answers[1];
            cbAnswer3.Text = answers[2];
            cbAnswer4.Text = answers[3];

            cbAnswer1.Checked = false;
            cbAnswer2.Checked = false;
            cbAnswer3.Checked = false;
            cbAnswer4.Checked = false;
        }

        // Метод для перевірки та підрахунку відповіді
        private void CheckAnswer()
        {
            // Збираємо вибрані відповіді
            List<string> selectedAnswers = new List<string>();
            if (cbAnswer1.Checked) selectedAnswers.Add(cbAnswer1.Text);
            if (cbAnswer2.Checked) selectedAnswers.Add(cbAnswer2.Text);
            if (cbAnswer3.Checked) selectedAnswers.Add(cbAnswer3.Text);
            if (cbAnswer4.Checked) selectedAnswers.Add(cbAnswer4.Text);

            string[] correctAnswersArray = rightAnswer.Split('|');

            bool isAnswerCorrect;

            if (rightAnswer == "") // Якщо немає правильної відповіді
            {
                // Якщо користувач не вибрав жодної відповіді, то вона є правильною
                isAnswerCorrect = selectedAnswers.Count == 0;
            }
            else
            {
                // Якщо є правильні відповіді, то ми перевіряємо їх як раніше
                isAnswerCorrect = selectedAnswers.Count == correctAnswersArray.Length && selectedAnswers.All(answer => correctAnswersArray.Contains(answer));
            }

            if (isAnswerCorrect) correctAnswers++;

            // Збереження відповіді користувача для поточного питання
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

        // Обробка події натискання кнопки "Далі"
        private void btnNext_Click(object sender, EventArgs e)
        {
            // Перевірка та підрахунок відповіді
            CheckAnswer();

            if (currentQuestion < questions.Count - 1)
            {
                // Збільшення індексу поточного питання та відображення наступного питання
                currentQuestion++;
                DisplayQuestion(currentQuestion);

                // Відновлення збереженої відповіді користувача для поточного питання
                RestoreUserAnswers(currentQuestion);

                // Вимкнення кнопки "Далі", якщо це останнє питання
                btnNext.Enabled = currentQuestion < questions.Count - 1;
            }
            else
            {
                // Якщо це останнє питання, завершити тест
                EndTest();
            }

            // Увімкнення кнопки "Назад", якщо користувач не перебуває на першому питанні
            backButton.Enabled = currentQuestion > 0;
        }

        // Обробка події закриття форми
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Обробка події натискання кнопки "Вихід"
        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Обробка події натискання кнопки "Назад"
        private void backButton_Click(object sender, EventArgs e)
        {
            if (currentQuestion > 0)
            {
                // Зменшення індексу поточного питання та відображення попереднього питання
                currentQuestion--;
                DisplayQuestion(currentQuestion);

                // Відновлення збереженої відповіді користувача для поточного питання
                RestoreUserAnswers(currentQuestion);

                // Вимкнення кнопки "Назад", якщо користувач повернувся на перше питання
                backButton.Enabled = currentQuestion > 0;
            }

            // Увімкнення кнопки "Далі", якщо користувач не перебуває на останньому питанні
            btnNext.Enabled = currentQuestion < questions.Count - 1;
        }

        // Асинхронний метод для запуску таймера
        private async void StartTimer(TimeSpan duration, CancellationToken cancellationToken)
        {
            try
            {
                for (TimeSpan remainingTime = duration; remainingTime >= TimeSpan.Zero; remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1)))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    Invoke((Action)(() =>
                    {
                        lblTimer.Text = $"Час, що залишився: {remainingTime:hh\\:mm\\:ss}";
                    }));

                    await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                }

                Invoke((Action)(() =>
                {
                    MessageBox.Show("Час вийшов! Зараз тест закінчиться.", "Час вийшов!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    EndTest();
                }));
            }
            catch (OperationCanceledException)
            {
                // Таймер був скасований, нічого не робимо.
            }
        }

        // Метод для завершення тесту, коли час вийшов або всі питання відповідано
        private void EndTest()
        {
            Form finalForm = new FinalForm(PersonName, TestTheme, questions.Count, correctAnswers);
            finalForm.Show();
            this.Hide();
        }
    }
}