using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
    public GameObject switchOn;
    public GameObject switchOff;
    
    public Activatable Activatable;

   
    public void Activate()
    {
        switchOff.SetActive(!switchOff.activeSelf);
        switchOn.SetActive(!switchOn.activeSelf);

        Activatable.Activate();
    
    }
}
