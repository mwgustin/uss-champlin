using Ardalis.Specification;

namespace Champlin.Common
{
    public class GetAllCrewSpec : BaseSpecification<Crew>
    {
        public GetAllCrewSpec()
        {
            AddCriteria(x => true);
        }
    }
}