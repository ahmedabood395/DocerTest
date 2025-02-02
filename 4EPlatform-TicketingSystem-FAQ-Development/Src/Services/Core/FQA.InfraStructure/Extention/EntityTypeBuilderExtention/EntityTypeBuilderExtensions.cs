namespace FAQ.Infrastructure.Extention.ModelBuilderExtention
{
    public static class EntityTypeBuilderExtensions
    {
        /// <summary>
        /// Extension method for EntityTypeBuilder that applies is not deleted filter on entities
        /// </summary>
        /// <typeparam name="T">The entity to apply filter on</typeparam>
        /// <param name="builder"></param>
/*        public static void HasIsNotDeletedQueryFilter<T>(this EntityTypeBuilder builder) where T : BaseEntity<Guid>
        {
            builder.HasQueryFilter((T x) => x.State != State.Deleted);
        }*/
    }
}
