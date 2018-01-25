namespace Appscore.Entities
{
    public class SearchParameters
    {
        public string Name { get; set; }
        public Gender? Gender { get; set; }             
    }

    public class AdvancedSearchParameters : SearchParameters
    {
        public Direction Direction { get; set; }
    }
}
