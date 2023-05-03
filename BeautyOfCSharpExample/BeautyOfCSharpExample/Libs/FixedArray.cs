using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixedArray_test
{

    public sealed class FixedArray<T> : IDisposable
        where T : unmanaged
    {

        private T[] _data;

        private Memory<T> _memory;

        private System.Buffers.MemoryHandle _handle;

        /*列*//*行*/
        private int _cols, _rows;

        public FixedArray(int cols, int rows)
        {
            _cols = cols;
            _rows = rows;

            _data = new T[cols * rows];
            _memory = new Memory<T>(_data);
            _handle = _memory.Pin();

            Console.WriteLine(_memory.Length);

        }


        public int Columns => _cols;

        public int Rows => _rows;


        public Span<T> GetRow(int row)
        {
            return new Span<T>(_data, (row - 1) * _cols, _cols);
        }


        //public Span<float> GetColumn(int column)
        //{g
        //    return new Span<float>()
        //}


        public T[] Data => _data;



        public unsafe T* Pointer => (T*)_handle.Pointer;



        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < _data.Length; i++)
            {
                sb.Append(_data[i]);
                sb.Append(',');
                if (i != 0 && (i + 1) % _cols == 0)
                {
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }


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
                _data = null;

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
