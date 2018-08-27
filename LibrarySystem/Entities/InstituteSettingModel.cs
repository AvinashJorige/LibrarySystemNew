using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class InstituteSettingModel : Entities.Entity
    {
        public int institute_id { get; set; }
        public string institute_name { get; set; }
        public string institute_address { get; set; }
        public string institute_email { get; set; }
        public string institute_logo { get; set; }
        public string institute_favicon { get; set; }
        public string institute_phone { get; set; }
        public string institute_currency { get; set; }
        public string institute_fk_currency { get; set; }
        public string institute_terms_conditions { get; set; }
        public bool institute_is_active { get; set; }
    }
}
