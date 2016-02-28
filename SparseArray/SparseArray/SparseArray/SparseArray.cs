using System;
using System.Collections;
using System.Collections.Generic;

namespace SparseArray
{
    /// <summary>
    /// Sparse array implementation
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SparseArray<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : IComparable
    {
        #region Fields

        /// <summary>
        /// Stores default value
        /// </summary>
        private readonly TValue _defaultValue;

        /// <summary>
        /// Stores key value pairs
        /// </summary>
        private readonly Dictionary<TKey, TValue> _container;

        #endregion

        #region Indexer

        /// <summary>
        /// Indexer gets value by specific key. When value doesn't exists - returns default value.
        /// </summary>
        /// <param name="key">TKey key</param>
        /// <returns>Value of type TValue</returns>
        public TValue this[TKey key]
        {
            get
            {
                TValue value;
                if (!_container.TryGetValue(key, out value))
                {
                    value = _defaultValue;
                }
                return value;
            }
            set { _container[key] = value; }
        }

        #endregion

        #region Properties

        public TValue Current
        {
            get { return _container.Values.GetEnumerator().Current; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public SparseArray()
        {
            _container = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Constructor for set default value
        /// </summary>
        /// <param name="defaultValue"></param>
        public SparseArray(TValue defaultValue)
            : this()
        {
            _defaultValue = defaultValue;
        }

        #endregion

        #region IEnumerable members

        /// <summary>
        /// Returns enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _container.GetEnumerator();
        }

        /// <summary>
        /// Returns enumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _container.Values.GetEnumerator();
        }

        /// <summary>
        /// Moves anuerator position
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            return GetEnumerator().MoveNext();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Checks if container has specific key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return _container.ContainsKey(key);
        }

        /// <summary>
        /// Checks if container has specific value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ContainsValue(TValue value)
        {
            return _container.ContainsValue(value);
        }

        /// <summary>
        /// Removes element with specific key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            return _container.Remove(key);
        }

        /// <summary>
        /// Clears container
        /// </summary>
        public void Clear()
        {
            _container.Clear();
        }

        /// <summary>
        /// Trying get value
        /// </summary>
        /// <param name="key">Key of TKey type</param>
        /// <param name="value">Value of TValue type</param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _container.TryGetValue(key, out value);
        }

        #endregion
    }
}
