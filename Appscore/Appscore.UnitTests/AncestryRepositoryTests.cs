using Appscore.Entities;
using Appscore.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Appscore.UnitTests
{
    [TestClass]
    public class AncestryRepositoryTests
    {        
        [TestMethod]
        public void Test_SimpleSearch()
        {
            var dbPath = (Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) + @"\data_small.json").Replace(@"file:\", string.Empty);

            AncestryRepository ancestryRepository = new AncestryRepository(dbPath);

            string nameContains = "le";

            var results = ancestryRepository.SimpleSearch(new SimpleSearchParameters { Name = nameContains, Gender = Entities.Gender.U });

            results.SearchResults.ToList().ForEach(result =>
            {
                Assert.IsTrue(result.Name.Contains(nameContains));
            });
        }
    }
}
