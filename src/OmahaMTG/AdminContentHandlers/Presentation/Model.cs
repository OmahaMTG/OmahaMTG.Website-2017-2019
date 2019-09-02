using System.Collections.Generic;

namespace OmahaMTG.AdminContentHandlers.Presentation
{
    public class Model
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public IEnumerable<int> PresenterIds { get; set; }
    }
}