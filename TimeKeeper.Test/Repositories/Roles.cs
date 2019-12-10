using NUnit.Framework;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Test.Repositories
{
    class TestRoles
    {
        [OneTimeSetUp]
        public async Task Setup()
        {
            await Database.Setup();
        }

        [Test, Order(1)]
        public async Task InsertRoles()
        {
            await Database.roles.Insert(new Role { Name = "free user", HourlyRate = 10, MonthlyRate = 100 });
            await Database.roles.Insert(new Role { Name = "paid user", HourlyRate = 10, MonthlyRate = 100 });
            await Database.roles.Insert(new Role { Name = "subscriber", HourlyRate = 10, MonthlyRate = 100 });
            int n = await Database.Context.SaveChangesAsync();
            Assert.AreEqual(3, n);
        }

        [Test, Order(2)]
        public async Task AllRoles()
        {
            var data = await Database.roles.Get();
            Assert.AreEqual(8, data.Count);
        }

        [Test, Order(3)]
        [TestCase(6, "free user")]
        [TestCase(7, "paid user")]
        [TestCase(8, "subscriber")]
        public async Task RoleById(int id, string name)
        {
            var data = await Database.roles.Get(id);
            Assert.AreEqual(name, data.Name);
        }

        [Test, Order(4)]
        public async Task RoleByWrongId()
        {
            var data = await Database.roles.Get(9);
            Assert.IsNull(data);
        }

        [Test, Order(5)]
        public async Task UpdateRole()
        {
            Role role = new Role { Id = 6, Name = "admina" };
            await Database.roles.Update(role, role.Id);
            int n = await Database.Context.SaveChangesAsync();
            Assert.AreEqual(1, n);
        }

        [Test, Order(6)]
        public async Task DeleteRole()
        {
            await Database.roles.Delete(8);
            int n = await Database.Context.SaveChangesAsync();
            Assert.AreEqual(1, n);
        }
    }
}
