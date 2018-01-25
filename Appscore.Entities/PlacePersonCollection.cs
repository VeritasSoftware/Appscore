using System.Collections.Generic;

namespace Appscore.Entities
{
    public class PlacePersonCollection
    {
        public IEnumerable<Place> places { get; set; }
        public IEnumerable<Person> people { get; set; }
    }
}
