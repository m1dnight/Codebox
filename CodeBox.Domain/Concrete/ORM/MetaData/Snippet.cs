using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CodeBox.Domain.Concrete.ORM
{
    [MetadataType(typeof(SnippetMetaDataSource))]
    public partial class Snippet
    {
    }

    public class SnippetMetaDataSource
    {

        //[ScaffoldColumn(false)]
        [HiddenInput(DisplayValue = false)]
        public Int32 SnippetId { get; set; }

        [Required(ErrorMessage = "A Name is required!")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }

        [HiddenInput(DisplayValue = false)]
        [DataType(DataType.DateTime)]
        public DateTime? CreationDate { get; set; }

        [HiddenInput(DisplayValue = false)]
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }

        [ScaffoldColumn(false)]
        public Int32 UserId { get; set; }

        [ScaffoldColumn(false)]
        public Int32 LanguageId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Code { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }


    }
}
