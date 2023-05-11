using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HTMLparser
{
    public class Core
    {
        static public bool IsConnectedToInternet()
        {
            try
            {
                var client = new HttpClient();

                var response = client.GetAsync("http://www.google.com");

                return response.Result.StatusCode == HttpStatusCode.OK ? true : false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Відсутнє підключення до інтернету!\nКод помилки :" +
                    $"{e.Message}", "Помилка!",MessageBoxButtons.OK,MessageBoxIcon.Error);

                return false;
            }
        }
        static public IWebDriver _webDriver { get; set; }
        static public void OpenBrowser()
        {
            string path = GetPathOfChromeBrowser();

            if (path != null)
            {
                _webDriver = GetBrowser(path);
            }

            _webDriver.Navigate().GoToUrl("https://www.sparkchess.com/");    
            
        }
        static IWebDriver GetBrowser(string path)
        {
            try 
            {
                return new ChromeDriver(path);
            }
            catch
            {
                return null;
            }
            
        }
        static string GetPathOfChromeBrowser()
        {
            RegistryKey key = Registry.LocalMachine;

            var subkey = key.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Google Chrome");

            string path = subkey.GetValue("InstallLocation").ToString();

            return path;
        }
        public static bool ExistsOfChromeDriver()
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
