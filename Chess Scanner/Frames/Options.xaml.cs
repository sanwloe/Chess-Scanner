using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace Chess_Scanner.Frames
{
    /// <summary>
    /// Логика взаимодействия для Options.xaml
    /// </summary>
    public partial class Options : Page
    {
        public Options()
        {
            InitializeComponent();

            LoadXmlFile();

            LoadPathMach3();

            LoadColorTheme();
        }
        
        ~Options()
        {
            xmlDocument = null;
        }
        XmlDocument xmlDocument = new XmlDocument();

        string pathfile = @"Configuration\PathsInfo.xml";

        private void LoadXmlFile()
        {
            if (File.Exists(pathfile))
            {
                xmlDocument.Load(pathfile);
            }
            else
            {
                File.WriteAllText(pathfile, "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n\r\n<Values></Values>");

                xmlDocument.Load(pathfile);
            }
        }
        private void LoadPathMach3()
        {
            var element = xmlDocument.SelectSingleNode("/Values/PathToMach3");

            if (element != null)
            {
                Text_Box2.Text = element.Attributes.GetNamedItem("path").Value;
            }
            else
            {
                XmlElement newelement = xmlDocument.CreateElement("PathToMach3");

                newelement.SetAttribute("path", "");

                xmlDocument.Save(pathfile);
            }

        }
        private void LoadColorTheme()
        {
            var element = xmlDocument.SelectSingleNode("/Values/ColorTheme");

            if (element != null)
            {
                var atr = element.Attributes.GetNamedItem("Color").Value;

                if (atr == "Dark")
                {
                    RadioButton2.IsChecked = true;
                }
                else if (atr == "White")
                {
                    RadioButton1.IsChecked = true;
                }
            }
            else
            {
                XmlElement xmlElement = xmlDocument.CreateElement("ColorTheme");

                xmlElement.SetAttribute("Color", "White");

                RadioButton1.IsChecked = true;
            }
        }
        private void Text_Box2_LostFocus(object sender, RoutedEventArgs e)
        {
            string text = Text_Box2.Text;

            if (!string.IsNullOrEmpty(text))
            {
                var element = xmlDocument.DocumentElement["PathToMach3"];

                if (element != null)
                {
                    if (element.GetAttributeNode("path").Value != Text_Box2.Text)
                    {
                        element.SetAttribute("path", Text_Box2.Text);

                        xmlDocument.Save(pathfile);
                    }
                }
                else
                {
                    XmlElement xmlelement = xmlDocument.CreateElement("PathToMach3");

                    xmlelement.SetAttribute("path", Text_Box2.Text);

                    xmlDocument.DocumentElement.PrependChild(xmlelement);

                    xmlDocument.Save(pathfile);
                }
            }
        }
        private void RadioButton1_Checked(object sender, RoutedEventArgs e)
        {
            string color = "White";

            var element = xmlDocument.DocumentElement["ColorTheme"];

            if (element != null)
            {
                element.SetAttribute("Color", color);

                xmlDocument.Save(pathfile);

                MainWindow.Instance.LoadTheme();
            }
            else
            {
                XmlElement xmlElement = xmlDocument.CreateElement("ColorTheme");

                xmlElement.SetAttribute("Color", color);

                xmlDocument.DocumentElement.AppendChild(xmlElement);

                xmlDocument.Save(pathfile);

                MainWindow.Instance.LoadTheme();
            }
        }

        private void RadioButton2_Checked(object sender, RoutedEventArgs e)
        {
            string color = "Dark";

            var element = xmlDocument.DocumentElement["ColorTheme"];

            if (element != null)
            {
                element.SetAttribute("Color", color);

                xmlDocument.Save(pathfile);

                MainWindow.Instance.LoadTheme();
            }
            else
            {
                XmlElement xmlElement = xmlDocument.CreateElement("ColorTheme");

                xmlElement.SetAttribute("Color", color);

                xmlDocument.DocumentElement.AppendChild(xmlElement);

                xmlDocument.Save(pathfile);

                MainWindow.Instance.LoadTheme();
            }
        }
    }
}
