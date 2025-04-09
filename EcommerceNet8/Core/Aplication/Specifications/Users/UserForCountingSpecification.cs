using EcommerceNet8.Core.Domain;

namespace EcommerceNet8.Core.Aplication.Specifications.Users
{
    public class UserForCountingSpecification : BaseSpecification<Usuario>
    {
        public UserForCountingSpecification(UserSpecificationParams userParams) : base(
            x => (string.IsNullOrEmpty(userParams.Search) || x.Name!.Contains(userParams.Search) ||
            x.LastName!.Contains(userParams.Search) || x.Email!.Contains(userParams.Search))

            )
        {
        }
    }
}
