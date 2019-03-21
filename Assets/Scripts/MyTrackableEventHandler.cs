using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MyTrackableEventHandler : DefaultTrackableEventHandler {

    public GameObject[] gameObjects;

    private bool _isMenuOpened;

    // INDICA CUAL OBJETIVO SE ESTA VISUALIZANDO
    private int _currentGameObject;

    protected override void Start()
    {
        base.Start();

        HideAll();
    }

    protected override void OnTrackingFound()
    {
        if(!_isMenuOpened)
            gameObjects[_currentGameObject].GetComponent<Renderer>().enabled = true;
    }

    protected override void OnTrackingLost()
    {
        gameObjects[_currentGameObject].GetComponent<Renderer>().enabled = false;
    }

    public void OnMenuOpen()
    {
        _isMenuOpened = true;
        OnTrackingLost();
    }

    public void OnMenuClose()
    {
        _isMenuOpened = false;
        if (m_NewStatus == TrackableBehaviour.Status.TRACKED)
            OnTrackingFound();
    }

    public void SetObject(int index)
    {
        _currentGameObject = index;
        HideAll();
        OnMenuClose();
    }

    private void HideAll()
    {
        foreach (var g in gameObjects)
        {
            g.GetComponent<Renderer>().enabled = false;
        }
    }
}
