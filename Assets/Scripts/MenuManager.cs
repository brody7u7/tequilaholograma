using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject Container;
    public Text CompanyText;
    public Text LegendText;
    public Image[] Dots;
    public string[] Companies;
    public string[] Legends;
    public float TimeBetweenTransitions;

    private Color activeColor = new Color(0.9176471f, 9019608f, 0.4196078f);
    private Color inactiveColor = new Color(0, 0, 0, 0.4f);

    private int indexLegend = 1;

	// Use this for initialization
	void Start () {
        // REVISA SI ES IGUAL LA CANTIDAD DE LEYENDAS A LAS COMPAÑIAS Y PUNTOS
        if(Companies.Length != Legends.Length || Companies.Length != Dots.Length || Legends.Length != Dots.Length)
        {
            throw new System.Exception("Debe ser en igual cantidad compañias, leyendas y puntos");
        }

        // DESPUES DE UN LAPSO DE TIEMPO, MUESTRA LAS LEGENDAS
        Invoke("StartLegends", TimeBetweenTransitions);

	}

    void StartLegends()
    {
        // MUESTRA EL CONTENEDOR
        Container.SetActive(true);

        // MUESTRA LA PRIMERA LEYENDA Y COMPAÑÍA
        CompanyText.text = Companies[0];
        LegendText.text = Legends[0];
        Dots[0].color = activeColor;

        // CAMBIA LA LEGENDA CADA LAPSO DE TIEMPO
        InvokeRepeating("ChangeLegend", TimeBetweenTransitions, TimeBetweenTransitions);
    }

    void ChangeLegend()
    {
        // SI SE RECORRIÓ TODAS LAS LEYENDAS, SE REINICIA EL INDEX
        if(indexLegend >= Legends.Length)
        {
            indexLegend = 0;
        }

        // MUESTRA LE LEYENDA Y COMPAÑÍA SIGUIENTE
        CompanyText.text = Companies[indexLegend];
        LegendText.text = Legends[indexLegend];

        // CAMBIA DE COLOR EL PUNTO
        Dots[indexLegend].color = activeColor;
        // RESTABLECE EL PUNTO ANTERIOR A SU COLOR INACTIVO
        int oldIndex = (indexLegend - 1 >= 0) ? indexLegend - 1 : Dots.Length - 1;
        Dots[oldIndex].color = inactiveColor;

        // AUMENTA EL INDEX
        indexLegend++;
    }

    public void openExperience()
    {
        SceneManager.LoadScene("ExperienceScene");
    }
}
