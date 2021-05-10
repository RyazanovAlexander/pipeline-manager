using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;
using Worker.PipelineAgent.Services;

namespace Worker.PipelineAgent.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PipelineController : ControllerBase
    {
        private readonly IPipelineExecutor _pipelineExecutor;
        private readonly ILogger<PipelineController> _logger;

        public PipelineController(
            IPipelineExecutor pipelineExecutor,
            ILogger<PipelineController> logger)
        {
            _pipelineExecutor = pipelineExecutor;
            _logger = logger;
        }

        /// <summary>
        /// Executes the pipeline in the given worker.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/v1/Pipeline
        ///     {
        ///       "commands": [
        ///         {
        ///           "executorName": "test",
        ///           "commandLine": "echo 1"
        ///         }
        ///       ]
        ///     }
        ///
        /// </remarks>
        /// <param name="pipeline"></param>
        /// <returns>Pipeline execution result</returns>
        /// <response code="200">Returns if the pipeline successfully completed</response>
        /// <response code="400">If the pipeline is incorrect</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(PipelineExecutionResult), (int)HttpStatusCode.OK)]
        public ActionResult<PipelineExecutionResult> Execute([FromBody] Pipeline pipeline)
        {
            if (pipeline.Commands == null || !pipeline.Commands.Any())
            {
                return BadRequest("Need to pass at least one command");
            }

            return _pipelineExecutor.Execute(pipeline);
        }
    }
}
