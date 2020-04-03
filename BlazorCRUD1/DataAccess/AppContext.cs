using Microsoft.EntityFrameworkCore;

namespace BlazorCRUD1.DataAccess
{
    public class AppContext : DbContext
    {
        public AppContext() { }

        public AppContext(DbContextOptions<AppContext> options) : base(options) { }
    }
}
