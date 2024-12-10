using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Models.BusinessLogic
{
    public class DatabaseClass
    {
        #region Context
        public DentCareEntities db { get; set; }
        #endregion
        #region Constructor
        public DatabaseClass(DentCareEntities db)
        {
            this.db = db;
        }
        #endregion
    }
}
