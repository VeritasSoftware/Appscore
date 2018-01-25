using Appscore.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Appscore.Repository
{
    /// <summary>
    /// Class AncestryRepository
    /// </summary>
    public class AncestryRepository : IAncestryRepository
    {
        private readonly PlacePersonCollection _placePersonCollection;

        public int MaxNoOfSearchResults { get; set; }

        public AncestryRepository(string jsonFilePath)
        {
            string jsonDb = File.ReadAllText(jsonFilePath);

            _placePersonCollection = JsonConvert.DeserializeObject<PlacePersonCollection>(jsonDb);
        }

        /// <summary>
        /// Simple search
        /// </summary>
        /// <param name="searchParameters">The search parameters</param>
        /// <returns><see cref="SimpleSearchResultCollection"/></returns>
        public SimpleSearchResultCollection SimpleSearch(SearchParameters searchParameters)
        {
            //Use dynamic LINQ for Where clause
            var query = _placePersonCollection.people.Join(_placePersonCollection.places, p => p.place_id, pl => pl.id, (p, pl) => new SearchResult
            {
                BirthPlace = pl.name,
                ID = p.id,
                Gender = p.gender,
                Name = p.name
            }).AsQueryable();

            if (searchParameters.Gender != Gender.U)
            {
                query = query.Where(ssr => ssr.Gender == searchParameters.Gender);
            }

            query = query.Where(ssr => ssr.Name.ToLower().Contains(searchParameters.Name.ToLower()));

            return new SimpleSearchResultCollection
            {
                SearchResults = query.OrderBy(r => r.Name).Take(this.MaxNoOfSearchResults).ToList()
            };        
        }

        /// <summary>
        /// Advanced search
        /// </summary>
        /// <param name="searchParameters">The search parameters</param>
        /// <returns><see cref="AdvancedSearchResultCollection"/></returns>
        public AdvancedSearchResultCollection AdvancedSearch(AdvancedSearchParameters searchParameters)
        {
            var results = new AdvancedSearchResultCollection
            {
                SearchResults = new List<AdvancedSearchResult>()
            };

            var foundPerson = _placePersonCollection.people.FirstOrDefault(p => string.Compare(p.name, searchParameters.Name, true) == 0);

            if (foundPerson != null)
            {
                IEnumerable<Person> people = null;
                List<Person> hierarchy = new List<Person>();

                hierarchy.Add(foundPerson);

                if (searchParameters.Direction == Direction.Ancestors)
                {
                    people = FindByDirection(_placePersonCollection.people.Where(p => p.id == foundPerson.father_id || p.id == foundPerson.mother_id), searchParameters.Gender, searchParameters.Direction, hierarchy);                    
                }
                else
                {
                    people = FindByDirection(_placePersonCollection.people.Where(p => p.father_id == foundPerson.id || p.mother_id == foundPerson.id), searchParameters.Gender, searchParameters.Direction, hierarchy);
                }

                var query = people.Join(_placePersonCollection.places, p => p.place_id, p => p.id, (p, pl) => new AdvancedSearchResult
                {
                    ID = p.id,
                    Name = p.name,
                    Gender = p.gender,
                    Level = p.level,
                    BirthPlace = pl.name
                }).AsQueryable();

                if (searchParameters.Direction == Direction.Ancestors)
                {
                    query = query.OrderByDescending(asr => asr.Level).ThenBy(asr => asr.Name);
                }
                else
                {
                    query = query.OrderBy(asr => asr.Level).ThenBy(asr => asr.Name);
                }                

                results.SearchResults = query.Take(this.MaxNoOfSearchResults).ToList();
            }

            return results;
        }

        /// <summary>
        /// Find by direction - Recursive function
        /// </summary>
        /// <param name="persons">The persons</param>
        /// <param name="gender">The genders</param>
        /// <param name="direction">The direction</param>
        /// <returns><see cref="IEnumerable<Person>"/></returns>
        private IEnumerable<Person> FindByDirection(IEnumerable<Person> persons, Gender? gender, Direction direction, List<Person> hierarchy = null)
        {            
            var result = new List<Person>(); 
            
            if (persons == null || !persons.Any())
            {
                return result;
            }

            if (hierarchy.Count() > this.MaxNoOfSearchResults)
            {
                return result;
            }

            if (hierarchy.Any(p => persons.Any(p1 => p1.id == p.id)))
            {
                throw new Exception("Invalid hierarchy loop.");
            }

            var query = persons.AsQueryable();

            if (gender != null && gender != Gender.U)
            {
                query = query.Where(p => p.gender == gender);
            }

            var genderFilteredPersons = query.ToList();

            result.AddRange(genderFilteredPersons);

            if (genderFilteredPersons != null && genderFilteredPersons.Any())
            {
                foreach (Person person in genderFilteredPersons)
                {                                       
                    if (person != null)
                    {
                        if (direction == Direction.Ancestors)
                        {
                            var peopleFound = FindByDirection(_placePersonCollection.people.Where(p => p.id == person.father_id || p.id == person.mother_id), gender, direction, hierarchy);                                                 

                            result.AddRange(peopleFound);
                            hierarchy.AddRange(peopleFound);                         
                        }
                        else
                        {
                            var peopleFound = FindByDirection(_placePersonCollection.people.Where(p => p.father_id == person.id || p.mother_id == person.id), gender, direction, hierarchy);                            

                            result.AddRange(peopleFound);
                            hierarchy.AddRange(peopleFound);                                                     
                        }
                    }                    
                }                
            }                      

            return result;
        }
    }
}