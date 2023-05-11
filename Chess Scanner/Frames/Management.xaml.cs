using AppLib_InputGCode;
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

namespace Chess_Scanner.Frames
{
    /// <summary>
    /// Логика взаимодействия для Management.xaml
    /// </summary>
    public partial class Management : Page
    {
        public Management()
        {
            InitializeComponent();
        }

        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {
            Input.Reset();
        }

        private void Button_CycleStart_Click(object sender, RoutedEventArgs e)
        {
            Input.CycleStart();
        }

        private void Button_Zero_Click(object sender, RoutedEventArgs e)
        {
            Input.Zero();
        }

        private void Button_Stop_Click(object sender, RoutedEventArgs e)
        {
            Input.Stop();
        }

        private void Input_CMD_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Input.ExecuteCode(Input_CMD.Text);
                Input_CMD.Clear();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string path = @"Configuration\Mach3Config.xml";

            if (File.Exists(path))
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(path);

                XmlElement xmlElement = xmlDoc.DocumentElement;

                var place = xmlElement["PlaceOfChess"];

                var size = xmlElement["SizeOfChess"];

                var height = xmlElement["HeightOfChess"];

                var heightofmagnet = xmlElement["HeightOfMagnet"];

                if(place!= null)
                {
                    Text_Box2_Insert_Place.Text = place.GetAttribute("place");
                }
                if(size!= null)
                {
                    Text_Box3_Insert_Chess_Size.Text = size.GetAttribute("Size");
                }
                if(height!= null)
                {
                    Text_Box4_Insert_Chess_Height.Text = height.GetAttribute("Height");
                }
                if(heightofmagnet!= null)
                {
                    Text_Box5_Insert_Magnit_Height.Text = heightofmagnet.GetAttribute("Height");
                }
            }
            else
            {
                File.WriteAllText(path, "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n<Values></Values>");
            }
        }
        private void Text_Box2_Insert_Place_LostFocus(object sender, RoutedEventArgs e)
        {
            string path = @"Configuration\Mach3Config.xml";

            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(path);

            var element = xmlDocument.DocumentElement;

            var child = element["PlaceOfChess"];

            if(child!= null)
            {
                string atr = child.GetAttribute("place");

                if(!String.IsNullOrEmpty(atr))
                {
                    if(atr!= Text_Box2_Insert_Place.Text)
                    {
                        child.SetAttribute("place",Text_Box2_Insert_Place.Text);
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
