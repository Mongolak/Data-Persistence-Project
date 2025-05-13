using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script se incluye en la "Main Camera" de todas las escenas, para poder cambiar el color de fondo y el sonido.
public class Settings : MonoBehaviour
{

    public Camera cameraSettings;


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        if (DataSettingsManager.Instance != null)
        {
            //Debug.Log("Entra en Update Settings");
            cameraSettings.backgroundColor = DataSettingsManager.Instance.backgroundColorActual;

        }


    }
    //Método para acceder al color de fondo.
    //Se accede desde los botones de color, del Canvas en la escena "Settings".
    public void setBackgroundColor(int color)
    {
        Debug.Log("Entra en setBackgroundColor en Settings");

        DataSettingsManager.Instance.setActualColor(color);

    }
    public void SaveActualSettings()
    {
        Debug.Log("Entra en setBackgroundColor en Settings");

        DataSettingsManager.Instance.SaveSettings();

    }
}
