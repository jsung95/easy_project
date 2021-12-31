using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyProject.Model;

namespace EasyProject.Dao
{
    public interface ICategoryDao
    {
        //전첼 카테고리 리스트 가져오기
        List<CategoryModel> GetCategories();

        //카테고리 이름으로 카테고리 번호 가져오기
        int GetCategoryID(CategoryModel category_dto);
    }//interface

}//namespace
