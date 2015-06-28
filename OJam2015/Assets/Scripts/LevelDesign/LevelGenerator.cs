using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

	public GameObject platformPrefab;

    private int candyCount = 0;

    private bool _alreadyTriggered = false;

	void Awake()
	{
		Generator gen = Generator.Instance;
		gen.platform = platformPrefab;
		gen.levelHolder = this.gameObject;
	}

    void Update()
    {
        if (!_alreadyTriggered && candyCount == 0 && SugarBar.Instance.GetRemainingSeconds() <= 0f)
        {
            _alreadyTriggered = true;

            if (BrainCloudManager.Instance != null)
            {
                BrainCloudManager.Instance.AddAchievement(BrainCloudManager.AchievementTypes.Deprivation);
            }
        }
    }

    public void IncrementCandy()
    {
        ++candyCount;
    }
}
