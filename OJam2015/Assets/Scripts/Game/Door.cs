using UnityEngine;
using System.Collections;

public class Door : Activatable
{
    public float FinalHeight = 15f;
    public float AnimationDuration = 1f;

    protected override void ActivateAnimation()
    {
        StartCoroutine(SlideDoor());
    }

    private IEnumerator SlideDoor()
    {
        float ratio = 0f;

        Vector3 initialPosition = transform.localPosition;
        Vector3 finalPosition = new Vector3(transform.localPosition.x, FinalHeight, transform.localPosition.z);

        while (ratio < 1f)
        {
            ratio += Time.deltaTime / AnimationDuration;

            transform.localPosition = Vector3.Lerp(initialPosition, finalPosition, Mathf.Pow(ratio, 5));

            Debug.Log(transform.localPosition.y);

            yield return null;
        }
    }
}
