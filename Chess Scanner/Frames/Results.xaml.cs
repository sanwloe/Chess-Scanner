using AppLib_Parser;
using System;
using System.Collections.Generic;
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

namespace Chess_Scanner.Frames
{
    /// <summary>
    /// Логика взаимодействия для Results.xaml
    /// </summary>
    public partial class Results : Page
    {
        public Results Instance;
        public Results()
        {
            InitializeComponent();

            Instance = this;

            Core.addmove += AddMove;

            List_Box1.Background = Brushes.FloralWhite;

            List_Box2.Background = Brushes.FloralWhite;
        }
        ~Results()
        {
            Core.addmove -= AddMove;
        }
        public void FirstLoad()
        {
            if (Core.moveChess!=null && Core.moveChess.Count > 0 )
            {
                for (int i = 0; i < Core.moveChess.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        List_Box1.Items.Insert(0,Core.moveChess[i].MoveFrom + "\t" + Core.moveChess[i].MoveTo);

                    }
                    else
                    {
                        List_Box2.Items.Insert(0,Core.moveChess[i].MoveFrom + "\t" + Core.moveChess[i].MoveTo);
                    }
                }
                //MessageBox.Show(Core.moveChess.Count.ToString());
            }
            else
            {
                
            }
        }
        public void AddMove(Move move, int number)
        {
            if(number % 2 == 0)
            {
                List_Box1.Dispatcher.Invoke(() =>
                {
                    List_Box1.Items.Insert(0,move.MoveFrom + "\t" + move.MoveTo);
                    //List_Box1.Items.Insert(0, move._MoveFromInt + "\t" + move._MoveToInt);
                });
            }
            else
            {
                List_Box2.Dispatcher.Invoke(() =>
                {
                    List_Box2.Items.Insert(0,move.MoveFrom + "\t" + move.MoveTo);
                    //List_Box2.Items.Insert(0,move._MoveFromInt + "\t" + move._MoveToInt);
                });              
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FirstLoad();  
        }
    }
}

