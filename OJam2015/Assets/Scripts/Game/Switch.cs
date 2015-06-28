using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
    public GameObject switchOn;
    public GameObject switchOff;
    
    public Activatable Activatable;

    private bool _animating = false;

    public GameObject actionButton;
    private bool activated = false;

   
    public void Activate()
    {
        if (!_animating)
        {
            switchOff.SetActive(!switchOff.activeSelf);
            switchOn.SetActive(!switchOn.activeSelf);

            _animating = true;
            Activatable.Activate(() => { _animating = false; });

			activated = true;
        }
    }

    public void ShowAction() {
      
    public void ShowAction() {
        actionButton.SetActive(true);
    }

    public void HideAction() {
        actionButton.SetActive(false);
    }
}
