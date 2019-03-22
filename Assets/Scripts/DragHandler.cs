using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public bool onClickEnable = false;

    private MenuBehaviour _menuBehaviour;
    private bool _isDragging;

    public void SetMenuBehaviour(MenuBehaviour menuBehaviour)
    {
        _menuBehaviour = menuBehaviour;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_menuBehaviour == null || _isDragging || !onClickEnable) return;
        // VERIFICA SI DEBE CERRAR EL MENU
        if (_menuBehaviour.IsMenuOpen)
            _menuBehaviour.CloseMenu();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_menuBehaviour != null)
            _menuBehaviour.ChangeMenuPosition(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _menuBehaviour.SetOpenOrClose();
        _isDragging = false;
    }
}
