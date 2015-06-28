using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public Text Title;
    public Text Description;
    public GameObject Popup;

    private RectTransform _rectTransform;

    public static PopupManager Instance
    {
        get { return _instance; }
    }

    private static PopupManager _instance;

	void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;

            _rectTransform = Popup.GetComponent<RectTransform>();
        }
    }

    public void ShowPopup(string title, string description)
    {
        Debug.Log("Showing " + title + " popup!");

        Title.text = title;
        Description.text = description;

        StartCoroutine(ShowPopupCoroutine());
    }

    private IEnumerator ShowPopupCoroutine()
    {
        Vector3 _initialPosition = _rectTransform.localPosition;
        Vector3 _finalPosition = _initialPosition - new Vector3(1f, 0f, 0f) * 180;

        float ratio = 0f;

        while (ratio < 1f)
        {
            ratio += Time.unscaledDeltaTime / 0.5f;

            _rectTransform.localPosition = Vector3.Lerp(_initialPosition, _finalPosition, ratio);

            yield return null;
        }


        float elapsedTime = 0f; // So it can work with unscaled time too

        while (elapsedTime < 5f)
        {
            elapsedTime += Time.unscaledDeltaTime;

            yield return null;
        }


        ratio = 0f;

        while (ratio < 1f)
        {
            ratio += Time.unscaledDeltaTime / 0.5f;

            _rectTransform.localPosition = Vector3.Lerp(_finalPosition, _initialPosition, ratio);

            yield return null;
        }
    }
}
