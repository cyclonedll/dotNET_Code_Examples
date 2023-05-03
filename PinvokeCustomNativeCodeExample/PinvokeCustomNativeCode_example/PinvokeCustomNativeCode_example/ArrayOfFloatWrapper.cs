

namespace PinvokeCustomNativeCode_example
{
    using System.Runtime.InteropServices;

    public unsafe class ArrayOfFloatWrapper : IDisposable
    {

        private const string Library_X64 = "NativeLib.dll";

        [DllImport(Library_X64, EntryPoint = "AOF_Create")]
        private static extern float* AOF_Create(int count);

        [DllImport(Library_X64, EntryPoint = "AOF_Delete")]
        private static extern void AOF_Delete(float* aof);

        private readonly float* _aof;

        private readonly int _count;

        public ArrayOfFloatWrapper(int count)
        {
            _aof = AOF_Create(count);
            _count = count;
        }

        public int Count => _count;

        public float* Pointer => _aof;

        public float this[int index]
        {
            get => *(_aof + index);
            set
            {
                if (index < 0 || index >= _count)
                    throw new ArgumentOutOfRangeException("index");

                *(_aof + index) = value;
            }
        }

        public void Dispose()
        {
            AOF_Delete(_aof);
        }
    }
}
