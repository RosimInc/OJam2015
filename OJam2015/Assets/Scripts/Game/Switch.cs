using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
    public GameObject switchOn;
    public GameObject switchOff;
    
    public Activatable Activatable;


    public GameObject actionButton;
    private bool activated = false;

   
    public void Activate()
    {
        switchOff.SetActive(!switchOff.activeSelf);
        switchOn.SetActive(!switchOn.activeSelf);

        Activatable.Activate();

        activated = true;
    
    }



    public void ShowAction() {
        actionButton.SetActive(true);
    }

    public void HideAction() {
        actionButton.SetActive(false);
    }
}
