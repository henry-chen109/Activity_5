using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GhibliApiClient {
    
    class Film {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty("original_title_romanised")]
        public string OriginalTitleRomanised { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("director")]
        public string Director { get; set; }

        [JsonProperty("producer")]
        public string Producer { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("running_time")]
        public string RunningTime { get; set; }

        [JsonProperty("rt_score")]
        public string RtScore { get; set; }
    }

    class Person {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("age")]
        public string Age { get; set; }

        [JsonProperty("eye_color")]
        public string EyeColor { get; set; }

        [JsonProperty("hair_color")]
        public string HairColor { get; set; }
    }

    class Location {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("climate")]
        public string Climate { get; set; }

        [JsonProperty("Terrain")]
        public string Terrain { get; set; }

        [JsonProperty("surface_water")]
        public string SurfaceWater { get; set; }
    }

    class Species {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("classification")]
        public string Classification { get; set; }

        [JsonProperty("eye_colors")]
        public string EyeColors { get; set; }

        [JsonProperty("hair_colors")]
        public string HairColors { get; set; }
    }

    class Vehicle {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("vehicle_class")]
        public string VehicleClass { get; set; }

        [JsonProperty("length")]
        public string Length { get; set; }
    }

    class Program {

        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args) {
            await ProcessApi();
        }
        private static async Task ProcessApi() {

            try {
                
                Console.WriteLine("\nTry out the Ghibili API, follow the instructions below to continue!\n");
                while (true) {
                    Console.Write("Please enter one of the following films/people/locations/species/vehicles --- press Enter with no other inputs to quit: ");
                    var input = Console.ReadLine().ToLower();

                    if (string.IsNullOrEmpty(input)) {
                        break;
                    }

                    var results = await client.GetAsync("https://ghibliapi.herokuapp.com/" + input); 
                    var resultsRead = await results.Content.ReadAsStringAsync();
  
                    switch(input) {
                        case "films": {
                            List<Film> films = JsonConvert.DeserializeObject<List<Film>>(resultsRead);
                            Console.WriteLine("\nFilms\n");
                            foreach( var film in films ) {
                                Console.WriteLine(new string('-', 150) + "\n");
                                Console.WriteLine("Title: " + film.Title);
                                //can't display japanese characters
                                // Console.WriteLine("Original Title: " + film.OriginalTitle);
                                Console.WriteLine("Original Title Romanised: " + film.OriginalTitleRomanised);
                                Console.WriteLine("Description:\n\n" + film.Description + "\n");
                                Console.WriteLine("Director: " + film.Director);
                                Console.WriteLine("Producer: " + film.Producer);
                                Console.WriteLine("Release Date: " + film.ReleaseDate);
                                Console.WriteLine("Running Time: " + film.RunningTime);
                                Console.WriteLine("Rating: " + film.RtScore + "\n");
                            }

                            break;
                        }
                        case "people": {
                            List<Person> people = JsonConvert.DeserializeObject<List<Person>>(resultsRead);
                            Console.WriteLine("\nPeople\n");
                            foreach( var person in people ) {
                                Console.WriteLine(new string('-', 150) + "\n");
                                Console.WriteLine("Name: " + person.Name);
                                Console.WriteLine("Gender: " + person.Gender);
                                Console.WriteLine("Age: " + person.Age);
                                Console.WriteLine("Eye Color: " + person.EyeColor);
                                Console.WriteLine("Hair Color: " + person.HairColor + "\n");
                            }
                            break;
                        }
                        case "locations": {
                            List<Location> locations = JsonConvert.DeserializeObject<List<Location>>(resultsRead);
                            Console.WriteLine("\nLocations\n");
                            foreach( var location in locations ) {
                                Console.WriteLine(new string('-', 150) + "\n");
                                Console.WriteLine("Location Name: " + location.Name);
                                Console.WriteLine("Climate: " + location.Climate);
                                Console.WriteLine("Terrain: " + location.Terrain);
                                Console.WriteLine("Surface Water: " + location.SurfaceWater + "\n");
                            }
                            break;
                        }
                        case "species": {
                            List<Species> species = JsonConvert.DeserializeObject<List<Species>>(resultsRead);
                            Console.WriteLine("\nSpecies\n");
                            foreach( var animalae in species ) {
                                Console.WriteLine(new string('-', 150) + "\n");
                                Console.WriteLine("Species Name: " + animalae.Name);
                                Console.WriteLine("Classification: " + animalae.Classification);
                                Console.WriteLine("Eye Colors: " + animalae.EyeColors );
                                Console.WriteLine("Hair Colors: " + animalae.HairColors + "\n");
                            }
                            break;
                        }
                        case "vehicles": {
                            List<Vehicle> vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(resultsRead);
                            Console.WriteLine("\nVehicles\n");
                            foreach( var vehicle in vehicles ) {
                                Console.WriteLine(new string('-', 150) + "\n");
                                Console.WriteLine("Vehicle Name: " + vehicle.Name);
                                Console.WriteLine("Description:\n\n" + vehicle.Description + "\n");
                                Console.WriteLine("Vehicle Type: " + vehicle.VehicleClass);
                                Console.WriteLine("Length: " + vehicle.Length + "\n");
                            }
                            break;
                        }
                        default: {
                            break;
                        }
                    }

                
                }
                
            } catch(Exception) {
                Console.WriteLine("ERROR");
            }
        }

        
    }

}