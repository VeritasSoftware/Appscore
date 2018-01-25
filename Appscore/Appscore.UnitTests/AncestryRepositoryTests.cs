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
        public void Test_SimpleSearch_WithGenderUnspecified()
        {
            var dbPath = (Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) + @"\data_small.json").Replace(@"file:\", string.Empty);

            AncestryRepository ancestryRepository = new AncestryRepository(dbPath) { MaxNoOfSearchResults = 10 };

            string nameContains = "le";

            var results = ancestryRepository.SimpleSearch(new SearchParameters { Name = nameContains });

            results.SearchResults.ToList().ForEach(result =>
            {
                Assert.IsTrue(result.Name.Contains(nameContains));
            });
        }

        [TestMethod]
        public void Test_SimpleSearch_WithGenderSpecified()
        {
            var dbPath = (Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) + @"\data_small.json").Replace(@"file:\", string.Empty);

            AncestryRepository ancestryRepository = new AncestryRepository(dbPath) { MaxNoOfSearchResults = 10 };

            string nameContains = "le";
            var gender = Gender.F;

            var results = ancestryRepository.SimpleSearch(new SearchParameters { Name = nameContains, Gender = gender });

            results.SearchResults.ToList().ForEach(result =>
            {
                Assert.IsTrue(result.Name.Contains(nameContains) && result.Gender == gender);
            });
        }

        [TestMethod]
        public void Test_AdvancedSearch_Ancestors_WithGenderSpecified()
        {
            var dbPath = (Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) + @"\data_small.json").Replace(@"file:\", string.Empty);

            AncestryRepository ancestryRepository = new AncestryRepository(dbPath) { MaxNoOfSearchResults = 10 };

            string nameContains = "Codi Clarie";
            var gender = Gender.F;

            var results = ancestryRepository.AdvancedSearch(new AdvancedSearchParameters { Name = nameContains, Gender = gender, Direction = Direction.Ancestors });

            Assert.IsTrue(results.SearchResults.Count() == 2);
            Assert.IsTrue(results.SearchResults.All(ssr => ssr.Gender == Gender.F));
            Assert.IsTrue(results.SearchResults.Last().Level == 0);
        }

        [TestMethod]
        public void Test_AdvancedSearch_Descendents_WithGenderSpecified()
        {
            var dbPath = (Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) + @"\data_small.json").Replace(@"file:\", string.Empty);

            AncestryRepository ancestryRepository = new AncestryRepository(dbPath) { MaxNoOfSearchResults = 10 };

            string nameContains = "Codi Clarie";
            var gender = Gender.M;

            var results = ancestryRepository.AdvancedSearch(new AdvancedSearchParameters { Name = nameContains, Gender = gender, Direction = Direction.Descendents });

            Assert.IsTrue(results.SearchResults.Count() == 1);
            Assert.IsTrue(results.SearchResults.All(ssr => ssr.Gender == Gender.M));
        }

        [TestMethod]
        public void Test_AdvancedSearch_Ancestors_WithGenderUnspecified()
        {
            var dbPath = (Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) + @"\data_small.json").Replace(@"file:\", string.Empty);

            AncestryRepository ancestryRepository = new AncestryRepository(dbPath) { MaxNoOfSearchResults = 10 };

            string nameContains = "Codi Clarie";

            var results = ancestryRepository.AdvancedSearch(new AdvancedSearchParameters { Name = nameContains, Direction = Direction.Ancestors });

            Assert.IsTrue(results.SearchResults.Count() == 6);
            Assert.IsTrue(results.SearchResults.Last().Level == 0);
        }

        [TestMethod]
        public void Test_AdvancedSearch_Descendents_WithGenderUnspecified()
        {
            var dbPath = (Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) + @"\data_small.json").Replace(@"file:\", string.Empty);

            AncestryRepository ancestryRepository = new AncestryRepository(dbPath) { MaxNoOfSearchResults = 10 };

            string nameContains = "Codi Clarie";

            var results = ancestryRepository.AdvancedSearch(new AdvancedSearchParameters { Name = nameContains, Direction = Direction.Descendents });

            Assert.IsTrue(results.SearchResults.Count() == 3);
            Assert.IsTrue(results.SearchResults.Last().Level == 4);
        }
    }
}
