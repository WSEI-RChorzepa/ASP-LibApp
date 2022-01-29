using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace LibApp.WebUI.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private readonly IMapper _mapper;

        protected IMapper Mapper => _mapper;

        public BaseApiController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
