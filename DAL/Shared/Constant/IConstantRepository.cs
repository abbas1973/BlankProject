using Domain.Entities;
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

    }
}