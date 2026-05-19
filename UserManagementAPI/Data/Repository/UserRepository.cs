using UserManagementAPI.Midlewares;
using UserManagementAPI.Model;

namespace UserManagementAPI.Data.Repository
{
    public class UserRepository
    {
        public readonly AppDbContext _db;
        private readonly ILogger<MReqRespLog> _logger;
        public UserRepository(AppDbContext db)
        {
            _db = db;
        }
        public bool InsertUser(User user)
        {
            try
            {
                _db.Add(user);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error inserting user: ", ex);
                return false;
            }
        }

        public List<User> SelectUsers()
        {
            return _db.Users.ToList();
        }

        public bool DeleteUser(User user)
        {
            try
            {
                _db.Remove(user);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting user: ", ex);
                return false;

            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                _db.Update(user);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating user: ", ex);
                return false;
            }
        }

        public User SearchUserById (int id)
        {
            return _db.Users.FirstOrDefault(u => u.id == id);
        }
    }
}

