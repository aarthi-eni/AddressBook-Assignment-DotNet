using Entities;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;

namespace AddressBookUnitTest
{
    public static class ContextData
    {
        /// <summary>
        /// Thids method is used to add data in database
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static AddressBookContext AddData(AddressBookContext context)
        {
           // string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            //string addressBookPath = Path.Combine(baseDir, @"..\..\..\DbContext\data\AddressBook.csv");
            //string[] userValues = File.ReadAllText(Path.GetFullPath(addressBookPath)).Split('\n');

            //foreach (string item in userValues)
            //{
                //if (!string.IsNullOrEmpty(item))
                //{
                    //string[] row = item.Split(",");

                    context.User.Add(
                    new UserLogin()
                    {
                        AddressBookId = Guid.Parse("dec51b1b-aaee-4cde-b5d0-a3fb7cefc01a"),
                        Id=Guid.Parse("db8480d2-c2e3-47ac-8f54-88af14bf35a9"),
                        UserName = "Aarthi",
                        Password = "Aarthir@01",
                        IsActive = true,

                    }
                    );
                    context.AddressBook.Add(
                        new Entities.Model.AddressBook()
                        {
                            Id = Guid.Parse("dec51b1b-aaee-4cde-b5d0-a3fb7cefc01a"),
                            FirstName = "Test",
                            LastName = "Data",
                            IsActive = true,
                            CreatedBy= Guid.Parse("db8480d2-c2e3-47ac-8f54-88af14bf35a9"),
                            Createdon = DateTime.Now,
                        }
                    );
                    context.Address.Add(
                            new Address()
                            {
                                AddressBookId = Guid.Parse("dec51b1b-aaee-4cde-b5d0-a3fb7cefc01a"),
                                 Id = Guid.Parse("149572d0-2247-4a8e-89b8-3e6c979285ee"),
                                Line1 = "140",
                                Line2 = "xxx",
                                City = "yyy",
                                ZipCode = "12345",
                                StateName = "TN",
                                Type = "Office",
                                Country = "India",
                                IsActive = true,
                            }
                        );
                        context.Phone.Add(
                            new PhoneNumber()
                            {
                                AddressBookId = Guid.Parse("dec51b1b-aaee-4cde-b5d0-a3fb7cefc01a"),
                                Id = Guid.Parse("139372d0-2247-4a8e-89b8-3e6c979285ee"),
                                Phone = "1234567890",
                                Type = "Office",
                                IsActive = true,
                            }
                        );
                        context.Email.Add(
                            new EmailAddress()
                            {
                                AddressBookId = Guid.Parse("dec51b1b-aaee-4cde-b5d0-a3fb7cefc01a"),
                                Id = Guid.Parse("129272d0-2247-4a8e-89b8-3e6c979285ee"),
                                Email = "Test@test.com",
                                Type = "Office",
                                IsActive = true,
                            }
                        );


               // }
           // }
            Asset assetdto = new Asset { AddressBookId =Guid.Parse("dec51b1b-aaee-4cde-b5d0-a3fb7cefc01a"),Id = Guid.Parse("876072b6-04e4-4577-b21c-946e96bef643")};
            context.Asset.Add(assetdto);

            context.SaveChanges();
            return context;
        }
    }
}