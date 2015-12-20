using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OmahaMtg.Data
{
    /// <summary>
    /// Entity class for a banner add.
    /// </summary>
    public class BannerAdd
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BannerAdd"/> class.
        /// </summary>
        public BannerAdd()
        {
            int nextYear = DateTime.Now.Year + 1;

            //Set rotation start to the beginning of next year
            RotationStart = new DateTime(nextYear, 1, 1);

            //Set rotation end to the end of next year
            RotationEnd = new DateTime(nextYear, 12, 31, 23, 59, 59);
        }
        #endregion
        #region Properties
        [Description("Specifies if this banner add is currently in rotation.")]
        [DisplayName("Is Current")]
        public bool IsCurrent {
            get
            {
                return DateTime.Now >= RotationStart &&
                    DateTime.Now <= RotationEnd;
            }
        }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Description("The identifying number of this banner add.")]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the rotation start.
        /// </summary>
        /// <value>
        /// The rotation start.
        /// </value>
        [Description("Specifies when this banner add should start showing in the rotation.")]
        [DisplayName("Rotation Start")]
        [Required]
        public DateTime RotationStart { get; set; }
        /// <summary>
        /// Gets or sets the rotation end.
        /// </summary>
        /// <value>
        /// The rotation end.
        /// </value>
        [Description("Specifies when this banner add should end showing in the rotation.")]
        [DisplayName("Rotation End")]
        [Required]
        public DateTime RotationEnd { get; set; }
        /// <summary>
        /// Gets or sets the add name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value
        [Description("The name of this banner add.")]
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        [Description("The file name for this banner add.")]
        [DisplayName("File Name")]
        [MaxLength(100)]
        [Required]
        public string FileName { get; set; }
        /// <summary>
        /// Gets or sets the add URL.
        /// </summary>
        /// <value>
        /// The add URL.
        /// </value>
        [Description("The file name for this banner add.")]
        [DisplayName("File Name")]
        [MaxLength(100)]
        public string AddUrl { get; set; }
    
        #endregion
    }
}
