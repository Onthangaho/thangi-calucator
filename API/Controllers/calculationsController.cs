using thangi_calucator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace APi.controllers
{
    

    [ApiController]
    [Route("api/calculations")]
    public class CalculationsController: ControllerBase
    {
        
        private readonly CalculatorService _calculator;

        //registering the CalculatorService via Dependency Injection so that it can be used in this controller

        public CalculationsController(CalculatorService calculator)
        {
            _calculator=calculator;
        }

        [HttpGet]//GET /api/calculations
        public async Task<IActionResult> GetAll()
        {
            var calculations = await _calculator.GetAllAsync();

            return Ok(calculations);
        }
        [HttpPost]
        public async Task<IActionResult> Calculate(CalculationRequest request)
        {
            
            var results = await _calculator.CalculateAsync(request);
            return Ok(results);
        }
    }
}
