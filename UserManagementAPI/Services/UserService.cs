using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Data.Repository;
using UserManagementAPI.Model;

namespace UserManagementAPI.Services
{
    public  class UserService
    {
        private readonly UserRepository _ur;

        public UserService(UserRepository ur)
        {
            _ur = ur;
        }

        public  List<User ?> SelectUsers()
        {
            var result = _ur.SelectUsers();

            return result;
        }

        public int MaxID()
        {            
            return !_ur.SelectUsers().Any() ? 0 : _ur.SelectUsers().Max(u => u.id);                       
        }

        public bool insertUser(string name)
        {
            try
            {
                int id = MaxID() + 1;
                _ur.InsertUser(new User(id,name));

                return true;
            }
            catch (Exception ex)
            {
                // Logger<FakeTesteUserService>.LogError(ex, "Error inserting user");
                return false;
            }    
        }


        public bool deleteUser(int id)
        {
            try
            {
                User? user = _ur.SearchUserById(id);
                _ur.DeleteUser(user);
                return true;
            }
            catch(Exception ex) 
            {
                return false;
            }
        }

        public bool updateUser(int id, string newName)
        {
            
            try
            {
                User? user = _ur.SearchUserById(id);
                if (user != null)
                {
                    user.name = newName;
                }
                else
                {
                    return false;
                }
                return _ur.UpdateUser(user);
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    } 

}
