using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehaviour : MonoBehaviour {

    private const float ANIMATION_DURATION = 0.25f;
    private const float ANIMATION_WAIT = 0.03f;
    private const float OPEN_POSITION_X = 0f;
    private const float CLOSE_POSITION_X = -750f;

    private RectTransform rectTransform;

    private float startPositionX;
    private float stepsPosition;

	// Use this for initialization
	void Start () {
        rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void openMenu()
    {
        StartCoroutine(AnimateOpenMenu());
    }

    public void closeMenu()
    {
        StartCoroutine(AnimateCloseMenu());
    }

    IEnumerator AnimateOpenMenu()
    {
        float x = rectTransform.anchoredPosition.x;
        float step = Mathf.Abs((x - OPEN_POSITION_X) * ANIMATION_WAIT / ANIMATION_DURATION);
        for (; x <= 0; x += step)
        {
            Vector2 position = rectTransform.anchoredPosition;
            position.x = x;
            rectTransform.anchoredPosition = position;
            yield return new WaitForSeconds(ANIMATION_WAIT);
        }

        Vector2 position2 = rectTransform.anchoredPosition;
        position2.x = OPEN_POSITION_X;
        rectTransform.anchoredPosition = position2;
    }

    IEnumerator AnimateCloseMenu()
    {
        float x = rectTransform.anchoredPosition.x;
        float step = Mathf.Abs((CLOSE_POSITION_X - x) * ANIMATION_WAIT / ANIMATION_DURATION);
        for (; x >= CLOSE_POSITION_X; x -= step)
        {
            Vector2 position = rectTransform.anchoredPosition;
            position.x = x;
            rectTransform.anchoredPosition = position;
            yield return new WaitForSeconds(ANIMATION_WAIT);
        }

        Vector2 position2 = rectTransform.anchoredPosition;
        position2.x = CLOSE_POSITION_X;
        rectTransform.anchoredPosition = position2;
    }
}
