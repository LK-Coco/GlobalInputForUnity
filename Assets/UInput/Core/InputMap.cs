using System;
using System.Collections.Generic;

namespace UInput
{
    public class InputMap
    {
        public string Name => _name;
        public IReadOnlyList<InputAction> Actions => _actions;

        public bool IsApply;
        public bool IsGlobal;

        private string _name;
        private List<InputAction> _actions;

        public InputMap(string name)
        {
            _name = name;
            IsApply = false;
            IsGlobal = true;
            _actions = new List<InputAction>();
        }

        public void AddAction(InputAction action)
        {
            _actions.Add(action);
        }

        public void RemoveAction(InputAction action)
        {
            _actions.Remove(action);
        }

        public void ClearAllAction()
        {
            _actions.Clear();
        }       


        public InputAction GetAction(string actionName)
        {
            for (int i = 0; i < _actions.Count; i++)
            {
                if (_actions[i].ActionName == actionName)
                {
                    return _actions[i];
                }
            }

            return null;
        }
    }
}