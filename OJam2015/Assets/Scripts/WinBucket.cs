using UnityEngine;
using System.Collections;

public class WinBucket : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {
        
        if( other.tag == "Vampire"){

            gameObject.SetActive(false);

            GameManager.Instance.Win();

        }
    }
}
