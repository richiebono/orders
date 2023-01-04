using Bono.Orders.Domain.Validations;

namespace Bono.Orders.Domain.Interfaces.Validations
{
    public interface IValidation<in TEntity>
    {
        ValidationResult Valid(TEntity entity);
    }
}
