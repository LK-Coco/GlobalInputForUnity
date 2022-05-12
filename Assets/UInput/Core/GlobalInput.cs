namespace UInput
{
    public class GlobalInput
    {
        internal static bool[] _keyDownStates = new bool[255];
        internal static bool[] _keyUpStates = new bool[255];
        internal static bool[] _keyStates = new bool[255];

        public static bool GetKeyDown(VirtualKeycode vKey)
        {
            return _keyDownStates[(int)vKey];
        }

        public static bool GetKeyUp(VirtualKeycode vKey)
        {
            return _keyUpStates[(int)vKey];
        }

        public static bool GetKey(VirtualKeycode vKey)
        {
            return _keyStates[(int)vKey];
        }

    }
}