using System;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork34
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await EnsureDatabaseCreated();
            await InsertCity();
            await InsertAddress();
            await InsertDocument();
            await InsertPosition();
            await InsertStatus();
            await InsertEmployee();
            await InsertDocumentStatus();
        }

        private static async Task EnsureDatabaseCreated()
        {
            await using var context = new DocumentStorageContext();
            await context.Database.EnsureCreatedAsync();
        }

        private static async Task InsertAddress()
        {
            await using var context = new DocumentStorageContext();

            var city = context.Cities.First();
            var address = new Address(city, "3rd Land", "82A");
            context.Add(address);
            await context.SaveChangesAsync();
        }

        private static async Task InsertCity()
        {
            await using var context = new DocumentStorageContext();
            context.Cities.AddRange(
                new City("Moscow"),
                new City("Tokyo")
                );
            await context.SaveChangesAsync();
        }

        private static async Task InsertDocument()
        {
            await using var context = new DocumentStorageContext();
            context.Documents.AddRange(
                new Document("Pagesingster", 50),
                new Document("Pokroster",80)
                );
            await context.SaveChangesAsync();
        }

        private static async Task InsertDocumentStatus()
        {
            await using var context = new DocumentStorageContext();

            var document = context.Documents.First();
            var senderEmployee = context.Employees.First();
            var senderAdress = context.Addresses.First();
            var receiverEmployee = senderEmployee;
            var receiverAddress = senderAdress;
            var status = context.Statuses.First();
            var dateTime = DateTime.UtcNow;
            var documentStatus = new DocumentStatus(document, senderEmployee, senderAdress, receiverEmployee, receiverAddress, status, dateTime);
            context.Add(documentStatus);
            await context.SaveChangesAsync();
        }

        private static async Task InsertEmployee()
        {
            await using var context = new DocumentStorageContext();

            var position = context.Positions.First();
            var employee = new Employee("Zeus", position);
            context.Add(employee);
            await context.SaveChangesAsync();
        }

        private static async Task InsertPosition()
        {
            await using var context = new DocumentStorageContext();
            context.Positions.AddRange(
                new Position("First"),
                new Position("Second")
                );
            await context.SaveChangesAsync();
        }

        private static async Task InsertStatus()
        {
            await using var context = new DocumentStorageContext();
            context.Statuses.AddRange(
                new Status("Done"),
                new Status("Not done")
                );
            await context.SaveChangesAsync();
        }
    }
}
