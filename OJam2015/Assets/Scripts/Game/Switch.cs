using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
    public Activatable Activatable;

    public void Activate()
    {
        Activatable.Activate();
    }
}
