using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sining
{
    [AsyncMethodBuilder(typeof(AsyncSVoidMethodBuilder))]
    [StructLayout(LayoutKind.Auto)]
    public struct SVoid
    {
        public void Coroutine(){}
    }
}