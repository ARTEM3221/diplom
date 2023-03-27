using System;
using System.Windows.Forms;
using System.Xml;
using System.Linq;

namespace TestsEditor
{
    // Клас форми для редагування питань
    public partial class QwestForm : Form
    {
        // Оголошення змінних для записувача XML та лічильника питань
        XmlTextWriter testWriter;
        int count;

        // Конструктор форми
        public QwestForm(int k, XmlTextWriter Writer)
        {
            // Ініціалізація компонентів форми
            testWriter = Writer;
            count = k;
            InitializeComponent();

            // Зміна заголовку форми
            this.Text = "Редагування питання №" + count;

            // Додаємо варіанти відповідей до списку правильних відповідей
            AddAnswerVariants();

            // Додаємо обробники подій для текстових полів відповідей
            AddTextChangedEventHandlers();
        }

        // Метод для додавання варіантів відповідей
        private void AddAnswerVariants()
        {
            RightAnswersBox.Items.Add(answ1.Text);
            RightAnswersBox.Items.Add(answ2.Text);
            RightAnswersBox.Items.Add(answ3.Text);
            RightAnswersBox.Items.Add(answ4.Text);
        }

        // Метод для додавання обробників подій текстових полів
        private void AddTextChangedEventHandlers()
        {
            answ1.TextChanged += Answ_TextChanged;
            answ2.TextChanged += Answ_TextChanged;
            answ3.TextChanged += Answ_TextChanged;
            answ4.TextChanged += Answ_TextChanged;
        }

        // Обробник події зміни тексту в текстових полях відповідей
        private void Answ_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            int index = int.Parse(textBox.Name.Substring(textBox.Name.Length - 1)) - 1;
            RightAnswersBox.Items[index] = textBox.Text;
        }

        // Обробник події закриття форми
        private void QwestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        // Обробник події кліку на кнопку "Далі"
        private void NextButton_Click(object sender, EventArgs e)
        {
            // Перевірка на заповненість полів та наявність правильних відповідей
            if (AreFieldsFilled() && AreRightAnswersSelected())
            {
                // Запис даних у XML
                WriteQuestionToXml();

                // Закриття форми
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Заповніть всі поля і оберіть хоча б одну правильну відповідь!", "Помилка!");
            }
        }

        // Метод перевірки на заповненість полів
        private bool AreFieldsFilled()
        {
            return QwestBox.Text != "" && answ1.Text != "" && answ2.Text != "" && answ3.Text != "" && answ4.Text != "";
        }

        // Метод перевірки наявності правильних відповідей
        private bool AreRightAnswersSelected()
        {
            return RightAnswersBox.CheckedItems.Count > 0;
        }

        // Метод запису питання та відповідей у XML
        private void WriteQuestionToXml()
        {
            testWriter.WriteStartElement("q" + count);

            testWriter.WriteStartAttribute("text");
            testWriter.WriteString(QwestBox.Text);
            testWriter.WriteEndAttribute();

            testWriter.WriteStartAttribute("right");

            var rightAnswers = string.Join("|", RightAnswersBox.CheckedItems.Cast<string>());
            testWriter.WriteString(rightAnswers);

            testWriter.WriteEndAttribute();

            testWriter.WriteStartElement("answers");
            testWriter.WriteString(answ1.Text + "|" + answ2.Text + "|" + answ3.Text + "|" + answ4.Text);
            testWriter.WriteEndElement();

            testWriter.WriteEndElement();
        }
    }
}