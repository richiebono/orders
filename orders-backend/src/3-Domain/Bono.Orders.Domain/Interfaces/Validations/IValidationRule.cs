using System;
using System.Collections.Generic;
using System.Text;

namespace Bono.Orders.Domain.Interfaces.Validations
{
    public interface IValidationRule<in TEntity>
    {
        string ErrorMessage { get; }
        bool Valid(TEntity entity);
    }
}
