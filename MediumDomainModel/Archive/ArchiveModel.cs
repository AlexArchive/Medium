using System.Collections.Generic;
using System.Linq;

namespace Medium.DomainModel.Archive
{
    public class ArchiveModel
    {
        public IEnumerable<IGrouping<int, IGrouping<string, PostModel>>> Years { get; set; }
    }
}