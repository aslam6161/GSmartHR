using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSmartHR.Web.Infrastructure
{
    public class BaseGSmartHREntityModel
    {
        public virtual Guid? Id { get; set; }

        public bool IsEditMode(Guid? id)
        {
            return id.HasValue && id != Guid.Empty;
        }
    }
}
