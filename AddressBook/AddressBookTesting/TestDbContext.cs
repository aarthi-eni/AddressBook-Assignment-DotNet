using Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace AddressBookUnitTest
{
    public static class TestDbContext
    {
        /// <summary>
        /// This method is used to create the InMemeorydatabase
        /// </summary>
        /// <returns></returns>
        public static AddressBookContext addressBookDbContext()
        {
            DbContextOptions<AddressBookContext> options = new DbContextOptionsBuilder<AddressBookContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AddressBookContext context = new AddressBookContext(options);

            return context;
        }
    }
}
