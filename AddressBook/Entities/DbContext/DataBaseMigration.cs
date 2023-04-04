using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
namespace Entities

 {
        public static class DataBaseMigration

    {

        public static void UpdateDatabase(IServiceProvider serviceProvider)
        {

            AddressBookContext context = serviceProvider.GetRequiredService<AddressBookContext>();

             context.Database.Migrate();

            context.SaveChangesAsync(true);

        }

    }

}