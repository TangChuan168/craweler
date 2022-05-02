using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using ConsoleSpider.Domain.Models;

namespace Guru99Demo
{
    class CGuru99Demo
    {
        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void test()
        {
            //driver.Url = "http://www.google.co.nz
            driver.Navigate().GoToUrl("https://www.trademe.co.nz/a/marketplace");
            //var results = driver.FindElement(By.XPath("//*[@id=\"search\"]"));
            //ReadOnlyCollection<IWebElement> elements2 = driver.FindElements(By.XPath("//*[contains(@id,\"modelDetail\")]"));
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/tm-root/div[1]/main/div/tm-marketplace-landing-page/div[2]/tm-marketplace-cat-splat/nav/ul/li/a")));
            ReadOnlyCollection<IWebElement> elements45 = driver.FindElements(By.XPath("/html/body/tm-root/div[1]/main/div/tm-marketplace-landing-page/div[2]/tm-marketplace-cat-splat/nav/ul/li/a"));

            looper2(elements45);
            
        }

        public void looper2(ReadOnlyCollection<IWebElement> elements )
        {       

                List<string> urls = new List<string>();
                foreach (var node in elements)
                {
                    //get url from each node;
                    var url = node.GetAttribute("href");

                    //push url to urls list
                    urls.Add(url);
                    //dataBase.save(cate);
                    Console.WriteLine(node.Text);

                    Console.WriteLine("--------------------------------------");
                };

                foreach (var url2 in urls)
                {
                
                    driver.Navigate().GoToUrl(url2);
                    ReadOnlyCollection<IWebElement> childNodes = GetNodes(url2);
                if (childNodes != null)
                    {
                        looper2(childNodes);
                    }                
            };

        }

        //function for return child nodes 
        public ReadOnlyCollection<IWebElement> GetNodes(string url)
        {
            driver.Navigate().GoToUrl(url);
            Console.WriteLine("openning... url@@@@@@@@@@@@@@@@@@@@@@@@@");
            Console.WriteLine(url);

            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div/tm-category-suggestions/div/ul/li/a")));
                return driver.FindElements(By.XPath("//div/tm-category-suggestions/div/ul/li/a"));
            }catch( Exception e)
            {
                Console.WriteLine("no more node!!!!!!!!!!!!!!!!!!!!");
                Console.WriteLine(e);
                
                return null;
            }
        }


        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }

    }
}
