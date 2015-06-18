using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCPizza.Controllers;
using System.Collections.Generic;

namespace MVCPizza.Tests.Controllers
{
    [TestClass]
    public class productsControllerTest
    {
        [TestMethod]
        public void SaveWith3Errors()
        {
            // Arrange
            productsController controller = new productsController();
            products testrecord = new products();
            testrecord.barcode = "Karl";
            testrecord.productcode = "";
            testrecord.productprice = 2;

            // Act
            ViewResult result = controller.Edit(testrecord, null) as ViewResult;

            // Assert
            Assert.IsTrue(result.ViewBag.MessageList.Contains("Pizza darf nicht weniger als 3 Euro kosten")
                             && result.ViewBag.MessageList.Contains("ProductCode darf nicht leer sein")
                             && result.ViewBag.MessageList.Contains("Barcode sollte kein a enthalten"));
        }

        [TestMethod]
        public void SaveWithEmptyProductCode()
        {
            // Arrange
            productsController controller = new productsController();
            products testrecord = new products();
            testrecord.barcode = "Hugo";
            testrecord.productcode = "";
            testrecord.productprice = 5;

            // Act
            ViewResult result = controller.Edit(testrecord, null) as ViewResult;

            // Assert
            Assert.IsTrue(result.ViewBag.MessageList.Contains("ProductCode darf nicht leer sein"));
        }

        [TestMethod]
        public void SaveWithWrongBarCode()
        {
            // Arrange
            productsController controller = new productsController();
            products testrecord = new products();
            testrecord.barcode = "Karl";
            testrecord.productcode = "";
            testrecord.productprice = 5;

            // Act
            ViewResult result = controller.Edit(testrecord, null) as ViewResult;

            // Assert
            Assert.IsTrue(result.ViewBag.MessageList.Contains("Barcode sollte kein a enthalten"));
        }

        [TestMethod]
        public void SaveWithLowPrice()
        {
            // Arrange
            productsController controller = new productsController();
            products testrecord = new products();
            testrecord.barcode = "Hugo";
            testrecord.productcode = "";
            testrecord.productprice = 2;

            // Act
            ViewResult result = controller.Edit(testrecord, null) as ViewResult;

            // Assert
            Assert.IsTrue(result.ViewBag.MessageList.Contains("Pizza darf nicht weniger als 3 Euro kosten")); 
        }

        [TestMethod]
        public void SaveOK()

        {
            // Arrange
            productsController controller = new productsController();
            products testrecord = new products();
            testrecord.barcode = "Hugo";
            testrecord.productcode = "PCode";
            testrecord.productprice = 5;

            //// Act
            //ViewResult result = controller.Edit(testrecord, null) as ViewResult;
            //// Assert
            //Assert.IsTrue(result.ViewBag.MessageList.Count == 0);

            // stattdessen:
            testrecord.OnAdd();
            List<string> resultList = testrecord.ErrorMessages;
            Assert.IsTrue(resultList.Count == 0);            

        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
