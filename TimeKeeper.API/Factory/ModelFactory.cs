using System.Linq;
using TimeKeeper.API.Models;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Factory
{
    public static class ModelFactory
    {
        public static TeamModel Create(this Team team, bool withList)
        {
            TeamModel model = new TeamModel
            {
                Id = team.Id,
                Name = team.Name,
                Description = team.Description,
                TeamSize = team.Members.Count,
                ProjectSize = team.Projects.Count
            };
            if (withList)
            {
                model.Members = team.Members.Select(x => x.Master("team")).ToList();
                model.Projects = team.Projects.Select(x => x.Master()).ToList();
            }
            return model;
        }

        public static RoleModel Create(this Role role)
        {
            return new RoleModel
            {
                Id = role.Id,
                Name = role.Name,
                HourlyRate = role.HourlyRate,
                MonthlyRate = role.MonthlyRate,
                Members = role.Members.Select(y => y.Master("role")).ToList()
            };
        }

        public static MemberModel Create(this Member member)
        {
            return new MemberModel
            {
                Id = member.Id,
                Team = member.Team.Master(),
                Employee = member.Employee.Master(),
                Role=member.Role.Master(),
                HoursWeekly = member.HoursWeekly
            };
        }

        public static CustomerModel Create(this Customer customer)
        {
            return new CustomerModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                Contact = customer.Contact,
                Email = customer.Email,
                Image = customer.Image,
                Phone = customer.Phone,
                Status = customer.Status.ToString(),
                Projects = customer.Projects.Select(x => x.Master()).ToList()
            };
        }

        public static ProjectModel Create(this Project project)
        {
            return new ProjectModel
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Customer = new MasterModel { Id = project.Customer.Id, Name = project.Customer.Name },
                Team = new MasterModel { Id = project.Team.Id, Name = project.Team.Name },
                BeginDate = project.BeginDate,
                EndDate = project.EndDate,
                Status = project.Status.ToString(),
                Pricing = project.Pricing.ToString(),
                Amount = project.Amount
            };
        }

        public static EmployeeModel Create(this Employee emp)
        {
            return new EmployeeModel
            {
                Id = emp.Id,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                FullName = emp.FullName,
                Email = emp.Email,
                Phone = emp.Phone,
                Position = emp.Position,
                Image = emp.Image,
                Status = emp.Status.ToString(),
                BeginDate = emp.BeginDate,
                EndDate = emp.EndDate,
                BirthDay = emp.BirthDay,
                Engagement = emp.Engagement.Select(x => x.Master("role")).ToList()
            };
        }

        public static DayModel Create(this Day day)
        {
            return new DayModel
            {
                Id = day.Id,
                Employee = new MasterModel { Id = day.Employee.Id, Name = day.Employee.FullName },
                DayType = day.DayType.ToString(),
                Date = day.Date,
                TotalHours = day.TotalHours,
                Details = day.Details.Select(x => x.Create()).ToList()
            };
        }

        public static DetailModel Create(this Detail det)
        {
            return new DetailModel
            {
                Id = det.Id,
                Description = det.Description,
                Hours = det.Hours,
                Project = new MasterModel { Id = det.Project.Id, Name = det.Project.Name }
            };
        }

        public static UserModel Create(this User user)
        {
            return new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}
