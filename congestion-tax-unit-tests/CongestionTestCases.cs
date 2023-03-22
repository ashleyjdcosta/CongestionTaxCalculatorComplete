using System;
using congestion.calculator;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace congestion_tax_unit_tests
{
    
    [TestClass]
    public class CongestionTestCases
    {        
        private CongestionTaxCalculator1 calculator;

        [TestInitialize]
        public void Initialize()
        {
            calculator = new CongestionTaxCalculator1();            
        }

        [TestMethod]
        public void GetTax_ShouldReturnCorrectTaxAmount()
        {
            // Arrange
            var vehicle = new Car();
            var dates = new[]
            {
                new DateTime(2013, 04, 02, 7, 30, 0),
                new DateTime(2013, 04, 02, 8, 30, 0),
                new DateTime(2013, 04, 02, 14, 0, 0)
            };

            // Act
            var result = calculator.GetTax(vehicle, dates);

            // Assert
            Assert.AreEqual(26, result);
        }

        [TestMethod]
        public void GetTax_ShouldReturnZeroForTollFreeVehicle()
        {
            // Arrange
            var vehicle = new Motorbike();
            var dates = new[]
            {
                new DateTime(2013, 06, 05, 7, 30, 0),
                new DateTime(2013, 06, 05, 8, 30, 0),
                new DateTime(2013, 06, 05, 14, 0, 0)
            };

            // Act
            var result = calculator.GetTax(vehicle, dates);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetTax_ShouldReturnMaxDailyFeeIfExceeded()
        {
            // Arrange
            var vehicle = new Car();
            var dates = new[]
            {
                new DateTime(2013, 04, 02, 7, 30, 0),
                new DateTime(2013, 04, 02, 8, 32, 0),
                new DateTime(2013, 04, 02, 10, 30, 0),
                new DateTime(2013, 04, 02, 11, 32, 0),
                new DateTime(2013, 04, 02, 14, 0, 0),
                new DateTime(2013, 04, 02, 15, 2, 0),
                new DateTime(2013, 04, 02, 16, 5, 0),
                new DateTime(2013, 04, 02, 17, 0, 0)
            };

            // Act
            var result = calculator.GetTax(vehicle, dates);

            // Assert
            Assert.AreEqual(60, result);
        }

        [TestMethod]
        public void IsTollFreeVehicle_ShouldReturnTrueForTollFreeVehicle()
        {
            // Arrange
            var vehicle = new Motorbike();

            // Act
            var result = calculator.IsTollFreeVehicle(vehicle);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsTollFreeVehicle_ShouldReturnFalseForNonTollFreeVehicle()
        {
            // Arrange
            var vehicle = new Car();

            // Act
            var result = calculator.IsTollFreeVehicle(vehicle);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
