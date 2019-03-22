using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {

    private MenuBehaviour _menuBehaviour;
    private bool _isDragging;

	// Use this for initialization
	void Start () {
		
	}

    public void setMenuBehaviour(MenuBehaviour menuBehaviour)
    {
        _menuBehaviour = menuBehaviour;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_menuBehaviour == null || _isDragging) return;
        // VERIFICA SI DEBE CERRAR EL MENU
        if (_menuBehaviour.IsMenuOpen)
            _menuBehaviour.CloseMenu();

        Debug.Log("Pointer Click");
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        _isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(eventData.position);
        if (_menuBehaviour != null)
            _menuBehaviour.ChangeMenuPosition(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        _isDragging = false;
    }
}
