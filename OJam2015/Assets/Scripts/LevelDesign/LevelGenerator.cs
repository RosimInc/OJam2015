using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

	public GameObject platformPrefab;

	void Awake()
	{
		Generator gen = Generator.Instance;
		gen.platform = platformPrefab;
		gen.levelHolder = this.gameObject;
	}
}
