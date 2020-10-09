namespace FaultTrack.Business
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Data;

    internal class ProjectTreeDeserializer
    {
        public IEnumerable<ProjectCollection> Deserialize(IEnumerable<ProjectTreeNode> items)
        {
            var collectionLookup = new Dictionary<int, ProjectCollection>();
            var projectLookup    = new Dictionary<int, Project>();
            var versionLookup    = new Dictionary<int, ProjectVersion>();

            DeserializeProjectCollections(collectionLookup, items.Where(p => p.Type == ProjectTreeNodeType.ProjectCollection));
            DeserializeProjects(collectionLookup, projectLookup, items.Where(p => p.Type == ProjectTreeNodeType.Project));
            DeserializeProjectVersions(projectLookup, versionLookup, items.Where(p => p.Type == ProjectTreeNodeType.ProjectVersion));
            
            return collectionLookup.Values;
        }

        private void DeserializeProjectCollections(Dictionary<int, ProjectCollection> collectionLookup, IEnumerable<ProjectTreeNode> items)
        {
            foreach (var item in items)
            {
                collectionLookup.Add(item.ProjectCollectionId,
                                     new ProjectCollection
                                     {
                                         ProjectCollectionId = item.ProjectCollectionId,
                                         Name = item.Name
                                     });
            }
        }

        private void DeserializeProjects(Dictionary<int, ProjectCollection> collectionLookup, Dictionary<int, Project> projectLookup, IEnumerable<ProjectTreeNode> items)
        {
            foreach (var item in items)
            {
                Debug.Assert(item.ProjectId.HasValue);

                projectLookup.Add(item.ProjectId.Value,
                                  new Project
                                  {
                                      ProjectId           = item.ProjectId.Value,
                                      ProjectCollectionId = item.ProjectCollectionId,
                                      ParentProjectId     = item.ParentProjectId,
                                      Name                = item.Name
                                  });
            }

            foreach (var item in projectLookup.Values)
            {
                ProjectCollection parentCollection;

                if (collectionLookup.TryGetValue(item.ProjectCollectionId, out parentCollection))
                {
                    //item.ProjectCollection = parentCollection;

                    if (!item.ParentProjectId.HasValue)
                    {
                        parentCollection.Projects.Add(item);
                    }
                }

                if (item.ParentProjectId.HasValue)
                {
                    Project parentProject;

                    if (projectLookup.TryGetValue(item.ParentProjectId.Value, out parentProject))
                    {
                        //item.ParentProject = parentProject;
                        parentProject.Projects.Add(item);
                    }
                }
            }
        }

        private void DeserializeProjectVersions(Dictionary<int, Project> projectLookup, Dictionary<int, ProjectVersion> versionLookup, IEnumerable<ProjectTreeNode> items)
        {
            foreach (var item in items)
            {
                Debug.Assert(item.ProjectId.HasValue);
                Debug.Assert(item.ProjectVersionId.HasValue);

                versionLookup.Add(item.ProjectVersionId.Value,
                                  new ProjectVersion
                                  {
                                      ProjectId              = item.ProjectId.Value,
                                      ProjectVersionId       = item.ProjectVersionId.Value,
                                      ParentProjectVersionId = item.ParentProjectVersionId,
                                      Name                   = item.Name
                                  });
            }

            foreach (var item in versionLookup.Values)
            {
                Project parentProject;

                if (projectLookup.TryGetValue(item.ProjectId, out parentProject))
                {
                    //item.Project = parentProject;

                    if (!item.ParentProjectVersionId.HasValue)
                    {
                        parentProject.ProjectVersions.Add(item);
                    }
                }

                if (item.ParentProjectVersionId.HasValue)
                {
                    ProjectVersion parentVersion;

                    if (versionLookup.TryGetValue(item.ParentProjectVersionId.Value, out parentVersion))
                    {
                        //item.ParentProjectVersion = parentVersion;
                        parentVersion.ProjectVersions.Add(item);
                    }
                }
            }
        }
    }
}