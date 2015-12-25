using System.ComponentModel.DataAnnotations;

namespace Demo.Model
{
    public class MetadataModel
    {
        [Required]
        public string Requred { get; set; }

        [Range(1, 100)]
        public int IntRange { get; set; }

        [Range(0.5, 1.0)]
        public int DoubleRange { get; set; }

        [MaxLength(1024)]
        public string MaxLength { get; set; }

        [MinLength(32)]
        public string MinLength { get; set; }

        [StringLength(1024)]
        public string StringLength { get; set; }

        [StringLength(100, MinimumLength = 1)]
        public string StringLengthWithMinimum { get; set; }
    }
}