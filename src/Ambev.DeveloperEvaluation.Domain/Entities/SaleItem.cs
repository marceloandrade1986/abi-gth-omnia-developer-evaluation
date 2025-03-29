using System;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents an item in a sale transaction.
    /// </summary>
    public class SaleItem : BaseEntity, ISaleItem
    {
        /// <summary>
        /// Gets the product name.
        /// </summary>
        public string Product { get; private set; }

        /// <summary>
        /// Gets the quantity of the product in the sale.
        /// Must be between 1 and 20.
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Gets the unit price of the product.
        /// Must be a positive value.
        /// </summary>
        public decimal UnitPrice { get; private set; }

        /// <summary>
        /// Gets the applied discount percentage (between 0 and 1).
        /// </summary>
        public decimal Discount { get; private set; }

        /// <summary>
        /// Gets the total value after applying the discount.
        /// </summary>
        public decimal TotalValue => Quantity * UnitPrice * (1 - Discount);

        public Guid SaleId { get; set; }

        /// <summary>
        /// Initializes a new instance of the SaleItem class.
        /// </summary>
        /// <param name="product">The product name</param>
        /// <param name="quantity">The quantity of the product</param>
        /// <param name="unitPrice">The unit price of the product</param>
        public SaleItem(string product, int quantity, decimal unitPrice)
        {
            if (string.IsNullOrWhiteSpace(product))
                throw new ArgumentException("O nome do produto não pode estar vazio.");

            if (quantity < 1 || quantity > 20)
                throw new ArgumentException("A quantidade deve estar entre 1 e 20.");

            if (unitPrice <= 0)
                throw new ArgumentException("O preço unitário deve ser maior que zero.");

            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = 0m;
        }

        /// <summary>
        /// Parameterless constructor for Entity Framework Core.
        /// </summary>
        protected SaleItem() { }

        /// <summary>
        /// Applies a discount based on the quantity of items.
        /// </summary>
        public void ApplyDiscount(decimal v)
        {
            if (Quantity >= 10)
                Discount = 0.20m; // 20% de desconto
            else if (Quantity >= 4)
                Discount = 0.10m; // 10% de desconto
        }

        /// <summary>
        /// Validates the sale item entity.
        /// </summary>
        /// <returns>ValidationResultDetail containing validation status and errors.</returns>
        public ValidationResultDetail Validate()
        {
            var validator = new SaleItemValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
