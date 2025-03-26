using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_Core.Models;

namespace EShop.ViewModels
{
    public class AddCategoryVM
    {
        [Required(ErrorMessage = "Please Provide valid Product Name")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Product name must contain at least 3 letter and max 100 letter")]
        public string Name { get; set; }


        //[Required(ErrorMessage = "Please Provide valid Product Description")]
        //[StringLength(1000, MinimumLength = 10, ErrorMessage = "Product Description must contain at least 10 letter and max 1000 letter")]
        [MultiLine]
        //[DataType(dataType:DataType.MultilineText)]
        public string Description { get; set; }

        public Category ToCategoryEntity()
        {
            throw new NotImplementedException();
        }
    }
}
