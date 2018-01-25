namespace Appscore.Entities
{
    public enum Direction
    {
        Unspecified = 0,
        Ancestors = 1,
        Descendents = 2
    }

    public enum Gender
    {
        U = 0,
        M = 1,
        F = 2
    }

    public class Person
    {
        public long id { get; set; }
        public string name { get; set; }
        public  Gender gender { get; set; }
        public long? father_id { get; set; }
        public long? mother_id { get; set; }
        public int place_id { get; set; }
        public int level { get; set; }
    }
}
