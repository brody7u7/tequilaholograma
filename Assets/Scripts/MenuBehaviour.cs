﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehaviour : MonoBehaviour {

    private const float ANIMATION_DURATION = 0.25f;
    private const float ANIMATION_WAIT = 0.03f;
    private const float OPEN_POSITION_X = 0f;
    private const float CLOSE_POSITION_X = -750f;

    private RectTransform _rectTransform;

    private float _screenWidth;
    private float _menuWith;
    private float _stepsPosition;

    public bool IsMenuOpen { private set; get; }

	// Use this for initialization
	void Start ()
    {
        _rectTransform = GetComponent<RectTransform>();
        _screenWidth = Screen.width;
        _menuWith = gameObject.transform.Find("Navigation Drawer").GetComponent<RectTransform>().sizeDelta.x;

        //var drag = GetComponentInChildren<DragHandler>();
        //if (drag != null)
        //drag.SetMenuBehaviour(this);
        var drags = GetComponentsInChildren<DragHandler>(true);
        foreach (var drag in drags)
            drag.SetMenuBehaviour(this);
    }
	
    public void OpenMenu()
    {
        OpenMenu(true);
    }

    public void CloseMenu()
    {
        CloseMenu(true);
    }

    public void OpenMenu(bool animate)
    {
        if (animate)
        {
            StartCoroutine(AnimateOpenMenu());
        }
        else
        {
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _screenWidth);
            IsMenuOpen = true;
        }
    }

    public void CloseMenu(bool animate)
    {
        if (animate)
        {
            StartCoroutine(AnimateCloseMenu());
        }
        else
        {
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 790f);
            IsMenuOpen = false;
        }
    }

    public void ChangeMenuPosition(Vector2 dragValue)
    {
        float distance = _menuWith - dragValue.x;
        Vector2 position = _rectTransform.anchoredPosition;
        position.x -= position.x + distance;
        position.x = Mathf.Clamp(position.x, CLOSE_POSITION_X, OPEN_POSITION_X);
        _rectTransform.anchoredPosition = position;
    }

    public void SetOpenOrClose()
    {
        float positionX = _rectTransform.anchoredPosition.x;
        if(positionX > (CLOSE_POSITION_X - OPEN_POSITION_X) / 2)
        {
            if (positionX >= OPEN_POSITION_X)
                OpenMenu(false);
            else
                OpenMenu(true);

        }
        else
        {
            if (positionX <= CLOSE_POSITION_X)
                CloseMenu(false);
            else
                CloseMenu(true);
        }
    }

    IEnumerator AnimateOpenMenu()
    {
        float x = _rectTransform.anchoredPosition.x;
        float step = Mathf.Abs((x - OPEN_POSITION_X) * ANIMATION_WAIT / ANIMATION_DURATION);
        for (; x <= 0; x += step)
        {
            Vector2 position = _rectTransform.anchoredPosition;
            position.x = x;
            _rectTransform.anchoredPosition = position;
            yield return new WaitForSeconds(ANIMATION_WAIT);
        }

        Vector2 position2 = _rectTransform.anchoredPosition;
        position2.x = OPEN_POSITION_X;
        _rectTransform.anchoredPosition = position2;

        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _screenWidth);
        IsMenuOpen = true;
    }

    IEnumerator AnimateCloseMenu()
    {
        float x = _rectTransform.anchoredPosition.x;
        float step = Mathf.Abs((CLOSE_POSITION_X - x) * ANIMATION_WAIT / ANIMATION_DURATION);
        for (; x >= CLOSE_POSITION_X; x -= step)
        {
            Vector2 position = _rectTransform.anchoredPosition;
            position.x = x;
            _rectTransform.anchoredPosition = position;
            yield return new WaitForSeconds(ANIMATION_WAIT);
        }

        Vector2 position2 = _rectTransform.anchoredPosition;
        position2.x = CLOSE_POSITION_X;
        _rectTransform.anchoredPosition = position2;

        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 790f);
        IsMenuOpen = false;
    }

}
