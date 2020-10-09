namespace FaultTrack.Web
{
    using System;
    using System.Threading.Tasks;

    public class ProjectService : ApiService
    {
        public ProjectService(Uri account) : base(account)
        {
        }

        public virtual async Task RenameProjectCollectionAsync(RenameProjectCollectionRequest request)
        {
            await Post("api/Project/RenameProjectCollection", request);
        }

        public virtual async Task<CreateProjectCollectionResponse> CreateProjectCollectionAsync(CreateProjectCollectionRequest request)
        {
            return await Post<CreateProjectCollectionRequest, CreateProjectCollectionResponse>("api/Project/CreateProjectCollection", request);
        }

        public virtual async Task<CreateProjectResponse> CreateProjectAsync(CreateProjectRequest request)
        {
            return await Post<CreateProjectRequest, CreateProjectResponse>("api/Project/CreateProject", request);
        }
        
        public virtual async Task<GetProjectTreeResponse> GetProjectTreeAsync()
        {
            return await Get<GetProjectTreeResponse>("api/Project/GetProjectTree");
        }

        public virtual async Task UpdateProjectAsync(UpdateProjectRequest request)
        {
            await Post("api/Project/UpdateProject", request);
        }
    }
}