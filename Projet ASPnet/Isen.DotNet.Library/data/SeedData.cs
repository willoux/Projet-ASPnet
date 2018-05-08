using System;
using System.Collections.Generic;
using System.Linq;
using Isen.DotNet.Library.Models.Implementation;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

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

        public SeedData(
            ApplicationDbContext context,
            ILogger<SeedData> logger,
            ICityRepository cityRepository,
            IPersonRepository personRepository,
            IDepartementRepository departementRepository,
            ICommuneRepository communeRepository,
            IAddressRepository addressRepository,
            ICatPoiRepository catpoiRepository)
        {
            _context = context;
            _logger = logger;
            _cityRepository = cityRepository;
            _personRepository = personRepository;
            _departRepository = departementRepository;
            _communeRepository = communeRepository;
            _addressRepository = addressRepository;
            _catpoiRepository = catpoiRepository;
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

        public void AddDepart()
        {
            if (_departRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding departement");

            var departement = new List<Departement>
            {
                new Departement { Name = "Var", Numero = 83 },
                new Departement { Name = "Vaucluse", Numero = 84 },
                new Departement { Name = "Alpes-maritimes", Numero = 06},
                new Departement { Name = "Bouches-du-Rhône", Numero = 13 },
                new Departement { Name = "Languedoc-Roussillon", Numero = 34 }
            };
            _departRepository.UpdateRange(departement);
            _departRepository.Save();

            _logger.LogWarning("Added departement");
        }

        public void AddCommunes()
        {
            if (_communeRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding communes");

            var communes = new List<Commune>
            {
                new Commune
                {
                    Name = "Toulon",
                    Longitude = 123,
                    Latitude = 345,
                    Departement = _departRepository.Single("Var")
                }
            };
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
                    Name = "Adresse A",
                    Text = "12 Impasse Gasquet",
                    Zipcode = 83200,
                    Commune = _communeRepository.Single("Toulon"),
                    Longitude = 123,
                    Latitude = 345
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

            var catpoi = new List<CatPoi>
            {
                new CatPoi { Name = "Art Comtemporain", Description = "Faire de l'art comtemporain" },
                new CatPoi { Name = "Musique",  Description = "Faire de la musique"},
                new CatPoi { Name = "Sport",  Description = "Faire du sport"},
                
            };
            _catpoiRepository.UpdateRange(catpoi);
            _catpoiRepository.Save();

            _logger.LogWarning("Added CatPoi");
        }
    }
}