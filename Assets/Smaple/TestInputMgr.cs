using System.Collections.Generic;
using System.Runtime.InteropServices;
using UInput;
using UnityEngine;

namespace AAAAAAA
{

    

    public class TestInputMgr : MonoBehaviour
    {
        public VirtualKeycode key;

        public InputComponent Mgr;

        private Dictionary<string,string> m_input = new Dictionary<string, string>();

        public string path = "ssss";

        private void Start()
        {
            //var map = new InputMap("test");
            //var acton = new InputAction(new List<KeyCode>() { KeyCode.LeftControl, KeyCode.X });
            //acton.OnDown.AddListener(() => { Debug.Log("X!!!!!"); });
            //map.AddAction(acton);
            //GlobalInput._keyDownStates = null;
            //Debug.Log(null != path);
        }

        private void Update()
        {
            Debug.Log("ffff"+ GlobalInput.GetKeyDown(key));
            if (GlobalInput.GetKeyDown(key))
            {
                Debug.LogError($"GetKeyDown:[{key}]");

            }
            if(GlobalInput.GetKeyUp(key))
                Debug.LogError($"GetKeyUp:[{key}]");
        }
    }
}

