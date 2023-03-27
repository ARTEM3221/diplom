using System;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace TestsEditor
{
    public partial class HelloForm : Form
    {
        XmlTextWriter testWriter;

        public HelloForm()
        {
            InitializeComponent();
            LoadFolders();
        }

        // Метод для збереження тесту на сервері
        private async Task SaveTestToServer(string testXml, string directory, string testName)
        {
            using (var client = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(testXml, Encoding.UTF8, "text/xml"), "testXml");
                content.Add(new StringContent(directory), "directory");
                content.Add(new StringContent(testName), "testName");

                var response = await client.PostAsync("http://localhost/Tests/save_test.php", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Помилка при збереженні тесту на сервері");
                }
            }
        }

        // Обробник події кліку на кнопку "Далі"
        private async void NextButton_Click(object sender, EventArgs e)
        {
            if (comboBoxFolders.Text != "" && ThemeBox.Text != "" && NameBox.Text != "")
            {
                try
                {
                    using (var ms = new MemoryStream())
                    {
                        // Створення XML з тестом
                        CreateTestXml(ms);

                        // Збереження XML з тестом на сервер
                        ms.Position = 0;
                        using (var reader = new StreamReader(ms))
                        {
                            string testXml = reader.ReadToEnd();
                            await SaveTestToServer(testXml, comboBoxFolders.Text, NameBox.Text);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Помилка!");
                }
            }
            else
            {
                MessageBox.Show("Заповніть всі поля!", "Помилка!");
            }
        }

        // Метод створення XML з тестом
        private void CreateTestXml(MemoryStream ms)
        {
            testWriter = new XmlTextWriter(ms, Encoding.UTF8);

            testWriter.Formatting = Formatting.Indented;
            testWriter.WriteStartDocument();
            testWriter.WriteStartElement("test");
            testWriter.WriteStartElement("Theme");
            testWriter.WriteString(ThemeBox.Text);
            testWriter.WriteEndElement();
            testWriter.WriteStartElement("qw");
            testWriter.WriteStartAttribute("numbers");
            testWriter.WriteString(NumQwBox.Value.ToString());
            testWriter.WriteEndAttribute();

            for (int i = 1; i <= NumQwBox.Value; i++)
            {
                QwestForm QF = new QwestForm(i, testWriter);
                QF.ShowDialog();
            }

            testWriter.WriteEndElement();
            testWriter.WriteStartElement("Duration");
            testWriter.WriteString(DurationBox.Value.ToString());
            testWriter.WriteEndElement();
            testWriter.WriteEndElement();
            testWriter.WriteEndDocument();
            testWriter.Flush(); 
        }

        private Dictionary<string, string> _folderMapping = new Dictionary<string, string>();

        // Метод завантаження папок з сервера
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

        // Метод витягування папок з HTML-вмісту
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

        // Метод розкодування імені папки
        private string DecodeFolderName(string folderName)
        {
            return WebUtility.HtmlDecode(folderName);
        }

        // Метод витягування файлів з HTML-вмісту
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