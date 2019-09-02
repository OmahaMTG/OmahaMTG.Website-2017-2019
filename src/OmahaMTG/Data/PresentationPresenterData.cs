using System.ComponentModel.DataAnnotations.Schema;

namespace OmahaMTG.Data
{
    [Table("Presentations_Presenters")]
    class PresentationPresenterData
    {
        public int PresentationId { get; set; }
        public PresentationData Presentation { get; set; }
        public int PresenterId { get; set; }
        public PresenterData Presenter { get; set; }
    }
}
