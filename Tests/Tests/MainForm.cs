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
        

        // Constructor
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

        // Async method for loading test data from XML
        private async void LoadTestAsync(string xmlPath)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string testXml = await httpClient.GetStringAsync(xmlPath);
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(testXml);
            }

            // Read the test theme
            XmlNode themeNode = xmlDoc.SelectSingleNode("/test/Theme");
            string testTheme = themeNode?.InnerText ?? "";
            this.TestTheme = testTheme;

            questions = xmlDoc.SelectNodes("/test/qw/*");
            userAnswers = new List<string>(new string[questions.Count]);
            currentQuestion = 0;
            correctAnswers = 0;

            Theme = xmlDoc.SelectSingleNode("/test/Theme")?.InnerText ?? "Unknown";
            Questions = questions.Count;

            DisplayQuestion(currentQuestion);

            XmlNode durationNode = xmlDoc.SelectSingleNode("/test/Duration");
            int testDurationInMinutes;
            if (int.TryParse(durationNode?.InnerText, out testDurationInMinutes))
            {
                // Calculate hours, minutes, and seconds from minutes
                int hours = testDurationInMinutes / 60;
                int minutes = testDurationInMinutes % 60;
                int seconds = 0;

                // Create a DateTime object with the calculated values
                TimeSpan testDuration = new TimeSpan(hours, minutes, seconds);

                if (testDuration > TimeSpan.Zero)
                {
                    timerCancellationTokenSource = new CancellationTokenSource();
                    StartTimer(testDuration, timerCancellationTokenSource.Token);
                }
            }
        }

        // Display the question based on index
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

        // Method to check and count the answer
        private void CheckAnswer()
        {
            // Collect the selected answers
            List<string> selectedAnswers = new List<string>();
            if (cbAnswer1.Checked) selectedAnswers.Add(cbAnswer1.Text);
            if (cbAnswer2.Checked) selectedAnswers.Add(cbAnswer2.Text);
            if (cbAnswer3.Checked) selectedAnswers.Add(cbAnswer3.Text);
            if (cbAnswer4.Checked) selectedAnswers.Add(cbAnswer4.Text);

            string[] correctAnswersArray = rightAnswer.Split('|');

            bool isAnswerCorrect;

            if (rightAnswer == "") // If there is no correct answer
            {
                // If the user did not select any answers, then it is correct
                isAnswerCorrect = selectedAnswers.Count == 0;
            }
            else
            {
                // If there are correct answers, then we check them as before
                isAnswerCorrect = selectedAnswers.Count == correctAnswersArray.Length && selectedAnswers.All(answer => correctAnswersArray.Contains(answer));
            }

            if (isAnswerCorrect) correctAnswers++;

            // Save the user's answer for the current question
            userAnswers[currentQuestion] = string.Join("|", selectedAnswers);
        }

        // Method to restore the user's saved answer for the current question
        private void RestoreUserAnswers(int questionIndex)
        {
            string[] selectedAnswers = userAnswers[questionIndex]?.Split('|');
            cbAnswer1.Checked = selectedAnswers?.Contains(cbAnswer1.Text) ?? false;
            cbAnswer2.Checked = selectedAnswers?.Contains(cbAnswer2.Text) ?? false;
            cbAnswer3.Checked = selectedAnswers?.Contains(cbAnswer3.Text) ?? false;
            cbAnswer4.Checked = selectedAnswers?.Contains(cbAnswer4.Text) ?? false;
        }


        // Handle the event of clicking the "Next" button
        private void btnNext_Click(object sender, EventArgs e)
        {
            // Check and count the answer
            CheckAnswer();

            if (currentQuestion < questions.Count - 1)
            {
                // Increase the current question index and display the next question
                currentQuestion++;
                DisplayQuestion(currentQuestion);

                // Restore the user's saved answer for the current question
                RestoreUserAnswers(currentQuestion);

                // Disable the "Next" button if this is the last question
                btnNext.Enabled = currentQuestion < questions.Count - 1;
            }
            else
            {
                // If it was the last question, then finish the test
                EndTest();
            }

            // Enable the "Back" button if the user is no longer on the first question
            backButton.Enabled = currentQuestion > 0;
            // Collect the selected answers
            List<string> selectedAnswers = new List<string>();
            if (cbAnswer1.Checked) selectedAnswers.Add(cbAnswer1.Text);
            if (cbAnswer2.Checked) selectedAnswers.Add(cbAnswer2.Text);
            if (cbAnswer3.Checked) selectedAnswers.Add(cbAnswer3.Text);
            if (cbAnswer4.Checked) selectedAnswers.Add(cbAnswer4.Text);

            string[] correctAnswersArray = rightAnswer.Split('|');

            bool isAnswerCorrect;

            if (rightAnswer == "") // If there is no correct answer
            {
                // If the user did not select any answers, then it is correct
                isAnswerCorrect = selectedAnswers.Count == 0;
            }
            else
            {
                // If there are correct answers, then we check them as before
                isAnswerCorrect = selectedAnswers.Count == correctAnswersArray.Length && selectedAnswers.All(answer => correctAnswersArray.Contains(answer));
            }

            if (isAnswerCorrect) correctAnswers++;
        }

       
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
       

        // Handle the event of clicking the "Exit" button
        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Handle the event of clicking the "Back" button
        private void backButton_Click(object sender, EventArgs e)
        {
            if (currentQuestion > 0)
            {
                // Decrease the current question index and display the previous question
                currentQuestion--;
                DisplayQuestion(currentQuestion);

                // Restore the user's saved answer for the current question
                RestoreUserAnswers(currentQuestion);

                // Disable the "Back" button if the user is back at the first question
                backButton.Enabled = currentQuestion > 0;
            }

            // Enable the "Next" button if the user is no longer on the last question
            btnNext.Enabled = currentQuestion < questions.Count - 1;
        }

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
                // Timer was canceled, do nothing.
            }
        }

        // Method to end the test when the time is up
        private void EndTest()
        {
            Form finalForm = new FinalForm(PersonName, TestTheme, questions.Count, correctAnswers);
            finalForm.Show();
            this.Hide();
        }
    }
}