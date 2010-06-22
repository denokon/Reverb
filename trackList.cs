using System;
using System.Collections;
using System.Collections.Generic;

namespace Reverb
{
    public enum ChangeType { Added, Changed, Inserted, Removed }

    public class EventList<T> : IList<T>
    {
        private IList<T> internalList;

        public class ListChangedEventArgs : EventArgs
        {
            public int index;
            public T item;
            public ChangeType changeType;
            public ListChangedEventArgs(int index, T item, ChangeType changeType)
            {
                this.index = index;
                this.item = item;
                this.changeType = changeType;
            }
        }

        public delegate void ListChangedEventHandler(object source, ListChangedEventArgs e);
        public delegate void ListClearedEventHandler(object source, EventArgs e);
        public event ListChangedEventHandler ListChanged;
        public event ListClearedEventHandler ListCleared;

        public EventList()
        {
            internalList = new List<T>();
        }

        public EventList(IList<T> list)
        {
            internalList = list;
        }

        public EventList(IEnumerable<T> collection)
        {
            internalList = new List<T>(collection);
        }

        protected virtual void OnListChanged(ListChangedEventArgs e)
        {
            if (ListChanged != null)
                ListChanged(this, e);
        }

        protected virtual void OnListCleared(EventArgs e)
        {
            if (ListCleared != null)
                ListCleared(this, e);
        }

        public int IndexOf(T item)
        {
            return internalList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            internalList.Insert(index, item);
            OnListChanged(new ListChangedEventArgs(index, item, ChangeType.Inserted));
        }

        public void RemoveAt(int index)
        {
            T item = internalList[index];
            internalList.Remove(item);
            OnListChanged(new ListChangedEventArgs(index, item, ChangeType.Removed));
        }

        public T this[int index]
        {
            get { return internalList[index]; }
            set
            {
                internalList[index] = value;
                OnListChanged(new ListChangedEventArgs(index, value, ChangeType.Changed));
            }
        }

        public void Add(T item)
        {
            internalList.Add(item);

            OnListChanged(new ListChangedEventArgs(internalList.IndexOf(item), item, ChangeType.Added));
        }

        public void Clear()
        {
            internalList.Clear();
            OnListCleared(new EventArgs());
        }

        public bool Contains(T item)
        {
            return internalList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return internalList.Count; }
        }

        public bool IsReadOnly
        {
            get { return IsReadOnly; }
        }

        public bool Remove(T item)
        {
            lock (this)
            {
                int index = internalList.IndexOf(item);
                try
                {
                    internalList.RemoveAt(index);
                    OnListChanged(new ListChangedEventArgs(index, item, ChangeType.Removed));
                    return true;
                }
                catch (Exception ex)
                {
                    if (!(ex is ArgumentOutOfRangeException) && !(ex is NotSupportedException))
                        throw;

                    return false;
                }

            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)internalList).GetEnumerator();
        }
    }
}