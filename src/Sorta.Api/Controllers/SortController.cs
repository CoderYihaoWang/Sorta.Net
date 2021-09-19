using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sorta.Abstractions;

namespace Sorta.Api.Controllers
{
    [ApiController]
    [Route("/api")]
    public class SortController : ControllerBase
    {
        private readonly IEnumerable<ISort> _sorts;
        private readonly ILogger<SortController> _logger;

        public SortController(IEnumerable<ISort> sorts, ILogger<SortController> logger)
        {
            _sorts = sorts;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _sorts.Select(s => s.Algorithm);
        }

        [HttpGet("{algorithm}")]
        public ActionResult<SortStats> Sort([FromRoute] string algorithm, 
            [FromQuery] string data)
        {
            var sort = _sorts.FirstOrDefault(s => s.Algorithm.Equals(algorithm, StringComparison.OrdinalIgnoreCase));

            if (sort is null)
            {
                return NotFound();
            }

            var input = data.Split(',').Select(d => int.Parse(d));

            var context = new RecordingSortContext(input, 10);

            try
            {
                sort.Sort(context);
            }
            catch(MaxStepsReachedException exception)
            {
                return Ok(exception.Results);
            }

            return Ok(context.Results);
        }
    }
}
