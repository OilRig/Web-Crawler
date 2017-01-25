using Crawler.DAL.BusinessEntities;
using Crawler.DAL.Entities;
using System.Collections.Generic;

namespace Crawler.DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAllUsers();
        void AddList(List<User> users);
        User GetUserById(int id);
        IList<UserFoundRow> GetUserInfoByName(string userName);
    }
}
