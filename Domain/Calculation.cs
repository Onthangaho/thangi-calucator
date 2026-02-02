using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

/// <summary>
/// Owns calculator behaviour and internal state.
/// 
/// This class:
/// - Performs calculations
/// - Applies business rules
/// - Maintains history
/// 
/// Booking analogy:
/// similar to a booking logic / rules component.
/// </summary>
public class Calculation
{
       public Guid Id { get; }
        public double Left { get; }
        public double Right { get; }
        public OperationType Operation { get; }
        public double Result { get; }
        public DateTime CreatedAt { get; }

        public Calculation(
            double left,
            double right,
            OperationType operation,
            double result)
        {
            Id = Guid.NewGuid();
            Left = left;
            Right = right;
            Operation = operation;
            Result = result;
            CreatedAt = DateTime.UtcNow;
        }
    }
   
    
