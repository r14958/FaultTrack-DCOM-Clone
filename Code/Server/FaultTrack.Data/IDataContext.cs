namespace FaultTrack.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IDataContext : IDisposable
    {
        IQueryable<ProjectCollection> ProjectCollections { get; }
        IQueryable<Project> Projects { get; }
        IQueryable<ProjectVersion> ProjectVersions { get; }
        IQueryable<User> Users { get; }
        IQueryable<UserToken> UserTokens { get; }
        T Find<T>(params object[] keyValues) where T : class;
        void Add<T>(T item) where T : class;
        void Delete<T>(T item) where T : class;
        void Update<T>(T item) where T : class;
        void Commit();
        IEnumerable<T> SqlQuery<T>(string sql);
    }
}