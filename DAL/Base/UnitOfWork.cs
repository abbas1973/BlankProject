using DAL.Interface;
using Microsoft.EntityFrameworkCore;

namespace DAL
{

    /// <summary>
    /// POWERED BY Abbas MohammadNezhad
    /// <para>
    /// email: abbas.mn1973@gmail.com
    /// </para>
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DbContext context)
        {
            Context = context;
        }


        #region private properties 
        protected readonly DbContext Context;

        #region Auth System
        private MenuRepository _menus;
        private RoleRepository _roles;
        private RoleMenuRepository _roleMenus;
        private UserRepository _users;
        #endregion

        #endregion


        #region Repository Getters 

        #region Auth System
        public IMenuRepository Menus
        {
            get
            {
                if (_menus == null)
                    _menus = new MenuRepository(Context);
                return _menus;
            }
        }

        public IRoleRepository Roles
        {
            get
            {
                if (_roles == null)
                    _roles = new RoleRepository(Context);
                return _roles;
            }
        }

        public IRoleMenuRepository RoleMenus
        {
            get
            {
                if (_roleMenus == null)
                    _roleMenus = new RoleMenuRepository(Context);
                return _roleMenus;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (_users == null)
                    _users = new UserRepository(Context);
                return _users;
            }
        }

        #endregion

        #endregion


        #region Commit
        /// <summary>
        /// ذخیره تغییرات انجام شده
        /// </summary>
        /// <returns></returns>
        public bool Commit()
        {
            try
            {
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        /// <summary>
        /// ذخیره تغییرات انجام شده
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CommitAsync()
        {
            //try
            //{
            await Context.SaveChangesAsync();
            return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }
        #endregion


        #region ExecuteNonQuery

        /// <summary>
        /// اجرای یک کوئری بدون مقدار بازگشتی
        /// </summary>
        /// <param name="Query">متن کوئری</param>
        /// <param name="Parameters">پارامتر های کوئری</param>
        /// <returns></returns>
        public bool ExecuteNonQuery(string Query, params object[] Parameters)
        {
            try
            {
                int NumberOfRowEffected = Context.Database.ExecuteSqlRaw(Query, Parameters);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }



        /// <summary>
        /// اجرای یک کوئری بدون مقدار بازگشتی
        /// </summary>
        /// <param name="Query">متن کوئری</param>
        /// <param name="Parameters">پارامتر های کوئری</param>
        /// <returns></returns>
        public async Task<bool> ExecuteNonQueryAsync(string Query, params object[] Parameters)
        {
            try
            {
                int NumberOfRowEffected = await Context.Database.ExecuteSqlRawAsync(Query, Parameters);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion


        /// <summary>
        /// پاک کردن آبجکت unit of work و Context از رم
        /// </summary>
        public void Dispose()
        {
            Context.Dispose();
        }


    }
}
