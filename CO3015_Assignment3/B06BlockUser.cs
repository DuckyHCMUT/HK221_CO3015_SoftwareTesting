using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;

namespace CO3015_Assignment3
{
    public class B06BlockUser
    {
        private readonly string? _userName = ConfigurationManager.AppSettings["username"];
        private readonly string? _password = ConfigurationManager.AppSettings["password"];
        private readonly string? _projectDirectory;

        public B06BlockUser()
        {
            // Define License Context for EPPlus package
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            _projectDirectory = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;

            if (string.IsNullOrEmpty(_projectDirectory))
            {
                throw new DirectoryNotFoundException($"{_projectDirectory} not found!");
            }
        }

        public void RunTest()
        {
            //TcB0601();
            //TcB0602();
            //TcB0603();
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
        private string? GetCellDataFromExcelFile(string excelFileName, string cell)
        {
            using var xlPackage = new ExcelPackage(new FileInfo(
                Path.Combine(_projectDirectory ?? throw new InvalidOperationException(), $"resource\\{excelFileName}")));

            var myWorksheet = xlPackage.Workbook.Worksheets.First();
            return myWorksheet.Cells[cell].Select(x => x.Value.ToString()).First();
        }

        private void RecoverBlockedUser(IWebDriver driver)
        {
            var targetId = GetCellDataFromExcelFile("B06_data.xlsx", "B3");

            if (string.IsNullOrEmpty(targetId))
            {
                throw new NullReferenceException("Null Target Id");
            }

            Login(driver);

            Console.WriteLine("Restoration started");

            driver.FindElement(By.XPath("//a[contains(@id, 'message-drawer-toggle')]")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//button[contains(@aria-controls, 'view-overview-messages-target')]"))
                .Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath($"//a[contains(@data-conversation-id, '{targetId}')]")).Click();
            Thread.Sleep(2000);

            var requestUnblockBtnElement = driver.FindElement(By.XPath("//button[contains(@data-action, 'request-unblock')]"));

            if (requestUnblockBtnElement.Displayed)
            {
                requestUnblockBtnElement.Click();
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("Not need to restore");
                return;
            }

            driver.FindElement(By.XPath("//button[contains(@data-action, 'confirm-unblock')]")).Click();
            Thread.Sleep(2000);

            Console.WriteLine("Restoration finished");
        }

        /// <summary>
        /// Test case for Block admin
        /// </summary>
        public void TcB0601()
        {
            IWebDriver driver = new ChromeDriver();

            try
            {
                var targetId = GetCellDataFromExcelFile("B06_data.xlsx", "B2");

                if (string.IsNullOrEmpty(targetId))
                {
                    throw new NullReferenceException("Null Target Id");
                }

                Console.WriteLine($"Test case {nameof(TcB0601)} started");

                Login(driver);

                driver.FindElement(By.XPath("//a[contains(@id, 'message-drawer-toggle')]")).Click();
                Thread.Sleep(2000);

                driver.FindElement(By.XPath("//button[contains(@aria-controls, 'view-overview-messages-target')]"))
                    .Click();
                Thread.Sleep(2000);

                driver.FindElement(By.XPath($"//a[contains(@data-conversation-id, '{targetId}')]")).Click();
                Thread.Sleep(2000);

                driver.FindElement(By.Id("conversation-actions-menu-button")).Click();
                Thread.Sleep(2000);

                driver.FindElement(By.LinkText("Block user")).Click();
                Thread.Sleep(2000);

                driver.FindElement(By.XPath("//button[contains(@data-action, 'okay-confirm')]")).Click();
                Thread.Sleep(2000);

                Console.WriteLine($"Test case {nameof(TcB0601)} executed successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{nameof(TcB0601)}: {e.Message}");
                Console.WriteLine($"Test case {nameof(TcB0601)} failed");
            }
            finally
            {
                driver.Close();
                Console.WriteLine("==========================================================");
            }
        }

        /// <summary>
        /// Test case for Cancel blocking
        /// </summary>
        public void TcB0602()
        {
            IWebDriver driver = new ChromeDriver();
            RecoverBlockedUser(driver);

            try
            {
                Console.WriteLine($"Test case {nameof(TcB0602)} started");

                driver.FindElement(By.Id("conversation-actions-menu-button")).Click();
                Thread.Sleep(2000);

                driver.FindElement(By.LinkText("Block user")).Click();
                Thread.Sleep(2000);

                driver.FindElement(By.XPath("//button[contains(@data-action, 'cancel-confirm')]")).Click();
                Thread.Sleep(2000);

                Console.WriteLine($"Test case {nameof(TcB0602)} executed successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{nameof(TcB0602)}: {e.Message}");
                Console.WriteLine($"Test case {nameof(TcB0602)} failed");
            }
            finally
            {
                driver.Close();
                Console.WriteLine("==========================================================");
            }
        }

        /// <summary>
        /// Test case for Attempt to block a blocked user
        /// </summary>
        public void TcB0603()
        {
            IWebDriver driver = new ChromeDriver();
            RecoverBlockedUser(driver);

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

                Console.WriteLine($"Test case {nameof(TcB0603)} executed successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{nameof(TcB0603)}: {e.Message}");
                Console.WriteLine($"Test case {nameof(TcB0603)} failed");
            }
            finally
            {
                driver.Close();
                Console.WriteLine("==========================================================");
            }
        }

        /// <summary>
        /// Test case for Block user successfully
        /// </summary>
        public void TcB0604()
        {
            IWebDriver driver = new ChromeDriver();
            RecoverBlockedUser(driver);

            try
            {
                Console.WriteLine($"Test case {nameof(TcB0604)} started");

                driver.FindElement(By.Id("conversation-actions-menu-button")).Click();
                Thread.Sleep(2000);

                driver.FindElement(By.LinkText("Block user")).Click();
                Thread.Sleep(2000);

                driver.FindElement(By.XPath("//button[contains(@data-action, 'confirm-block')]")).Click();
                Thread.Sleep(2000);

                Console.WriteLine($"Test case {nameof(TcB0604)} executed successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{nameof(TcB0604)}: {e.Message}");
                Console.WriteLine($"Test case {nameof(TcB0604)} failed");
            }
            finally
            {
                driver.Close();
                Console.WriteLine("==========================================================");
            }
        }
    }
}
