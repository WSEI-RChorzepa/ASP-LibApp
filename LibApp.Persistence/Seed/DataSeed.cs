using LibApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Persistence.Seed
{
    public class DataSeed : IDataSeed
    {
        private readonly ApplicationDbContext _context;

        public DataSeed(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InitializeGenre()
        {
            if (_context.Genre.Any())
                return;

            var genres = new List<Genre>
            {
                new Genre
                {
                    Id = 1,
                    Name = "Programowanie"
                },
                new Genre
                {
                    Id = 2,
                     Name = "Big Data"
                },
                new Genre
                {
                    Id = 3,
                    Name = "Technologie webowe"
                }
            };

            _context.Genre.AddRange(genres);
            await _context.SaveChangesAsync();
        }

        private async Task InitializeMembershipTypes()
        {
            if (_context.MembershipTypes.Any())
                return;

            var membershipTypes = new List<MembershipType>
            {
                  new MembershipType
                    {
                        Id = 1,
                        Name = "Pay as You Go",
                        SignUpFee = 0,
                        DurationInMonths = 0,
                        DiscountRate = 0
                    },
                    new MembershipType
                    {
                        Id = 2,
                        Name = "Monthly",
                        SignUpFee = 30,
                        DurationInMonths = 1,
                        DiscountRate = 10
                    },
                    new MembershipType
                    {
                        Id = 3,
                        Name = "Quaterly",
                        SignUpFee = 90,
                        DurationInMonths = 3,
                        DiscountRate = 15
                    },
                    new MembershipType
                    {
                        Id = 4,
                        Name = "Yearly",
                        SignUpFee = 300,
                        DurationInMonths = 12,
                        DiscountRate = 20
                    }
            };

            await _context.MembershipTypes.AddRangeAsync(membershipTypes);
            await _context.SaveChangesAsync();
        }

        private async Task InitializeRentals()
        {
            if (_context.Rentals.Any())
                return;

            var rentals = new List<Rental>
            {
                new Rental
                {
                    Customer = new Customer
                    {
                       Name = "John Doe",
                        HasNewsletterSubscribed = true,
                        MembershipTypeId = 1,
                        Birthdate = new DateTime(1987, 01, 01)
                    },
                    Book = new Book
                    {
                        Name="C#. Rusz głową! Wydanie IV",
                        AuthorName="Andrew Stellman, Jennifer Greene",
                        GenreId = 1,
                        DateAdded = DateTime.Now,
                        ReleaseDate = new DateTime(2022, 01, 25),
                        NumberInStock = 10,
                        NumberAvailable = 10
                    },
                    DateRented = new DateTime(2022, 01, 01),
                    DateReturned = new DateTime(2022, 02, 02)
                },
                new Rental
                {
                    Customer = new Customer
                    {
                        Name = "John Sixpack",
                        HasNewsletterSubscribed = false,
                        MembershipTypeId = 2,
                        Birthdate = new DateTime(1987, 02, 03)
                    },
                    Book = new Book
                    {
                        Name="Statystyka praktyczna w data science. 50 kluczowych zagadnień w językach R i Python. Wydanie II",
                        AuthorName="Peter Bruce, Andrew Bruce, Peter Gedeck",
                        GenreId = 2,
                        DateAdded = DateTime.Now,
                        ReleaseDate = new DateTime(2021, 06, 16),
                        NumberInStock = 5,
                        NumberAvailable = 5
                    },
                    DateRented = new DateTime(2022, 01, 03),
                    DateReturned = new DateTime(2022, 02, 10)
                },
                new Rental
                {
                    Customer = new Customer
                    {
                        Name = "John Doakes",
                        HasNewsletterSubscribed = false,
                        MembershipTypeId = 4,
                        Birthdate = new DateTime(1987, 02, 03)
                    },
                    Book = new Book
                    {
                        Name="React. Wstęp do programowania",
                        AuthorName="Paweł Kamiński",
                        GenreId = 2,
                        DateAdded = DateTime.Now,
                        ReleaseDate = new DateTime(2021, 11, 20),
                        NumberInStock = 15,
                        NumberAvailable = 15
                    },
                    DateRented = new DateTime(2022, 01, 07),
                    DateReturned = new DateTime(2022, 02, 11)
                }
            };

            await _context.Rentals.AddRangeAsync(rentals);
            await _context.SaveChangesAsync();
        }

        public async Task Initialize()
        {
            await InitializeGenre();
            await InitializeMembershipTypes();
            await InitializeRentals();
        }
    }
}
