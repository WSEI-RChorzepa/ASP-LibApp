using LibApp.Application.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApp.WebUI.ViewModels
{
    public class RentalsViewModel
    {
        public IReadOnlyCollection<CustomerDto> Customers { get; set; }
        public IReadOnlyCollection<BookDto> Books { get; set; }
    }
}
