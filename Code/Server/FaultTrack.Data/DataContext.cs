namespace FaultTrack.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    
    /// <summary>
    /// Data access context for accessing data entities.
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        public DataContext() : base("name=FaultTrack")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        /// <param name="connectionString">A <see cref="string"/> containing connection information.</param>
        public DataContext(DbContextOptions<DataContext> options) : base(connectionString)
        {
        }

        /// <summary>
        /// Gets or sets the collection that contains <see cref="Project"/> entities.
        /// </summary>
        public virtual DbSet<Project> Projects
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the collection that contains <see cref="ProjectCollection"/> entities.
        /// </summary>
        public virtual DbSet<ProjectCollection> ProjectCollections
        {
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets the collection that contains <see cref="User"/> entities.
        /// </summary>
        public virtual DbSet<ProjectVersion> ProjectVersions
        {
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets the collection that contains <see cref="Role"/> entities.
        /// </summary>
        public virtual DbSet<Role> Roles
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the collection that contains <see cref="User"/> entities.
        /// </summary>
        public virtual DbSet<User> Users
        {
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets the collection that contains <see cref="UserRole"/> entities.
        /// </summary>
        public virtual DbSet<UserRole> UserRoles
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the collection that contains <see cref="UserToken"/> entities.
        /// </summary>
        public virtual DbSet<UserToken> UserTokens 
        { 
            get; 
            set;
        }

        /// <inheritdoc />
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DataContext>(null);
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}