using System;
using System.Collections.Generic;
using System.Text;
using Bono.Orders.Domain.Models;

namespace Bono.Orders.Application.ViewModels
{
    public class UserRoleViewModel : EntityViewModel
    {
        public UserViewModel User { get; private set; }
        public RoleViewModel Role { get; private set; }
    }
}
