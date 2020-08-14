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

            GetInput();
        }

        static void GetInput()
        {
            Console.Write("insert github username: ");
            string input = Console.ReadLine();

            string url = "https://github.com/" + input + "?tab=repositories";
            fetchRepositories(url);
        }


        static void fetchRepositories(string githubProfileURL)
        {
            driver.Url = githubProfileURL;
            Thread.Sleep(3000);

            int videos = 30;

            Console.WriteLine(" ");

            int total = 0, totalLang = 0;
            string[] languages = new string[30], totalLanguages = new string[30];
            int[] values = new int[30];

            for (int i = 0; i < videos; i++)
            {
                int id = i + 1;

                try {

                    // FINDING REPOSITORIES AND THEIR LANGUAGES
                    IWebElement repository;
                    IWebElement language;
                    repository = driver.FindElement(By.XPath("/html/body/div[4]/main/div[2]/div/div[2]/div[2]/div/div[2]/ul/li["+id+"]/div[1]/div[1]/h3/a"));

                    try {
                        // IF THE REPO HAS NO TAGS DO THIS
                        language = driver.FindElement(By.XPath("/html/body/div[4]/main/div[2]/div/div[2]/div[2]/div/div[2]/ul/li[" + id + "]/div[1]/div[3]/span/span[2]"));
                    }
                    catch (Exception e) {
                        // DO THIS INSTEAD
                        language = driver.FindElement(By.XPath("/html/body/div[4]/main/div[2]/div/div[2]/div[2]/div/div[2]/ul/li[" + id + "]/div[1]/div[4]/span/span[2]"));
                    }

                    // PRINTING THE FOUND LANGUAGES
                    Console.WriteLine("[NAME]: {0} \n[LANGUAGE]: {1} \n",repository.Text, language.Text.ToUpper());

                    // INSERTING A NEW LANGUAGE
                    bool canInclude = true;
                    foreach (string lang in languages) {
                        if (lang == language.Text.ToUpper()) canInclude = false;
                    }

                    if (canInclude) {
                        languages[total] = language.Text.ToUpper();
                        total++;
                    }

                    // INSERTING ALL LANGUAGES FOUND
                    totalLanguages[totalLang] = language.Text.ToUpper();
                    totalLang++;
                }
                catch(Exception e) {}
            }

            // CALCULATING THE AMOUNT OF LANGUAGES
            for (int lID = 0; lID < languages.Length; lID++) {
                foreach (string lang in totalLanguages) {
                    if (languages[lID] == lang && languages[lID] != null) {
                        values[lID]++;
                    }
                }
            }

            // PRINTING THE AMOUNT OF EACH LANGUAGE
            for (int lID = 0; lID < languages.Length; lID++) {
                if (languages[lID] != null)
                    Console.WriteLine("[{0}]: {1}",languages[lID],values[lID]);
            }

            // GETTING THE PERCENTAGES
            int totalValues = 0;

            for (int i = 0; i < values.Length; i++) {
                totalValues += values[i];
            }

            Console.WriteLine("\n------------------------\nPERCENTAGES\n");

            for (int lID = 0; lID < languages.Length; lID++)
            {
                // CALCULATING THE PERCENTAGE
                float percentage = (float)values[lID] / totalValues; 
                percentage *= 100;

                // PRINTING THE PERCENTAGE
                if (languages[lID] != null)
                    Console.WriteLine("[{0}]: {1}", languages[lID], percentage.ToString("0.00") + "%");
            }

            Console.WriteLine(" ");
            GetInput();
        }
    }
}
