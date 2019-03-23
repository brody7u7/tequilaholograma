using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScalerFix : MonoBehaviour {

    private UnityEngine.UI.CanvasScaler _canvasScaler;
    private Coroutine _coroutine;

    void Start () {
        _canvasScaler = GetComponent<UnityEngine.UI.CanvasScaler>();
        //_coroutine = StartCoroutine(ChangePixelPerUnit());
	}

    public void ChangePixelPerUnit()
    {
        _canvasScaler.dynamicPixelsPerUnit = 10f;
        //StopCoroutine(_coroutine);
    }
}
