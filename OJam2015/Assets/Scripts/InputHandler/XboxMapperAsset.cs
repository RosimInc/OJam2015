using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace InputHandler
{
    // TODO: REFACTOR THE WHOLE CLASS, IT STINKS

    [SerializeField]
    public class XboxMapperAsset : InputMapperAsset
    {
        [Serializable]
        public class XboxContext
        {
            public string name;
            public XboxAction[] ButtonActions;
            public XboxRange[] AxisRanges;
            public XboxState[] ButtonStates;
        }

        [Serializable]
        public class XboxAction
        {
            public string name;
            public XboxInputConstants.Buttons Button;
        }

        [Serializable]
        public class XboxRange
        {
            public string name;
            public XboxInputConstants.Axis Axis;
        }

        [Serializable]
        public class XboxState
        {
            public string name;
            public XboxInputConstants.Buttons Button;
        }

        public XboxContext[] Contexts;

        // Context -> InputMap
        public override Dictionary<string, InputContext> GetMappedContexts()
        {
            Dictionary<string, InputContext> mappedContexts = new Dictionary<string, InputContext>();

            foreach (XboxContext xboxContext in Contexts)
            {
                InputMap inputMap = new InputMap();

                inputMap.ButtonToActionMap = new InputToActionMap[xboxContext.ButtonActions.Length];
                inputMap.ButtonToStateMap = new InputToActionMap[xboxContext.ButtonStates.Length];
                inputMap.AxisToRangeMap = new InputToActionMap[xboxContext.AxisRanges.Length];

                for (int i = 0; i < xboxContext.ButtonActions.Length; i++)
                {
                    XboxAction buttonAction = xboxContext.ButtonActions[i];

                    inputMap.ButtonToActionMap[i] = new InputToActionMap() { action = buttonAction.name, input = (int)buttonAction.Button };
                }

                for (int i = 0; i < xboxContext.ButtonStates.Length; i++)
                {
                    XboxState buttonState = xboxContext.ButtonStates[i];

                    inputMap.ButtonToStateMap[i] = new InputToActionMap() { action = buttonState.name, input = (int)buttonState.Button };
                }

                for (int i = 0; i < xboxContext.AxisRanges.Length; i++)
                {
                    XboxRange axisRange = xboxContext.AxisRanges[i];

                    inputMap.AxisToRangeMap[i] = new InputToActionMap() { action = axisRange.name, input = (int)axisRange.Axis };
                }

                InputContext context = new InputContext(xboxContext.name, inputMap);

                mappedContexts.Add(xboxContext.name, context);
            }

            return mappedContexts;
        }
    }
}