using Appscore.Entities;

namespace Appscore.Repository
{
    public interface IAncestryRepository
    {
        SimpleSearchResultCollection SimpleSearch(SearchParameters searchParameters);

        AdvancedSearchResultCollection AdvancedSearch(AdvancedSearchParameters searchParameters);
    }
}
