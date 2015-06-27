using UnityEngine;
using System.Collections;
using Collectibles;

[RequireComponent(typeof(Collider2D))]
public abstract class Candy : MonoBehaviour
{
    public float SecondsValue;

    private CollectibleTriggerAnim _collectibleTrigger;

    protected abstract void CollectCandy();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Vampire")
	    {
            VampireAnimator animator = other.GetComponent<VampireAnimator>();
            animator.Smile();
            CollectCandy();
	    }
    }
}
