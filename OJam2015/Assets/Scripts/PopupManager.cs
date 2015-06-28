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

            _rectTransform.anchorMin = new Vector2(1.05f, _rectTransform.anchorMin.y);
            _rectTransform.anchorMax = new Vector2(1.3754727f, _rectTransform.anchorMax.y);
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
        Vector2 _initialMinPosition = new Vector2(1.05f, _rectTransform.anchorMin.y);
        Vector2 _finalMinPosition = new Vector2(0.5f, _rectTransform.anchorMin.y);

        Vector2 _initialMaxPosition = new Vector2(1.3754727f, _rectTransform.anchorMax.y);
        Vector2 _finalMaxPosition = new Vector2(0.92f, _rectTransform.anchorMax.y);
        

        float ratio = 0f;

        while (ratio < 1f)
        {
            ratio += Time.unscaledDeltaTime / 0.5f;

            _rectTransform.anchorMin = Vector2.Lerp(_initialMinPosition, _finalMinPosition, ratio);
            _rectTransform.anchorMax = Vector2.Lerp(_initialMaxPosition, _finalMaxPosition, ratio);

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

            _rectTransform.anchorMin = Vector2.Lerp(_finalMinPosition, _initialMinPosition, ratio);
            _rectTransform.anchorMax = Vector2.Lerp(_finalMaxPosition, _initialMaxPosition, ratio);

            yield return null;
        }
    }
}
