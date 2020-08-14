using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace GithubRespositoryNameFetcher
{
    class MainClass
    {
        static string a;
        static IWebDriver driver;

        public static void Main(string[] args)
        {
            driver = new FirefoxDriver();
            string url = "https://github.com/Syritx?tab=repositories";
            fetchNames(url);
        }


        static void fetchNames(string url)
        {
            driver.Url = url;
            Thread.Sleep(3000);

            int repositories = 30;
            for (int i = 0; i < repositories; i++)
            {
                int id = i + 1;
                try {

                    IWebElement repository;
                    IWebElement language;

                    repository = driver.FindElement(By.XPath("/html/body/div[4]/main/div[2]/div/div[2]/div[2]/div/div[2]/ul/li["+id+"]/div[1]/div[1]/h3/a"));
                    try {
                        language = driver.FindElement(By.XPath("/html/body/div[4]/main/div[2]/div/div[2]/div[2]/div/div[2]/ul/li[" + id + "]/div[1]/div[3]/span/span[2]"));
                    } catch (Exception e) {
                        language = driver.FindElement(By.XPath("/html/body/div[4]/main/div[2]/div/div[2]/div[2]/div/div[2]/ul/li[" + id + "]/div[1]/div[4]/span/span[2]"));
                    }

                    Console.WriteLine("[NAME]: {0}, [LANGUAGE]: {1} \n",repository.Text, language.Text);
                }
                catch(Exception e) {}
            }
        }
    }
}
