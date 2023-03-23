using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Security.Cryptography.X509Certificates;
//using OpenQA.Selenium.Interactions;
using System.Xml;
using AutoItX3Lib;

namespace SmartRetail
{
    public class Tests
    {
        WebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void Test()
        {
            driver.Url = "http://localhost/smartretail/#/login";
            driver.Manage().Window.Maximize();

            //title command
            String Title = driver.Title;
            Console.WriteLine(Title);
            //URL command
            String PageUrl = driver.Url;
            Console.WriteLine(PageUrl);


            login();




            //try
            //{
            //    if (driver.FindElement(By.XPath("/html/body/div[1]/div/div/form/div[3]/button[2]")).Displayed)
            //    {
            //        driver.FindElement(By.XPath("/html/body/div[1]/div/div/form/div[3]/button[2]")).Click();
            //    }
            //}
            //catch (NoSuchElementException)
            //{
            //    Console.WriteLine("in catch block");
            //}

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);



            //explicit wait
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            By waitEleBy = By.XPath("//i[@class='fa fa-bars N_P_A']");
            //IWebElement firstResult = wait.Until(e => e.FindElement(waitEleBy));
            wait.Until(ExpectedConditions.ElementExists(waitEleBy));

            //validation assert

            IWebElement dash = driver.FindElement(By.XPath("//span[text()='Dashboard']"));
            Assert.IsTrue(dash.Displayed);

           // supplier();
            HostProcess();


            //sidenav

            //driver.FindElement(By.XPath("//i[@class='fa fa-bars N_P_A']")).Click();
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            //driver.FindElement(By.XPath("//span[text()='Item Management']")).Click();
            //driver.FindElement(By.XPath("//span[text()='Item Maintenance']")).Click();

            //NewItem();
            //deleteBarcode();
            //NewItem();
            //deleteBarcode();





        }
        public void login()
        {

            //By Notepad

            //try
            //{
            //    var filepath = @"C:\Users\ajal.jose\Desktop\error.txt";
            //    string[] cred = File.ReadAllText(filepath).Split(',');

            //    var name = driver.FindElement(By.Id("userNameText"));
            //    name.Click();
            //    name.SendKeys(cred[0]);
            //    driver.FindElement(By.Name("password")).SendKeys(cred[1]);
            //}
            //catch (FileNotFoundException)

            //{
            //    Console.WriteLine("file not Existing");

            //}




            //By JSON

            string text = File.ReadAllText(@"./data.json");
            var data = JsonConvert.DeserializeObject<JsonModel>(text);

            var name = driver.FindElement(By.Id("userNameText"));
            name.Click();
            name.SendKeys(data.username);
            driver.FindElement(By.Name("password")).SendKeys(data.password);

            //By XML
            //XmlDocument xdoc = new XmlDocument();
            //xdoc.Load(@"C:\Users\ajal.jose\source\repos\SmartRetail\cred.xml");
            //XmlElement root = xdoc.DocumentElement;
            //XmlNodeList xmlNodeList = root.GetElementsByTagName("username");
            //XmlNodeList pass = root.GetElementsByTagName("password");
            //string s = xmlNodeList.Item(0).InnerText;
            //string p = pass.Item(0).InnerText;


            //IWebElement name = driver.FindElement(By.Id("userNameText"));
            //name.Click();
            //name.SendKeys(s);
            //driver.FindElement(By.Name("password")).SendKeys(p);


            //foreach (XmlNode node in nodes)
            //{
            //    XmlNode username = node.SelectSingleNode("username");
            //    XmlNode password = node.SelectSingleNode("password");

            //    IWebElement name = driver.FindElement(By.Id("userNameText"));
            //    name.Click();
            //    name.SendKeys(username.InnerText);
            //    driver.FindElement(By.Name("password")).SendKeys(password.InnerText);
            //}


            // button
            driver.FindElement(By.TagName("button")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
        }
        public void NewItem()
        {
            //Json

            string text = File.ReadAllText(@"./data.json");
            var data = JsonConvert.DeserializeObject<JsonModel>(text);

            //new Pop up window
            driver.FindElement(By.XPath("//span[text()='New']")).Click();

            //dropdown
            SelectElement barcodetype = new SelectElement(driver.FindElement(By.XPath("//select[@name='barCodeTypeId']")));
            barcodetype.SelectByIndex(1);



            //textBox barcode
            driver.FindElement(By.XPath("//input[@name='barcode']")).SendKeys(data.barcode);
            //Thread.Sleep(1000);
            //text description
            driver.FindElement(By.XPath("//input[@name='itemDescription']")).SendKeys(data.desc);
            // Thread.Sleep(1000);
            //checkbox
            IWebElement chkbx = driver.FindElement(By.XPath("/html/body/div[1]/div/div/form/div[1]/div[1]/gm-create-item-identifier/ng-form/div[2]/div[3]/div/input"));
            chkbx.Click();
            bool selected = chkbx.Selected; //selected tag
            Console.WriteLine("checkbox after click: " + selected);



            //Department

            IWebElement dept = driver.FindElement(By.XPath("//span[text()='Department']//following::input[1]")).Displayed ?
                driver.FindElement(By.XPath("//span[text()='Department']//following::input[1]")) :
                driver.FindElement(By.XPath("/html/body/div[1]/div/div/form/div[1]/div[2]/div[1]/item-categorisation/ng-form/div/div[1]/div/div[2]/div/gm-single-selectize/div/div[1]/div"));
            Console.WriteLine(dept.GetAttribute("data-value"));
            //IWebElement dept = driver.FindElement(By.XPath("//span[text()='Department']//following::input[1]"));
            if (dept.GetAttribute("data-value") == null)
            {
                //IWebElement dept = driver.FindElement(By.XPath("//span[text()='Department']//following::input[1]"));
                dept.Click();
                dept.SendKeys(data.dept);
                dept.SendKeys(Keys.Enter);
                //Console.WriteLine("After dept");
            }

            //Subdepartment
            IWebElement sub = driver.FindElement(By.XPath("//span[text()='Sub Department']//following::input[1]")).Displayed ?
                driver.FindElement(By.XPath("//span[text()='Sub Department']//following::input[1]")) :
                driver.FindElement(By.XPath("/html/body/div[1]/div/div/form/div[1]/div[2]/div[1]/item-categorisation/ng-form/div/div[2]/div/div[2]/div/gm-single-selectize/div/div[1]/div"));
            if (sub.GetAttribute("data-value") == null)
            {
                sub.Click();
                sub.SendKeys(data.subdept);
                sub.SendKeys(Keys.Enter);
                //Console.WriteLine("After subdept");
            }

            //comodity
            IWebElement Com = driver.FindElement(By.XPath("//span[text()='Commodity']//following::input[1]")).Displayed ?
            driver.FindElement(By.XPath("//span[text()='Commodity']//following::input[1]")) :
            driver.FindElement(By.XPath("/html/body/div[1]/div/div/form/div[1]/div[2]/div[1]/item-categorisation/ng-form/div/div[3]/div/div[2]/div/gm-single-selectize/div/div[1]/div"));
            if (Com.GetAttribute("data-value") == null)
            {
                Com.Click();
                Com.SendKeys(data.comodity);
                Com.SendKeys(Keys.Enter);
                // Console.WriteLine("After dept");
            }
            //family
            IWebElement fam = driver.FindElement(By.XPath("//span[text()='Family']//following::input[1]")).Displayed ?
            driver.FindElement(By.XPath("//span[text()='Family']//following::input[1]")) :
            driver.FindElement(By.XPath("/html/body/div[1]/div/div/form/div[1]/div[2]/div[1]/item-categorisation/ng-form/div/div[5]/div/div[2]/div/gm-single-selectize/div/div[1]/div"));
            if (fam.GetAttribute("data-value") == null)
            {
                fam.Click();
                fam.SendKeys(data.family);
                fam.SendKeys(Keys.Enter);
            }

            //dropdown supplier
            IWebElement supplier = driver.FindElement(By.XPath("(//span[text()='Supplier'])[2]//following::input[1]"));
            supplier.Click();
            supplier.SendKeys(data.supplier);
            supplier.SendKeys(Keys.Enter);
            // Thread.Sleep(1000);



            //order code -- valiadation check
            /*            try
                        {
                            int val = 155;
                            IWebElement order = driver.FindElement(By.XPath("//input[@name='orderCode']"));
                            order.SendKeys(val.ToString());
                            order.Click();
                            Thread.Sleep(2000);
                            while (driver.FindElement(By.XPath("//span[@class='tooltip-info ng-binding ng-scope']")).Displayed)
                            {

                                val = val + 1;
                                order.Clear();
                                order.SendKeys(val.ToString());
                                Thread.Sleep(2000);
                                order.Click();
                            }
                        }
                        catch (NoSuchElementException)
                        {
                            Console.WriteLine("validation check catch block");
                        }*/
            //gst
            SelectElement gst = new SelectElement(driver.FindElement(By.XPath("//select[@name='gstCodeSelector']")));
            gst.SelectByIndex(3);
            //  Thread.Sleep(1000);

            // checking gst value disabled or not

            IWebElement gstvalue = driver.FindElement(By.XPath("//input[@name='gstRateSelector']"));
            bool enabled = gstvalue.Enabled;
            Console.WriteLine("GST value enabled:" + enabled);

            //carbon cost

            IWebElement cost = driver.FindElement(By.XPath("//*[@id=\"cost\"]")).Displayed ?
            driver.FindElement(By.XPath("//*[@id=\"cost\"]")) : driver.FindElement(By.XPath("//*[@id=\"costEx\"]"));
            // displayed or not
            bool status = cost.Displayed;
            Console.WriteLine("Cost is displayed:" + status);
            cost.Click();
            cost.SendKeys(data.cost);
            // Thread.Sleep(1000);
            driver.FindElement(By.XPath("//input[@name='unitPricingSize']")).SendKeys("5");
            //Thread.Sleep(1000);

            //dropdown - unit of measure
            SelectElement unit = new SelectElement(driver.FindElement(By.XPath("//select[@name='unitPricingMeasure']")));
            unit.SelectByIndex(6);
            //toggle
            driver.FindElement(By.XPath("//span[@class='slider round']")).Click();
            Thread.Sleep(1000);

            //submit button

            IWebElement submit = driver.FindElement(By.XPath("//button[@class='button-save']"));
            submit.Submit();
            //Thread.Sleep(6000);


        }
        public void deleteBarcode()
        {

            WebDriverWait waits = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            By waitElemBy = By.XPath("(//button[@class='dropdown-toggle ItemMenulist'])[1]");

            waits.Until(ExpectedConditions.ElementExists(waitElemBy));


            //deleting barcode 

            IWebElement bar = driver.FindElement(By.XPath("(//button[@class='dropdown-toggle ItemMenulist'])[1]"));
            bar.Click();
            //Thread.Sleep(1000);
            IWebElement barcde = driver.FindElement(By.XPath("(//span[text()='Barcodes'])[1]"));
            barcde.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            // Thread.Sleep(3000);

            //hover

            IWebElement delete = driver.FindElement(By.XPath("//*[@id=\"gridBARCODES\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr[1]/td[8]/div"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(delete).Perform();

            IWebElement dlt = driver.FindElement(By.XPath("//*[@id=\"gridBARCODES\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr[1]/td[8]/div/div/div[2]/i"));
            dlt.Click();
            //Thread.Sleep(2000);
            driver.FindElement(By.XPath("//button[@id='submitButton']")).Click();
            //Thread.Sleep(3000);

            // save

            IWebElement save = driver.FindElement(By.XPath("//button[@class='button-save ng-scope']"));
            save.Click();
            Thread.Sleep(6000);
            IWebElement tab = driver.FindElement(By.XPath("(//i[@class='fi flaticon-delete'])[3]"));
            tab.Click();
        }

        public void supplier()
        {
            driver.FindElement(By.XPath("//i[@class='fa fa-bars N_P_A']")).Click();
            IWebElement search = driver.FindElement(By.XPath("//input[@id='search-text']"));
            search.SendKeys("Supplier Maintenance");
            search.SendKeys(Keys.Enter);

            //new popup
            driver.FindElement(By.XPath("//span[text()='New']")).Click();

            //text box
            IWebElement code = driver.FindElement(By.XPath("//span[text()='Supplier Code']//following::input[1]"));
            code.Click();
            code.SendKeys("total");


            IWebElement sname = driver.FindElement(By.XPath("//span[text()='Supplier Name']//following::input[1]"));
            sname.Click();
            sname.SendKeys("TOTAL ");

            //checkbox
            IWebElement down = driver.FindElement(By.XPath("//button[@class='dropdown-toggle inline-error ng-binding btn btn-default']"));
            down.Click();
            driver.FindElement(By.XPath("//input[@class='checkboxInput']")).Click();
            Thread.Sleep(1000);

            IWebElement save = driver.FindElement(By.XPath("//button[@class='button-save']"));
            save.Click();

            //next page
            IWebElement chk = driver.FindElement(By.XPath("//span[text()='Hosted']//preceding::input[1]"));
            chk.Click();

            IWebElement config = driver.FindElement(By.XPath("//span[text()='Configure']"));
            config.Click();

            //next window

            SelectElement hosttype = new SelectElement(driver.FindElement(By.XPath("//select[@id=\"hostType\"]")));
            hosttype.SelectByIndex(0);

            SelectElement version = new SelectElement(driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/form/div[1]/div[2]/div/div[2]/div/select")));
            version.SelectByIndex(6);

            SelectElement host = new SelectElement(driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/form/div[1]/div[3]/div/div[2]/div/select")));
            host.SelectByIndex(4);

            //configure
            driver.FindElement(By.XPath("//span[text()='Configure']")).Click();

            //next window

            driver.FindElement(By.XPath("/html/body/div[1]/div/div/form/div[2]/div[2]/div[3]/a/span")).Click();
            Thread.Sleep(1000);
            //next window

            driver.FindElement(By.XPath("(//span[text()='Save'])[1]")).Click();
            Thread.Sleep(1000);
            //back window
            driver.FindElement(By.XPath("(//span[text()='Save'])[1]")).Click();
            //backwindow

            IWebElement hostid = driver.FindElement(By.XPath("//input[@name='hostID']"));
            hostid.SendKeys("3633");
            IWebElement offer = driver.FindElement(By.XPath("//input[@name='offerSpacing']"));
            offer.SendKeys("10000");
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/form/div[2]/button[2]/span")).Click();

        }

        void HostProcess()
        {
            //Host Processing
            driver.FindElement(By.XPath("//i[@class='fa fa-bars N_P_A']")).Click();
            IWebElement search1 = driver.FindElement(By.XPath("//input[@id='search-text']"));
            search1.SendKeys("host processing");
            search1.SendKeys(Keys.Enter);

            IWebElement option = driver.FindElement(By.XPath("//span[text()='Options']"));
            option.Click();

            IWebElement collect = driver.FindElement(By.XPath("/html/body/div[1]/div/span/div[2]/div[1]/div/div/gm-appbar/div/div[2]/div[2]/div/div[1]/div/div/ul/li/ul/li[1]/collect-host/span/a"));
            collect.Click();

            //hover

            IWebElement options = driver.FindElement(By.XPath("//*[@id=\"grid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr[2]/td[4]/div"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(options).Perform();


            IWebElement upload = driver.FindElement(By.XPath("(//i[@uib-tooltip='Upload'])[2]"));
            upload.Click();
            //string path = @"C:\Users\ajal.jose\Desktop\cred.txt";
            //IWebElement uploadElement = driver.FindElement(By.XPath("(//i[@uib-tooltip='Upload'])[2]"));
            //uploadElement.SendKeys(path);
            AutoItX3 auto = new AutoItX3();
           // auto.WinActivate("file upload");
            auto.Send(@"C:\Users\ajal.jose\Desktop\cred.txt");
            Thread.Sleep(1000);
            auto.Send("{ENTER}");

        }

        
    }
    //[TearDown]
    //public void TearDown()
    //{

    //    // driver.Close();
    //}
}











//   
//}

