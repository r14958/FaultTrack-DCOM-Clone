namespace FaultTrack.Web.Api.Controllers
{
    using System.Collections.Generic;
    using Data;
    using System.Web.Http;
    using System.Web.Script.Serialization;
    using Business;

    public class ProjectController : ApiController
    {
        private readonly ProjectComponent component;
        private bool disposed;
        
        public ProjectController()
        {
            component = new ProjectComponent(new DataContext());
        }

        public ProjectController(ProjectComponent component)
        {
            this.component = component;
        }

        [HttpPost]
        public CreateProjectCollectionResponse CreateProjectCollection(CreateProjectCollectionRequest request)
        {
            var collection = new ProjectCollection
                             {
                                 Name = request.Name
                             };

            component.CreateProjectCollection(collection);

            return new CreateProjectCollectionResponse
                   {
                       NewId = collection.ProjectCollectionId,
                       Created = true
                   };
        }

        [HttpPost]
        public CreateProjectResponse CreateProject(CreateProjectRequest request)
        {
            var project = new Project
                          {
                              ProjectCollectionId = request.ProjectCollectionId,
                              ParentProjectId = request.ParentProjectId,
                              Name = request.Name
                          };

            component.CreateProject(project);

            return new CreateProjectResponse
                   {
                       ProjectId = project.ProjectId
                   };
        }

        [HttpPost]
        public void UpdateProject(UpdateProjectRequest request)
        {
            var project = new Project
                          {
                              ProjectId           = request.ProjectId,
                              ProjectCollectionId = request.ProjectCollectionId,
                              ParentProjectId     = request.ParentProjectId,
                              Name                = request.Name
                          };

            component.UpdateProject(project);
        }

        [HttpGet]
        public GetProjectTreeResponse GetProjectTree()
        {
            var tree       = component.GetProjectTree();
            var serializer = new JavaScriptSerializer();
            var json       = serializer.Serialize(tree);
            var items      = serializer.Deserialize<IEnumerable<ProjectCollectionTreeNode>>(json);

            return new GetProjectTreeResponse
                   {
                       Items = items
                   };
        }

        [HttpPost]
        public void RenameProjectCollection(RenameProjectCollectionRequest request)
        {
            var collection = new ProjectCollection
                             {
                                 ProjectCollectionId = request.Id,
                                 Name = request.NewName
                             };

            component.RenameProjectCollection(collection);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed || !disposing)
            {
                return;
            }

            component.Dispose();

            base.Dispose(true);

            disposed = true;
        }
    }
}