using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dictionary
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class dictionaryPage : ContentPage
    {
        private Entry entr;

        private Label label, label2;

        private Button button1, button2, button3;

        public const string fileName = "dictionaryfile1.txt";

        public dictionaryPage()
        {
            entr = new Entry
            {
                Placeholder = "Введите слово на английском: "
            };

            label2 = new Label
            {
                Text = "Переведите выведенное слово!",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            button1 = new Button
            {
                Text = "Добавить слово",
                HorizontalOptions = LayoutOptions.Center
            };
            button1.Clicked += button1_Clicked; ;


            button2 = new Button
            {
                Text = "Список слов",
                HorizontalOptions = LayoutOptions.Center
            };
            button2.Clicked += button2_Clicked; ;

            label = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            button3 = new Button
            {
                Text = "Вывести слово",
                HorizontalOptions = LayoutOptions.Center
            };

            button3.Clicked += button3_Clicked; ;

            Content = new StackLayout
            {
                Children = { entr, label2, button1, button2, button3, label }
            };

            CreateDictionaryFile();
        }

        private void button3_Clicked(object sender, EventArgs e)
        {
            string[] capitalsList = ReadWordsFromFile();

            if (capitalsList != null && capitalsList.Length >= 2)
            {
                string wordOutput = string.Empty;

                Random random = new Random();

                string randomWord = capitalsList[random.Next(capitalsList.Length)];

                label.Text = wordOutput + randomWord;
            }
            else
            {
                label.Text = "Слов не найдено";
            }

        }

        private void button2_Clicked(object sender, EventArgs e)
        {
            string[] capitalsList = ReadWordsFromFile();

            if (capitalsList != null && capitalsList.Length >= 2)
            {
                string wordOutput = string.Empty;

                foreach (string word in capitalsList)
                {
                    wordOutput += word + "\n";
                }

                label.Text = wordOutput;

            }
            else
            {
                label.Text = "Не удалось извлечь слова из файла";
            }
        }

        private void button1_Clicked(object sender, EventArgs e)
        {
            string word = entr.Text;

            if (!string.IsNullOrWhiteSpace(word))
            {
                label.Text = $"Слово: {word}\n";

                SaveCapitalsToFile(word);
            }
            else
            {
                label.Text = "Пожалуйста, введите оба слова";
            }

            entr.Text = string.Empty;
        }

        private void SaveCapitalsToFile(string word2)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
            string content = $"{word2}\n";

            using (StreamWriter write = File.AppendText(filePath))
            {
                write.WriteLine(word2);

            }
        }
        

        public static string[] ReadWordsFromFile()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                string[] words = content.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                return words;
            }


            return null;
        }

        private void CreateDictionaryFile()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

            if (!File.Exists(filePath))
            {
                string content = string.Empty;
                File.WriteAllText(filePath, content);
            }
        }
    }
}