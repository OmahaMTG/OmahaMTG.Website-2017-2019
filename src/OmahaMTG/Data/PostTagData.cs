using System.ComponentModel.DataAnnotations.Schema;

namespace OmahaMTG.Data
{
    [Table("PostTag")]
    class PostTagData 
    {
        public int TagId { get; set; }
        public TagData Tag { get; set; }
        public int PostId { get; set; }
        public PostData Post { get; set; }
    }
}
