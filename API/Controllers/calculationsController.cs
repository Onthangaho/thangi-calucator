using thangi_calucator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using API.Persistence;
using thangi_calucator.Persistence;



namespace API.controllers
{
    [ApiController]
    [Route("api/calculations")]
    public class CalculationsController : ControllerBase
    {
        private readonly CalculatorService _calculator;
        private readonly ICalculationStore _calculatorStore;

        public CalculationsController(CalculatorService calculator, ICalculationStore calculatorStore)
        {
            _calculator = calculator;
            _calculatorStore = calculatorStore;
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
            await _calculatorStore.SaveAsync(calculation);

            var response = new CalculationResultDto
            {
                Result = calculation.Result,
                Operation = calculation.Operation.ToString()
            };

            return Ok(response);

        }









    }

}