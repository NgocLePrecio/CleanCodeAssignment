using System;

// KeepIt
namespace TrickyBookStore.Services.Payment
{
    public interface IPaymentService
    {
        double GetPaymentAmount(long customerId, int month, int year);
    }
}
