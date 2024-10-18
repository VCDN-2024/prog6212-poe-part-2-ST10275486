using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AfriclaimMVC.Controllers;  
using AfriclaimMVC.Models;     
using System.IO;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace AfriclaimMVC.Tests
{
    public class AccountControllerTests
    {
        /***************************************************************************************
       *   Title: Pro C 7 with .NET and .NET Core
       *    Author: Andrew Troelsen; Philip Japikse
       *    Date: 2017
       *    Code version: Version 1
       *    Availability: Textbook/Ebook
       *
       ***************************************************************************************/
        [Fact]
        public void Login_Admin_RedirectsToHome()
        {
            
            var controller = new AccountController();  // Instantiates the AccountController

            // Create  dictionary to simulate session values
            var sessionValues = new Dictionary<string, string>();

           
            var mockHttpContext = new Mock<HttpContext>();
            var mockSession = new Mock<ISession>();

           
            mockSession.Setup(s => s.Set(It.IsAny<string>(), It.IsAny<byte[]>())) //Method to store session values in the dictionary
                .Callback<string, byte[]>((key, value) =>
                {
                    sessionValues[key] = System.Text.Encoding.UTF8.GetString(value);
                });

         
            mockSession.Setup(s => s.TryGetValue(It.IsAny<string>(), out It.Ref<byte[]>.IsAny)) //Method to retrieve session values from the dictionary
                .Returns((string key, out byte[] value) =>
                {
                    if (sessionValues.ContainsKey(key))
                    {
                        value = System.Text.Encoding.UTF8.GetBytes(sessionValues[key]);
                        return true;
                    }
                    value = null;
                    return false;
                });

           
            mockHttpContext.Setup(s => s.Session).Returns(mockSession.Object);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            var loginModel = new LoginViewModel // Create a LoginViewModel with admin email
            {
                Email = "admin@IIE.admin.za",
                Password = "password123"
            };

            // Act
            var result = controller.Login(loginModel) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
            Assert.True(sessionValues.ContainsKey("UserRole"));
            Assert.Equal("Admin", sessionValues["UserRole"]);
        }
    }
}


namespace AfriclaimMVC.Tests
{
    public class ClaimControllerTests
    {
        /***************************************************************************************
       *   Title: Pro C 7 with .NET and .NET Core
       *    Author: Andrew Troelsen; Philip Japikse
       *    Date: 2017
       *    Code version: Version 1
       *    Availability: Textbook/Ebook
       *
       ***************************************************************************************/
        [Fact]
        public void Create_ValidClaim_RedirectsToIndex()
        {
           
            var controller = new ClaimController();  // Instantiate the ClaimController
            var claimModel = new ClaimViewModel // Instantiate the ClaimViewModel
            {
                // Create a new ClaimViewModel with test data
                Name = "Jadin",
                Surname = "Naicker",
                Amount = 15,
                Module = "Prog6212"
            };

           
            var result = controller.Create(claimModel, null) as RedirectToActionResult;

          
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }
    }
}

namespace AfriclaimMVC.Tests
{
    public class TotalSalaryCalculatedTest
    {
        /***************************************************************************************
       *   Title: Pro C 7 with .NET and .NET Core
       *    Author: Andrew Troelsen; Philip Japikse
       *    Date: 2017
       *    Code version: Version 1
       *    Availability: Textbook/Ebook
       *
       ***************************************************************************************/
        [Fact]
        public void Create_ValidClaim_CalculatesTotalSalaryCorrectly()
        {
            
            ClaimController.claims.Clear(); 
            var controller = new ClaimController();   // Instantiate the ClaimController
            var claimModel = new ClaimViewModel   // Instantiate the ClaimViewModel
            {
                // Create a new ClaimViewModel with test data
                Name = "Jadin",
                Surname = "Naicker",
                Amount = 15,
                Module = "Prog6212"
            };

           
            controller.Create(claimModel, null); // No file provided

           
            var addedClaim = ClaimController.claims.FirstOrDefault();
            Assert.NotNull(addedClaim); // Ensure the claim was added
            Assert.Equal(15 * 130.00m, addedClaim.TotalSalary); // 15 hours * R130 rate = total salary
        }
    }
}
