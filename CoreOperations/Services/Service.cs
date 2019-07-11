using CoreOperations.Models;
using CoreOperations.Repositories.Implementations;
using CoreOperations.Repositories.Interfaces;

namespace CoreOperations.Services
{
    public class Service
    {
        public readonly IUnitOfWork UnitOfWork;

        public Service()
        {
            UnitOfWork = new UnitOfWork<adventureworksContext>();

        }
    }
}
