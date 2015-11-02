namespace TestCrawler
{
    #region

    using System;
    using System.Linq;

    using OpenQA.Selenium;
    using OpenQA.Selenium.PhantomJS;

    #endregion

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to Crawler Test!");
            Console.WriteLine("The test url is http://data.shishicai.cn/jxssc/haoma/");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Press input your draw no(e.g. 001):");

            var drawNo = Console.ReadLine();

            var drawResult = GetJxsscResult(drawNo);

            Console.WriteLine("Draw result:{0}", drawResult);
        }

        #region private

        private static string GetJxsscResult(string drawNo)
        {
            var url = "http://data.shishicai.cn/jxssc/haoma/";

            var driver = new PhantomJSDriver { Url = url };
            driver.Navigate();

            // the driver can now provide you with what you need (it will execute the script)
            // get the source of the page
            var source = driver.PageSource;

            // fully navigate the dom
            var drawResultElements = driver.FindElementByClassName("newNum").FindElements(By.CssSelector("table[class='data_tab'] tbody tr"));
            var result = string.Empty;

            foreach (var draws in drawResultElements)
            {
                var drawElemnet = draws.Text.Split(' ');
                if (drawElemnet[0].Contains(drawNo))
                {
                    result = string.Join(",", drawElemnet.Select(o => o.ToString()).ToArray());
                    break;
                }
            }

            driver.Close();
            return result;
        }

        #endregion
    }
}