using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Isen.DotNet.Library.Models.Implementation;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace Isen.DotNet.Library.Data
{
    public class SeedData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SeedData> _logger;
        private readonly ICityRepository _cityRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IDepartementRepository _departRepository;
        private readonly ICommuneRepository _communeRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ICatPoiRepository _catpoiRepository;
        private readonly IPoiRepository _poiRepository;

        public SeedData(
            ApplicationDbContext context,
            ILogger<SeedData> logger,
            ICityRepository cityRepository,
            IPersonRepository personRepository,
            IDepartementRepository departementRepository,
            ICommuneRepository communeRepository,
            IAddressRepository addressRepository,
            ICatPoiRepository catpoiRepository,
            IPoiRepository poiRepository)
        {
            _context = context;
            _logger = logger;
            _cityRepository = cityRepository;
            _personRepository = personRepository;
            _departRepository = departementRepository;
            _communeRepository = communeRepository;
            _addressRepository = addressRepository;
            _catpoiRepository = catpoiRepository;
            _poiRepository = poiRepository;
        }

        public void DropDatabase()
        {
            var deleted = _context.Database.EnsureDeleted();
            var not = deleted ? "" : "not ";
            _logger.LogWarning($"Database was {not}dropped.");
        }

        public void CreateDatabase()
        {
            var created = _context.Database.EnsureCreated();
            var not = created ? "" : "not ";
            _logger.LogWarning($"Database was {not}created.");
        }

        public void AddCities()
        {
            if (_cityRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding cities");
           
            var cities = new List<City>
            {
                new City { Name = "Toulon" },
                new City { Name = "Marseille" },
                new City { Name = "Nice" },
                new City { Name = "Paris" },
                new City { Name = "Epinal" }
            };
            _cityRepository.UpdateRange(cities);
            _cityRepository.Save();

            _logger.LogWarning("Added cities");  
        }

        public void AddPersons()
        {
            if (_personRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding persons");

            var persons = new List<Person>
            {
                new Person
                {
                    FirstName = "Calendau",
                    LastName = "GUQUET",
                    BirthDate = new DateTime(1980,2,28),
                    City = _cityRepository.Single("Toulon")
                },
                new Person
                {
                    FirstName = "John",
                    LastName = "APPLESEED",
                    BirthDate = new DateTime(1971,12,14),
                    City = _cityRepository.Single("Marseille")
                },
                new Person
                {
                    FirstName = "Steve",
                    LastName = "JOBS",
                    BirthDate = new DateTime(1949,2,24),
                    City = _cityRepository.Single("Marseille")
                }
            };
            _personRepository.UpdateRange(persons);
            _personRepository.Save();

            _logger.LogWarning("Added persons");
        }



// ----------------------------------------------------------------------------------------------------------

        public void AddDepart()
        {
            if (_departRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding departement");

            
            var m_departement = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("../Isen.DotNet.Library/bin/Depart.json"));
            var departement = new List<Departement> { };
            
            foreach(var m_depart in m_departement.Departement)
            {
                departement.Add(new Departement {
                    Name = m_depart.Nom.ToString(), 
                    Numero = m_depart.Numero 
                });
            }
            _departRepository.UpdateRange(departement);
            _departRepository.Save();

            _logger.LogWarning("Added departement");
        }

        public void AddCommunes()
        {
            if (_communeRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding communes");

            var m_commune = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("../Isen.DotNet.Library/bin/Commune.json"));
            var communes = new List<Commune> { };
            String dept;
            
            foreach(var m_com in m_commune.Communes)
            { 
                dept = m_com.Departement.ToString();
                communes.Add( new Commune {
                    Name = m_com.Nom.ToString(),
                    Departement = _departRepository.Single(dept)
                });
            }
            _communeRepository.UpdateRange(communes);
            _communeRepository.Save();

            _logger.LogWarning("Added communes");
        }

        public void AddAddress()
        {
            if (_addressRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding addresses");

            var addresses = new List<Address>
            {
                new Address
                {
                    Name = "Zenith",
                    Text = "Boulevard Commandant Nicolas",
                    Zipcode = 83000,
                    Commune = _communeRepository.Single("SIGOYER"),
                    Longitude = 5.932092,
                    Latitude = 43.128574
                },
                new Address
                {
                    Name = "Opera Toulon",
                    Text = "Boulevard de Strasbourg",
                    Zipcode = 83000,
                    Commune = _communeRepository.Single("SIGOYER"),
                    Longitude = 5.932652,
                    Latitude = 43.124430
                },
                new Address
                {
                    Name = "Mucem Marseille",
                    Text = "7 Prom. Robert Laffont",
                    Zipcode = 13002,
                    Commune = _communeRepository.Single("SIGOYER"),
                    Longitude = 5.3609848999999485,
                    Latitude = 43.2967885
                },
                new Address
                {
                    Name = "Parc Borély",
                    Text = "Allée Borely",
                    Zipcode = 13008,
                    Commune = _communeRepository.Single("SIGOYER"),
                    Longitude = 5.473697000000016,
                    Latitude = 43.4545252
                },
                new Address
                {
                    Name = "Musée de la Marine Toulon",
                    Text = "Place Monsenergue, Quai de Norfolk",
                    Zipcode = 83000,
                    Commune = _communeRepository.Single("SIGOYER"),
                    Longitude = 5.928404999999998,
                    Latitude = 43.121862
                },
                new Address
                {
                    Name = "Théatre Antique",
                    Text = "Rue Madeleine Roche",
                    Zipcode = 84100,
                    Commune = _communeRepository.Single("SIGOYER"),
                    Longitude = 4.80803149999997,
                    Latitude = 44.1360363
                }
            };
            _addressRepository.UpdateRange(addresses);
            _addressRepository.Save();

            _logger.LogWarning("Added addresses");
        }
        public void AddCatPoi()
        {
            if (_catpoiRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding CatPoi");

            var m_catego = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("../Isen.DotNet.Library/bin/CatPoi.json"));
            var categories = new List<CatPoi> { };
            
            foreach(var m_cat in m_catego.Categories)
            { 
                categories.Add( new CatPoi {
                    Name = m_cat.Nom.ToString(),
                    Description = m_cat.Description.ToString()
                });
            }
            _catpoiRepository.UpdateRange(categories);
            _catpoiRepository.Save();

            _logger.LogWarning("Added CatPoi");
        }

        public void AddPoi()
        {
            if (_poiRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding poi");

            var m_pointinteret = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("../Isen.DotNet.Library/bin/Poi.json"));
            var poi = new List<Poi> { };
            String dept;
            String dept2;
            
            foreach(var m_poi in m_pointinteret.Poi)
            { 
                dept = m_poi.Adresse.ToString();
                dept2 = m_poi.Categorie.ToString();
                
                poi.Add( new Poi {
                    Name = m_poi.Nom.ToString(),
                    Description = m_poi.Description.ToString(),
                    Address = _addressRepository.Single(dept),
                    Category = _catpoiRepository.Single(dept2)
                });
            }
            
            _poiRepository.UpdateRange(poi);
            _poiRepository.Save();

            _logger.LogWarning("Added poi");
        }
    }
}