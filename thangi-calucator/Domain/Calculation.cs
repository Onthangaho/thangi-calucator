using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace thangi_calucator
{
    public class Calculation
    {
        public int Id { get; }
        public double Left { get; }
        public double Right { get; }
        public OperationType Operation { get; }
        public double Result { get; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Calculation()
        {

        }
        public Calculation(
            double left,
            double right,
            OperationType operation,
            double result)
        {

            Left = left;
            Right = right;
            Operation = operation;
            Result = result;
            CreatedAt = DateTime.UtcNow;
        }
    }
}


