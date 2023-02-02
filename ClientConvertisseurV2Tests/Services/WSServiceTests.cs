using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientConvertisseurV2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientConvertisseurV2.Models;

namespace ClientConvertisseurV2.Services.Tests
{
    [TestClass()]
    public class WSServiceTests
    {
        [TestMethod()]
        public void WSServiceTest()
        {
            WSService wsService = new WSService("https://localhost:7056/api/");
            Assert.IsNotNull(wsService);
        }

        [TestMethod()]
        public void GetDevisesAsyncTest()
        {
            //Arrange
            WSService service = new WSService("https://localhost:7056/api/devises");
            var result = service.GetDevisesAsync("devises").Result;
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Devise>));
            Assert.AreEqual(3, result.Count());
        }
    }
}