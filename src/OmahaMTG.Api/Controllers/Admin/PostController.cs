using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OmahaMTG.Data;
using OmahaMTG.AdminContentHandlers.Post;
namespace OmahaMTG.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Default
        [HttpGet]
        public async Task<ActionResult<SkipTakeSet<Model>>> Get([FromQuery]Query.Command getRequest)
        {
            return await _mediator.Send(getRequest);
        }

        //// GET: api/Default/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Model>> Get(int id)
        //{
        //    return await _contentManager.GetResource(id);
        //}

        // POST: api/Default
        [HttpPost]
        public async Task<ActionResult<Model>> Post([FromBody] Create.Command createRequest)
        {
            return await _mediator.Send(createRequest);
        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Model>> Put(int id, [FromBody] Update.Command updateRequest)
        {
            updateRequest.Id = id;
            return await _mediator.Send(updateRequest);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, [FromQuery]bool perm)
        {
            await _mediator.Send(new Delete.Command(){Id = id, Perm = perm});
            return Ok();
        }
    }
}
