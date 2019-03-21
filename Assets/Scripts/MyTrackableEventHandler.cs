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
        if (!_isMenuOpened)
            toggleGameObject(gameObjects[_currentGameObject], true);
    }

    protected override void OnTrackingLost()
    {
        toggleGameObject(gameObjects[_currentGameObject], false);
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

    /// <summary>
    /// OCULTA LOS GAMEOBJECTS
    /// </summary>
    private void HideAll()
    {
        // RECORRE CADA GAME OBJECT DEL ARREGLO
        foreach (var g in gameObjects)
            toggleGameObject(g, false);
    }

    /// <summary>
    /// MUESTRA/OCULTA EL RENDER, CANVAS Y COLLIDER DEL GAMEOBJECT Y SUS HIJOS
    /// </summary>
    /// <param name="g">EL GAMEOBJECT</param>
    /// <param name="enable">ACTIVA O DESACTIVA LOS COMPONENTES DEL GAMEOBJECT</param>
    private void toggleGameObject(GameObject g, bool enable)
    {
        // OCULTA RENDER, CANVAS Y COLLIDERS
        var r = g.GetComponent<Renderer>();
        var c = g.GetComponent<Canvas>();
        var col = g.GetComponent<Collider>();

        if (r != null)
            r.enabled = enable;
        if (c != null)
            c.enabled = enable;
        if (col != null)
            col.enabled = enable;

        // OCULTA RENDER, CANVAS Y COLLIDERS DE LOS HIJOS
        var rendererComponents = g.GetComponentsInChildren<Renderer>(true);
        var colliderComponents = g.GetComponentsInChildren<Collider>(true);
        var canvasComponents = g.GetComponentsInChildren<Canvas>(true);

        foreach (var component in rendererComponents)
            component.enabled = enable;

        foreach (var component in colliderComponents)
            component.enabled = enable;

        foreach (var component in canvasComponents)
            component.enabled = enable;

        // PAUSA LOS VIDEOS DE LOS HIJOS
        if (!enable)
        {
            var videoComponents = g.GetComponentsInChildren<VideoController>(true);
            foreach (var component in videoComponents)
                component.Pause();
        }
    }
}
