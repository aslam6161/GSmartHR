using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSmartHR.Repository.DapperHelper.Helper
{
    public class CSharpDataTypes
    {
        public List<Type> Types { get; set; }
        public CSharpDataTypes()
        {
            Types = new List<Type>();

            Types.Add(typeof(Byte));
            Types.Add(typeof(SByte));

            Types.Add(typeof(Int16));
            Types.Add(typeof(UInt16));


            Types.Add(typeof(Int32));
            Types.Add(typeof(UInt32));

            Types.Add(typeof(Int64));
            Types.Add(typeof(UInt64));


            Types.Add(typeof(float));
            Types.Add(typeof(Double));
            Types.Add(typeof(Decimal));

            Types.Add(typeof(Char));
            Types.Add(typeof(String));

            Types.Add(typeof(bool));

            Types.Add(typeof(Guid));

            Types.Add(typeof(DateTime));


            Types.Add(typeof(Byte?));
            Types.Add(typeof(SByte?));

            Types.Add(typeof(Int16?));
            Types.Add(typeof(UInt16?));


            Types.Add(typeof(Int32?));
            Types.Add(typeof(UInt32?));

            Types.Add(typeof(Int64?));
            Types.Add(typeof(UInt64?));


            Types.Add(typeof(float?));
            Types.Add(typeof(Double?));
            Types.Add(typeof(Decimal?));

            Types.Add(typeof(Char?));

            Types.Add(typeof(bool));

            Types.Add(typeof(Guid?));

            Types.Add(typeof(DateTime?));
        }

        public bool InDataTypes(Type type)
        {
            return Types.Any(x => x == type);
        }
    }
}
