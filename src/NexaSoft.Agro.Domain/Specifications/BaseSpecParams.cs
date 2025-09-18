namespace NexaSoft.Agro.Domain.Specifications
{
    public class BaseSpecParams : PagingParams
    {
        public string? Sort { get; set; }

        public string? SearchFields { get; set; }

        private string? _search;

        public string Search
        {
            get => _search ?? "";
            set => _search = value?.ToLower();
        }

        public long? Id { get; set; }
        public bool NoPaging { get; set; } = false;
        //public long? PadreId { get; set; }
    }

    public class BaseSpecParams<TKey> : BaseSpecParams
        where TKey : struct, IEquatable<TKey>
    {
        public List<TKey> Ids { get; set; } = new();
    }
}
