using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeKeeper.Domain
{
    public class Employee : BaseClass
    {
        public Employee()
        {
            Calendar = new List<Day>();
            Engagement = new List<Member>();
        }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public DateTime BirthDay { get; set; }
        [Required]
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public EmployeeStatus Status { get; set; }
        [Required]
        public string Position { get; set; }
        public virtual IList<Day> Calendar { get; set; }
        public virtual IList<Member> Engagement { get; set; }
    }
}
