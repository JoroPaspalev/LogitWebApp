
using System;

namespace LogitWebApp.Data.CommonModels
{
    public interface IAuditInfo
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
