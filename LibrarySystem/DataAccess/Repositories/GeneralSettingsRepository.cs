using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Entities;

namespace DataAccess.Repositories
{
    internal class GeneralSettingsRepository : RepositoryBase, IGeneralSettingsRepository
    {
        public GeneralSettingsRepository(IDbTransaction transaction) : base(transaction)
        { }

        public IEnumerable<InstituteSettingModel> All()
        {
            return Connection.Query<InstituteSettingModel>(
                "SELECT * FROM tbl_institute_info",
                transaction: Transaction
            ).ToList();
        }

        public InstituteSettingModel Find(int id)
        {
            return Connection.Query<InstituteSettingModel>(
                "SELECT * FROM tbl_institute_info WHERE institute_id = @instituteId",
                param: new { instituteId = id },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Add(InstituteSettingModel entity)
        {
            string columns = "institute_name, institute_address, institute_email, institute_contact, institute_logo, institute_favicon, institute_fk_currency, institute_terms_conditions";
            string replaceColumns = "@Institute_name, @Institute_address, @Institute_email, @Institute_contact, @Institute_logo, @Institute_favicon, @Institute_fk_currency, @Institute_terms_conditions";


            entity.institute_id = Connection.ExecuteScalar<int>(
                "INSERT INTO tbl_institute_info(" + columns + ") VALUES(" + replaceColumns + "); SELECT SCOPE_IDENTITY()",
                param: new
                {
                    Institute_name = entity.institute_name,
                    Institute_address = entity.institute_address,
                    Institute_email = entity.institute_name,
                    Institute_contact = entity.institute_name,
                    Institute_logo = entity.institute_name,
                    Institute_favicon = entity.institute_name,
                    Institute_fk_currency = entity.institute_name,
                    Institute_terms_conditions = entity.institute_name
                },
                transaction: Transaction
            );
        }

        public void Update(InstituteSettingModel entity)
        {
            string updateColumns = "Institute_name= @institute_name" +
                                   "Institute_address= @institute_address" +
                                   "Institute_email= @institute_email" +
                                   "Institute_contact= @institute_contact" +
                                   "Institute_logo= @institute_logo" +
                                   "Institute_favicon= @institute_favicon" +
                                   "Institute_fk_currency= @institute_fk_currency" +
                                   "Institute_terms_conditions= @institute_terms_conditions";

            Connection.Execute(
                "UPDATE tbl_institute_info SET " + updateColumns + " WHERE institute_id = @instituteId",
                param: new
                {
                    InstituteId = entity.institute_id,
                    Institute_name = entity.institute_name,
                    Institute_address = entity.institute_address,
                    Institute_email = entity.institute_email,
                    Institute_contact = entity.institute_phone,
                    Institute_logo = entity.institute_logo,
                    Institute_favicon = entity.institute_favicon,
                    Institute_fk_currency = entity.institute_fk_currency,
                    Institute_terms_conditions = entity.institute_terms_conditions
                },
                transaction: Transaction
            );
        }

        public void Delete(int id)
        {
            Connection.Execute(
                "DELETE FROM tbl_institute_info WHERE institute_id = @instituteId",
                param: new { instituteId = id },
                transaction: Transaction
            );
        }

        public void Delete(InstituteSettingModel entity)
        {
            Delete(entity.institute_id);
        }

        public InstituteSettingModel FindByName(string name)
        {
            return Connection.Query<InstituteSettingModel>(
                "SELECT * FROM tbl_institute_info WHERE institute_name = @Name",
                param: new { Name = name },
                transaction: Transaction
            ).FirstOrDefault();
        }
    }
}
