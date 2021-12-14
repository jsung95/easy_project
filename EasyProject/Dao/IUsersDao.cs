using System;
using System.Collections.Generic;
using EasyProject.Model;


namespace EasyProject.Dao
{
    public interface IUsersDao
    {
        List<UserModel> GetUserInfo(string auth);

        void UserAuthChange(string auth, List<UserModel> no);
    }// interface

}//namespace
