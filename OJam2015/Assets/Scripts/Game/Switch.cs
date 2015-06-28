using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
    public GameObject switchOn;
    public GameObject switchOff;
    
    public Activatable Activatable;

    private bool _animating = false;
   
    public void Activate()
    {
        if (!_animating)
        {
            switchOff.SetActive(!switchOff.activeSelf);
            switchOn.SetActive(!switchOn.activeSelf);

            _animating = true;
            Activatable.Activate(() => { _animating = false; });
        }
    }
}
