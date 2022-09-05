using WebApplication1.Models.Interfaces;

namespace WebApplication1.Models.Implement
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(MyDbContext db) : base(db)
        {
        }
    }
}
