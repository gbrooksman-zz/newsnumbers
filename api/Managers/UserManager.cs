using System;
using LiteDB;
using api.Models;


namespace api.Managers
{

    public class UserManager
    {
        public UserManager()
        {

        }

        public User Save(User user)
        {
            using ( var db = new LiteDatabase(@"NewsNumbers.db"))
            {
                var col = db.GetCollection<User>("users");

                if (user.Id == Guid.Empty)
                {
                    user.Id = Guid.NewGuid();
                    user.CreateDate = DateTime.Now;
                    col.Insert(user);
                }
                else
                { 
                    col.Update(user);
                }
            }

            return user;
        }




    }
}