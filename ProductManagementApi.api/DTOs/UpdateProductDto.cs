using ProductManagementApi.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementApi.api.DTOs
{
    public class UpdateProductDto
    {
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; }
        
        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        public string? ValidateState()
        {
            string? errorMessage;
            errorMessage = ValidateQuantity();
            errorMessage = ValidatePrice()?? errorMessage;
            errorMessage = ValidateDescription()?? errorMessage;
            errorMessage = ValidateName()?? errorMessage;
            return errorMessage;
        }
        private string? ValidateName()
        {
            return !string.IsNullOrWhiteSpace(Name) && Name.Length < 3 ?
                "Product name must be at least 3 characters long." : null;
        }

        private string? ValidateDescription()
        {
            return !string.IsNullOrWhiteSpace(Description) && Description.Length > 500 ?
                "Product description cannot exceed 500 characters." : null;
        }

        private string? ValidatePrice()
        {
            return Price is not null && Price < 0.01m ?
                "Product price must be at least 0.01." : null;
        }

        private string? ValidateQuantity()
        {
            return Quantity is not null && Quantity < 0 ?
                "Stock quantity must be a non-negative value." : null;
        }

        public Product ExtractChanges(Product product)
        {
            return new Product
            {
                //Id = product.Id,
                Description = this.Description?? product.Description,
                Price = this.Price ?? product.Price,
                Quantity = this.Quantity ?? product.Quantity,
                Name = this.Name?? product.Name,
            };

        }



    }
}
