using LibApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Domain.Enums;

namespace LibApp.Persistence.Seed
{
    public class DataSeed : IDataSeed
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Customer> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public DataSeed(ApplicationDbContext context,
            UserManager<Customer> userManager,
            RoleManager<IdentityRole<int>> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task CreateUserWithRoles()
        {
            if (!_context.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole<int>
                {
                    Name = RoleEnum.User.ToString(),
                    NormalizedName = RoleEnum.User.ToString()
                });

                await _roleManager.CreateAsync(new IdentityRole<int>
                {
                    Name = RoleEnum.StoreManager.ToString(),
                    NormalizedName = RoleEnum.StoreManager.ToString()
                });

                await _roleManager.CreateAsync(new IdentityRole<int>
                {
                    Name = RoleEnum.Owner.ToString(),
                    NormalizedName = RoleEnum.Owner.ToString()
                });
            }

            if (!_context.Users.Any())
            {
                var _user = new Customer
                {
                    Name = "John User",
                    HasNewsletterSubscribed = true,
                    MembershipTypeId = 1,
                    Birthdate = new DateTime(1987, 01, 01)
                };

                var _manager = new Customer
                {
                    Name = "John Manager",
                    HasNewsletterSubscribed = false,
                    MembershipTypeId = 2,
                    Birthdate = new DateTime(1987, 02, 03)
                };

                var _owner = new Customer
                {
                    Name = "John Owner",
                    HasNewsletterSubscribed = false,
                    MembershipTypeId = 4,
                    Birthdate = new DateTime(1987, 02, 03)
                };

                await _userManager.CreateAsync(_user, "!Test_1234");
                await _userManager.CreateAsync(_manager, "!Test_1234");
                await _userManager.CreateAsync(_owner, "!Test_1234");

                //await _userManager.AddToRoleAsync(_user, RoleEnum.User.ToString());
                //await _userManager.AddToRoleAsync(_manager, RoleEnum.StoreManager.ToString());
                //await _userManager.AddToRoleAsync(_owner, RoleEnum.Owner.ToString());
            }
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

        private async Task InitializeBooks()
        {
            if (_context.Books.Any())
                return;

            var books = new List<Book>
            {
                new Book
                    {
                        Name="C#. Rusz głową! Wydanie IV",
                        AuthorName="Andrew Stellman, Jennifer Greene",
                        GenreId = 1,
                        DateAdded = DateTime.Now,
                        ReleaseDate = new DateTime(2022, 01, 25),
                        NumberInStock = 10,
                        NumberAvailable = 10
                    },
                new Book
                    {
                        Name="Statystyka praktyczna w data science. 50 kluczowych zagadnień w językach R i Python. Wydanie II",
                        AuthorName="Peter Bruce, Andrew Bruce, Peter Gedeck",
                        GenreId = 2,
                        DateAdded = DateTime.Now,
                        ReleaseDate = new DateTime(2021, 06, 16),
                        NumberInStock = 5,
                        NumberAvailable = 5
                    },
                new Book
                    {
                        Name="React. Wstęp do programowania",
                        AuthorName="Paweł Kamiński",
                        GenreId = 2,
                        DateAdded = DateTime.Now,
                        ReleaseDate = new DateTime(2021, 11, 20),
                        NumberInStock = 15,
                        NumberAvailable = 15
                    },
            };

            await _context.Books.AddRangeAsync(books);
            await _context.SaveChangesAsync();
        }

        public async Task Initialize()
        {
            await CreateUserWithRoles();
            await InitializeGenre();
            await InitializeMembershipTypes();
            await InitializeBooks();
        }
    }
}
