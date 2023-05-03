namespace PinvokeCustomNativeCode_example
{
    public class FixedArray
    {

        private float[] _data;

        private Memory<float> _memory;

        private System.Buffers.MemoryHandle _handle;

        private int _count;

        public FixedArray(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");

            _count = count;
            _data = new float[count];
            _memory = new Memory<float>(_data);
            _handle = _memory.Pin();
        }


        public int Count => _count;

        public float[]? Data => _data;

        public unsafe float* Pointer => (float*)_handle.Pointer;

        public float this[int index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
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

                _disposed = true;
            }
        }


        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }



        #endregion

    }
}
