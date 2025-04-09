using EcommerceNet8.Core.Domain;

namespace EcommerceNet8.Core.Aplication.Specifications.Users
{
    public class UserSpecification : BaseSpecification<Usuario>
    {
        public UserSpecification(UserSpecificationParams userParams) : base (
            x=>(string.IsNullOrEmpty(userParams.Search) || x.Name!.Contains(userParams.Search) ||
            x.LastName!.Contains(userParams.Search)|| x.Email!.Contains(userParams.Search))
            
            )
        {

            ApplyPaging(userParams.PageSize * (userParams.PageIndex - 1), userParams.PageSize);


            if (!string.IsNullOrEmpty(userParams.Sort))
            {
                switch (userParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBY(p => p.Name!);
                        break;

                    case "nameDesc":
                        AddOrderBYDescending(p => p.Name!);
                        break;

                    case "LastAsc":
                        AddOrderBY(p => p.LastName!);
                        break;


                    case "LastDesc":
                        AddOrderBYDescending(p => p.LastName!);
                        break;


                    default:AddOrderBY(p => p.Name!);
                            break;
                }
            }
            else
            {
                AddOrderBYDescending(p => p.Name!);
            }
        }
    }
}
