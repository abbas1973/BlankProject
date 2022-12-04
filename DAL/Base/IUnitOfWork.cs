namespace DAL.Interface
{

    /// <summary>
    /// POWERED BY Abbas MohammadNezhad
    /// <para>
    /// email: abbas.mn1973@gmail.com
    /// </para>
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        #region Auth System
        IMenuRepository Menus { get; }

        IRoleRepository Roles { get; }

        IRoleMenuRepository RoleMenus { get; }

        IUserRepository Users { get; }

        #endregion


        #region LogSystem
        IUserLogRepository UserLogs { get; }

        #endregion



        bool Commit();

        Task<bool> CommitAsync();

        bool ExecuteNonQuery<TContext>(string Query, params object[] Parameters);

        Task<bool> ExecuteNonQueryAsync<TContext>(string Query, params object[] Parameters);
    }
}
