using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Repositories;

namespace DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IGeneralSettingsRepository GeneralSettingsRepository { get; }

        void Commit();
    }
}
