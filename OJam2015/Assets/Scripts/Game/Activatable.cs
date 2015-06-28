using UnityEngine;
using System.Collections;
using System;

public abstract class Activatable : MonoBehaviour
{
    public abstract void Activate(Action callback);
}
