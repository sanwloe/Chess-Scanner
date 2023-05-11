using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace ConfigEditor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string path = @"Configuration\Mach3Config.xml";

            string pathconfig = @"Configuration\PathsInfo.xml";

            if (File.Exists(path))
            {
                XmlDocument mach3config = new XmlDocument();

                mach3config.Load(path);

                XmlElement xmlElement = mach3config.DocumentElement;

                XmlDocument pathfile = new XmlDocument();

                pathfile.Load(@"Configuration\PathsInfo.xml");

                XmlElement xmlElementPath = pathfile.DocumentElement;

                var pathMach3 = xmlElementPath["PathToMach3"];

                var place = xmlElement["PlaceOfChess"];

                var size = xmlElement["SizeOfChess"];

                var height = xmlElement["HeightOfChess"];

                var heightofmagnet = xmlElement["HeightOfMagnet"];

                if(pathMach3 != null)
                {
                    Text_Box1_Path.Text = pathMach3.GetAttribute("path");
                }
                if (place != null)
                {
                    Text_Box2_Insert_Place.Text = place.GetAttribute("place");
                }
                if (size != null)
                {
                    Text_Box3_Insert_Chess_Size.Text = size.GetAttribute("Size");
                }
                if (height != null)
                {
                    Text_Box4_Insert_Chess_Height.Text = height.GetAttribute("Height");
                }
                if (heightofmagnet != null)
                {
                    Text_Box5_Insert_Magnit_Height.Text = heightofmagnet.GetAttribute("Height");
                }
            }
            else
            {
                File.WriteAllText(path, "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n<Values></Values>");
            }
        }
        private void Text_Box1_Path_LostFocus(object sender, RoutedEventArgs e)
        {
            string path = @"Configuration\PathsInfo.xml";

            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(path);

            var element = xmlDocument.DocumentElement;

            var child = element["PathToMach3"];

            if(child!=null)
            {
                child.SetAttribute("path",Text_Box1_Path.Text);

                xmlDocument.Save(path);
            }
            else
            {
                var newelement = xmlDocument.CreateElement("PathToMach3");

                newelement.SetAttribute("path",Text_Box1_Path.Text);

                element.AppendChild(child);

                xmlDocument.Save(path);
            }
        }
        private void Text_Box2_Insert_Place_LostFocus(object sender, RoutedEventArgs e)
        {
            string path = @"Configuration\Mach3Config.xml";

            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(path);

            var element = xmlDocument.DocumentElement;

            var child = element["PlaceOfChess"];

            if (child != null)
            {
                string atr = child.GetAttribute("place");

                if (!String.IsNullOrEmpty(atr))
                {
                    if (atr != Text_Box2_Insert_Place.Text)
                    {
                        child.SetAttribute("place", Text_Box2_Insert_Place.Text);
                        xmlDocument.Save(path);
                    }
                }
            }
            else
            {
                var newlement = xmlDocument.CreateElement("PlaceOfChess");

                newlement.SetAttribute("place", Text_Box2_Insert_Place.Text);

                element.AppendChild(newlement);

                xmlDocument.Save(path);
            }


        }
        private void Text_Box3_Insert_Chess_Size_LostFocus(object sender, RoutedEventArgs e)
        {
            string path = @"Configuration\Mach3Config.xml";

            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(path);

            var element = xmlDocument.DocumentElement;

            var child = element["SizeOfChess"];

            if (child != null)
            {
                string atr = child.GetAttribute("Size");

                if (!String.IsNullOrEmpty(atr))
                {
                    if (atr != Text_Box3_Insert_Chess_Size.Text)
                    {
                        child.SetAttribute("Size", Text_Box3_Insert_Chess_Size.Text);
                        xmlDocument.Save(path);
                    }
                }
            }
            else
            {
                var newlement = xmlDocument.CreateElement("SizeOfChess");

                newlement.SetAttribute("Size", Text_Box3_Insert_Chess_Size.Text);

                element.AppendChild(newlement);

                xmlDocument.Save(path);
            }
        }
        private void Text_Box4_Insert_Chess_Height_LostFocus(object sender, RoutedEventArgs e)
        {
            string path = @"Configuration\Mach3Config.xml";

            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(path);

            var element = xmlDocument.DocumentElement;

            var child = element["HeightOfChess"];

            if (child != null)
            {
                string atr = child.GetAttribute("Height");

                if (!String.IsNullOrEmpty(atr))
                {
                    if (atr != Text_Box4_Insert_Chess_Height.Text)
                    {
                        child.SetAttribute("Height", Text_Box4_Insert_Chess_Height.Text);
                        xmlDocument.Save(path);
                    }
                }
            }
            else
            {
                var newlement = xmlDocument.CreateElement("HeightOfChess");

                newlement.SetAttribute("Height", Text_Box4_Insert_Chess_Height.Text);

                element.AppendChild(newlement);

                xmlDocument.Save(path);
            }
        }
        private void Text_Box5_Insert_Magnit_Height_LostFocus(object sender, RoutedEventArgs e)
        {
            string path = @"Configuration\Mach3Config.xml";

            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(path);

            var element = xmlDocument.DocumentElement;

            var child = element["HeightOfMagnet"];

            if (child != null)
            {
                string atr = child.GetAttribute("Height");

                if (!String.IsNullOrEmpty(atr))
                {
                    if (atr != Text_Box5_Insert_Magnit_Height.Text)
                    {
                        child.SetAttribute("Height", Text_Box5_Insert_Magnit_Height.Text);
                        xmlDocument.Save(path);
                    }
                }
            }
            else
            {
                var newlement = xmlDocument.CreateElement("HeightOfMagnet");

                newlement.SetAttribute("Height", Text_Box5_Insert_Magnit_Height.Text);

                element.AppendChild(newlement);

                xmlDocument.Save(path);
            }
        }
    }
}
