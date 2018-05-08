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

        public SeedData(
            ApplicationDbContext context,
            ILogger<SeedData> logger,
            ICityRepository cityRepository,
            IPersonRepository personRepository,
            IDepartementRepository departementRepository)
        {
            _context = context;
            _logger = logger;
            _cityRepository = cityRepository;
            _personRepository = personRepository;
            _departRepository = departementRepository;
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
    }
}