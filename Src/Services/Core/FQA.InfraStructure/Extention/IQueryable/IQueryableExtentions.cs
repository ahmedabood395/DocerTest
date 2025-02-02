namespace FAQ.Infrastructure.Extention.IQueryable
{

    public static class IQueryableExtentions
    {
        /// <summary>
        ///  function execute soft delete to Queryable objects
        /// </summary>
        public static void ExecuteBulkSoftDelete<T>(this IQueryable<T> queryable) where T : BaseEntity

        {
            queryable.ExecuteUpdate(a => a.SetProperty(x => x.State, State.Deleted));

        }

        /// <summary>
        ///  function execute soft delete to Collection Of Items
        /// </summary>
        public static void ExecuteBulkSoftDelete<T>(this ICollection<T> collection, IGRepository<T> repository) where T : BaseEntity

        {
            if (collection is not null || collection.Count > 0)
            {
                foreach (var item in collection)
                {
                    item.State = State.Deleted;
                    repository.UpdateEntity(item);
                }
            }


        }

    }
}
