using System;
using System.Data.Entity;
using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public partial class Context : DbContext
    {
        public Context() : base("name=Context")
        {
        }

        public virtual DbSet<Catalog> Catalogs { get; set; }

        private void AddSqlClientReference()
        {
            //https://stackoverflow.com/questions/18455747/no-entity-framework-provider-found-for-the-ado-net-provider-with-invariant-name
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
            if (type == null)
                throw new Exception("Do not remove, ensures static reference to System.Data.Entity.SqlServer");
        }
    }
}
