using System;
using System.ComponentModel.DataAnnotations;

namespace TimeKeeper.Domain
{
    public class BaseClass
    {
        public BaseClass()
        {
            Created = DateTime.Now;
            Creator = CurrentUser.Id;
            Deleted = false;
        }
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int Creator { get; set; }
        public bool Deleted { get; set; }
    }
}
