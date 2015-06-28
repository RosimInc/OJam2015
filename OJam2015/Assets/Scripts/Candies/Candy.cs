using UnityEngine;
using System.Collections;
using Collectibles;

[RequireComponent(typeof(Collider2D))]
public class Candy : MonoBehaviour
{
    public float SecondsValue;
	public GameObject sprite;

    private CollectibleTriggerAnim _collectibleTrigger;

	void Start()
	{
		sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>
			("Candy" + Random.Range(0, 9));
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Vampire")
	    {
            VampireAnimator animator = other.GetComponent<VampireAnimator>();
            MusicManager.Instance.PlayCandySound();
            animator.Smile();

			// Give points here
			SugarBar.Instance.AddCandy(SecondsValue);
	    }
    }
}
