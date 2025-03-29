using System;
using System.Collections.Generic;
using System.Linq;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Microsoft.AspNetCore.Identity;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale transaction.
    /// </summary>
    public class Sale : BaseEntity, ISale
    {
        /// <summary>
        /// Gets the unique sale number.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets the date of the sale.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets the customer name.
        /// </summary>
        public string Customer { get; set; } = string.Empty;

        /// <summary>
        /// Gets the total value of the sale, calculated from its items.
        /// </summary>
        public decimal TotalValue => Items.Sum(i => i.TotalValue);

        /// <summary>
        /// Gets the branch where the sale was made.
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Gets the list of items in the sale.
        /// </summary>
        public List<SaleItem> Items { get; set; } = new List<SaleItem>();

        /// <summary>
        /// Indicates whether the sale has been cancelled.
        /// </summary>
        public bool IsCancelled { get; private set; }

        /// <summary>
        /// Gets the date and time when the sale was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets the date and time of the last update to the sale.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets the unique identifier of the sale.
        /// </summary>
        Guid ISale.Id => Id;

        /// <summary>
        /// Initializes a new instance of the Sale class.
        /// </summary>
        public Sale()
        {
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Performs validation of the sale entity using the SaleValidator rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - IsValid: Indicates whether all validation rules passed
        /// - Errors: Collection of validation errors if any rules failed
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new SaleValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

        /// <summary>
        /// Cancels the sale.
        /// Changes the sale's status to cancelled and updates the timestamp.
        /// </summary>
        public void Cancel()
        {
            IsCancelled = true;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Reactivates a previously cancelled sale.
        /// Changes the sale's status back to active.
        /// </summary>
        public void Reactivate()
        {
            IsCancelled = false;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Marks the sale as completed.
        /// </summary>
        public void Complete()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
