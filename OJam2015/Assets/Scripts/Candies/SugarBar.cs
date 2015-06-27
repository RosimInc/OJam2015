using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RatioProgression))]
public class SugarBar : MonoBehaviour
{
    public float TimeBeforeDeath = 20f;

    private RatioProgression _ratioProgression;
    private float _secondsElapsed = 0f;

    public static SugarBar Instance
    {
        get { return _instance; }
    }

    private static SugarBar _instance;

    void Awake()
    {
        _instance = this;
        _ratioProgression = GetComponent<RatioProgression>();
    }

    public void AddCandy(float secondsValue)
    {
        _secondsElapsed = Mathf.Clamp(_secondsElapsed - secondsValue, 0f, TimeBeforeDeath);
    }

    public float GetRemainingSeconds()
    {
        return TimeBeforeDeath - _secondsElapsed;
    }

	void Update ()
    {
        _secondsElapsed += Time.deltaTime;

        _ratioProgression.SetCompletedRatio(Mathf.Lerp(1f, 0f, _secondsElapsed / TimeBeforeDeath));
	}
}
