using System;

#if CLIENT
namespace Proxy
#else
namespace Demo.Model
#endif
{
    public partial class AllDataTypesModel
    {
        public static AllDataTypesModel FilledAllPropertiesInstance = new AllDataTypesModel
        {
            Bool = true,
            Byte = 124,
            Char = 'C',
            DateTime = new DateTime(2015, 12, 14),
            DateTimeOffset = new DateTimeOffset(2015, 12, 31, 23, 59, 59, TimeSpan.FromHours(7)),
            Decimal = new decimal(12345679.654987),
            Double = -321654.978987,
            Float = 0.00001f,
            Guid = Guid.Parse("{0CF98FA1-438B-40E4-855C-99C1A79D2AF6}"),
            Int = -654564,
            Long = 5432165498798797897,
            NullableDateTime = null,
            NullableDouble = -654.9687,
            NullableGuid = Guid.Empty,
            NullableInt = null,
            Sbyte = 5,
            Short = 789,
            String = "str",
            TimeSpan = TimeSpan.FromSeconds(1024),
            Uint = 987,
            Ulong = 8798798798,
            Ushort = 123
        };

        public const string Const = "Const";
        public static string StaticProperty { get; set; }
        public string GetOnlyProperty { get; }
        protected int ProtectedProperty { get; set; }
        public int PrivateSetterProperty { get; private set; }
        private int PrivateProperty { get; set; }

        public int PublicField;

    }
}