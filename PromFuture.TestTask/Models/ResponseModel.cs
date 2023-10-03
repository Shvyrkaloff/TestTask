namespace PromFuture.TestTask.Models
{
    public class ResponseModel<TEntity>
    {
        public int page_number { get; set; }

        public int items_per_page {  get; set; }

        public int items_count { get; set; }

        public bool success { get; set; }

        public IEnumerable<TEntity> items { get; set; }
    }

    public class ResponseSingleModel<TEntity>
    {
        public bool success { get; set; }

        public string error { get; set; }

        public TEntity item { get; set; }
    }
}
