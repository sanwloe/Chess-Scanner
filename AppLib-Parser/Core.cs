using Microsoft.Win32;
using System;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Windows.Forms;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using OpenQA.Selenium.DevTools;
using AngleSharp;
using System.Windows.Controls;
using AngleSharp.Dom;

namespace AppLib_Parser
{
    public delegate void SendMove(Move move,int number);
    public static class Core
    {
        public static event SendMove addmove; // Подія
        static public ChromeDriver WebDriver { get; set; } // Основний драйвер
        static public WebDriverWait webDriverWait { get; set; } // Драйвер що виконує дії на веб сторінці
        static public List<Move> moveChess { get; set; } = new List<Move>();// List усіх знайдених ходів
        static void Invoke(Move move, int number)
        {
            if (move != null & move._MoveFromInt.Length == 2 && move._MoveToInt.Length == 2)
                addmove.Invoke(move, number);
        }
        public static void AddMove(Move move,int number) // Метод через який добавляєм хід в List та викликаємо подію addevent
        {
            if(move._MoveFromInt.Length == 2 && move._MoveToInt.Length == 2)
            {
                moveChess.Add(move);

                Invoke(move,number);
            }
        }
        static public bool IsConnectedToInternet() // Провірка підключення до інтернету
        {
            try
            {
                var client = new HttpClient();

                var response = client.GetAsync("http://www.google.com");

                return response.Result.StatusCode == HttpStatusCode.OK ? true : false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Відсутнє підключення до інтернету!\nПомилка :" +
                    $"{e.Message}", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }
        public static void LoadGame()
        {
            WebDriver = GetBrowser(GetPathOfChromeBrowser()); // Ініціалізація

            webDriverWait = new WebDriverWait(WebDriver, new TimeSpan(0, 0, 3)); // Ініціалізація

            if (WebDriver != null && webDriverWait != null)
            {
                WebDriver.Navigate().GoToUrl("https://www.chess.com/"); // Відкриття браузера і перехід на chess.com

                var mainwindow = WebDriver.CurrentWindowHandle;

                WebDriver.SwitchTo().Window(mainwindow); //Перехід з вікна AdBlock на сторінку гри
                WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

                //Нажимання кнопки на веб сторінці
                webDriverWait.Until(dri => dri.FindElement(By.XPath("/html/body/div[1]/div[2]/main/div/div/section[1]/div[2]/div[2]/a"))).Click();
                WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

                //Нажимання кнопки на веб сторінці
                webDriverWait.Until(dri => dri.FindElement(By.XPath("/html/body/div[25]/div[2]/div/div/div[2]/div[2]/button"))).Click();
                WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

                //Нажимання кнопки на веб сторінці
                webDriverWait.Until(dri => dri.FindElement(By.XPath("//*[@id=\"board-layout-sidebar\"]/div/div[2]/button"))).Click();
                WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

                //Нажимання кнопки на веб сторінці
                webDriverWait.Until(dri => dri.FindElement(By.XPath("//*[@id=\"board-layout-sidebar\"]/div/div[2]/button"))).Click();
                //WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

                //Задача яка виконує цикл в іншому потоці

                Task.Run(() =>
                {
                    var result = WebDriver.PageSource;

                    var config = Configuration.Default;

                    var context = BrowsingContext.New(config);

                    Move newmove = new Move("11", "11");

                    Move oldmove = new Move("11", "11");

                    string oldto = "11", oldfrom = "11",newto = "11", newfrom = "11"; // строки що містять дані ходів

                    while (true)
                    {
                        var htmlpage = context.OpenAsync(req => req.Content(WebDriver.PageSource)); // загрузка розмітки сторінки

                        var move = htmlpage.Result.QuerySelectorAll("#board-vs-personalities > div[class*=\"highlight square-\"]"); // шлях до ходів

                        var moveresult = htmlpage.Result.QuerySelector("#board-layout-sidebar > div > vertical-move-list"); // шлях до результатів для перевірки де було побито шахмату

                        if(move.Count() == 2) // перевірка на наявність здійсненого ходу
                        {
                            newto = move[0].GetAttribute("class");newfrom = move[1].GetAttribute("class"); // отримання ходів

                            if (moveresult.Children.Count() > 0 && (newto != oldto || newfrom != oldfrom)) // перевірка чи було записано хід на сайті в табло та чи не повторюється цей хід
                            {
                                var currentchild = moveresult.Children.Last().Children.Last(); // отримання останього здійсненого ходу

                                Move currentmove = new Move(newfrom, newto); // даний хід

                                if(currentchild.TextContent.Contains('='))
                                {
                                    string localmove = currentchild.TextContent.Substring(0, currentchild.TextContent.IndexOf('='));

                                    if (currentmove.MoveFrom.ToString() == localmove.ToUpper())
                                    {
                                        AddMove(new Move(newto, newfrom), moveChess.Count);
                                        oldfrom = newfrom;
                                        oldto = newto;
                                        //MessageBox.Show(currentmove.MoveFrom + " " + currentmove.MoveTo);
                                        //MessageBox.Show("5");
                                    }
                                    else if (currentmove.MoveTo.ToString() == localmove.ToUpper())
                                    {
                                        AddMove(new Move(newfrom, newto), moveChess.Count);
                                        oldfrom = newfrom;
                                        oldto = newto;
                                        //MessageBox.Show(currentmove.MoveFrom + " " + currentmove.MoveTo);
                                        //MessageBox.Show("6");
                                    }
                                }
                                //MessageBox.Show(currentmove._MoveFromInt + " " + currentmove._MoveToInt);
                                if (currentchild.TextContent == "O-O-O")
                                {
                                    Move move1 = new Move("O-O-O", "");

                                    AddMove(move1, moveChess.Count);
                                }
                                if (currentchild.TextContent == "O-O")
                                {
                                    Move move1 = new Move("O-O", "");

                                    AddMove(move1, moveChess.Count);
                                }
                                if(currentchild.TextContent.Contains('#'))
                                {
                                    string findlocalshortmove = currentchild.TextContent.Replace("#", "");

                                    string localshortmove = findlocalshortmove.Substring(currentchild.TextContent.Length - 1,2);

                                    if (currentmove.MoveFrom.ToString() == localshortmove.ToUpper())
                                    {
                                        AddMove(new Move(newto, newfrom), moveChess.Count);
                                        oldfrom = newfrom;
                                        oldto = newto;
                                        //MessageBox.Show(currentmove.MoveFrom + " " + currentmove.MoveTo);
                                        //MessageBox.Show("5");
                                    }
                                    else if (currentmove.MoveTo.ToString() == localshortmove.ToUpper())
                                    {
                                        AddMove(new Move(newfrom, newto), moveChess.Count);
                                        oldfrom = newfrom;
                                        oldto = newto;
                                        //MessageBox.Show(currentmove.MoveFrom + " " + currentmove.MoveTo);
                                        //MessageBox.Show("6");
                                    }
                                }


                                if (currentchild.TextContent.Length == 5) // перевірка на результат ходу якщо його величина 5 символів
                                {
                                    if (currentchild.TextContent.Contains('+')) // Перевірка чи поставили шах
                                    {
                                        string localshortmove = currentchild.TextContent.Replace("+", ""); 

                                        localshortmove = localshortmove.Remove(0, 2);

                                        if (currentmove.MoveFrom.ToString() == localshortmove.ToUpper())
                                        {
                                            AddMove(new Move(newto, newfrom), moveChess.Count);
                                            oldfrom = newfrom;
                                            oldto = newto;
                                            //MessageBox.Show(currentmove.MoveFrom + " " + currentmove.MoveTo);
                                            //MessageBox.Show("5");
                                        }
                                        else if (currentmove.MoveTo.ToString() == localshortmove.ToUpper())
                                        {
                                            AddMove(new Move(newfrom, newto), moveChess.Count);
                                            oldfrom = newfrom;
                                            oldto = newto;
                                            //MessageBox.Show(currentmove.MoveFrom + " " + currentmove.MoveTo);
                                            //MessageBox.Show("6");
                                        }
                                    }
                                    else
                                    {
                                        string shortmove = currentchild.TextContent.Remove(0, 3);

                                        if (currentmove.MoveFrom.ToString() == shortmove.ToUpper())
                                        {
                                            AddMove(new Move(newto, newfrom), moveChess.Count);
                                            oldfrom = newfrom;
                                            oldto = newto;
                                            //MessageBox.Show(currentmove.MoveFrom + " " + currentmove.MoveTo);
                                            //MessageBox.Show("5");
                                        }
                                        else if (currentmove.MoveTo.ToString() == shortmove.ToUpper())
                                        {
                                            AddMove(new Move(newfrom, newto), moveChess.Count);
                                            oldfrom = newfrom;
                                            oldto = newto;
                                            //MessageBox.Show(currentmove.MoveFrom + " " + currentmove.MoveTo);
                                            //MessageBox.Show("6");
                                        }
                                    }
                                }
                                if (currentchild.TextContent.Length == 4) // перевірка чи сягає результат 4 символів
                                {
                                    string shortmove = currentchild.TextContent.Remove(0, 2);

                                    //MessageBox.Show(shortmove);
                                    if (currentchild.TextContent.Contains('+'))
                                    {
                                        shortmove = currentchild.TextContent.Remove(0, 1);
                                        shortmove = shortmove.Replace("+", "");

                                        if (currentmove.MoveFrom.ToString() == shortmove.ToUpper())
                                        {
                                            AddMove(new Move(newto, newfrom), moveChess.Count);
                                            oldfrom = newfrom;
                                            oldto = newto;
                                            //MessageBox.Show(currentmove.MoveFrom + " " + currentmove.MoveTo);
                                            //MessageBox.Show("5");
                                        }
                                        else if (currentmove.MoveTo.ToString() == shortmove.ToUpper())
                                        {
                                            AddMove(new Move(newfrom, newto), moveChess.Count);
                                            oldfrom = newfrom;
                                            oldto = newto;
                                            //MessageBox.Show(currentmove.MoveFrom + " " + currentmove.MoveTo);
                                            //MessageBox.Show("6");
                                        }
                                    }

                                    if (currentmove.MoveFrom.ToString() == shortmove.ToUpper())
                                    {
                                        AddMove(new Move(newto, newfrom), moveChess.Count);
                                        oldfrom = newfrom;
                                        oldto = newto;
                                        //MessageBox.Show(currentmove.MoveFrom + " " + currentmove.MoveTo);
                                        //MessageBox.Show("5");
                                    }
                                    else if (currentmove.MoveTo.ToString() == shortmove.ToUpper())
                                    {
                                        AddMove(new Move(newfrom, newto), moveChess.Count);
                                        oldfrom = newfrom;
                                        oldto = newto;
                                        //MessageBox.Show(currentmove.MoveFrom + " " + currentmove.MoveTo);
                                        //MessageBox.Show("6");
                                    }
                                }
                                else if (currentchild.TextContent.Length == 3) // перевірка чи хід був здійснений особливою шахою
                                {
                                    if(currentchild.TextContent.Contains('='))
                                    {
                                        string localshortmove = currentchild.TextContent.Replace("=", "");

                                        if (currentmove.MoveFrom.ToString() == localshortmove.ToUpper())
                                        {
                                            AddMove(new Move(newto, newfrom), moveChess.Count);
                                            oldfrom = newfrom;
                                            oldto = newto;
                                        }
                                        else if (currentmove.MoveTo.ToString() == localshortmove.ToUpper())
                                        {
                                            AddMove(new Move(newfrom, newto), moveChess.Count);
                                            oldfrom = newfrom;
                                            oldto = newto;
                                        }
                                    }
                                    string shortmove = currentchild.TextContent.Remove(0, 1);

                                    if (currentmove.MoveFrom.ToString() == shortmove.ToUpper())
                                    {
                                        AddMove(new Move(newto, newfrom), moveChess.Count);
                                        oldfrom = newfrom;
                                        oldto = newto;
                                        //MessageBox.Show(currentmove.MoveFrom + " " + currentmove.MoveTo);
                                        //MessageBox.Show("3");
                                    }
                                    else if (currentmove.MoveTo.ToString() == shortmove.ToUpper())
                                    {
                                        AddMove(new Move(newfrom, newto), moveChess.Count);
                                        oldfrom = newfrom;
                                        oldto = newto;
                                        //MessageBox.Show(currentmove.MoveFrom + " " + currentmove.MoveTo);
                                        //MessageBox.Show("4");
                                    }
                                }
                                else if (currentmove.MoveFrom.ToString() == currentchild.TextContent.ToString().ToUpper()) // перевірка на звичайні 2-значні ходи
                                {
                                    AddMove(new Move(newto, newfrom), moveChess.Count);
                                    oldfrom = newfrom;
                                    oldto = newto;
                                    //MessageBox.Show(currentmove.MoveFrom + " " + currentmove.MoveTo);
                                    //MessageBox.Show("2");
                                }
                                else if (currentmove.MoveTo.ToString() == currentchild.TextContent.ToString().ToUpper())
                                {
                                    AddMove(new Move(newfrom, newto), moveChess.Count);
                                    oldfrom = newfrom;
                                    oldto = newto;
                                    //MessageBox.Show(currentmove.MoveFrom + " " + currentmove.MoveTo);
                                    //MessageBox.Show("1");
                                }
                                move = null;
                            }
                        }
                    }
                });
            }
        }
        public static string[] GetNameExtensions() // Пошук розширень для браузера Chrome
        {
            string[] info = Directory.GetFiles(@"Extensions\");

            string[] extensions = new string[info.Length];

            for (int i = 0; i < extensions.Length; i++)
            {
                extensions[i] = Path.GetFileName(info[i]);
            }

            return extensions;
        }
        public static ChromeDriver GetBrowser(string path) // Ініціалізація розширень і повернення екземпляра Основного драйвера
        {
            try
            {
                var options = new ChromeOptions();
                
                string[] extensions = GetNameExtensions();

                if (extensions.Length > 0)

                    foreach (var item in extensions)
                    {
                        options.AddExtensions(@"Extensions\" + item);
                    }

                var service = ChromeDriverService.CreateDefaultService(path); 

                service.HideCommandPromptWindow = true;// Відключення консолі логування роботи Драйвера

                return new ChromeDriver(service,options);
            }
            catch(Exception ex)
            { 
                MessageBox.Show(ex.Message);
                return null;
                
            }

        }
        public static void Close()
        {
            //task.Dispose();

            WebDriver.Dispose();
        }
        public static string GetPathOfChromeBrowser() // Пошук шляху до chromedriver.exe
        {
            RegistryKey key = Registry.LocalMachine;

            var subkey = key.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Google Chrome");

            string path = subkey.GetValue("InstallLocation").ToString();

            return path;
        }
        public static bool ExistsOfChromeDriver() // Перевірка наявності файла chromedriver.exe 
        {
            if (!File.Exists(GetPathOfChromeBrowser() + "\\chromedriver.exe"))
            {
                MessageBox.Show("Помилка : Відсутній файл chromedriver.exe в коренній папці Chrome!\n" +
                    "Завантажте відповідної версії chromedriver для відповідної" +
                    "версії вашого Chrome за посиланням : \n" +
                    "http://chromedriver.storage.googleapis.com/index.html", "Відсутній файл!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else return true;
        }
    }
}
