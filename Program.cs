using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://booking.communitytest.gov.hk/form/index_tc.jsp");
            var id_box = driver.FindElement(By.Id("step_1_documentId_HKIC_prefix"));
            id_box.SendKeys("R740622");
            var id_num_box = driver.FindElement(By.Id("step_1_documentId_HKIC_check_digit"));
            id_num_box.SendKeys("2");
            System.Threading.Thread.Sleep(1000);

            driver.FindElement(By.CssSelector("[data-role=BOOKING]")).Click();

            System.Threading.Thread.Sleep(5000);
            driver.FindElement(By.CssSelector("[title = 請勾選同意以上條款並繼續]")).Click();
            driver.FindElement(By.Id("note_2_confirm")).Click();

            System.Threading.Thread.Sleep(3500);
            driver.FindElement(By.Id("step_2_language_preference_chinese")).Click();
            driver.FindElement(By.Id("step_2_surname")).SendKeys("黎");
            driver.FindElement(By.Id("step_2_givenname")).SendKeys("桂喜");
            driver.FindElement(By.Id("step_2_tel_for_sms_notif")).SendKeys("51131786");
            driver.FindElement(By.Id("step_2_tel_for_sms_notif_confirm")).SendKeys("51131786");


            while (true)
            {
                var distinct = driver.FindElement(By.Id("step_2_district"));
                System.Threading.Thread.Sleep(500);
                var distinctOption = new SelectElement(distinct);
                distinctOption.SelectByValue("5");

                //driver.FindElement(By.CssSelector("[value = 5]")).Click();
                var location = driver.FindElement(By.Id("step_2_center"));
                System.Threading.Thread.Sleep(500);
                var locationOption = new SelectElement(location);
                locationOption.SelectByValue("3");

                //driver.FindElements(By.CssSelector("[role=button]"))[3].Click();
                //driver.FindElements(By.CssSelector("[role=button]"))[4].Click();


                //var secondDate = driver.FindElements(By.TagName("li"))[3].Click();
                //var thirdDate = driver.FindElements(By.TagName("li"))[4];
                //var options = secondDate.FindElements(By.CssSelector("[type=radio]"));
                for (int x = 2; x <= 4; x++)
                {
                    var currentDate = driver.FindElements(By.CssSelector("[role=button]"))[x];
                    if(currentDate.GetAttribute("aria-expanded") == "false")
                    {
                        currentDate.Click();
                    }

                    var radioGroup = driver.FindElements(By.ClassName("timeslots"))[x].FindElements(By.ClassName("radio_group"));
                    //var radioGroup = driver.FindElements(By.TagName("li"))[x].FindElement(By.Id("collapse2")).FindElements(By.ClassName("radio_group"));
                    var options = new List<IWebElement>();
                    foreach (var timeslot in radioGroup)
                    {
                        options.Add(timeslot.FindElement(By.Name("step_2_booking_timeslot")));
                    }

                    //var options = driver.FindElements(By.TagName("li"))[x].FindElements(By.CssSelector("[type=radio]"));
                    foreach (var option in options)
                    {
                        if (option.Enabled == true)
                        {
                            option.Click();
                            Console.WriteLine("Found available slot");
                            driver.FindElement(By.Id("step_2_form_control_confirm")).Click();
                            System.Threading.Thread.Sleep(1000);
                            driver.FindElement(By.CssSelector("[title = 確認输入的资料]")).Click();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Cant find available slot");
                        }
                    }
                    System.Threading.Thread.Sleep(500);

                }
                distinct = driver.FindElement(By.Id("step_2_district"));
                System.Threading.Thread.Sleep(500);
                distinctOption = new SelectElement(distinct);
                distinctOption.SelectByValue("4");

            }





        }
    }
}
