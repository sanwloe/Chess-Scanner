using System;
using System.Windows;
using System.Windows.Controls;
using AppLib_Parser;
using AppLib_Analysis;
using System.Diagnostics;
using System.Xml;
using System.Windows.Media;
using System.IO;
using System.Windows.Markup;
using System.Xml.Linq;

namespace Chess_Scanner
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    { 
        public static MainWindow Instance { get; private set; }
        public static Frame RootFrame { get; private set; }
        public MainWindow()
        {
            InitializeComponent();

            LoadTheme();

            Core.addmove += AnalysisChessMove.Analysis; // Підпис на подію знаходження здійснених ходів

            if (Core.IsConnectedToInternet())
            {
                
            }
            else
            {
                Dispose();
            }
            Instance = this; // Передача екземпляра класу в поле статичного класу MainWindow
        }
        ~MainWindow()
        {
            Dispose();
        }
        // Кнопка закриття програми
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Dispose();
        }
        public void LoadTheme()
        {
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(@"Configuration\PathsInfo.xml");

            XmlElement xmlElement = xmlDocument.DocumentElement["ColorTheme"];

            if(xmlElement!=null)
            {
                string color = xmlElement.GetAttribute("Color");

                if(color == "White")
                {
                    Style menuButton = (Style)XamlReader.Parse(Pattern.PatternButton.MenuColorThemeWhite);

                    Button_Options.Style = menuButton;

                    Button_Results.Style = menuButton;

                    Button_Managment.Style = menuButton;

                    Background = Brushes.White;

                    Main_TextBlock.Foreground = Brushes.Black;

                    Header.Background = Brushes.RoyalBlue;

                    Main_Frame1.BorderBrush = null;
                    
                    Main_TextBlock.Background = Header.Background;

                    Button_Window_Hide.Background = Header.Background;

                    Main_Menu1.Background = Brushes.AliceBlue;

                    {
                        Style style = (Style)XamlReader.Parse(Pattern.PatternButton.ColorThemeButtonCloseWhite);

                        Button_Close.Style = style;
                    }

                }
                else if(color == "Dark")
                {
                    Style menuButton = (Style)XamlReader.Parse(Pattern.PatternButton.MenuColorThemeDark);

                    Button_Options.Style = menuButton;

                    Button_Results.Style = menuButton;

                    Button_Managment.Style = menuButton;

                    Background = new SolidColorBrush(Color.FromArgb(100,98,98,98));

                    Header.Background = null;

                    Main_Menu1.Background = null;

                    Button_Window_Hide.Background = Background;

                    {
                        Style style = (Style)XamlReader.Parse(Pattern.PatternButton.ColorThemeButtonCloseDark);

                        Button_Close.Style = style;
                    }

                    Main_TextBlock.Background = Background;

                    Main_TextBlock.Foreground = Brushes.White;
                }
            }
        }
        //Перехід на список ходів
        private void Button_Results_Click(object sender, RoutedEventArgs e)
        {
            Main_Frame1.Focus();

            Main_Frame1.Content = new Frames.Results();
            
        }
        //Скриття екрану
        private void Button_Window_Hide_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        //Пересування екрану лівою кнопною миші
        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) 
        {
            DragMove();
        }
        public void Dispose() // Висвободження усіх ресурсів програми
        {
            Core.Close();

            if(!process.HasExited)
                process.Kill();

            //Environment.Exit(0); // Завершення роботи процесу

            Application.Current.Shutdown(); // Завершення роботи програми
        }
        //Перехід на сторінку опцій
        private void Button_Options_Click(object sender, RoutedEventArgs e)
        {
            Main_Frame1.Content = new Frames.Options();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            process = new Process(); // Процесс Mach3

            process.StartInfo.FileName = GetPathMach3();

            if (process.StartInfo.FileName != null)
            {
                process.Start();
                // Запуск браузера і початок ігрової сесії
                Core.LoadGame();
            }
            else
            {
                MessageBox.Show("Не вдалося запустити процес!\nПеревірте чи правильно вказаний шлях до Mach3.exe та перезапустіть програму\n");
            }

            Main_Frame1.Content = new Frames.Management();
        }
        Process process;
        private void Button_Managment_Click(object sender, RoutedEventArgs e)
        {
            Main_Frame1.Content = new Frames.Management();
        }
        private string GetPathMach3()
        {
            XmlDocument xmlDocument = new XmlDocument();
            
            xmlDocument.Load(@"Configuration\PathsInfo.xml");

            if(xmlDocument!= null)
            {
                XmlElement xmlElement = xmlDocument.DocumentElement;

                var element = xmlDocument.SelectSingleNode("/Values/PathToMach3");

                if(element != null)
                {
                    return element.Attributes.GetNamedItem("path").Value;
                }
                else
                {
                    MessageBox.Show("Не вказано шлях до Mach3.exe");
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Не знайдено файл з налаштуваннями!\nЗайдіть в налаштування та вкажіть шлях!");
                return null;
            }
        }
    }
}
