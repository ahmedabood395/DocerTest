namespace Infrastructure.GenericRepo
{
    public interface IGRepository<T> where T : class
    {
        #region Find Methods
        T Find(params object[] keys);
        Task<T> FindAsync(params object[] keys);
        T Find(Func<T, bool> where);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        #endregion

        #region Add Methods
        T Add(T entity);
        Task AddAsync(T t);
        void AddRange(IEnumerable<T> entities);
        Task AddRangeAsync(IEnumerable<T> entities);
        #endregion

        #region Count Methods
        int Count();
        Task<int> CountAsync();
        #endregion

        #region Remove Methods
        EntityEntry<T> Remove(T entity);
        EntityEntry<T> RemoveLinkTable(T entity);
        void RemoveRange(IEnumerable<T> entities);

        void Save();
        // void Truncate();

        #endregion

        #region Get Methods
        IQueryable<T> GetAll();
        Task<IQueryable<T>> GetAllAsync();
        IQueryable<T> GetAll(Expression<Func<T, bool>> where);
        IQueryable<T> GetAll(Expression<Func<T, bool>> where, int pageIndex, int pageSize, ref ResponseDTO responseDto);
        IQueryable<T> GetAll(Expression<Func<T, bool>> where, int pageIndex, int pageSize, ref ResponseDTO responseDto, Expression<Func<T, object>> orderDesc);
        IQueryable<T> GetAll(int pageNumber, int pageSize, ref ResponseDTO responseDto);
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> where);
        IQueryable<object> GetAll(Expression<Func<T, bool>> where, Expression<Func<T, object>> select);
        Task<IQueryable<object>> GetAllAsync(Expression<Func<T, bool>> where, Expression<Func<T, object>> select);
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<IQueryable<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] includeProperties);

        T GetFirst();
        Task<T> GetFirstAsync();
        T GetLast();
        Task<T> GetLastAsync();
        T GetFirst(Expression<Func<T, bool>> where);
        Task<T> GetFirstAsync(Expression<Func<T, bool>> where);
        T GetLast(Expression<Func<T, bool>> where);
        Task<T> GetLastAsync(Expression<Func<T, bool>> where);

        #endregion

        #region Update Method
        EntityEntry<T> Update(T entity);
        EntityEntry<T> UpdateEntity(T entity);
        #endregion

        #region Release Unmanaged Resources
        void Dispose(bool disposing);
        #endregion

        #region GetMinimum Methods
        T GetMinimum();
        Task<T> GetMinimumAsync();
        object GetMinimum(Expression<Func<T, object>> selector);
        Task<object> GetMinimumAsync(Expression<Func<T, object>> selector);

        #endregion

        #region GetMaximum Methods
        T GetMaximum();
        Task<T> GetMaximumAsync();
        object GetMaximum(Expression<Func<T, object>> selector);
        Task<object> GetMaximumAsync(Expression<Func<T, object>> selector);
        #endregion


        #region IsExist Methods
        bool IsExist(params object[] keys);
        Task<bool> IsExistAsync(params object[] keys);
        bool IsExist(Func<T, bool> where);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> match);
        #endregion

    }

}
