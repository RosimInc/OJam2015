using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class DeadZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Vampire")
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoseFromFall();
            }
        }
    }
}
