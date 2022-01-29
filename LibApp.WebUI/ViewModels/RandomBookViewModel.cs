using System.Collections.Generic;
using LibApp.Domain.Entities;

namespace LibApp.WebUI.ViewModels
{
    public class RandomBookViewModel
    {
        public Book Book { get; set; }
        public List<Customer> Customers { get; set; }
    }
}