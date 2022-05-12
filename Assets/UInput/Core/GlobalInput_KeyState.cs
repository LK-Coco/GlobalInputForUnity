using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

namespace UInput
{

    internal class GlobalInput_KeyState
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int GetAsyncKeyState(int vKey);

        public void UpdateKeyState()
        {
            foreach (VirtualKeycode key in Enum.GetValues(typeof(VirtualKeycode)))
            {
                int keyId = (int)key;
                var ret = GetAsyncKeyState(keyId);
                if ((ret & 0x8000) != 0)
                {
                    // 按下
                    if (!GlobalInput._keyStates[keyId])
                        GlobalInput._keyDownStates[keyId] = true;
                    GlobalInput._keyStates[keyId] = true;
                }
                else // if ((ret & 0x8000) == 0)
                {
                    // 抬起
                    if (GlobalInput._keyStates[keyId])
                        GlobalInput._keyUpStates[keyId] = true;
                    GlobalInput._keyStates[keyId] = false;
                }
            }
        }

        public void SetAllKeyDownState(bool state)
        {
            for(int i=0; i<GlobalInput._keyDownStates.Length;i++)
            {
                GlobalInput._keyDownStates[i] = state;
            }
        }

        public void SetAllKeyUpState(bool state)
        {
            for (int i = 0; i < GlobalInput._keyUpStates.Length; i++)
            {
                GlobalInput._keyUpStates[i] = state;
            }
        }
    }

}