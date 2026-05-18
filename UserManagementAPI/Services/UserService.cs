using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Model;

namespace UserManagementAPI.Services
{
    public  class UserService
    {
        public List<User> Users { get; set; } = new List<User>();
        public  List<User> SelectUsers()
        {
            return Users;
        }

        public int MaxID()
        {            
            return !Users.Any() ? 0 : Users.Max(u => u.id);                       
        }

        public bool insertUser(string name)
        {
            try
            {
                Users.Add(new User
                {
                    name = name,
                    id = MaxID() + 1
                });

                return true;
            }
            catch (Exception ex)
            {
                // Logger<FakeTesteUserService>.LogError(ex, "Error inserting user");
                return false;
            }    
        }


        public bool deleteUser(string name)
        {
            try
            {
                Users.RemoveAll(u => u.name == name);
                return true;
            }
            catch(Exception ex) 
            {
                return false;
            }
        }

        public bool updateUser(string name, string newName)
        {
            var user = Users.FirstOrDefault(u => u.name == name);
            if (user != null)
            {
                user.name = newName;
                return true;
            }
            return false;
        }
    } 

}
