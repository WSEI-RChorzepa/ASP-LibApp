using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibApp.WebUI.Controllers
{
    public class BaseController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        protected IMediator Mediator => _mediator;

        protected IMapper Mapper => _mapper;

        public BaseController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
    }
}
