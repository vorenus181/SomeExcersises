using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SortedQueue
{
    public class SortedQueue<TElement> : IEnumerable<TElement>
        where TElement : IComparable
    {
        #region Fields

        /// <summary>
        /// Capacity of queue
        /// </summary>
        private readonly int _maxCapacity = 20;
        private readonly SortedList<Guid, TElement> _container;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SortedQueue()
        {
            _container = new SortedList<Guid, TElement>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maxCapacity">Capacity of queue</param>
        public SortedQueue(int maxCapacity)
            : this()
        {
            _maxCapacity = maxCapacity;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Adds element
        /// </summary>
        /// <param name="element">Element of type TElement</param>
        public void Enqueue(TElement element)
        {
            if (_container.Count > _maxCapacity)
            {
                RemoveMin();
            }
            _container.Add(Guid.NewGuid(), element);
        }

        /// <summary>
        /// Gets element adn removes from queue
        /// </summary>
        /// <returns>Element of TElement type</returns>
        public TElement DequeueMaxElement()
        {
            return RemoveAndGetMax();
        }

        #endregion

        #region IEnumerable members

        /// <summary>
        /// Gets enumerator
        /// </summary>
        /// <returns>Enemuretor of type TElement</returns>
        public IEnumerator<TElement> GetEnumerator()
        {
            return _container.Values.GetEnumerator();
        }

        /// <summary>
        /// Gets enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
        
        #region Private methods

        /// <summary>
        /// Removes element of minimal value
        /// </summary>
        /// <returns></returns>
        private void RemoveMin()
        {
            var minValue = _container.Values.Min();
            var indexOfValue = _container.IndexOfValue(minValue);
            _container.RemoveAt(indexOfValue);
        }

        /// <summary>
        /// Gets and removes element of maximum value
        /// </summary>
        /// <returns></returns>
        private TElement RemoveAndGetMax()
        {
            var maxValue = _container.Values.Max();
            var indexOfValue = _container.IndexOfValue(maxValue);
            _container.RemoveAt(indexOfValue);
            return maxValue;
        }

        #endregion

    }
}
