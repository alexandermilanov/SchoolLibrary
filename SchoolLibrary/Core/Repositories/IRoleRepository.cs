using SchoolLibrary.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace SchoolLibrary.Core.Repositories
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
