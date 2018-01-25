using System;
using Appscore.Entities;

namespace Appscore.Repository
{
    public class AncestryRepository : IAncestryRepository
    {
        private readonly PlacePersonCollection _placePersonCollection;

        public AncestryRepository()
        {
        }

        public SimpleSearchResultCollection SimpleSearch(SimpleSearchParameters searchParameters)
        {
            throw new NotImplementedException();
        }
    }
}
