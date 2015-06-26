using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace InputHandler
{
    public class InputContext
    {
        private Dictionary<int, string> _mappedButtons;
        private Dictionary<int, string> _mappedStates;
        private Dictionary<int, string> _mappedAxis;

        public InputContext(string contextName, InputMap inputMap)
        {
            _mappedButtons = new Dictionary<int, string>();
            _mappedStates = new Dictionary<int, string>();
            _mappedAxis = new Dictionary<int, string>();

            foreach (InputToActionMap buttonToActionMap in inputMap.ButtonToActionMap)
            {
                _mappedButtons.Add(buttonToActionMap.input, buttonToActionMap.action);
            }

            foreach (InputToActionMap buttonToStateMap in inputMap.ButtonToStateMap)
            {
                _mappedStates.Add(buttonToStateMap.input, buttonToStateMap.action);
            }

            foreach (InputToActionMap axisToRangeMap in inputMap.AxisToRangeMap)
            {
                _mappedAxis.Add(axisToRangeMap.input, axisToRangeMap.action);
            }
        }

        public string GetActionForButton(int button)
        {
            return _mappedButtons.ContainsKey(button) ? _mappedButtons[button] : null;
        }

        public string GetStateForButton(int button)
        {
            return _mappedStates.ContainsKey(button) ? _mappedStates[button] : null;
        }

        public string GetRangeForAxis(int axis)
        {
            return _mappedAxis.ContainsKey(axis) ? _mappedAxis[axis] : null;
        }
    }
}