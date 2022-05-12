using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace UInput
{

    public class InputComponent : MonoBehaviour,IInputMgr
    {
        public static InputComponent Instance;

        private Dictionary<string,InputMap> _allMap = new Dictionary<string,InputMap>();

        #region Mono

        private void Awake()
        {
            Instance = this;

            OnInit();
        }

        private void Start()
        {
            OnStart();
        }

        private void Update()
        {
            OnUpdate();
        }

        private void LateUpdate()
        {
            OnLateUpdate();
        }

        private void OnDestroy()
        {
            OnStop();
            OnClose();
        }

        #endregion

        #region Component

        public void RegisterMap(InputMap map)
        {
            if (Contain(map.Name)) return;
            _allMap.Add(map.Name, map);
            ApplyMap(map);
        }

        public void UnRegisterMap(string mapName)
        {
            if (!Contain(mapName)) return;
            var map = GetMapByName(mapName);
            UnRegisterMap(map);
        }

        public void UnRegisterMap(InputMap map)
        {
            if (!Contain(map.Name)) return;
            UnApplyMap(map);
            _allMap.Remove(map.Name);
        }


        public bool Contain(string mapName)
        {
            return _allMap.ContainsKey(mapName);
        }

        public InputMap GetMapByName(string mapName)
        {
            return _allMap[mapName];
        }


        public bool TryGetMapByName(string mapName, out InputMap inputMap)
        {
            return _allMap.TryGetValue(mapName, out inputMap);
        }

        public IReadOnlyDictionary<string, InputMap> GetAllMap()
        {
            return _allMap;
        }

        private void ApplyMap(InputMap map)
        {
            if (map.IsApply) return;
            map.IsApply = true;
            _executor.AddMap(map);
        }

        private void UnApplyMap(InputMap map)
        {
            if (!map.IsApply) return;
            map.IsApply = false;
            _executor.RemoveMap(map);
        }

        #endregion

        #region IInputMgr

        private InputExecutor _executor;
        private GlobalInput_KeyState _input;

        public void OnInit()
        {
            _input = new GlobalInput_KeyState();
            _executor = new InputExecutor();
        }

        public void OnStart()
        {
            _executor.Begin();
        }

        public void OnUpdate()
        {
            _input.UpdateKeyState();
            _executor.OnUpdate();
        }

        public void OnLateUpdate()
        {
            _input.SetAllKeyDownState(false);
            _input.SetAllKeyUpState(false);
        }

        public void OnStop()
        {
            _executor.End();
        }

        public void OnClose()
        {
            _executor.Destroy();
        }

        #endregion        
        
    }
}