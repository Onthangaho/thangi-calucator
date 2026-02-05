using System.ComponentModel.DataAnnotations;
using thangi_calucator;

public class CreateCalculationDto
{

    [Required]
    public double Left { get; set; }

    [Required]
    public double Right { get; set; }

    [Required]
    public OperationType Operand { get; set; }
}