#define X86

namespace PinvokeCustomNativeCode_example
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    public class VectorOfFloatWrapper : IEnumerable<float>, IDisposable
    {
        internal const string Library_X64 = "NativeLib.dll";

        #region native extern

        [DllImport(Library_X64, EntryPoint = "VOF_Create")]
        private static extern IntPtr Create();

        [DllImport(Library_X64, EntryPoint = "VOF_Create_Count")]
        private static extern IntPtr Create_Count(int count);


        [DllImport(Library_X64, EntryPoint = "VOF_Add")]
        private static extern void Add(IntPtr vof, float value);


        [DllImport(Library_X64, EntryPoint = "VOF_Remove")]
        private static extern void Remove(IntPtr vof, int index);


        [DllImport(Library_X64, EntryPoint = "VOF_RemoveRange")]
        private static extern void RemoveRange(IntPtr vof, int begin, int length);


        [DllImport(Library_X64, EntryPoint = "VOF_GetElement")]
        private static extern float GetElement(IntPtr vof, int index);


        [DllImport(Library_X64, EntryPoint = "VOF_SetElement")]
        private static extern float SetElement(IntPtr vof, int index, float value);


        [DllImport(Library_X64, EntryPoint = "VOF_Zeros")]
        private static extern float Zeros(IntPtr vof);


        [DllImport(Library_X64, EntryPoint = "VOF_TrimExcess")]
        private static extern void TrimExcess(IntPtr vof);


        [DllImport(Library_X64, EntryPoint = "VOF_Clear")]
        private static extern void Clear(IntPtr vof);


        [DllImport(Library_X64, EntryPoint = "VOF_GetSize")]
        private static extern IntPtr GetSize(IntPtr vof);


        [DllImport(Library_X64, EntryPoint = "VOF_GetCapacity")]
        private static extern IntPtr GetCapacity(IntPtr vof);


        [DllImport(Library_X64, EntryPoint = "VOF_IsEmpty")]
        private static extern IntPtr IsEmpty(IntPtr vof);


        [DllImport(Library_X64, EntryPoint = "VOF_GetDataPointer")]
        private static extern IntPtr GetDataPointer(IntPtr vof);


        [DllImport(Library_X64, EntryPoint = "VOF_Delete")]
        private static extern void Delete(IntPtr vof);


        [DllImport(Library_X64, EntryPoint = "VOF_GetMax")]
        private static extern float GetMax(IntPtr vof);

        [DllImport(Library_X64, EntryPoint = "Dot")]
        public static extern unsafe float Dot(float* a, float* b, uint len);


        #endregion


        private IntPtr _vof;

        public VectorOfFloatWrapper() => _vof = Create();

        public VectorOfFloatWrapper(int count) => _vof = Create_Count(count);

        public void Add(float value) => Add(_vof, value);

        public void AddRange(IEnumerable<float> values)
        {
            foreach (var ele in values) Add(_vof, ele);
        }

        public async Task AddRangeAsync(IEnumerable<float> values)
        {
            await Task.Run(() =>
            {
                AddRange(values);
            });
        }


        public void Remove(int index) => Remove(_vof, index);

        public void Remove(int begin, int length) => RemoveRange(_vof, begin, length);

        public float this[int index]
        {
            get => GetElement(_vof, index);
            set => SetElement(_vof, index, value);
        }


        public void Zeros() => Zeros(_vof);

        public void TrimExcess() => TrimExcess(_vof);

        public void Clear() => Clear(_vof);

#if X64
        //直接int虽然怪异 但是方便 就这样吧先
        public long Size => GetSize(_vof).ToInt64();

        public long Capacity => GetCapacity(_vof).ToInt64();
#elif X86
        public int Size => GetSize(_vof).ToInt32();

        public int Capacity => GetCapacity(_vof).ToInt32();
#endif

        public unsafe float* Pointer => (float*)GetDataPointer(_vof);


        public float GetMax() => GetMax(_vof);

        public float[] ManagedArray
        {
            get
            {
                var rt = new float[Size];
                Marshal.Copy(GetDataPointer(_vof), rt, 0, (int)Size);
                return rt;
            }
        }

        public void Dispose() => Delete(_vof);

        public IEnumerator<float> GetEnumerator()
        {
            return new VectorOfFloatEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new VectorOfFloatEnumerator(this);
        }


        public class VectorOfFloatEnumerator : IEnumerator<float>
        {

            private VectorOfFloatWrapper _vof;
            private int _position = -1;

            public VectorOfFloatEnumerator(VectorOfFloatWrapper vof)
            {
                _vof = vof;
            }

            public float Current => _vof[_position];

            object IEnumerator.Current => Current;


            public bool MoveNext()
            {
                _position++;
                return (_position < _vof.Size);
            }

            public void Reset()
            {
                _position = -1;
            }

            public void Dispose()
            {
                _vof = null;
            }

        }
    }
}
