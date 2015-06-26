using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace InputHandler
{
    public abstract class InputMapperAsset : ScriptableObject
    {
        public enum InputTypes { Action, State, Range }

        public abstract Dictionary<string, InputContext> GetMappedContexts();
    }
}