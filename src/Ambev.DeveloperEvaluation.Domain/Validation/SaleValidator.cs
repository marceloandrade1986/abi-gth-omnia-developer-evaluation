using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    /// <summary>
    /// Validator for the Sale entity.
    /// Ensures that the sale contains valid data before being processed.
    /// </summary>
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(sale => sale.SaleNumber)
                .NotEmpty().WithMessage("Sale number cannot be empty.")
                .Matches(@"^[A-Za-z0-9\-]+$").WithMessage("Sale number must contain only letters, numbers, or hyphens.")
                .MaximumLength(20).WithMessage("Sale number cannot be longer than 20 characters.");

            RuleFor(sale => sale.SaleDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Sale date cannot be in the future.");

            RuleFor(sale => sale.Customer)
                .NotEmpty().WithMessage("Customer name cannot be empty.")
                .MinimumLength(3).WithMessage("Customer name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Customer name cannot be longer than 100 characters.");

            RuleFor(sale => sale.Branch)
                .NotEmpty().WithMessage("Branch name cannot be empty.")
                .MaximumLength(50).WithMessage("Branch name cannot be longer than 50 characters.");

            RuleFor(sale => sale.Items)
                .NotEmpty().WithMessage("A sale must contain at least one item.")
                .Must(items => items.All(i => i.Quantity > 0))
                .WithMessage("All sale items must have a quantity greater than zero.");

            RuleForEach(sale => sale.Items)
                .SetValidator(new SaleItemValidator()); // Assumindo que existe um SaleItemValidator para validar os itens da venda
        }
    }
}
