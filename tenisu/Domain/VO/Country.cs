namespace tenisu.Domain.VO
{
    public class Country
    {
        public string Code { get; }
        public string Picture { get; }

        public Country(string code, string picture)
        {
            Code = code;
            Picture = picture;
        }
    }
}
