using Domain.Entities;
using DTO.Base;
using DTO.DataTable;
using Infrastructure.Data;

namespace BLL.Interface
{
    public interface IConstantManager : IManager<Constant, ApplicationContext>
    {
        /// <summary>
        /// گرفتن لیست برای نمایش در پنل مدیریت
        /// </summary>
        /// <returns></returns>
        DataTableResponseDTO<Constant> GetDataTableDTO(DataTableSearchDTO searchData);



        /// <summary>
        /// ویرایش اطلاعات
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        BaseResult Update(long Id, string Value);





        /// <summary>
        /// گرفتن تعداد دفعات مجاز برای لاگین ناموفق
        /// </summary>
        /// <returns></returns>
        int GetFailedLoginCount();



    }
}