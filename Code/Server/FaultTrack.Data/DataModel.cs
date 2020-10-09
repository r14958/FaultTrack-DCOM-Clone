namespace FaultTrack.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    internal class DataModel : DbContext
    {
        public DataModel() : base("name=DataModel")
        {
        }

        public DataModel(string connectionString) : base(connectionString)
        {
        }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectCollection> ProjectCollections { get; set; }
        public virtual DbSet<ProjectVersion> ProjectVersions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DataModel>(null);
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}