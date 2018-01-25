namespace Appscore.Entities
{
    public enum Gender
    {
        M = 0,
        F = 1,
        U = 2
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
