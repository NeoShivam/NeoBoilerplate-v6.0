using NeoBoilerplate.Identity;
using NeoBoilerplate.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;

namespace NeoBoilerplate.API.IntegrationTests.Base
{
    public class DbFixture : IDisposable
    {
        private readonly  ApplicationDbContext _applicationDbContext;
        private readonly IdentityDbContext _identityDbContext;
        public readonly string ApplicationDbName = $"Application-{Guid.NewGuid()}";
        public readonly string IdentityDbName = $"Identity-{Guid.NewGuid()}";
        public readonly string HealthCheckDbName = $"HealthCheck";
        public readonly string HealthCheckConnString;
        public readonly string ApplicationConnString;
        public readonly string IdentityConnString;

        private bool _disposed;

        public DbFixture()
        {
            var applicationBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var identityBuilder = new DbContextOptionsBuilder<IdentityDbContext>();
            //#if (Database == "MSSQL")
            ApplicationConnString = $"Server=localhost,1433;Database={ApplicationDbName};User=sa;Password=2@LaiNw)PDvs^t>L!Ybt]6H^%h3U>M;TrustServerCertificate=True;";
                    IdentityConnString = $"Server=localhost,1433;Database={IdentityDbName};User=sa;Password=2@LaiNw)PDvs^t>L!Ybt]6H^%h3U>M;TrustServerCertificate=True;";
                    HealthCheckConnString = $"Server=localhost,1433;Database={HealthCheckDbName};User=sa;Password=2@LaiNw)PDvs^t>L!Ybt]6H^%h3U>M;TrustServerCertificate=True;";
                    applicationBuilder.UseSqlServer(ApplicationConnString);
                    identityBuilder.UseSqlServer(IdentityConnString);
            //#endif
            //#if (Database == "PGSQL")
            ApplicationConnString = $"Server=localhost;Port=5430;Database={ApplicationDbName};User Id=root;Password=root;CommandTimeout = 300;";
                    IdentityConnString = $"Server=localhost;Port=5430;Database={IdentityDbName};User Id=root;Password=root;CommandTimeout = 300;";

                    HealthCheckConnString = $"Server=localhost;Port=5430;Database={HealthCheckDbName};User Id=root;Password=root;CommandTimeout = 300;";
                    applicationBuilder.UseNpgsql(ApplicationConnString);
                    identityBuilder.UseNpgsql(IdentityConnString);
            //#endif
            //#if (Database == "MySQL")
            ApplicationConnString = $"Server=localhost;Port=3306;Database={ApplicationDbName};Userid=root;Password=root;";
                    IdentityConnString = $"Server=localhost;Port=3306;Database={IdentityDbName};Userid=root;Password=root;";
                    HealthCheckConnString = $"Server=localhost;Port=3306;Database={HealthCheckDbName};Userid=root;Password=root;";
                    applicationBuilder.UseMySql(ApplicationConnString, new MySqlServerVersion(new Version(8, 0, 11)));
                    identityBuilder.UseMySql(IdentityConnString, new MySqlServerVersion(new Version(8, 0, 11)));
            //#endif
            //#if (Database == "SQLite")
            ApplicationConnString = $"Data Source=..//..//..//db//{ApplicationDbName}";
            IdentityConnString = $"Data Source=..//..//..//db//{IdentityDbName}";
            HealthCheckConnString = $"Data Source=..//..//..//db//{HealthCheckDbName}";
            applicationBuilder.UseSqlite(ApplicationConnString);
            identityBuilder.UseSqlite(IdentityConnString);
            //#endif
            _applicationDbContext = new ApplicationDbContext(applicationBuilder.Options);
            _applicationDbContext.Database.EnsureCreated();

            _identityDbContext = new IdentityDbContext(identityBuilder.Options);
            _identityDbContext.Database.EnsureCreated();

            SeedIdentity seed = new SeedIdentity(_identityDbContext);
            seed.SeedUsers();
            seed.SeedUserRoles();
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // remove the temp db from the server once all tests are done
                    _applicationDbContext.Database.EnsureDeleted();
                    _identityDbContext.Database.EnsureDeleted();
                }
                _disposed = true;
            }
        }
    }

    [CollectionDefinition("Database")]
    public class DatabaseCollection : ICollectionFixture<DbFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
