using thangi_calucator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;



namespace API.controllers
{
    [ApiController]
    [Route("api/calculations")]
    public class CalculationsController : ControllerBase
    {
        private readonly CalculatorService _calculator;

        public CalculationsController(CalculatorService calculator)
        {
            _calculator = calculator;
        }

        /*  [HttpGet] //GET /api/calculations
          public async Task<IActionResult> GetAll()
          {
              var calculations = await _calculator.GetAllAsync();
              return Ok(calculations);
          }
         */
        [HttpPost] //POST /api/calculations
        public async Task<IActionResult> Calculate([FromBody] CreateCalculationDto dto)
        {
            var request = new CalculationRequest(
                dto.Left,
                dto.Right,
                dto.Operand
                );
            var calculation = await _calculator.CalculateAsync(request);

            var response = new CalculationResultDto
            {
                Result = calculation.Result,
                Operation = calculation.Operation.ToString()
            };

            return Ok(response);

        }









    }

}