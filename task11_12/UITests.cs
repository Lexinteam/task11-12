using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace task11_12
{
    class UITests
    {
        IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver(@"C:\Users\work\source\repos\task11-12\task11-12\task11_12");
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.gismeteo.ua/ua");
        }

        [Test]
        public void Test1()
        {
            // Find all divs on the page
            IReadOnlyCollection<IWebElement> divs = driver.FindElements(By.TagName("div"));

            // 2.Find all divs with h2 ---- probably will not find
            driver.FindElements(By.XPath("//div//h2"));

            // 3.Find all items with news titles(the block under list of cities)(х items)  ---- should be 4
            IReadOnlyCollection<IWebElement>  newsTitles = driver.FindElements(By.XPath("//div[@class='readmore_item']"));
            Assert.True(newsTitles.Count == 4);

            // 4.Find the last span for Cities
            driver.FindElements(By.XPath("//section[@class='cities cities_frame __frame clearfix']//div[@class='cities_item'][last()]//span"));

            // 5.Get all titles from items from #3
            List<String> allTitles = new List<String>();

            IEnumerator ie = newsTitles.GetEnumerator();
            while (ie.MoveNext()) 
            {
                IWebElement we3 = (IWebElement)ie.Current;        
                IWebElement we5 = we3.FindElement(By.XPath("//div[@class='readmore_title nolink white']")); // searching inside of Xpath found in #3
                allTitles.Add(we5.Text);
            }
            ie.Reset();
            foreach (var title in allTitles)
            {
                Console.WriteLine(title);
            }


            // 6.Find element with text Киев
            driver.FindElement(By.XPath("//*[text()='Київ']"));

            // 7.Find the element that describes city next after Киев
            driver.FindElement(By.XPath("//*[text()='Київ']/../../following-sibling::div[1][@class='cities_item']"));

            // 8.Find all top menu link
            IReadOnlyCollection<IWebElement> topMenu = driver.FindElements(By.XPath("//li[@role='menuitem']/a[@class='link blue']"));

            // 9.Go to  the Kharkiv city weather page(https://www.gismeteo.ua/ua/weather-kharkiv-5053/ at the moment) 
            IWebElement KharkivLink = driver.FindElement(By.XPath("//*[text()='Харків']/.."));
            KharkivLink.Click();

            // 10.Find element for 3 weekdays just under the header with the text "weather for today".
            driver.FindElement(By.XPath("//div[@class='forecast_frame hw_wrap']"));

            // 11.Find element for currently selected weekday
            driver.FindElement(By.XPath("//div[@class='tab  tooltip']"));

            // 12.Find both temperature for currently selected weekday
            String lowT = driver.FindElement(By.XPath("//div[@class='tab  tooltip']//div[@class='value']/span")).Text;
            String highT = driver.FindElement(By.XPath("//div[@class='tab  tooltip']//div[@class='value'][last()]/span")).Text;

            // 13. Find the same elements, but using CSS where possible
            // CCS not my stong suit :(

        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
