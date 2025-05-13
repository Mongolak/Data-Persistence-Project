using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataSettingsManager : MonoBehaviour
{

    public Color backgroundColorActual; //Variable para guardar el color de fondo.

    //Variables para escoger el color.
    public Color greenColor; //= new Color(0.1592439f, 0.4339623f, 0.08802065f);
    public Color blueColor; //= new Color(0.1968672f, 0.507506f, 0.5283019f);
    public Color orangeColor; //= new Color(0.4716981f, 0.2625228f, 0.06452475f);
    public Color blackColor; //= Color.black;


    //Código para acceder desde cualquier otro script. Patrón singleton.
    public static DataSettingsManager Instance;
    
    private void Awake()
    {

        if (Instance != null)
        {

            Destroy(gameObject);

            return;

        }

        Instance = this;

        DontDestroyOnLoad(gameObject);


    }

    //Método para obtener el color escogido a partir de hacer clic en los botones.
    
    public void setActualColor(int color)
    {
        switch (color)
        {

            case 0:

                backgroundColorActual = greenColor;

                break;

            case 1:

                backgroundColorActual = blueColor;

                break;

            case 2:

                backgroundColorActual = orangeColor;

                break;

            default:

                backgroundColorActual = blackColor;

                break;

        }

    }


}
