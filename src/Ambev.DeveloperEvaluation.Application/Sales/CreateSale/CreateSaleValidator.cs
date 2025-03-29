using FluentValidation;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using System;
using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.Application.SaleItems.Validation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Validation;

/// <summary>
/// Validator for CreateSaleCommand that defines validation rules for sale creation.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
    /// </summary>
    public CreateSaleCommandValidator()
    {
        RuleFor(sale => sale.SaleDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Sale date cannot be in the future.");

        RuleFor(sale => sale.ClientId)
            .NotEmpty().WithMessage("Client ID is required.");

        RuleFor(sale => sale.StoreId)
            .NotEmpty().WithMessage("Store ID is required.");

        RuleFor(sale => sale.Items)
            .NotEmpty().WithMessage("At least one item must be included in the sale.");

        RuleForEach(sale => sale.Items).SetValidator(new CreateSaleItemCommandValidator());

        RuleFor(sale => sale.TotalAmount)
            .GreaterThan(0).WithMessage("Total amount must be greater than zero.");
    }
}
