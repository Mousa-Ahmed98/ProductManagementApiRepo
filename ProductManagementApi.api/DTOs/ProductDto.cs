using System.ComponentModel.DataAnnotations;

namespace ProductManagementApi.api.DTOs
{
    public class ProductDto
    {

        [Required]
        public string? Name { get; set; }
        
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

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
            return string.IsNullOrWhiteSpace(Name) || Name.Length < 3 ?
                "Product name is required and must be at least 3 characters long." : null;
        }

        private string? ValidateDescription()
        {
            return Description != null && Description.Length > 500 ?
                "Product description cannot exceed 500 characters." : null;
        }

        private string? ValidatePrice()
        {
            return Price < 0.01m ?
                "Product price is required and must be at least 0.01." : null;
        }

        private string? ValidateQuantity()
        {
            return Quantity < 0 ?
                "Stock quantity is required and must be a non-negative value." : null;
        }
    }
}
