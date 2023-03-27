using System;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Tests
{
    public partial class LoadForm : Form
    {
        private string _selectedFolderPath;
        private string _selectedTestFilePath;
        public string XmlPath { get; set; }

        public LoadForm()
        {
            InitializeComponent();
            LoadFolders();
            comboBoxFolders.SelectedIndexChanged += ComboBoxFolders_SelectedIndexChanged;
            listBoxTests.SelectedIndexChanged += ListBoxTests_SelectedIndexChanged;
        }

        // Обробник події зміни вибору папки
        private void ComboBoxFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            string decodedSelectedFolderPath = comboBoxFolders.SelectedItem.ToString();
            _selectedFolderPath = _folderMapping[decodedSelectedFolderPath];
            LoadTests();
        }

        // Завантаження тестів з вибраної папки
        private void LoadTests()
        {
            listBoxTests.Items.Clear();
            string serverPath = $"http://localhost/Tests/{_selectedFolderPath}/";
            try
            {
                WebRequest request = WebRequest.Create(serverPath);
                using WebResponse response = request.GetResponse();
                using StreamReader reader = new StreamReader(response.GetResponseStream());
                string htmlContent = reader.ReadToEnd();
                List<string> files = ExtractFilesFromHtml(htmlContent);
                listBoxTests.Items.AddRange(files.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження тестів з сервера: {ex.Message}");
            }
        }

        // Обробник події зміни вибору тесту
        private async void ListBoxTests_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxTests.SelectedItem != null)
            {
                _selectedTestFilePath = $"{_selectedFolderPath}/{listBoxTests.SelectedItem}";
            }
            buttonRunTest.Enabled = listBoxTests.SelectedItem != null;
            string testUrl = $"http://localhost/Tests/{_selectedFolderPath}/{listBoxTests.SelectedItem}";
            string testTheme = "";
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    string testXml = await httpClient.GetStringAsync(testUrl);
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(testXml);

                    XmlNode themeNode = xmlDoc.SelectSingleNode("/test/Theme");
                    testTheme = themeNode?.InnerText ?? "";
                }
                catch (Exception)
                {
                    // Обробка винятків, які можуть виникнути при завантаженні XML або обробці теми
                }
            }

            // Оновлення підпису з темою тесту
            labelTestTheme.Text = $"Тема тесту: {testTheme}";
        }

        // Обробник події натискання кнопки "Вихід"
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Обробник події закриття форми
        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Ви впевнені, що хочете вийти?", "Вихід", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
            else
                this.Visible = true;
        }

        // Обробник події натискання кнопки "Запустити тест"
        private void ButtonRunTest_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                MessageBox.Show("Введіть ваше ім'я!", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                string xmlPath = $"http://localhost/Tests/{comboBoxFolders.Text}/{listBoxTests.Text}";
                MainForm MF = new MainForm(xmlPath);
                MF.PersonName = textBox1.Text; // Встановлення властивості PersonName
                MF.FormClosed += (s, ev) =>
                {
                string personName = textBox1.Text;
                string theme = MF.Theme;
                int numberOfQuestions = MF.Questions;
                int rightAnswers = MF.CorrectAnswers;
                    FinalForm finalForm = new FinalForm(personName, theme, numberOfQuestions, rightAnswers);
                    finalForm.Show();
                };
                MF.Show();
                this.Hide();
            }
        }

        private Dictionary<string, string> _folderMapping = new Dictionary<string, string>();

        // Завантаження списку папок з сервера
        private void LoadFolders()
        {
            comboBoxFolders.Items.Clear();
            string serverPath = "http://localhost/Tests/";
            try
            {
                WebRequest request = WebRequest.Create(serverPath);
                using WebResponse response = request.GetResponse();
                using StreamReader reader = new StreamReader(response.GetResponseStream());
                string htmlContent = reader.ReadToEnd();
                List<string> folders = ExtractFoldersFromHtml(htmlContent);
                foreach (string folder in folders)
                {
                    string decodedFolderName = DecodeFolderName(folder);

                    // Видалення закінчувального слеша з розкодованого імені папки
                    if (decodedFolderName.EndsWith("/"))
                    {
                        decodedFolderName = decodedFolderName.TrimEnd('/');
                    }

                    // Пропуск папки "Result" та будь-яких порожніх імен папок
                    if (decodedFolderName.Equals("Result", StringComparison.OrdinalIgnoreCase) ||
                        folder.Equals("Result/", StringComparison.OrdinalIgnoreCase) ||
                        string.IsNullOrEmpty(decodedFolderName))
                    {
                        continue;
                    }

                    _folderMapping.Add(decodedFolderName, folder);
                    comboBoxFolders.Items.Add(decodedFolderName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження папок з сервера: {ex.Message}");
            }
        }

        // Витягування папок з HTML-вмісту
        private List<string> ExtractFoldersFromHtml(string htmlContent)
        {
            var folders = new List<string>();
            string folderPattern = "<a href=\"([^\"]*?/)\">(.*?)</a>";
            MatchCollection matches = Regex.Matches(htmlContent, folderPattern);
            foreach (Match match in matches)
            {
                if (match.Groups.Count > 1 && !string.IsNullOrEmpty(match.Groups[1].Value))
                {
                    string decodedFolder = WebUtility.UrlDecode(match.Groups[1].Value);
                    folders.Add(decodedFolder);
                }
            }
            return folders;
        }

        // Розкодування імені папки
        private string DecodeFolderName(string folderName)
        {
            return WebUtility.HtmlDecode(folderName);
        }

        // Витягування файлів з HTML-вмісту
        private List<string> ExtractFilesFromHtml(string htmlContent)
        {
            var files = new List<string>();
            string filePattern = "<a href=\"([^\"]*?.xml)\">.*?</a>";
            MatchCollection matches = Regex.Matches(htmlContent, filePattern);
            foreach (Match match in matches)
            {
                if (match.Groups.Count > 1 && !string.IsNullOrEmpty(match.Groups[1].Value))
                {
                    files.Add(WebUtility.UrlDecode(match.Groups[1].Value));
                }
            }
            return files;
        }
    }
}