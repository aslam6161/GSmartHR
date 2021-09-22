using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSmartHR.Repository.DapperHelper.Helper
{
  public class IgnoreFieldResult
    {
        public object Object { get; set; }
        public List<string> Properties { get; set; }
    }
}
