using Application.MediatR.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SteamCardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SteamCardController(IMediator mediator) => _mediator = mediator;

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    return Ok(await _repository.GetAll());
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var command = new GetSteamCardCommand(id);
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(RegisterSteamCardCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        //[HttpPut]
        //public async Task<IActionResult> Put(AlteraPessoaCommand command)
        //{
        //    var response = await _mediator.Send(command);
        //    return Ok(response);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var obj = new ExcluiPessoaCommand { Id = id };
        //    var result = await _mediator.Send(obj);
        //    return Ok(result);
        //}
    }
}
