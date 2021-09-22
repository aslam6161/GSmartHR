using GSmartHR.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GSmartHR.Core.Domain
{
    public class BaseActivity : BaseEntity
    {
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime UpdatedDate{ get; set; }

        public User CreatedByObj { get; set; }
        public User UpdatedByObj { get; set; }
    }
}
