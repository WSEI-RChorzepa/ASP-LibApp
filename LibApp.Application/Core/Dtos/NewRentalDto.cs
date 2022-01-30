using System.Collections.Generic;

namespace LibApp.Application.Core.Dtos
{
    public class NewRentalDto
    {
        public int CustomerId { get; set; }
        public List<int> BookIds { get; set; }
    }
}
