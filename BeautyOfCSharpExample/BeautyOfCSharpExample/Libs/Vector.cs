using System.Collections;
using System.Text;

namespace Libs
{

    using Libs;

    /// <summary>
    /// A size-fixed one-dim array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Vector<T> : IDisposable, IEnumerable, IEnumerable<T>
        where T : unmanaged, IComparable, IComparable<T>
    {
        private T[] _data;

        private Memory<T> _memory;

        private System.Buffers.MemoryHandle _handle;

        private int _count;

        public Vector(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");

            _count = count;
            _data = new T[count];
            _memory = new Memory<T>(_data);
            _handle = _memory.Pin();
        }


        public Vector(T[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            _count = (int)array.Length;
            _data = array;
            _memory = new Memory<T>(_data);
            _handle = _memory.Pin();
        }

        public int Count => _count;

        public T[]? Data => _data;

        public unsafe T* Pointer => (T*)_handle.Pointer;

        public T this[int index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }


        public override string ToString()
        {


            var sb = new StringBuilder();

            const int threshold = 200;
            const int printCount = 20;

            if (_count < threshold)
            {
                sb.Append('[');
                foreach (var item in _data)
                {
                    sb.Append(item);
                    sb.Append(',');
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append(']');
            }
            else //>1000
            {
                sb.Append('[');
                for (nuint i = 0; i < printCount; i++)
                {
                    sb.Append(_data[i]);
                    sb.Append(',');
                }
                sb.Append("......");
                sb.Append(',');

                for (int i = _count - printCount; i < _count; i++)
                {
                    sb.Append(_data[i]);
                    sb.Append(',');
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append(']');
            }
            return sb.ToString();
        }



        public IEnumerator GetEnumerator() => _data.GetEnumerator();


        IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)_data).GetEnumerator();



        #region type conversion

        //把自己转成其它，此时需要为显式的
        public static explicit operator T[](Vector<T> vector)
            => vector._data;


        public static unsafe explicit operator T*(Vector<T> vector)
            => vector.Pointer;


        //language BUG:

        //public static implicit operator Vector<T>(float[] array)
        //    where
        //    => new Vector<float>(array);




        #endregion



        #region Max


        public T Max() => MaxHelper.Max(_data);


        public T Max(int start, int length) => MaxHelper.Max(_data, start, length);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="numberOfWorkers">This paramater specify the number of cores to compute, 
        /// this value is less than or equal to <strong>Environment.ProcessorCount</strong>.</param>
        /// <param name="useTPL">If ture, use TPL and ingore the paramater <strong>numberOfWorkers</strong>,otherwise use self-defined methods instead.</param>
        /// <returns></returns>
        public async Task<T> MaxAsync(int? numberOfWorkers = null, bool useTPL = false)
            => await _data.MaxAsync(numberOfWorkers, useTPL);


        /// <summary>
        /// Returns the maximum value in a parallel sequence of Data.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="numberOfWorkers">his paramater specify the number of cores to compute,this value is less than or equal to <strong>Environment.ProcessorCount</strong>.</param>
        /// <param name="useTPL">If ture, use TPL and ingore the paramater <strong>numberOfWorkers</strong>,otherwise use self-defined methods instead.</param>
        /// <returns></returns>
        public async Task<T> MaxAsync(int start, int length, int? numberOfWorkers = null, bool useTPL = false)
            => await _data.MaxAsync(start, length, numberOfWorkers, useTPL);



        /// <summary>
        /// Returns the maximum value and its index in a sequential sequence of Data.
        /// </summary>
        /// <returns></returns>
        public (Index, T) LocateMax() => MaxHelper.LocateMax(_data);


        public (Index, T) LocateMax(int start, int length) => MaxHelper.LocateMax(_data, start, length);


        public async Task<(Index, T)> LocateMaxAsync(int? numberOfWorkers = null, bool useTPL = false)
            => await _data.LocateMaxAsync(numberOfWorkers, useTPL);


        public async Task<(Index, T)> LocateMaxAsync(int start, int length, int? numberOfWorkers = null, bool useTPL = false)
            => await _data.LocateMaxAsync(start, length, numberOfWorkers, useTPL);

        #endregion



        #region Min

        public T Min() => MinHelper.Min(_data);


        public T Min(int start, int length) => MinHelper.Min(_data, start, length);


        public async Task<T> MinAsync(int? numberOfWorkers = null, bool useTPL = false)
            => await _data.MinAsync(numberOfWorkers, useTPL);


        public async Task<T> MinAsync(int start, int length, int? numberOfWorkers = null, bool useTPL = false)
            => await _data.MinAsync(start, length, numberOfWorkers, useTPL);


        public (Index, T) LocateMin() => MinHelper.LocateMin(_data);


        public (Index, T) LocateMin(int start, int length) => MinHelper.LocateMin(_data, start, length);


        public async Task<(Index, T)> LocateMinAsync(int? numberOfWorkers = null, bool useTPL = false)
            => await _data.LocateMinAsync(numberOfWorkers, useTPL);


        public async Task<(Index, T)> LocateMinAsync(int start, int length, int? numberOfWorkers = null, bool useTPL = false)
            => await _data.LocateMinAsync(start, length, numberOfWorkers, useTPL);


        #endregion




        #region dispose pattern

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _handle.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                //_data = null;

                _disposed = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Signal()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }



        #endregion

    }

}