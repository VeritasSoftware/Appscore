using Appscore.Entities;

namespace Appscore.Repository
{
    interface IAncestryRepository
    {
        SimpleSearchResultCollection SimpleSearch(SimpleSearchParameters searchParameters);
    }
}
