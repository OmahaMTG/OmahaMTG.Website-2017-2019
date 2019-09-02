namespace OmahaMTG.AdminContentHandlers.Presenter
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string OmahaMtgUserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}