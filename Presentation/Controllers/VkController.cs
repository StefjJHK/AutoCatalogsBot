using System;
using System.Threading.Tasks;
using MediatR;
using VkNet.Model;
using VkNet.Utils;
using VkNet.Model.GroupUpdate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.ParametrObjects;
using Presentation.Application.Commands.Api;
using BusinessLogic.Services.Commands;
using BusinessLogic.ParamterObjects.Service.Commands;
using BusinessLogic.DTO;

namespace Presentation.Controllers
{
    [Route("api/vk")]
    [ApiController]
    public class VkController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<VkController> _logger;

        private readonly VkParams _vkSettings;
        
        public VkController(IMediator mediator, VkParams vkSettings, ILogger<VkController> logger)
        {
            _mediator = mediator ?? 
                throw new ArgumentNullException(nameof(mediator));        
            _vkSettings = vkSettings ??
                throw new ArgumentNullException(nameof(vkSettings));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("callback")]
        public async Task<IActionResult> CallbackAsync([FromBody] RequestParams request)
        {
            _logger.LogInformation("{RequestType} request accepted from id={Id}\n{RequestObject}", 
                request.Type, request.Id, request.Object);

            if (request.Secret != _vkSettings.Secret)
            {
                _logger.LogInformation("{RequestType} for {id} was cancelled. Secret key incorrect.", 
                    request.Type, request.Id);

                return Forbid();
            }

            if (request.Type == "confirmation")
            {
                var confirmRequest = new ConfirmCommand();
                var answer = await _mediator.Send(confirmRequest);

                return Ok(answer);
            }

            if (request.Type == "message_new")
            {
                var message = Message.FromJson(new VkResponse(request.Object));
                var command = new ProcessUserRequestCommand(message);

                _mediator.Send(command);

                return Ok("ok");
            }

            if(request.Type == "wall_post_new")
            {
                var post = WallPost.FromJson(new VkResponse(request.Object));
                var command = new UpdatedCatalogCommand(post);

                _mediator.Send(command);

                return Ok("ok");
            }
            
            return BadRequest($"Request { request.Type } did not recognized");
        }
    }
}