using Appscore.Entities;

namespace Appscore.Repository
{
    public interface IAncestryRepository
    {
        SimpleSearchResultCollection SimpleSearch(SimpleSearchParameters searchParameters);
    }
}
