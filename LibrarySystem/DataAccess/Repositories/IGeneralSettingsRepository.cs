using System;
using System.Collections.Generic;
using System.Text;
using Entities;

namespace DataAccess.Repositories
{
    public interface IGeneralSettingsRepository
    {
        void Add(InstituteSettingModel entity);
        IEnumerable<InstituteSettingModel> All();
        void Delete(int id);
        void Delete(InstituteSettingModel entity);
        InstituteSettingModel Find(int id);
        InstituteSettingModel FindByName(string name);
        void Update(InstituteSettingModel entity);
    }
}
