using Appscore.Entities;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Appscore.Repository
{
    /// <summary>
    /// Class AncestryRepository
    /// </summary>
    public class AncestryRepository : IAncestryRepository
    {
        private readonly PlacePersonCollection _placePersonCollection;

        public AncestryRepository(string jsonFilePath)
        {
            string jsonDb = File.ReadAllText(jsonFilePath);

            _placePersonCollection = JsonConvert.DeserializeObject<PlacePersonCollection>(jsonDb);
        }

        /// <summary>
        /// Simple search
        /// </summary>
        /// <param name="searchParameters">The search parameters</param>
        /// <returns><see cref="SimpleSearchResultCollection"/></returns>
        public SimpleSearchResultCollection SimpleSearch(SimpleSearchParameters searchParameters)
        {
            //Use dynamic LINQ for Where clause
            var query = _placePersonCollection.people.Join(_placePersonCollection.places, p => p.place_id, pl => pl.id, (p, pl) => new SimpleSearchResult
            {
                BirthPlace = pl.name,
                ID = p.id,
                Gender = p.gender,
                Name = p.name
            }).AsQueryable();

            if (searchParameters.Gender != Gender.U)
            {
                query = query.Where(ssr => ssr.Gender == searchParameters.Gender);
            }

            query = query.Where(ssr => ssr.Name.ToLower().Contains(searchParameters.Name.ToLower()));

            return new SimpleSearchResultCollection
            {
                SearchResults = query.ToList()
            };        
        }
    }
}
