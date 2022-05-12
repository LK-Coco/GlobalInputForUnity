using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UInput
{
    public class InputExecutor
    {
        private List<InputMap> _maps = new List<InputMap>();

        private List<InputAction> _vActionDown = new List<InputAction>();
        private List<InputAction> _vActionUp   = new List<InputAction>();
        private List<InputAction> _vActionHold = new List<InputAction>();

        private bool _loop = false;


        public void AddMap(InputMap map)
        {
            if (_maps.Contains(map)) return;
            _maps.Add(map);
        }

        public void RemoveMap(InputMap map)
        {
            _maps.Remove(map);
        }


        public void Begin()
        {
            _loop = true;
        }

        public void End()
        {
            _loop = false;
        }

          
        // Update is called once per frame
        public void OnUpdate()
        {
            if (!_loop) return;

            foreach(var map in _maps)
            {
                var actions = map.Actions;
                var actionCount = actions.Count;
                for (int i = 0; i < actionCount; i++)
                {
                    if (GetKeyDown(actions[i], map.IsGlobal))
                    {
                        _vActionDown.Add(actions[i]);
                    }

                    if (GetKeyUp(actions[i], map.IsGlobal))
                    {
                        _vActionUp.Add(actions[i]);
                    }

                    if (GetKeyHold(actions[i], map.IsGlobal))
                    {
                        _vActionHold.Add(actions[i]);
                    }
                }
            }
            if (_vActionDown.Count > 0)
            {
                _vActionDown.Sort((a, b) => { if (b.UsedLength > a.UsedLength) return 1; else return -1; });
                int length = _vActionDown[0].UsedLength;
                for (int i = 0; i < _vActionDown.Count; i++)
                {
                    if (length == _vActionDown[i].UsedLength)
                        _vActionDown[i].InvokeOnDown();
                }
                _vActionDown.Clear();
            }

            if (_vActionUp.Count > 0)
            {
                _vActionUp.Sort((a, b) => { if (b.UsedLength > a.UsedLength) return 1; else return -1; });
                int length = _vActionUp[0].UsedLength;
                for (int i = 0; i < _vActionUp.Count; i++)
                {
                    if (length == _vActionUp[i].UsedLength)
                        _vActionUp[i].InvokeOnUp();
                }
                _vActionUp.Clear();
            }

            if (_vActionHold.Count > 0)
            {
                _vActionHold.Sort((a, b) => { if (b.UsedLength > a.UsedLength) return 1; else return -1; });
                int length = _vActionHold[0].UsedLength;
                for (int i = 0; i < _vActionHold.Count; i++)
                {
                    if (length == _vActionHold[i].UsedLength)
                        _vActionHold[i].InvokeOnHold();
                }
                _vActionHold.Clear();
            }
        }


        public void Destroy()
        {
            _maps.Clear();
            _maps = null;
 
            _vActionDown.Clear();
            _vActionDown = null;
            _vActionHold.Clear();
            _vActionHold = null;
            _vActionUp.Clear();
            _vActionUp = null;
        }

        private bool InternalIsDown(List<KeyCode> keys)
        {
            if (keys == null || keys.Count == 0) return false;
            bool result = true;
            for (int i = 0; i < keys.Count; i++)
            {
                if (i == keys.Count - 1)
                {
                    result = Input.GetKeyDown(keys[i]);
                }
                else if (!Input.GetKey(keys[i]))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private bool InternalIsHold(List<KeyCode> keys)
        {
            if (keys == null || keys.Count == 0) return false;
            bool result = true;
            for (int i = 0; i < keys.Count; i++)
            {
                if (i == keys.Count - 1)
                {
                    result = Input.GetKey(keys[i]);
                }
                else if (!Input.GetKey(keys[i]))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private bool InternalIsUp(List<KeyCode> keys)
        {
            if (keys == null || keys.Count == 0) return false;
            bool result = true;
            for (int i = 0; i < keys.Count; i++)
            {
                if (i == keys.Count - 1)
                {
                    result = Input.GetKeyUp(keys[i]);
                }
                else if (!Input.GetKey(keys[i]))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }


        private bool GlobalIsDown(List<VirtualKeycode> keys)
        {
            if (keys == null || keys.Count == 0) return false;
            bool result = true;
            for (int i = 0; i < keys.Count; i++)
            {

                if (i == keys.Count - 1)
                {
                    result = GlobalInput.GetKeyDown(keys[i]);
                }
                else if (!GlobalInput.GetKey(keys[i]))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private bool GlobalIsHold(List<VirtualKeycode> keys)
        {
            if (keys == null || keys.Count == 0) return false;
            bool result = true;
            for (int i = 0; i < keys.Count; i++)
            {
                if (i == keys.Count - 1)
                {
                    result = GlobalInput.GetKey(keys[i]);
                }
                else if (!GlobalInput.GetKey(keys[i]))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private bool GlobalIsUp(List<VirtualKeycode> keys)
        {
            if (keys == null || keys.Count == 0) return false;
            bool result = true;
            for (int i = 0; i < keys.Count; i++)
            {
                if (i == keys.Count - 1)
                {
                    result = GlobalInput.GetKeyUp(keys[i]);
                }
                else if (!GlobalInput.GetKey(keys[i]))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }


        private bool GetKeyDown(InputAction action, bool mapIsGlobal)
        {
            bool condition = false;
            if (action.IsApply)
            {
                condition = mapIsGlobal || action.IsGlobal ? GlobalIsDown(action.VKeys) : InternalIsDown(action.Keys);
            }
            return condition;
        }

        private bool GetKeyHold(InputAction action, bool mapIsGlobal)
        {
            bool condition = false;
            if (action.IsApply)
            {
                condition = mapIsGlobal || action.IsGlobal ? GlobalIsHold(action.VKeys) : InternalIsHold(action.Keys);
            }
            return condition;
        }

        private bool GetKeyUp(InputAction action, bool mapIsGlobal)
        {
            bool condition = false;
            if (action.IsApply)
            {
                condition = mapIsGlobal || action.IsGlobal ? GlobalIsUp(action.VKeys) : InternalIsUp(action.Keys);
            }

            return condition;
        }
    }
}