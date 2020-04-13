using BS_Utils.Utilities;
using IPA;
using System;
using System.Runtime.InteropServices;

namespace BSOD_On_Miss
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        [DllImport("ntdll.dll")]
        public static extern uint RtlAdjustPrivilege(int Privilege, bool bEnablePrivilege, bool IsThreadPrivilege, out bool PreviousValue);

        [DllImport("ntdll.dll")]
        public static extern uint NtRaiseHardError(uint ErrorStatus, uint NumberOfParameters, uint UnicodeStringParameterMask, IntPtr Parameters, uint ValidResponseOption, out uint Response);

        [OnEnable]
        public void OnEnable() => BSEvents.comboDidBreak += Crash;

        [OnDisable]
        public void OnDisable() => BSEvents.comboDidBreak -= Crash;

        static unsafe void Crash()
        {
            RtlAdjustPrivilege(19, true, false, out var t1);
            NtRaiseHardError(0xDEADDEAD, 0, 0, IntPtr.Zero, 6, out var t2);
        }
    }
}
