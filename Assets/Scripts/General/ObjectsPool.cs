using System;
using System.Collections.Generic;

namespace General
{
    public class ObjectsPool<T>
    {
        private Stack<T> _free;
        private HashSet<T> _inUse;
        private readonly Func<T> _factory;

        public ObjectsPool(Func<T> factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("Factory method can not be null!");
            }
            _factory = factory;
            _free = new();
            _inUse = new();
        }

        public T Get()
        {
            T result = default;
            if (_free.Count > 0)
            {
                result = _free.Pop();
            }
            else
            {
                result = _factory.Invoke();
            }
            _inUse.Add(result);
            return result;
        }

        public void Free(T inst)
        {
            _inUse.Remove(inst);
            _free.Push(inst);
        }
    }
}