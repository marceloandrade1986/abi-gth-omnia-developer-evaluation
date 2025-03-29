using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    /// <summary>
    /// Validator for the SaleItem entity.
    /// Ensures that each sale item contains valid data before being processed.
    /// </summary>
    public class SaleItemValidator : AbstractValidator<SaleItem>
    {
        public SaleItemValidator()
        {
            RuleFor(item => item.Product)
                .NotEmpty().WithMessage("Product name cannot be empty.")
                .MaximumLength(100).WithMessage("Product name cannot be longer than 100 characters.");

            RuleFor(item => item.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be at least 1.")
                .LessThanOrEqualTo(20).WithMessage("Quantity cannot exceed 20 items.");

            RuleFor(item => item.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than zero.");

            RuleFor(item => item.Discount)
                .Must((item, discount) => IsValidDiscount(item.Quantity, discount))
                .WithMessage("Invalid discount for the given quantity.");

            RuleFor(item => item.TotalValue)
                .Equal(item => CalculateTotalValue(item))
                .WithMessage("Total value does not match the expected value.");
        }

        /// <summary>
        /// Validates the discount based on quantity.
        /// </summary>
        private bool IsValidDiscount(int quantity, decimal discount)
        {
            if (quantity < 4) return discount == 0m;
            if (quantity >= 4 && quantity < 10) return discount == 0.10m;
            if (quantity >= 10 && quantity <= 20) return discount == 0.20m;
            return false;
        }

        /// <summary>
        /// Calculates the expected total value of the item.
        /// </summary>
        private decimal CalculateTotalValue(SaleItem item)
        {
            var discountFactor = 1 - item.Discount;
            return item.Quantity * item.UnitPrice * discountFactor;
        }
    }
}
