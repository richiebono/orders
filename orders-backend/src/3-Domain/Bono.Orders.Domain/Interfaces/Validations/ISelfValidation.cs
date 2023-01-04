using Bono.Orders.Domain.Validations;

namespace Bono.Orders.Domain.Interfaces.Validations
{
    public interface ISelfValidation
    {
        ValidationResult ValidationResult { get; }
        bool IsValid { get; }
    }
}
