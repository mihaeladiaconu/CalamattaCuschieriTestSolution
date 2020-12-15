using ChatApi.Models;
using ChatApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers
{
    /// <summary>
    /// Chat API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public ChatController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        /// <summary>
        /// Sends a message to an available agent
        /// </summary>
        /// <param name="chatModel">Input chat message</param>
        /// <response code="200">Message was sent successfully</response>  
        /// <response code="400">Invalid message</response>  
        /// <response code="422">No agents available to process the message</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult Send([FromBody]ChatModel chatModel)
        {
            if (chatModel == null || string.IsNullOrEmpty(chatModel.Message))
                return new BadRequestObjectResult("message is required");

            if (_publisherService.Publish(chatModel.Message))
                return new OkResult();

            return new UnprocessableEntityObjectResult("No agents are available to process your message");
        }
    }
}
