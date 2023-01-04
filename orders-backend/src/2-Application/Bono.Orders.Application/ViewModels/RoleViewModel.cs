using System;
using System.ComponentModel.DataAnnotations;


namespace Bono.Orders.Application.ViewModels
{
    public class RoleViewModel: EntityViewModel
    {
        [Required]        
        public string Name { get; set; }        
    }
}
