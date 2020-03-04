using BS_Utils.Utilities;
using IPA;
using System;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

namespace BSOD_On_Miss
{
    public class Plugin : IBeatSaberPlugin
    {
        [DllImport("ntdll.dll")]
        public static extern uint RtlAdjustPrivilege(int Privilege, bool bEnablePrivilege, bool IsThreadPrivilege, out bool PreviousValue);

        [DllImport("ntdll.dll")]
        public static extern uint NtRaiseHardError(uint ErrorStatus, uint NumberOfParameters, uint UnicodeStringParameterMask, IntPtr Parameters, uint ValidResponseOption, out uint Response);

        public void Init() { }

        public void OnApplicationStart() => BSEvents.comboDidBreak += Crash;

        public void OnApplicationQuit() { }

        public void OnFixedUpdate() { }

        public void OnUpdate() { }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene) { }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) { }

        public void OnSceneUnloaded(Scene scene) { }

        static unsafe void Crash()
        {
            RtlAdjustPrivilege(19, true, false, out var t1);
            NtRaiseHardError(0xDEADDEAD, 0, 0, IntPtr.Zero, 6, out var t2);
        }
    }
}
