using BHub.Lib.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BHub.Lib.Collections
{
    public class PagedEnumerable<T> : IPagedEnumerable<T> where T : class
    {
        private int m_filteredCount;
        private IEnumerable<T> m_source;

        private decimal m_currentCount => !(PagedEnumerableCallBack is null) ? m_filteredCount : m_source.Count();

        public PagedEnumerable(IEnumerable<T> source, int pageSize, int startPage = 0)
        {
            m_source = source;

            PageSize = pageSize;
            PageNumber = startPage;
        }

        public void GoPrevious()
        {
            if (CanGoPrevious)
            {
                PageNumber--;
            }
        }

        public void GoNext()
        {
            if (CanGoNext)
            {
                PageNumber++;
            }
        }

        public void GotoPage(int pageNumber)
        {
            if (pageNumber < 0 || pageNumber > PageCount)
            {
                return;
            }

            PageNumber = pageNumber;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var source = m_source;

            if (!(PagedEnumerableCallBack is null))
            {
                source = PagedEnumerableCallBack(m_source) as IEnumerable<T>;

                m_filteredCount = source.Count();
            }

            return source.Skip(PageNumber * PageSize).Take(PageSize).GetEnumerator();
        }

        public void UpdateSource(IEnumerable<T> newSource)
        {
            m_source = newSource;
        }


        public int PageSize { get; }

        public int PageNumber { get; private set; }

        public int PageCount => (int)Math.Ceiling(m_currentCount / PageSize);

        public Type ElementType => typeof(T);

        IEnumerator IEnumerable.GetEnumerator() => m_source.GetEnumerator();

        public int GetFilteredCount() => m_filteredCount;

        public int GetSourceCount() => m_source.Count();

        public bool CanGoPrevious => PageNumber > 0;

        public bool CanGoNext => PageNumber + 1 < PageCount;

        public Expression Expression => throw new NotImplementedException();

        public IQueryProvider Provider => throw new NotImplementedException();

        public PagedEnumerableCallBack PagedEnumerableCallBack { get; set; }
    }
}