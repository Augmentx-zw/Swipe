using System;

namespace ArkPotal.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
