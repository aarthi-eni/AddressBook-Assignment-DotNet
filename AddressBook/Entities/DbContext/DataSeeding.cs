using Microsoft.Extensions.DependencyInjection;
using Entities.Model;
using System;
namespace Entities
{
    public class DataSeeding
    {
        public static void seed(IServiceProvider serviceProvider)
        {
            AddressBookContext context = serviceProvider.GetRequiredService<AddressBookContext>();
           
            UserLogin user = new UserLogin()
               {
                        AddressBookId = Guid.Parse("dec51b1b-aaee-4cde-b5d0-a3fb7cefc01a"),
                        Id=Guid.Parse("db8480d2-c2e3-47ac-8f54-88af14bf35a9"),
                        UserName = "Aarthi",
                        Password = "Aarthir@02",
                        IsActive = true,
                };
                context.Database.EnsureCreated();
        }
    }
}   