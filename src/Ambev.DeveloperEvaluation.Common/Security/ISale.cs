namespace Ambev.DeveloperEvaluation.Common.Security
{
    /// <summary>
    /// Defines the contract for a sale transaction.
    /// </summary>
    public interface ISale
    {
        /// <summary>
        /// Gets the unique identifier of the sale.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the sale number.
        /// </summary>
        string SaleNumber { get; }

        /// <summary>
        /// Gets the date of the sale.
        /// </summary>
        DateTime SaleDate { get; }

        /// <summary>
        /// Gets the customer name.
        /// </summary>
        string Customer { get; }

        /// <summary>
        /// Gets the total value of the sale.
        /// </summary>
        decimal TotalValue { get; }

        /// <summary>
        /// Gets the branch where the sale was made.
        /// </summary>
        string Branch { get; }

        /// <summary>
        /// Gets the list of sale items.
        /// </summary>
      //  List<SaleItem> Items { get; }

        /// <summary>
        /// Indicates if the sale has been cancelled.
        /// </summary>
        bool IsCancelled { get; }

        /// <summary>
        /// Cancels the sale.
        /// </summary>
        void Cancel();
    }
}
