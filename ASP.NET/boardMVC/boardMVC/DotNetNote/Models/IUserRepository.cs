using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace boardMVC.DotNetNote.Models
{
    public interface IUserRepository
    {
        void AddUser(string userid, string password);

        users GetUserByUserId(string userId);

        bool IsCorrectUser(string userId, string password);

        void ModifyUser(int uid, string userId, string password);




    }
}