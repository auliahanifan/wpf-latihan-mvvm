using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WpfLatihan.Models;

namespace WpfLatihan.Data
{
    // SQLite Data Context EF
    [DbConfigurationType(typeof(SqliteDbConfiq))]
    class SqliteDbContext : DbContext
    {
        private static readonly string DataSourcePath = new SqliteDbConfiq().GetDataSource();
        public SqliteDbContext() :
            base(new SQLiteConnection()
            {
                ConnectionString = new SQLiteConnectionStringBuilder() { DataSource = DataSourcePath, ForeignKeys = true }.ConnectionString
            }, true)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Pelanggan> Pelanggan { get; set; }
    }

    // SQLite Config
    public class SqliteDbConfiq : DbConfiguration
    {
        private static readonly string DbSQLiteName = "latihan.db";
        private static readonly string DataSourcePath = $"{System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\{DbSQLiteName}";

        public SqliteDbConfiq()
        {
            SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
            SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);
            SetProviderServices("System.Data.SQLite", (DbProviderServices)SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices)));
        }

        public string GetDataSource()
        {
            return DataSourcePath;
        }
    }
}
