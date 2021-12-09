using EasyProject.Model;
using System;
using System.Collections.Generic;


namespace EasyProject.Dao
{

    public interface ISignupDao
    {
        List<DeptModel> GetDeptModels(string sql);
    } // interface 
} // namespace
