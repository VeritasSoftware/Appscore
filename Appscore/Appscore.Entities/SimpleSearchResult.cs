using System.Collections.Generic;

namespace Appscore.Entities
{
    public class SimpleSearchResult
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public string BirthPlace { get; set; }
    }

    public class SimpleSearchResultCollection
    {
        public IEnumerable<SimpleSearchResult> SearchResults { get; set; }
    }
}
