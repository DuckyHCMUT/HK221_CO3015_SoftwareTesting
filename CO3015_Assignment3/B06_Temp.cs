using System.Configuration;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OfficeOpenXml;

namespace CO3015_Assignment3;

public class B06Temp
{
    private readonly string? _userName = ConfigurationManager.AppSettings["username"];
    private readonly string? _password = ConfigurationManager.AppSettings["password"];
    private readonly string? _projectDirectory;

    public B06Temp()
    {
        // Define License Context for EPPlus package
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        _projectDirectory = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;

        if (string.IsNullOrEmpty(_projectDirectory))
        {
            throw new DirectoryNotFoundException($"{_projectDirectory} not found!");
        }
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

    private void RecoverBlockedUser(IWebDriver driver, string tcNum)
    {
        var targetId = GetCellDataFromExcelFile("B06_data", "B3");

        if (string.IsNullOrEmpty(targetId))
        {
            throw new NullReferenceException($"Unknown target Id {targetId}");
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

        var requestUnblockBtnElement = driver.FindElement(By.XPath("//button[contains(@data-action, 'request-unblock')]"));

        if (requestUnblockBtnElement.Enabled)
        {
            requestUnblockBtnElement.Click();
            Thread.Sleep(2000);
        }
        else
        {
            return;
        }

        driver.FindElement(By.XPath("//button[contains(@data-action, 'confirm-unblock')]")).Click();
        Thread.Sleep(2000);

        Console.WriteLine($"Restore completed, test case {tcNum} is starting soon...");
    }

    public void TcB0601()
    {
        try
        {
            var targetId = GetCellDataFromExcelFile("B06_data", "B2");

            if (string.IsNullOrEmpty(targetId))
            {
                throw new NullReferenceException($"Unknown target Id {targetId}");
            }

            Console.WriteLine($"Test case {nameof(TcB0601)} started");

            IWebDriver driver = new ChromeDriver();
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
            Console.WriteLine("==========================================================");
        }
    }

    public void TcB0602()
    {
        IWebDriver driver = new ChromeDriver();

        try
        {
            RecoverBlockedUser(driver, nameof(TcB0602));

            var targetId = GetCellDataFromExcelFile("B06_data.xlsx", "B2");

            if (string.IsNullOrEmpty(targetId))
            {
                throw new NullReferenceException($"Unknown target Id {targetId}");
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
}