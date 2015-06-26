using UnityEngine;
using System.Collections;

namespace InputHandler
{
    public struct InputMap
    {
        public InputToActionMap[] ButtonToActionMap;
        public InputToActionMap[] ButtonToStateMap;
        public InputToActionMap[] AxisToRangeMap;
    }

    public struct InputToActionMap
    {
        public int input;
        public string action;
    }
}
