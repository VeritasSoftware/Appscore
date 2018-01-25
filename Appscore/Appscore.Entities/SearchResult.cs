using System.Collections.Generic;

namespace Appscore.Entities
{
    public class SearchResult
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public string BirthPlace { get; set; }
    }

    public class AdvancedSearchResult : SearchResult
    {
        public int Level { get; set; }
    }

    public class SimpleSearchResultCollection
    {
        public IEnumerable<SearchResult> SearchResults { get; set; }
    }

    public class AdvancedSearchResultCollection
    {
        public IEnumerable<AdvancedSearchResult> SearchResults { get; set; }
    }
}
