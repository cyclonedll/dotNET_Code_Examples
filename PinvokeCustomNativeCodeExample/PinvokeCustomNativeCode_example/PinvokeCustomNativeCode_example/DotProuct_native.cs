using System.Reflection;
using System.Runtime.InteropServices;

namespace PinvokeCustomNativeCode_example
{
    public static class DotProuct_native
    {

        [DllImport("NativeLib.dll", EntryPoint = "Dot")]
        public static extern unsafe float Dot(float* a, float* b, uint len);


        [DllImport("NativeLib_sse.dll", EntryPoint = "Dot")]
        public static extern unsafe float Dot_sse(float* a, float* b, uint len);

        public static void ImportNativeLib()
        {
            // Register the import resolver before calling the imported function.
            // Only one import resolver can be set for a given assembly.
            NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), DllImportResolver);

        }
        private static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {

            if (libraryName == "NativeLib.dll")
            {
                // On systems with SSE support, load a different library.
                if (System.Runtime.Intrinsics.X86.Sse.IsSupported)
                {
                    return NativeLibrary.Load("NativeLib_sse.dll", assembly, searchPath);
                }
            }

            // Otherwise, fallback to default import resolver.
            return IntPtr.Zero;
        }
    }
}
