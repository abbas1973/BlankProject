using BLL.Interface;
using Domain.Entities;
using Domain.Enums;
using DTO.Base;
using DTO.DataTable;
using Infrastructure.Data;
using Utilities.Extentions;

namespace BLL
{
    public class ConstantManager : Manager<Constant, ApplicationContext>, IConstantManager
    {
        public ConstantManager(DbContexts _Contexts) : base(_Contexts)
        {
        }



        /// <summary>
        /// گرفتن لیست برای نمایش در پنل مدیریت
        /// </summary>
        /// <returns></returns>
        public DataTableResponseDTO<Constant> GetDataTableDTO(DataTableSearchDTO searchData)
        {
            return UOW.Constants.GetDataTableDTO(searchData);
        }




        /// <summary>
        /// ویرایش اطلاعات
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public BaseResult Update(long Id, string Value)
        {
            var constant = GetById(Id);
            var dataType = constant.Type.GetEnumCustomAttribute();

            #region ولیدیت
            if(dataType.Type == CustomDataType.Number)
            {
                int _val;
                if (!int.TryParse(Value, out _val))
                    return new BaseResult(false, "مقدار عددی وارد شده صحیح نیست!");

                if(dataType.Min != 0 && _val < dataType.Min)
                    return new BaseResult(false, $"مقدار وارد شده نباید کمتر از {dataType.Min} باشد!");
                if (dataType.Max != 0 && _val > dataType.Max)
                    return new BaseResult(false, $"مقدار وارد شده نباید بیشتر از {dataType.Max} باشد!");
            }
            else if(dataType.Type == CustomDataType.String)
            {
                if (dataType.Min != 0 && Value.Length < dataType.Min)
                    return new BaseResult(false, $"تعداد کاراکتر وارد شده نباید کمتر از {dataType.Min} باشد!");
                if (dataType.Max != 0 && Value.Length > dataType.Max)
                    return new BaseResult(false, $"تعداد کاراکتر وارد شده نباید بیشتر از {dataType.Max} باشد!");
            }
            #endregion


            #region تبدیل نوع بولین به متن true یا false برای ذخیره
            if (dataType.Type == CustomDataType.Bool)
            {
                if (Value == "on") Value = "true";
                else Value = "false";
            }
            #endregion


            constant.Value = Value;
            return Update(constant);
        }



        /// <summary>
        /// گرفتن تعداد دفعات مجاز برای لاگین ناموفق
        /// <para>
        /// مقدار پیشفرض 5 است.
        /// </para>
        /// </summary>
        /// <returns></returns>
        public int GetFailedLoginCount()
        {
            try
            {
                var value = UOW.Constants.GetOneDTO(x => x.Value, x => x.Type == ConstantType.FailedLoginCount);
                if (string.IsNullOrEmpty(value))
                    return 5;

                int count;
                if (!int.TryParse(value, out count))
                    return 5;

                var dataType = ConstantType.FailedLoginCount.GetEnumCustomAttribute();
                if (count < dataType.Min || count > dataType.Max)
                    return 5;

                return count;
            }
            catch
            {
                return 5;
            }

        }


    }
}