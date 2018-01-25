using Appscore.Entities;
using Appscore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Appscore.API.Controllers
{
    [Route("api/[controller]")]
    public class AncestryController : Controller
    {
        private readonly IAncestryRepository _ancestryRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ancestryRepository">The injected ancestry repository</param>
        public AncestryController(IAncestryRepository ancestryRepository)
        {
            _ancestryRepository = ancestryRepository ?? throw new ArgumentNullException(nameof(ancestryRepository));
        }

        /// <summary>
        /// Simple search API
        /// </summary>
        /// <param name="searchParameters">The search parameters</param>
        /// <returns><see cref="SimpleSearchResultCollection"/></returns>
        [HttpPost("simplesearch")]
        public SimpleSearchResultCollection SimpleSearch([FromBody] SimpleSearchParameters searchParameters)
        {
            return _ancestryRepository.SimpleSearch(searchParameters);
        }        
    }
}
