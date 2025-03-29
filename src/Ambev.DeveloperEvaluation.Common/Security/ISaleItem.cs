using Ambev.DeveloperEvaluation.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Common.Security
{
    /// <summary>
    /// Defines the contract for a sale item entity.
    /// </summary>
    public interface ISaleItem
    {
        /// <summary>
        /// Gets the unique identifier of the sale item.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the product name.
        /// </summary>
        string Product { get; }

        /// <summary>
        /// Gets the quantity of the product in the sale.
        /// Must be between 1 and 20.
        /// </summary>
        int Quantity { get; }

        /// <summary>
        /// Gets the unit price of the product.
        /// Must be a positive value.
        /// </summary>
        decimal UnitPrice { get; }

        /// <summary>
        /// Gets the applied discount percentage (between 0 and 1).
        /// </summary>
        decimal Discount { get; }

        /// <summary>
        /// Gets the total value after applying the discount.
        /// </summary>
        decimal TotalValue { get; }

        /// <summary>
        /// Applies a discount based on business rules.
        /// </summary>
        void ApplyDiscount(decimal v);

        /// <summary>
        /// Validates the sale item entity.
        /// </summary>
        /// <returns>Validation result details.</returns>
        ValidationResultDetail Validate();
    }
}
