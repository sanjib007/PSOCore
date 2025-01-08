using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Utility.Extentions;

namespace DAL.Domain
{
    public partial class E_PSOContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContext;

        public E_PSOContext(DbContextOptions<E_PSOContext> options, IHttpContextAccessor httpContext) : base(options)
        {
            _httpContext = httpContext;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            try
            {
                // When insert
                var addedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();
                addedEntities.ForEach(E =>
                {
                    //try
                    //{

                    //}
                    //catch (Exception)
                    //{

                    //    var checkIsActive = E.Members.Any(e => e.Metadata.Name == "IsActive");
                    //    if (checkIsActive)
                    //        E.Property("IsActive").CurrentValue = true;
                    //}

                    var checkCreatedDate = E.Members.Any(e => e.Metadata.Name == "IsActive");
                    if (checkCreatedDate)
                        E.Property("IsActive").CurrentValue = true;
                    var checkCreatedAt = E.Members.Any(e => e.Metadata.Name == "CreatedAt");
                    if (checkCreatedAt)
                        E.Property("CreatedAt").CurrentValue = DateTime.Now;

                    var checkCreatedBy = E.Members.Any(e => e.Metadata.Name == "CreatedBy");
                    if (checkCreatedBy)
                        E.Property("CreatedBy").CurrentValue = _httpContext.CurrentUserId().ToString();
                });

                // When update 
                var editedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();
                editedEntities.ForEach(E =>
                {

                    var checkUpdatedDate = E.Members.Any(e => e.Metadata.Name == "LastUpdatedAt");
                    if (checkUpdatedDate)
                        E.Property("LastUpdatedAt").CurrentValue = DateTime.Now;

                    var checkUpdatedBy = E.Members.Any(e => e.Metadata.Name == "LastUpdatedBy");
                    if (checkUpdatedBy)
                        E.Property("LastUpdatedBy").CurrentValue = _httpContext.CurrentUserId().ToString();

                });
            }
            catch (Exception ex) { };

            //return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
