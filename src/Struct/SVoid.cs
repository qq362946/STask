using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    [AsyncMethodBuilder(typeof(AsyncSVoidMethodBuilder))]
    [StructLayout(LayoutKind.Auto)]
    public readonly struct SVoid
    {
        public void Coroutine(){}
    }
}