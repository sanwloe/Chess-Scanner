using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using AppLib_InputGCode;
using AppLib_Parser;

namespace AppLib_Analysis
{
    public static class AnalysisChessMove
    {    
        static AnalysisChessMove()
        {
            GetXY();

            GetHeight();

            GetMagnetHeight();
            
            GetPlace();
        }
        static string[,] board = GetBoard();
        static int XY;
        static int Height;
        static int MagnetHeight;
        static string place;
        public static void NewGame()
        {
            board = GetBoard();
        }
        static void GetXY()
        {
            string path = @"Configuration\Mach3Config.xml";

            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(path);

            var element = xmlDocument.DocumentElement;

            var child = element["SizeOfChess"];

            if (child != null)
            {
                if (!String.IsNullOrEmpty(child.GetAttribute("Size")))
                {
                    XY = int.Parse(child.GetAttribute("Size"));
                }
            }
        }
        static void GetHeight()
        {
            string path = @"Configuration\Mach3Config.xml";

            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(path);

            var element = xmlDocument.DocumentElement;

            var child = element["HeightOfChess"];

            if (child != null)
            {
                if (!String.IsNullOrEmpty(child.GetAttribute("Height")))
                {
                    Height = int.Parse(child.GetAttribute("Height"));
                }
            }
        }
        static void GetMagnetHeight()
        {
            string path = @"Configuration\Mach3Config.xml";

            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(path);

            var element = xmlDocument.DocumentElement;

            var child = element["HeightOfMagnet"];

            if (child != null)
            {
                if (!String.IsNullOrEmpty(child.GetAttribute("Height")))
                {
                    MagnetHeight = int.Parse(child.GetAttribute("Height"));
                }
            }
        }
        public static void Analysis(Move move, int number)
        {
            if(move.MoveFrom == "O-O-O")
            {
                LongCastling(number);
            }
            else if (move.MoveFrom == "O-O")
            {
                ShortCastling(number);
            }
            if (move != null)
            {
                if (XY != 0 && !String.IsNullOrEmpty(place) && !String.IsNullOrEmpty(Height.ToString()) && !String.IsNullOrEmpty(MagnetHeight.ToString()))
                {
                    char[] a = move._MoveFromInt.ToCharArray(), b = move._MoveToInt.ToCharArray();
                    Array.Reverse(a);
                    Array.Reverse(b);
                    //MessageBox.Show(move.MoveFrom + " " + move.MoveTo + "  1");
                    MoveChess(new string(a), new string(b));                  
                }
                else
                {
                    MessageBox.Show("Не задані дані про шахмати!Заповніть в \"Управлінні\"");
                }
            }
        }
        public static void ShortCastling(int number)
        {
            if (XY != 0 && !String.IsNullOrEmpty(place) && !String.IsNullOrEmpty(Height.ToString()) && !String.IsNullOrEmpty(MagnetHeight.ToString()))
            {   //Коротка рокировка
                if(number % 2 == 0)
                {
                    var from1 = "15"; // E1

                    var to1 = "17";//G1

                    var from2 = "18";//H1

                    var to2 = "16";//F1

                    MoveChess(from1, to1);

                    MoveChess(from2, to2);
                }
                else
                {
                    var from1 = "85";//E8

                    var to1 = "87";//G7

                    var from2 = "88";//H8

                    var to2 = "86";//F6

                    MoveChess(from1, to1);

                    MoveChess(from2, to2);
                }
            }
            else
            {
                MessageBox.Show("Не задані дані про шахмати!Заповніть в \"Управлінні\"");
            }
        }
        public static void LongCastling(int number)
        {
            if (XY != 0 && !String.IsNullOrEmpty(place) && !String.IsNullOrEmpty(Height.ToString()) && !String.IsNullOrEmpty(MagnetHeight.ToString()))
            {   //Довга рокировка
                if (number % 2 == 0)
                {
                    var from1 = "11"; // A1

                    var to1 = "14"; // D1

                    var from2 = "15"; //E1

                    var to2 = "13";//C1

                    MoveChess(from1, to1);

                    MoveChess(from2, to2);
                }
                else
                {
                    var from1 = "81"; // A8

                    var to1 = "84"; // D8

                    var from2 = "85"; //E8

                    var to2 = "83"; //C8

                    MoveChess(from1, to1);

                    MoveChess(from2, to2);
                }
            }
            else
            {
                MessageBox.Show("Не задані дані про шахмати!Заповніть в \"Управлінні\"");
            }
        }
        static void MoveChess(string from, string to)
        {
            //MessageBox.Show(from + " " + to);

            int fromcol = Math.Abs(int.Parse(from[0].ToString()) - 9);
            
            int tocol = Math.Abs(int.Parse(to[0].ToString()) - 9);

            int fromrow = Math.Abs(int.Parse(from[1].ToString()));

            int torow = Math.Abs(int.Parse(to[1].ToString()));

            //MessageBox.Show(fromcol.ToString() + '\n' + tocol.ToString() + '\n' + fromrow.ToString() + '\n' + torow.ToString());

            int Yfrom = int.Parse(from[0].ToString());

            int Xfrom = int.Parse(from[1].ToString());

            int Yto = int.Parse(to[0].ToString());

            int Xto = int.Parse(to[1].ToString());

            if (board[tocol, torow] == "  " && board[fromcol, fromrow] != "  ")
            {
                board[tocol, torow] = board[fromcol, fromrow];

                Input.ExecuteCode("Z" + MagnetHeight);

                Input.ExecuteCode("X" + (Xfrom * XY) + "Y" + (Yfrom * XY));

                Input.ExecuteCode("Z" + Height);

                Input.ExecuteCode("M3");

                Input.ExecuteCode("Z" + MagnetHeight);

                Input.ExecuteCode("X" + (Xto * XY) + "Y"+(Yto * XY));

                Input.ExecuteCode("Z" + Height);

                Input.ExecuteCode("M5");

                Input.ExecuteCode("Z" + MagnetHeight);
                
                Input.ExecuteCode("X" + ((Xto * XY) + 3) + "Y" + ((Yto * XY) + 3));

                board[fromcol, fromrow] = "  ";

                Thread.Sleep(1000);          
            }
            else if (board[tocol, torow] != "  " && board[fromcol, fromrow] != "  ")
            {
                board[tocol, torow] = board[fromcol, fromrow];
                
                board[fromcol, fromrow] = "  ";

                Input.ExecuteCode("Z" + MagnetHeight);

                Input.ExecuteCode("X" + (Xto * XY) + "Y" + (Yto * XY));

                Input.ExecuteCode("Z" + Height);

                Input.ExecuteCode("M3");

                Input.ExecuteCode("Z" + MagnetHeight);

                Input.ExecuteCode(place);

                Input.ExecuteCode("M5");

                Input.ExecuteCode("X" + (Xfrom * XY) + "Y" + (Yfrom * XY));

                Input.ExecuteCode("Z" + Height);

                Input.ExecuteCode("M3");

                Input.ExecuteCode("Z" + MagnetHeight);

                Input.ExecuteCode("X" + (Xto * XY) + "Y" + (Yto * XY));

                Input.ExecuteCode("Z" + Height);

                Input.ExecuteCode("M5");

                Input.ExecuteCode("Z" + MagnetHeight);

                Input.ExecuteCode("X" + ((Xto * XY) + 3) + "Y" + ((Yto * XY) + 3));

                Thread.Sleep(1000);
            }
        }
        static void GetPlace()
        {
            string path = @"Configuration\Mach3Config.xml";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(path);

            XmlElement xmlElement = xmlDoc.DocumentElement;

            var node = xmlElement["PlaceOfChess"];

            if (node != null)
            {
                place = node.GetAttribute("place");
            } 
            else
            {
                Input.Stop();
                MessageBox.Show("Не знайдено місце для шахмат!\nВкажіть в Налаштуваннях місце!");
            }
        }
        static string [,] GetBoard()
        {
            string[,] board = new string[9, 9];

            string[] columns = { "  ", "A", "B", "C", "D", "E", "F", "G", "H" };

            for (int i = 1; i < 9; i++)
            {
                board[8, i] = columns[i] + "8";
            }
            for (int i = 1; i < 9; i++)
            {
                board[7, i] = columns[i] + "7";
            }
            for (int i = 1; i < 9; i++)
            {
                board[2, i] = columns[i] + "2";
            }
            for (int i = 1; i < 9; i++)
            {
                board[1, i] = columns[i] + "1";
            }
            for (int i = 3; i < 7; i++)
            {
                for (int k = 1; k < 9; k++)
                {
                    board[i, k] = "  ";
                }
            }
            return board;
        }
    }
}
