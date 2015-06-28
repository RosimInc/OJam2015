using UnityEngine;

public class Generator {

	private Generator() { }
	private static Generator _instance = new Generator();
	public static Generator Instance
	{
		get { return _instance; }
	}

	private Vector2 platformSize = new Vector2(2.7f, 2.6f);
	public GameObject platform, candy, levelHolder;

	public void AddPlatform(float startX, float startY, float length)
	{
		int numPieces = (int)(length / platformSize.x);
		GameObject newPlatform;
		for (int i = 0; i < numPieces; i++)
		{
			Vector3 position = new Vector3(startX + platformSize.x * i,
				startY, 0);
			newPlatform = GameObject.Instantiate(platform, position, Quaternion.identity) as GameObject;
			newPlatform.transform.parent = levelHolder.transform;
            newPlatform.GetComponent<SpriteRenderer>().sortingOrder = -10 + i % 2;
		}
	}
}
