using System.Collections.Generic;

namespace BHub.Lib.Interfaces
{
    /// <summary>
    /// Delegate type for callback prepaging of the data
    /// </summary>
    /// <param name="source">The source collection.</param>
    /// <returns></returns>
    public delegate IEnumerable<object> PagedEnumerableCallBack(IEnumerable<object> source);

    /// <summary>
    /// Wrapper class around <see cref="IEnumerable{T}" /> splitting up the collection
    /// into paged sections. Calling the standard enumerator for this class returns the paged data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="IEnumerable{T}" />
    public interface IPagedEnumerable<T> : IEnumerable<T>
    {
        /// <summary>
        /// Gets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize { get; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>
        /// The page number.
        /// </value>
        public int PageNumber { get; }

        /// <summary>
        /// Gets the page count.
        /// </summary>
        /// <value>
        /// The page count.
        /// </value>
        public int PageCount { get; }

        /// <summary>
        /// Gets a value indicating whether this instance can go previous.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can go previous; otherwise, <c>false</c>.
        /// </value>
        public bool CanGoPrevious { get; }

        /// <summary>
        /// Gets a value indicating whether this instance can go next.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can go next; otherwise, <c>false</c>.
        /// </value>
        public bool CanGoNext { get; }

        /// <summary>
        /// If possible, will turn to the previous page.
        /// </summary>
        public void GoPrevious();

        /// <summary>
        /// If possible, will turn to the next page.
        /// </summary>
        public void GoNext();

        /// <summary>
        /// If possible, will turn to page provided.
        /// </summary>
        public void GotoPage(int pageNumber);

        /// <summary>
        /// Gets the filtered count.
        /// </summary>
        /// <returns></returns>
        public int GetFilteredCount();

        /// <summary>
        /// Gets the source count. This does not return the same as Count()
        /// This is the count of all the items in the source, Count() returns the number of items in the current page context.
        /// </summary>
        /// <returns></returns>
        public int GetSourceCount();

        /// <summary>
        /// Updates the source object with the new parameter <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="newSource"></param>
        public void UpdateSource(IEnumerable<T> newSource);

        /// <summary>
        /// CallBack function which is called before the paged collection is returned.
        /// The function takes in the source collection and expects the same type <see cref="IEnumerable{T}"/> returned.
        /// </summary>
        /// <value>
        /// The page intercept callback method.
        /// </value>
        public PagedEnumerableCallBack PagedEnumerableCallBack { get; set; }
    }
}