using SchoolLibrary.Areas.Identity.Data;
using SchoolLibrary.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace SchoolLibrary.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
