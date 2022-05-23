using TrickyBookStore.Models;

// KeepIt
namespace TrickyBookStore.Services.Customers
{
    public interface ICustomerService
    {
        Customer GetCustomerById(long id);
    }
}
