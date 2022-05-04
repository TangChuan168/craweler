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
using ConsoleSpider.Domain.Services;

namespace Guru99Demo
{
    public class CGuru99Demo
    {

        IWebDriver driver;
        CateService _DataService;

        public CGuru99Demo(
          CateService DataService
         )
        {
            _DataService = DataService;
        }

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

            Looper2(elements45);
            
        }

        public async void Looper2(ReadOnlyCollection<IWebElement> elements,string fatherNode = "origin" )
        {       

                List<Categories> CateList = new List<Categories>();
                foreach (var node in elements)
                    {
                        //get url from each node;

                        var parentId = fatherNode == "origin"? "origin" : fatherNode;
                        var newCate = new Categories()
                        {
                            Url = node.GetAttribute("href"),
                            tagName = node.Text,
                            parentId = parentId,
                            namePassToChild = node.Text
                        };

                        //push data to list for children node
                        CateList.Add(newCate);

                        //push data to database
                       await _DataService.AddCategory(newCate);

                    };

                foreach (var url2 in CateList)
                {
                
                    driver.Navigate().GoToUrl(url2.Url);
                    ReadOnlyCollection<IWebElement> childNodes = GetNodes(url2.Url);
                if (childNodes != null)
                    {
                        Looper2(childNodes,url2.namePassToChild);
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
