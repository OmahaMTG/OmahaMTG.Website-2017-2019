using System.ComponentModel.DataAnnotations.Schema;

namespace OmahaMTG.Data
{
    [Table("Tags")]
    class TagData : DataEntityBase
    {
        public string Name { get; set; }
    }
}