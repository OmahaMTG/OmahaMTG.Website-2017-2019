namespace OmahaMTG.AdminContentHandlers.Host
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Blurb { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }

        public bool IsDeleted { get; set; }
    }
}