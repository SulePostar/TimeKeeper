using System.ComponentModel.DataAnnotations;

namespace TimeKeeper.Domain
{
    public class Member: BaseClass
    {
        [Required]
        public virtual Employee Employee { get; set; }
        [Required]
        public virtual Team Team { get; set; }
        [Required]
        public virtual Role Role { get; set; }
        [Required]
        public int HoursWeekly { get; set; }
    }
}
