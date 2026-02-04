using System.ComponentModel.DataAnnotations;
using thangi_calucator;

public class CreateCalculationDto
{

    [Required]
    public double left { get; set; }

    [Required]
    public double right { get; set; }

    [Required]
    public OperationType Operand { get; set; }
}