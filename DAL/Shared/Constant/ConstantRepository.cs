using DAL.Interface;
using Domain.Entities;
using DTO.DataTable;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Z.EntityFramework.Plus;

namespace DAL
{
    public class ConstantRepository : Repository<Constant>, IConstantRepository
    {
        public ConstantRepository(DbContext _Context) : base(_Context)
        {
        }




        /// <summary>
        /// گرفتن لیست اندازه ها برای نمایش در پنل مدیریت
        /// </summary>
        /// <returns></returns>
        public DataTableResponseDTO<Constant> GetDataTableDTO(DataTableSearchDTO searchData)
        {
            var model = new DataTableResponseDTO<Constant>();

            var recordTotal = Entities.DeferredCount().FutureValue();

            var filter = PredicateBuilder.New<Constant>(true);

            #region شرط ها

            //search
            if (!string.IsNullOrEmpty(searchData.searchValue))
            {
                var srch = searchData.searchValue;
                filter = filter.And(s => s.Title.Contains(srch)
                                              || s.Title.Contains(srch)
                                              || s.Id.ToString().Contains(srch));
            }
            #endregion

            var recordsFiltered = Entities.DeferredCount(filter).FutureValue();

            //sorting and paging
            var sortCol = searchData.sortColumnName;
            var selectedModel = Entities.Where(filter)
                                        .OrderBy(sortCol + " " + searchData.sortDirection)
                                        .Skip(searchData.start)
                                        .Take(searchData.length)
                                        .Future();

            model.data = selectedModel.ToList();
            model.recordsTotal = recordTotal.Value;
            model.recordsFiltered = recordsFiltered.Value;
            model.draw = searchData.draw;
            return model;
        }



    }
}