namespace TicketingSystem.FAQ.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ResolveReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ResolveReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpPost]
        //public async Task<ResponseDTO> PostResolveReport([FromForm] PostResolveReportCommand command) => await _mediator.Send(command);

        //[HttpPut]
        //[Route("PutResolveReport/{id}")]
        //public async Task<ResponseDTO> PutResolveReport([FromForm] PutResolveReportCommand command, Guid id) 
        //{
        //    command.Id = id;
            
        //    return await _mediator.Send(command);
        //}
        [HttpDelete]
        [Route("DeleteResolveReport/{id}")]
        public async Task<ResponseDTO> DeleteResolveReport(Guid id)
        {
            return await _mediator.Send(new DeleteResolveReportCommand {Id = id});
        }
    }
}
