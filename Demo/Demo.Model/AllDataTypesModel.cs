using System;

namespace Demo.Model
{
    public partial class AllDataTypesModel
    {
        public byte Byte { get; set; }
        public sbyte Sbyte { get; set; }
        public short Short { get; set; }
        public ushort Ushort { get; set; }
        public int Int { get; set; }
        public uint Uint { get; set; }
        public long Long { get; set; }
        public ulong Ulong { get; set; }
        public float Float { get; set; }
        public double Double { get; set; }
        public decimal Decimal { get; set; }
        public char Char { get; set; }
        public string String { get; set; }
        public bool Bool { get; set; }

        public DateTime DateTime { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public DateTimeOffset DateTimeOffset { get; set; }
        public Guid Guid { get; set; }

        public int? NullableInt { get; set; }
        public Nullable<double> NullableDouble { get; set; }
        public Nullable<DateTime> NullableDateTime { get; set; }
        public Guid? NullableGuid { get; set; }
    }
}