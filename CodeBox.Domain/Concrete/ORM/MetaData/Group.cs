using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CodeBox.Domain.Concrete.ORM
{
    [MetadataType(typeof(GroupMetaDataSource))]
    public partial class Group
    {
    }
    public class GroupMetaDataSource
    {
        [HiddenInput(DisplayValue = false)]
        public Int32 Id { get; set; }

        [Required(ErrorMessage = "Name required!")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [ScaffoldColumn(false)]
        public Nullable<global::System.Byte> ImageData { get; set; }

        [ScaffoldColumn(false)]
        public global::System.String ImageMimeType { get; set; }
    }
}
