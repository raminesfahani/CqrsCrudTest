using System;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository CustomerRepository { get; }
        Task Save();
    }
}
