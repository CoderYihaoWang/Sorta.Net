using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sorta.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sorta.Api.Controllers
{
    [ApiController]
    [Route("/api")]
    public class SortController : ControllerBase
    {
        private readonly IEnumerable<IAlgorithm> _sorts;
        private readonly int _maxSteps;

        public SortController(IEnumerable<IAlgorithm> sorts, IConfiguration configuration)
        {
            _sorts = sorts;
            _maxSteps = configuration.GetValue<int?>("MaxSteps") ?? 10000;
        }

        [HttpGet]
        public IDictionary<string, string> Get() 
        {
            var results = new Dictionary<string, string>();
            foreach (var sort in _sorts)
            {
                results[sort.Algorithm] = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/api/{sort.Algorithm.ToLowerInvariant().Replace(" ", "-")}";
            }
            return results;
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
