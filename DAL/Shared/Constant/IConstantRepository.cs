using Domain.Entities;
using Domain.Enums;
using DTO.DataTable;

namespace DAL.Interface
{
    public interface IConstantRepository : IRepository<Constant>
    {
        /// <summary>
        /// گرفتن لیست برای نمایش در پنل مدیریت
        /// </summary>
        /// <returns></returns>
        DataTableResponseDTO<Constant> GetDataTableDTO(DataTableSearchDTO searchData);





        /// <summary>
        /// گرفتن تعداد دفعات مجاز برای لاگین ناموفق
        /// </summary>
        /// <returns></returns>
        int GetFailedLoginCount();





        /// <summary>
        /// کاربر هر چند روز کلمه عبور را تغییر دهد
        /// <para>
        /// مقدار پیشفرض 60 است.
        /// </para>
        /// </summary>
        /// <returns></returns>
        int? GetChangePasswordCycle();




        /// <summary>
        /// خواندن مقدار عددی از مقادیر ثابت
        /// </summary>
        /// <param name="Type">نوع مقادیر ثابت مورد نظر</param>
        /// <returns></returns>
        int? GetNumberValue(ConstantType Type);
    }
}