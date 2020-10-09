namespace FaultTrack.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public sealed class DataContext : IDataContext
    {
        private readonly DataModel model;
        private bool disposed;

        public DataContext()
        {
            model = new DataModel();
        }

        public DataContext(string connectionString)
        {
            model = new DataModel(connectionString);
        }

        public IQueryable<ProjectCollection> ProjectCollections => model.ProjectCollections;
        public IQueryable<Project> Projects                     => model.Projects;
        public IQueryable<ProjectVersion> ProjectVersions       => model.ProjectVersions;
        public IQueryable<User> Users                           => model.Users;
        public IQueryable<UserToken> UserTokens                 => model.UserTokens;

        public T Find<T>(params object[] keyValues) where T : class
        {
            return model.Set<T>().Find(keyValues);
        }

        public IEnumerable<T> SqlQuery<T>(string sql)
        {
            return model.Database.SqlQuery<T>(sql);
        }

        public void Add<T>(T item) where T : class
        {
            model.Set<T>().Add(item);
        }

        public void Delete<T>(T item) where T : class
        {
            model.Set<T>().Remove(item);
        }

        public void Update<T>(T item) where T : class
        {
            var entry = model.Entry(item);

            if (entry.State == EntityState.Detached)
            {
                ThrowEntityDetachedException<T>();
            }
            
            entry.State = EntityState.Modified;
        }

        public void Commit()
        {
            model.SaveChanges();
        }

        private static void ThrowEntityDetachedException<T>()
        {
            throw new InvalidOperationException($"Instance of {typeof(ProjectCollection).Name} is not attached to the context.");
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing || disposed)
            {
                return;
            }

            model.Dispose();

            disposed = true;
        }

        #endregion
    }
}