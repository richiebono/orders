﻿using Bono.Orders.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Bono.Orders.Application.Interfaces
{
    public interface IService<TEntity> where TEntity : class
    {
        ValidationResult Post(TEntity entity);
        ValidationResult Put(TEntity entity);
        ValidationResult Delete(string Id);
        IEnumerable<TEntity> GetAll();
        TEntity GetById(string id);
    }
}
