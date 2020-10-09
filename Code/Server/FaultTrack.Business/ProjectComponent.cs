namespace FaultTrack.Business
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Data;

    public class ProjectComponent : IDisposable
    {
        private readonly IDataContext context;
        private bool disposed;

        public ProjectComponent(IDataContext context)
        {
            this.context = context;
        }

        public void CreateProjectCollection(ProjectCollection collection)
        {
            ValidateObject(collection);

            if (context.ProjectCollections.Any(p => p.Name.Equals(collection.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new BusinessException("A collection with the specified name already exists.");
            }

            context.Add(collection);
            context.Commit();
        }

        public void CreateProject(Project project)
        {
            ValidateObject(project);

            if (project.ParentProjectId.HasValue && !ParentCollectionIsSame(project))
            {
                throw new BusinessException(Strings.ProjectCollectionDoesNotOwnParentAndChild);
            }

            context.Add(project);
            context.Commit();
        }

        public void DeleteProject(Project project)
        {
            if (project.Projects.Any() || project.ProjectVersions.Any())
            {
                throw new BusinessException(Strings.CannotDeleteProjectWithChildren);
            }

            context.Delete(project);
            context.Commit();
        }

        public void DeleteProjectCollection(ProjectCollection collection)
        {
            if (collection.Projects.Any())
            {
                throw new BusinessException(Strings.CannotDeleteProjectCollectionWithChildren);
            }

            context.Delete(collection);
            context.Commit();
        }

        public IEnumerable<ProjectCollection> GetProjectTree()
        {
            IEnumerable<ProjectTreeNode> items = context.SqlQuery<ProjectTreeNode>("SELECT * FROM [dbo].[viewProjectTree] ORDER BY [Name]");

            var deserializer = new ProjectTreeDeserializer();

            var tree = deserializer.Deserialize(items);

            return tree;
        }

        public void RenameProjectCollection(ProjectCollection collection)
        {
            ValidateObject(collection);

            var entity = context.Find<ProjectCollection>(collection.ProjectCollectionId);

            if (entity == null)
            {
                throw new ArgumentException();
            }

            if (context.ProjectCollections.Where(p => p.ProjectCollectionId != collection.ProjectCollectionId).Any(p => p.Name.Equals(collection.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new BusinessException("A collection with the specified name already exists.");
            }

            entity.Name = collection.Name;

            context.Update(entity);
            context.Commit();
        }

        public void RenameProject(Project project)
        {
            ValidateObject(project);

            context.Update(project);
            context.Commit();
        }

        public void UpdateProject(Project project)
        {
            ValidateObject(project);

            var entity = context.Find<Project>(project.ProjectId);

            if (entity == null)
            {
                throw new ArgumentException();
            }

            if (project.ParentProjectId.HasValue && !ParentCollectionIsSame(project))
            {
                throw new BusinessException(Strings.ProjectCollectionDoesNotOwnParentAndChild);
            }

            entity.ProjectCollectionId = project.ProjectCollectionId;
            entity.ParentProjectId = project.ParentProjectId;
            entity.Name = project.Name;

            context.Update(entity);
            context.Commit();
        }

        private bool ParentCollectionIsSame(Project project)
        {
            int collectionId = context.Projects.Where(p => p.ProjectId == project.ParentProjectId.Value)
                                                 .Select(p => p.ProjectCollectionId)
                                                 .Single();

            return collectionId == project.ProjectCollectionId;
        }

        private void ValidateObject(object obj)
        {
            var context = new ValidationContext(obj);

            try
            {
                Validator.ValidateObject(obj, context);
            }
            catch (ValidationException ex)
            {
                throw new BusinessException($"Validation failed for type {obj.GetType().Name}. See the inner exception details.", ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed || !disposing)
            {
                return;
            }

            context.Dispose();

            disposed = true;
        }
    }
}