using congestion.calculator;
using congestion.calculator.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace congestion_tax_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxCongestionCalculatorController : ControllerBase
    {

        public class TaxCalculationRequest
        {
            public string VehicleValue { get; set; }
            public DateTime[] Dates { get; set; }
        }

        [HttpPost]
        public IActionResult CalculateTax([FromBody] TaxCalculationRequest request)
        {
            string json = System.IO.File.ReadAllText("appsettings.json");
            CongestionData congestionData = JsonConvert.DeserializeObject<CongestionData>(json);
            var calculator = new CongestionTaxCalculator1();
            Vehicle Vehicle = null;
            foreach (var toll_free_vehicle in congestionData.toll_free_vehicles)
            {
                if (request.VehicleValue.ToLower() == toll_free_vehicle)
                {
                    string messageFree = "Tax for your vehicle is free.";
                    return Ok(messageFree);
                }
            }
            if (request.VehicleValue.ToLower() == "car")
            {
                Vehicle = new Car();
            }
            else
            {
                return BadRequest("Invalid vehicle type.");
            }
            int tax = calculator.GetTax(Vehicle, request.Dates);
            string message = $"The Total Tax for your {request.VehicleValue} will be SEK {tax}.";
            return Ok(message);
        }
    }
}
