using System;
using System.ComponentModel.DataAnnotations;


namespace Bono.Orders.Application.ViewModels
{
    public class OrderViewModel: EntityViewModel
    {        
        [Required]
        
        public string CustomerName { get; set; }
        
        public string OrderTypeName { get; set; }

        public string OrderTypeId { get; set; }
        
        public string UserId { get; set; }
        public string UserName { get; internal set; }
    }
}
