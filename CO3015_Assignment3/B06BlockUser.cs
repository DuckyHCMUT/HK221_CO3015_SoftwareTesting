using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;

namespace CO3015_Assignment3
{
    public class B06BlockUser
    {
        private readonly string? _userName = ConfigurationManager.AppSettings["username"];
        private readonly string? _password = ConfigurationManager.AppSettings["password"];

        public void RunTest()
        {
            TcB0601();
            TcB0602();
            TcB0603();
            TcB0604();
        }

        private void Login(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://e-learning.hcmut.edu.vn/my/");
            Thread.Sleep(2000);

            driver.FindElement(By.LinkText("Teachers and Students of HCMUT")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.Name("username")).SendKeys(_userName);
            Thread.Sleep(2000);

            driver.FindElement(By.Name("password")).SendKeys(_password);
            Thread.Sleep(2000);

            driver.FindElement(By.Name("submit")).Click();
            Thread.Sleep(5000);
        }

        private void RecoverBlockedUser(IWebDriver driver)
        {
            try
            {
                Console.WriteLine($"Performing system restore before execution of {nameof(TcB0604)} started");

                Login(driver);

                driver.FindElement(By.XPath("//a[contains(@id, 'message-drawer-toggle')]")).Click();
                Thread.Sleep(2000);

                driver.FindElement(By.XPath("//button[contains(@aria-controls, 'view-overview-messages-target')]"))
                    .Click();
                Thread.Sleep(2000);

                driver.FindElement(By.XPath("//a[contains(@data-conversation-id, '233963')]")).Click();
                Thread.Sleep(2000);

                driver.FindElement(By.XPath("//button[contains(@data-action, 'request-unblock')]")).Click();
                Thread.Sleep(2000);

                driver.FindElement(By.XPath("//button[contains(@data-action, 'confirm-unblock')]")).Click();
                Thread.Sleep(2000);

                Console.WriteLine($"Restore completed, test case {nameof(TcB0604)} is starting soon...");
            }
            catch (ElementNotInteractableException)
            {
                throw new ElementNotInteractableException();
            }
        }

        public void TcB0601()
        {
            IWebDriver driver = new ChromeDriver();
            Login(driver);

            try
            {
                Console.WriteLine($"Test case {nameof(TcB0601)} started");

                driver.FindElement(By.XPath("//a[contains(@id, 'message-drawer-toggle')]")).Click();
                Thread.Sleep(2000);

                driver.FindElement(By.XPath("//button[contains(@aria-controls, 'view-overview-messages-target')]"))
                    .Click();
                Thread.Sleep(2000);

                driver.FindElement(By.XPath("//a[contains(@data-conversation-id, '66963')]")).Click();
                Thread.Sleep(2000);

                driver.FindElement(By.Id("conversation-actions-menu-button")).Click();
                Thread.Sleep(2000);

                driver.FindElement(By.LinkText("Block user")).Click();
                Thread.Sleep(2000);

                driver.FindElement(By.XPath("//button[contains(@data-action, 'okay-confirm')]")).Click();
                Thread.Sleep(2000);
                
                Console.WriteLine($"Test case {nameof(TcB0601)} ended");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{nameof(TcB0601)}: {e.Message}");
                Console.WriteLine($"Test case {nameof(TcB0601)} failed");
            }
            finally
            {
                driver.Close();
            }
            Console.WriteLine("==========================================================");
        }

        public void TcB0602()
        {
            IWebDriver driver = new ChromeDriver();

            try
            {
                RecoverBlockedUser(driver);
            }
            // If this exception is caught, the restoration step is not necessary
            catch (ElementNotInteractableException)
            {
                Console.WriteLine("Restoration is not required");
            }
            finally
            {
                try
                {
                    Console.WriteLine($"Test case {nameof(TcB0602)} started");

                    driver.FindElement(By.Id("conversation-actions-menu-button")).Click();
                    Thread.Sleep(2000);

                    driver.FindElement(By.LinkText("Block user")).Click();
                    Thread.Sleep(2000);

                    driver.FindElement(By.XPath("//button[contains(@data-action, 'cancel-confirm')]")).Click();
                    Thread.Sleep(2000);

                    Console.WriteLine($"Test case {nameof(TcB0602)} ended");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{nameof(TcB0602)}: {e.Message}");
                    Console.WriteLine($"Test case {nameof(TcB0602)} failed");
                }
                finally
                {
                    driver.Close();
                }
            }
            Console.WriteLine("==========================================================");
        }

        public void TcB0603()
        {
            IWebDriver driver = new ChromeDriver();

            try
            {
                RecoverBlockedUser(driver);
            }
            // If this exception is caught, the restoration step is not necessary
            catch (ElementNotInteractableException)
            {
                Console.WriteLine("Restoration is not required");
            }
            finally
            {
                try
                {
                    Console.WriteLine($"Test case {nameof(TcB0603)} started");

                    driver.FindElement(By.Id("conversation-actions-menu-button")).Click();
                    Thread.Sleep(2000);

                    driver.FindElement(By.LinkText("Block user")).Click();
                    Thread.Sleep(2000);

                    driver.FindElement(By.XPath("//button[contains(@data-action, 'confirm-block')]")).Click();
                    Thread.Sleep(2000);

                    driver.FindElement(By.Id("conversation-actions-menu-button")).Click();
                    Thread.Sleep(2000);

                    Console.WriteLine($"Test case {nameof(TcB0603)} ended");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{nameof(TcB0603)}: {e.Message}");
                    Console.WriteLine($"Test case {nameof(TcB0603)} failed");
                }
                finally
                {
                    driver.Close();
                }
            }
            Console.WriteLine("==========================================================");
        }


        public void TcB0604()
        {
            IWebDriver driver = new ChromeDriver();

            try
            {
                RecoverBlockedUser(driver);
            }
            // If this exception is caught, the restoration step is not necessary
            catch (ElementNotInteractableException)
            {
                Console.WriteLine("Restoration is not required");
            }
            finally
            {
                try
                {
                    Console.WriteLine($"Test case {nameof(TcB0604)} started");

                    driver.FindElement(By.Id("conversation-actions-menu-button")).Click();
                    Thread.Sleep(2000);

                    driver.FindElement(By.LinkText("Block user")).Click();
                    Thread.Sleep(2000);

                    driver.FindElement(By.XPath("//button[contains(@data-action, 'confirm-block')]")).Click();
                    Thread.Sleep(2000);

                    Console.WriteLine($"Test case {nameof(TcB0604)} ended");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{nameof(TcB0604)}: {e.Message}");
                    Console.WriteLine($"Test case {nameof(TcB0604)} failed");
                }
                finally
                {
                    driver.Close();
                }
            }
            Console.WriteLine("==========================================================");
        }
    }
}
