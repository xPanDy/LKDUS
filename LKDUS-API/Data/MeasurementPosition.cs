using System.ComponentModel.DataAnnotations.Schema;

namespace LKDUS_API.Data
{
    [Table("MeasurementPositions")]
    public class MeasurementPosition
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}