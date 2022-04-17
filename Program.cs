using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Net;
using System.IO;



class HelloSelenium
{

    static void Main()
    {
        var listOfStrings = new List<string>();

        new DriverManager().SetUpDriver(new ChromeConfig());
        IWebDriver driver = new ChromeDriver();
        try
        {
            // Navigate to Url
            driver.Navigate().GoToUrl("https://imgur.com/");
            // Store 'google search' button web element
           var searchBox = driver.FindElement(By.Name("q"));
           var searchButton = driver.FindElement(By.ClassName("Searchbar-submitInput"));

           searchBox.SendKeys("Cats");
           driver.FindElement(By.Name("q")).GetAttribute("value"); // => "Selenium"
                                                                    //Give wait time after
           driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1000);
           searchButton.Click();
            Actions actionProvider = new Actions(driver);
            // Perform click action on the element
           driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);
            IReadOnlyCollection<IWebElement> element = driver.FindElements(By.ClassName("image-list-link"));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2500);
            foreach (IWebElement e in element)
            {
                listOfStrings.Add(e.GetAttribute("href"));
           //   Console.WriteLine(e.GetAttribute("href"));


            }

            

        }
        finally

        {
            int count = 0;
            string originalWindow = driver.CurrentWindowHandle;
            foreach (var ele in listOfStrings)
            {
                string fileName = $"test{count}.webp", myStringWebResource = null;
                //Console.WriteLine(ele);
                //  myWebClient.DownloadFile(ele, fileName);
                driver.SwitchTo().NewWindow(WindowType.Tab);
                driver.Navigate().GoToUrl(ele);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);

                IReadOnlyCollection<IWebElement> imageStc = driver.FindElements(By.ClassName("image-placeholder"));
                WebClient myWebClient = new WebClient();

                for (int i = 0; i < imageStc.Count; i++)
                {
                    imageStc.ElementAt(i).GetAttribute("src");
               
                    myWebClient.DownloadFile(imageStc.ElementAt(i).GetAttribute("src"), fileName);
                }
                driver.Close();
                driver.SwitchTo().Window(originalWindow);
                count++;
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1000);

                //

            }

            driver.Quit();
        }
    }
}
