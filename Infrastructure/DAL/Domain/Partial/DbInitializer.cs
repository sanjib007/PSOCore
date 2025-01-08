using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Domain.Partial
{
    public static class DbInitializerExtension
    {
        public static IApplicationBuilder UseSeedDatabase(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<E_PSOContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {

            }

            return app;
        }
    }
    public static class DbInitializer
    {
        public static void Initialize(E_PSOContext context)
        {
            try
            {
                context.Database.EnsureCreated();

                // Look for any Client.
                if (context.Con_AccountApiAuthorizations.Any())
                {
                    return;   // DB has been seeded

                }

                // client 
                // pass: 1234

                //var client = new Client
                //{
                //    FirstName = "Mohammed",
                //    LastName = "Tanbir",
                //    Email = "ovibhuiyan43@gmail.com",
                //    Password = "57a0f401fbbe2df3bd258d89e5a73808fa51a10d6a8188f90aab1a988770e4b7ef533198adb539547df8887bc743d6bc76fb6ab5a8ee67929834b4066d70df8e",
                //    IsActive = true,
                //    AddedBy = 0,
                //    AddedDate = DateTime.UtcNow

                //};
                //// vehicle
                //var vehicle = new Vehicle
                //{
                //    DeviceId = "demodeviceId1",
                //    Name = "Car"
                //};
                //// position
                //var position1 = new Position
                //{
                //    Latitude = 32.55549194555017,
                //    Longitude = 131.6753259818856,
                //    AddedDate = DateTime.Parse("01/06/2021 2:02")
                //};
                //var position2 = new Position
                //{
                //    Latitude = 35.681610805430566,
                //    Longitude = 139.76923880376333,
                //    AddedDate = DateTime.Parse("02/06/2021 2:02")
                //};
                //var position3 = new Position
                //{
                //    Latitude = 35.681610805430566,
                //    Longitude = 139.76923880376333,
                //    AddedDate = DateTime.Parse("03/06/2021 2:02")
                //};
                //vehicle.Positions.Add(position1);
                //vehicle.Positions.Add(position2);
                //vehicle.Positions.Add(position3);
                //client.Vehicles.Add(vehicle);

                //var result_client = context.Clients.Add(client);
                //context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(" >>>>  Exception Seed  <<<<<" + ex);
                throw;
            }
        }
    }
}

