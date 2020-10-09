using System.Collections.Generic;

namespace FaultTrack.Web
{
    public class GetProjectTreeResponse
    {
        public IEnumerable<ProjectCollectionTreeNode> Items { get; set; }
    }
}
