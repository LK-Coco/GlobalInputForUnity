using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UInput
{
    public class InputAction
    {
        public string ActionName;
        public bool IsGlobal;
        public bool IsApply;

        public List<KeyCode> Keys;
        public List<VirtualKeycode> VKeys;

        public UnityEvent OnDown = new UnityEvent();
        public UnityEvent OnHold = new UnityEvent();
        public UnityEvent OnUp = new UnityEvent();

        private int _usedLength;
        public int UsedLength => _usedLength;

        public InputAction()
        {
            ActionName = "";
            IsGlobal = false;
            IsApply = true;
        }

        public InputAction(List<KeyCode> keys, bool isGlobal = false, bool isApply = true)
        {
            ActionName = "";
            IsGlobal = isGlobal;
            IsApply = isApply;
            SetBindKeys(keys);
        }

        public InputAction(string name, List<KeyCode> keys, bool isGlobal = false, bool isApply = true)
        {
            ActionName = name;
            IsGlobal = isGlobal;
            IsApply = isApply;
            SetBindKeys(keys);
        }

        public InputAction(string name, KeyCode[] keys, bool isGlobal = false, bool isApply = true)
        {
            ActionName = name;
            IsGlobal = isGlobal;
            IsApply = isApply;
            SetBindKeys(keys);
        }


        public void InvokeOnDown()
        {
            OnDown?.Invoke();
        }

        public void InvokeOnUp()
        {
            OnUp?.Invoke();
        }

        public void InvokeOnHold()
        {
            OnHold?.Invoke();
        }

        public void SetBindKeys(List<KeyCode> keys)
        {
            Keys ??= new List<KeyCode>();
            Keys.Clear();
            VKeys ??= new List<VirtualKeycode>();
            VKeys.Clear();

            if (keys == null) return;
            //更新按键
            _usedLength = 0;
            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i] != KeyCode.None)
                {
                    _usedLength++;
                    Keys.Add(keys[i]);
                    VKeys.Add(InputModuleExtra.KeycodeToGlobalKeycode[keys[i]]);
                }
            }
        }
        public void SetBindKeys(KeyCode[] keys)
        {
            Keys ??= new List<KeyCode>();
            Keys.Clear();
            VKeys ??= new List<VirtualKeycode>();
            VKeys.Clear();

            if (keys == null) return;
            //更新按键
            _usedLength = 0;
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i] != KeyCode.None)
                {
                    _usedLength++;
                    Keys.Add(keys[i]);
                    VKeys.Add(InputModuleExtra.KeycodeToGlobalKeycode[keys[i]]);
                }
            }
        }


    }
}