using System.ComponentModel.DataAnnotations.Schema;

namespace TimeKeeper.Domain
{
    [Table("Tasks")]
    public class Detail : BaseClass
    {
        public string Description { get; set; }
        public decimal Hours { get; set; }
        public virtual Day Day { get; set; }
        public virtual Project Project { get; set; }
    }
}