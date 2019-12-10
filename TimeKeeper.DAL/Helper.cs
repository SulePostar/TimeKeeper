using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public static class Helper
    {
        public static async Task Build<T>(this T entity, TimeContext context)
        {
            if (typeof(T) == typeof(Member))  await BuildRelations(entity as Member, context);
            if (typeof(T) == typeof(Project)) await BuildRelations(entity as Project, context);
            if (typeof(T) == typeof(Day))     await BuildRelations(entity as Day, context);
            if (typeof(T) == typeof(Detail))  await BuildRelations(entity as Detail, context);
            if (typeof(T) == typeof(User))          BuildPassword(entity as User);
        }

        private static void BuildPassword(User user)
        {
            if(!string.IsNullOrWhiteSpace(user.Password)) user.Password = user.Username.HashWith(user.Password);
        }

        private static async Task BuildRelations(Project entity, TimeContext context)
        {
            entity.Team = await context.Teams.FindAsync(entity.Team.Id);
            entity.Customer = await context.Customers.FindAsync(entity.Customer.Id);
        }

        private static async Task BuildRelations(Member entity, TimeContext context)
        {
            entity.Team = await context.Teams.FindAsync(entity.Team.Id);
            entity.Role = await context.Roles.FindAsync(entity.Role.Id);
            entity.Employee = await context.People.FindAsync(entity.Employee.Id);
        }

        private static async Task BuildRelations(Day entity, TimeContext context)
        {
            entity.Employee = await context.People.FindAsync(entity.Employee.Id);
        }

        private static async Task BuildRelations(Detail entity, TimeContext context)
        {
            entity.Day = await context.Calendar.FindAsync(entity.Day.Id);
            entity.Project = await context.Projects.FindAsync(entity.Project.Id);
        }

/*----------*----------*/

        public static void Update<T>(this T oldEnt, T newEnt)
        {
            if (typeof(T) == typeof(Member))  UpdateRelations(oldEnt as Member, newEnt as Member);
            if (typeof(T) == typeof(Project)) UpdateRelations(oldEnt as Project, newEnt as Project);
            if (typeof(T) == typeof(Day))     UpdateRelations(oldEnt as Day, newEnt as Day);
            if (typeof(T) == typeof(Detail))  UpdateRelations(oldEnt as Detail, newEnt as Detail);
        }

        private static void UpdateRelations(Project oldEnt, Project newEnt)
        {
            oldEnt.Team = newEnt.Team;
            oldEnt.Customer = newEnt.Customer;
        }

        private static void UpdateRelations(Member oldEnt, Member newEnt)
        {
            oldEnt.Team = newEnt.Team;
            oldEnt.Role = newEnt.Role;
            oldEnt.Employee = newEnt.Employee;
        }

        private static void UpdateRelations(Day oldEnt, Day newEnt)
        {
            oldEnt.Employee = newEnt.Employee;
        }

        private static void UpdateRelations(Detail oldEnt, Detail newEnt)
        {
            oldEnt.Day = newEnt.Day;
            oldEnt.Project = newEnt.Project;
        }

/*----------*----------*/

        public static bool CanDelete<T>(this T entity)
        {
            if (typeof(T) == typeof(Team)) return HasNoChildren(entity as Team);
            if (typeof(T) == typeof(Role)) return HasNoChildren(entity as Role);
            if (typeof(T) == typeof(Customer)) return HasNoChildren(entity as Customer);
            if (typeof(T) == typeof(Project)) return HasNoChildren(entity as Project);
            if (typeof(T) == typeof(Employee)) return HasNoChildren(entity as Employee);
            if (typeof(T) == typeof(Day)) return HasNoChildren(entity as Day);
            return true;
        }

        private static bool HasNoChildren(Team t)
        {
            return t.Projects.Count + t.Members.Count == 0;
        }

        private static bool HasNoChildren(Role r)
        {
            return r.Members.Count == 0;
        }

        private static bool HasNoChildren(Customer c)
        {
            return c.Projects.Count  == 0;
        }

        private static bool HasNoChildren(Project p)
        {
            return p.Details.Count == 0;
        }

        private static bool HasNoChildren(Employee e)
        {
            return e.Calendar.Count + e.Engagement.Count == 0;
        }

        private static bool HasNoChildren(Day d)
        {
            return d.Details.Count == 0;
        }
    }
}
