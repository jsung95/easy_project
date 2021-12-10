using EasyProject.Model;
using System;
using System.Collections.Generic;


namespace EasyProject.Dao
{

    public interface ISignupDao
    {
        List<DeptModel> GetDeptModels(string sql);
        void SignUp(string sql, NurseModel nurse_dto, DeptModel dept_dto);

        NurseModel IdCheck(string sql, NurseModel nurse_dto);
    } // interface 
} // namespace
