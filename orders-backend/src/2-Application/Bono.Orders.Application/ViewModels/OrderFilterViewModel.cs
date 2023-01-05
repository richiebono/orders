using System;
using System.ComponentModel.DataAnnotations;


namespace Bono.Orders.Application.ViewModels
{
    public class OrderFilterViewModel
    {        
        public string sort { get; set; }
        public string order { get; set; }
        public string filter { get; set; }
        public int start { get; set; }
        public int size { get; set; }
        
    }
}
