using NUnit.Framework;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Test.Repositories
{
    class TestTeams
    {
        [Test, Order(1)]
        public async Task TestInsert()
        {
            Database.teams.Insert(new Team { Name = "Green" });
            Database.teams.Insert(new Team { Name = "Yellow" });
            Database.teams.Insert(new Team { Name = "Black" });
            int n = await Database.Context.SaveChangesAsync();
            Assert.AreEqual(3, n);
        }

        [Test, Order(2)]
        public async Task TestAll()
        {
            var data = await Database.teams.Get();
            Assert.AreEqual(5, data.Count);
        }

        [Test, Order(3)]
        [TestCase(1, "Blue")]
        [TestCase(2, "Red")]
        [TestCase(3, "Green")]
        public async Task TestById(int id, string name)
        {
            var data = await Database.teams.Get(id);
            Assert.AreEqual(name, data.Name);
        }

        [Test, Order(4)]
        public async Task TestWrongId()
        {
            var data = await Database.teams.Get(9);
            Assert.IsNull(data);
        }

        [Test, Order(5)]
        public async Task TestUpdate()
        {
            Team team = new Team { Id = 3, Name = "Rainbow" };
            Database.teams.Update(team, team.Id);
            int n = await Database.Context.SaveChangesAsync();
            Assert.AreEqual(1, n);
        }

        [Test, Order(6)]
        public async Task TestDelete()
        {
            Database.teams.Delete(3);
            int n = await Database.Context.SaveChangesAsync();
            Assert.AreEqual(1, n);
        }
    }
}
