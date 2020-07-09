using Ardalis.Specification;

namespace Champlin.Common
{
    public class GetCrewByUrlSpec : BaseSpecification<Crew>
    {
        public GetCrewByUrlSpec(string url)
        {
            AddCriteria(x => x.OriginalUrl == url);
        }
    }
}