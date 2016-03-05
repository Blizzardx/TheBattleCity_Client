using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security;
using System.Collections.Generic;

namespace System.Collections.Generic
{
    /// <summary>
    /// 在.net2.0环境下提供HashSet(Of T)类型的模拟实现
    /// </summary>
    /// <typeparam name="T">集合中的元素类型</typeparam>
    public class HashSet<T> : ICollection<T>, IEnumerable<T>, IEnumerable, ISerializable, IDeserializationCallback
    {
        private readonly Dictionary<T, object> dict;

        #region constructors
        /// <summary>
        /// Initializes a new instance of the System.Collections.Generic.HashSet<T> class
        /// that is empty and uses the default equality comparer for the set type.
        /// </summary>
        public HashSet()
        {
            this.dict = new Dictionary<T, object>();
        }

        /// <summary>
        /// Initializes a new instance of the System.Collections.Generic.HashSet<T> class
        /// that uses the default equality comparer for the set type, contains elements
        /// copied from the specified collection, and has sufficient capacity to accommodate
        /// the number of elements copied.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new set.</param>
        /// <exception cref="ArgumentNullException">collection is null.</exception>
        public HashSet(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            this.dict = new Dictionary<T, object>();
            foreach (T item in collection)
                this.dict[item] = null;
        }

        /// <summary>
        /// Initializes a new instance of the System.Collections.Generic.HashSet<T> class
        /// that is empty and uses the specified equality comparer for the set type.
        /// </summary>
        /// <param name="comparer">
        /// The System.Collections.Generic.IEqualityComparer<T> implementation to use
        /// when comparing values in the set, or null to use the default System.Collections.Generic.EqualityComparer<T>
        /// implementation for the set type.
        /// </param>
        public HashSet(IEqualityComparer<T> comparer)
        {
            this.dict = new Dictionary<T, object>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the System.Collections.Generic.HashSet<T> class
        /// that uses the specified equality comparer for the set type, contains elements
        /// copied from the specified collection, and has sufficient capacity to accommodate
        /// the number of elements copied.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new set.</param>
        /// <param name="comparer">
        /// The System.Collections.Generic.IEqualityComparer<T> implementation to use
        /// when comparing values in the set, or null to use the default System.Collections.Generic.EqualityComparer<T>
        /// implementation for the set type.
        /// </param>
        /// <exception cref="ArgumentNullException">collection is null.</exception>
        public HashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            this.dict = new Dictionary<T, object>(comparer);
            foreach (T item in collection)
                this.dict[item] = null;
        }
        #endregion

        #region properties
        /// <summary>
        ///  Gets the System.Collections.Generic.IEqualityComparer<T> object that is used
        ///  to determine equality for the values in the set.
        /// </summary>
        public IEqualityComparer<T> Comparer { get { return this.dict.Comparer; } }

        /// <summary>
        /// Gets the number of elements that are contained in a set.
        /// </summary>
        public int Count { get { return this.dict.Count; } }

        #endregion

        #region methods

        /// <summary>
        /// Adds the specified element to a set.
        /// </summary>
        /// <param name="item">The element to add to the set.</param>
        /// <returns>
        /// true if the element is added to the System.Collections.Generic.HashSet<T> object; 
        /// false if the element is already present.
        /// </returns>
        public bool Add(T item)
        {
            if (this.dict.ContainsKey(item))
            {
                return false;
            }
            else
            {
                this.dict[item] = null;
                return true;
            }
        }

        /// <summary>
        /// Removes all elements from a System.Collections.Generic.HashSet<T> object.
        /// </summary>
        public void Clear()
        {
            this.dict.Clear();
        }

        /// <summary>
        /// Determines whether a System.Collections.Generic.HashSet<T> object contains the specified element.
        /// </summary>
        /// <param name="item">The element to locate in the System.Collections.Generic.HashSet<T> object.</param>
        /// <returns>true if the System.Collections.Generic.HashSet<T> object contains the specified element; otherwise, false.</returns>
        public bool Contains(T item)
        {
            return this.dict.ContainsKey(item);
        }

        /// <summary>
        /// Copies the elements of a System.Collections.Generic.HashSet<T> object to an array.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional array that is the destination of the elements copied
        /// from the System.Collections.Generic.HashSet<T> object. The array must have
        /// zero-based indexing.
        /// </param>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        public void CopyTo(T[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            this.dict.Keys.CopyTo(array, 0);
        }

        /// <summary>
        /// Copies the elements of a System.Collections.Generic.HashSet<T> object to an array, starting at the specified array index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional array that is the destination of the elements copied
        /// from the System.Collections.Generic.HashSet<T> object. The array must have
        /// zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        /// <exception cref="System.ArgumentException">arrayIndex is greater than the length of the destination array.  -or- count is larger than the size of the destination array.</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException();
            if (arrayIndex >= array.Length)
                throw new ArgumentException("arrayIndex is greater than the length of the destination array.");
            if (Count >= array.Length)
                throw new ArgumentException("the Count property is larger than the size of the destination array.");

            this.dict.Keys.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Copies the specified number of elements of a System.Collections.Generic.HashSet<T> object to an array, 
        /// starting at the specified array index.
        /// </summary>
        /// <param name="array">The one-dimensional array that is the destination of the elements copied from the System.Collections.Generic.HashSet<T> object. The array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <param name="count">The number of elements to copy to array.</param>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">arrayIndex is less than 0.  -or- count is less than 0.</exception>
        /// <exception cref="System.ArgumentException">arrayIndex is greater than the length of the destination array.  -or- count is greater than the available space from the index to the end of the destination array.</exception>
        public void CopyTo(T[] array, int arrayIndex, int count)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (arrayIndex < 0 || count < 0)
                throw new ArgumentOutOfRangeException();
            if (arrayIndex >= array.Length)
                throw new ArgumentException("arrayIndex is greater than the length of the destination array.");
            if (count > array.Length - arrayIndex)
                throw new ArgumentException("count is greater than the available space from the index to the end of the destination array.");

            int copiedCount = 0;
            int currentIndex = arrayIndex;
            foreach (T item in this)
            {
                array[currentIndex] = item;
                currentIndex++;
                copiedCount++;
                if (copiedCount >= count)
                    break;
            }
        }

        /// <summary>
        /// Removes all elements in the specified collection from the current System.Collections.Generic.HashSet<T> object.
        /// </summary>
        /// <param name="other">The collection of items to remove from the System.Collections.Generic.HashSet<T> object.</param>
        /// <exception cref="System.ArgumentNullException">other is null.</exception>
        public void ExceptWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            foreach (T item in other)
                this.dict.Remove(item);
        }

        /// <summary>
        /// Modifies the current System.Collections.Generic.HashSet<T> object to contain
        /// only elements that are present in that object and in the specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current System.Collections.Generic.HashSet<T> object.</param>
        public void IntersectWith(IEnumerable<T> other)
        {
            this.dict.Clear();
            foreach (T item in other)
                this.dict[item] = null;
        }

        /// <summary>
        /// Determines whether a System.Collections.Generic.HashSet<T> object is a proper subset of the specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current System.Collections.Generic.HashSet<T> object.</param>
        /// <returns>true if the System.Collections.Generic.HashSet<T> object is a proper subset of other; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">other is null.</exception>
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return IsSubsetOf(other, true);
        }

        /// <summary>
        /// Determines whether a System.Collections.Generic.HashSet<T> object is a subset of the specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current System.Collections.Generic.HashSet<T> object.</param>
        /// <returns>true if the System.Collections.Generic.HashSet<T> object is a subset of other; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">other is null.</exception>
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return IsSubsetOf(other, false);
        }

        private bool IsSubsetOf(IEnumerable<T> other, bool proper)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            int elementCount = 0;
            int mathingCount = 0;
            foreach (T item in other)
            {
                elementCount++;
                if (this.dict.ContainsKey(item))
                    mathingCount++;
            }
            if (proper)
                return mathingCount == this.dict.Count && elementCount > this.dict.Count;
            else
                return mathingCount == this.dict.Count;
        }

        /// <summary>
        /// Determines whether a System.Collections.Generic.HashSet<T> object is a proper superset of the specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current System.Collections.Generic.HashSet<T> object.</param>
        /// <returns>true if the System.Collections.Generic.HashSet<T> object is a proper superset of other; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">other is null.</exception>
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return IsSupersetOf(other, true);
        }

        /// <summary>
        /// Determines whether a System.Collections.Generic.HashSet<T> object is a superset of the specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current System.Collections.Generic.HashSet<T> object.</param>
        /// <returns>true if the System.Collections.Generic.HashSet<T> object is a superset of other; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">other is null.</exception>
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return IsSupersetOf(other, false);
        }

        private bool IsSupersetOf(IEnumerable<T> other, bool proper)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            int elementCount = 0;
            foreach (T item in other)
            {
                elementCount++;
                if (!this.dict.ContainsKey(item))
                    return false;
            }
            if (proper)
                return elementCount < this.dict.Count;
            else
                return true;
        }

        /// <summary>
        /// Determines whether the current System.Collections.Generic.HashSet<T> object overlaps the specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current System.Collections.Generic.HashSet<T> object.</param>
        /// <returns>true if the System.Collections.Generic.HashSet<T> object and other share at least one common element; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">other is null.</exception>
        public bool Overlaps(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            foreach (T item in other)
            {
                if (this.dict.ContainsKey(item))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the specified element from a System.Collections.Generic.HashSet<T> object.
        /// </summary>
        /// <param name="item">The element to remove.</param>
        /// <returns>
        /// true if the element is successfully found and removed; otherwise, false.
        /// This method returns false if item is not found in the System.Collections.Generic.HashSet<T> object.
        /// </returns>
        public bool Remove(T item)
        {
            if (this.dict.ContainsKey(item))
            {
                this.dict.Remove(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Removes all elements that match the conditions defined by the specified predicate from a System.Collections.Generic.HashSet<T> collection.
        /// </summary>
        /// <param name="match">The System.Predicate<T> delegate that defines the conditions of the elements to remove.</param>
        /// <returns>The number of elements that were removed from the System.Collections.Generic.HashSet<T> collection.</returns>
        public int RemoveWhere(Predicate<T> match)
        {
            int removeCount = 0;
            foreach (KeyValuePair<T, object> item in this.dict)
            {
                if (match(item.Key))
                {
                    this.dict.Remove(item.Key);
                    removeCount++;
                }
            }
            return removeCount;
        }

        /// <summary>
        /// Determines whether a System.Collections.Generic.HashSet<T> object and the specified collection contain the same elements.
        /// </summary>
        /// <param name="other">The collection to compare to the current System.Collections.Generic.HashSet<T> object.</param>
        /// <returns>true if the System.Collections.Generic.HashSet<T> object is equal to other; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">other is null.</exception>
        public bool SetEquals(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            int containsCount = 0;
            foreach (T item in other)
            {
                if (!this.dict.ContainsKey(item))
                    return false;
                else
                    containsCount++;
            }
            return containsCount == this.dict.Count;
        }

        /// <summary>
        /// Modifies the current System.Collections.Generic.HashSet<T> object to contain only elements that are present either in that object or in the specified collection, but not both.
        /// </summary>
        /// <param name="other">The collection to compare to the current System.Collections.Generic.HashSet<T>  object.</param>
        /// <exception cref="System.ArgumentNullException">other is null.</exception>
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            HashSet<T> tmpSet = new HashSet<T>(other);
            foreach (T item in tmpSet)
            {
                if (this.dict.ContainsKey(item))
                    this.dict.Remove(item);
                else
                    this.dict[item] = null;
            }
        }

        /// <summary>
        /// Sets the capacity of a System.Collections.Generic.HashSet<T> object to the
        /// actual number of elements it contains, rounded up to a nearby, implementation-specific
        /// value.
        /// </summary>
        public void TrimExcess()
        {
            //do nothing
        }

        /// <summary>
        /// Modifies the current System.Collections.Generic.HashSet<T> object to contain
        /// all elements that are present in both itself and in the specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current System.Collections.Generic.HashSet<T> object.</param>
        /// <exception cref="System.ArgumentNullException">other is null.</exception>
        public void UnionWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            foreach (T item in other)
                if (!this.dict.ContainsKey(item))
                    this.dict[item] = null;
        }
        #endregion

        #region ICollection<T>, IEnumerable<T>, IEnumerable成员

        /// <summary>
        /// Returns an enumerator that iterates through a System.Collections.Generic.HashSet<T> object.
        /// </summary>
        /// <returns>A System.Collections.Generic.HashSet<T>.Enumerator object for the System.Collections.Generic.HashSet<T> object.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.dict.Keys.GetEnumerator();
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotImplementedException();
        }

        /// <summary> 
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only. 
        /// </summary> 
        /// <returns> 
        /// true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false. 
        /// </returns> 
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary> 
        /// Returns an enumerator that iterates through the collection. 
        /// </summary> 
        /// <returns> 
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection. 
        /// </returns> 
        public IEnumerator<T> GetEnumerator()
        {
            return this.dict.Keys.GetEnumerator();
        }

        #endregion

        #region ISerializable 成员

        /// <summary> 
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object. 
        /// </summary> 
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data. </param><param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization. </param><exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception> 
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");
            dict.GetObjectData(info, context);
        }

        #endregion

        #region IDeserializationCallback 成员

        /// <summary> 
        /// Runs when the entire object graph has been deserialized. 
        /// </summary> 
        /// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented. </param> 
        public void OnDeserialization(object sender)
        {
            this.dict.OnDeserialization(sender);
        }

        #endregion

        #region Enumerator
        /// <summary>
        /// Enumerates the elements of a System.Collections.Generic.HashSet<T> object.
        /// </summary>
        private struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
        {
            /// <summary>
            /// Gets the element at the current position of the enumerator.
            /// </summary>
            /// <returns>The element in the System.Collections.Generic.HashSet<T> collection at the current position of the enumerator.</returns>
            public T Current
            {
                get { throw new NotImplementedException(); }
            }

            /// <summary>
            /// Releases all resources used by a System.Collections.Generic.HashSet<T>.Enumerator object.
            /// </summary>
            public void Dispose()
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// Advances the enumerator to the next element of the System.Collections.Generic.HashSet<T> collection.
            /// </summary>
            /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
            /// <exception cref="System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            object IEnumerator.Current
            {
                get { throw new NotImplementedException(); }
            }
        }
        #endregion
    }
}