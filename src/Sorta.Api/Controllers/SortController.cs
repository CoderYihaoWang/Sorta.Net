using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sorta.Abstractions;

namespace Sorta.Api.Controllers
{
    [ApiController]
    [Route("/api")]
    public class SortController : ControllerBase
    {
        private readonly IEnumerable<ISort> _sorts;
        private int _maxSteps;

        public SortController(IEnumerable<ISort> sorts, IConfiguration configuration)
        {
            _sorts = sorts;
            _maxSteps = configuration.GetValue<int?>("MaxSteps") ?? 10000;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _sorts.Select(s => s.Algorithm);
        }

        [HttpGet("{algorithm}")]
        public ActionResult<SortStats> Sort([FromRoute] string algorithm, 
            [FromQuery] string data,
            [FromQuery] int? maxSteps)
        {
            var sort = _sorts.FirstOrDefault(s => s.Algorithm.Equals(algorithm, StringComparison.OrdinalIgnoreCase));

            if (sort is null)
            {
                return NotFound();
            }

            var input = data.Split(',').Select(d => int.Parse(d));

            var context = new RecordingSortContext(input, maxSteps ?? _maxSteps);

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
