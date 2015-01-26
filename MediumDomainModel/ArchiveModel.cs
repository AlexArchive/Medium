using System.Collections.Generic;
using System.Linq;

namespace Medium.DomainModel
{
    public class ArchiveModel
    {
        public IEnumerable<IGrouping<int, IGrouping<string, PostModel>>> Years { get; set; }
    }
}