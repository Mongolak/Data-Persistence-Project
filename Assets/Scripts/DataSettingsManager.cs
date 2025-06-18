using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DataSettingsManager : MonoBehaviour
{
    //Variables para cambiar el color de fondo de las escenas.
    public Color backgroundColorActual = Color.black; //Variable para guardar el color de fondo.

    //Variables para escoger el color.
    private Color greenColor = new Color(0.1592439f, 0.4339623f, 0.08802065f);
    private Color blueColor = new Color(0.1968672f, 0.507506f, 0.5283019f);
    private Color orangeColor = new Color(0.4716981f, 0.2625228f, 0.06452475f);
    private Color blackColor = Color.black;
     
    private int colorSetting; //Variable para el tipo de color, 0 = verde, 1=...

    //Variable para activar o desactivar el sonido.
    public bool isSoundActive;

    //Variables para cambiar el color de los botones del sonido.
    public Color colorSoundButtonYes; 
    public Color colorSoundButtonNo; 

    private int colorButtonSetting; //Variable para el tipo de color de los botones (0 o 1).

    public Color darkGreen;
    public Color lightGreen;

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

        LoadSettings();
    }

    //Método para obtener el color escogido a partir de hacer clic en los botones.
    
    public void SetActualColor(int color)
    {
        colorSetting = color;

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

    //Método para activar el sonido.
    public void ActiveSoundBall(bool sound)
    {

        isSoundActive = sound;
   
    }

    //Método para obtener el color escogido en los botones del sonido.
    
    public void SetColorButton(int  color)
    {

        colorButtonSetting = color;

        if(color == 0)
        {

            colorSoundButtonYes = lightGreen;

            colorSoundButtonNo = darkGreen;


        } else if(color == 1)
        {
           
            colorSoundButtonYes = darkGreen;

            colorSoundButtonNo = lightGreen;

        }

    }


    //Añadir una clase Serializable para guardar los datos.
    [System.Serializable]
    class SaveDataSettings
    {

        public bool soundSelection;

        public int colorSelection;

        public int colorSoundSelection;

    }

    //Agregamos un método para guardar el sonido y el color.
    public void SaveSettings()
    {
        Debug.Log("Llama a save settings");
        SaveDataSettings data = new SaveDataSettings
        {

            soundSelection = isSoundActive,
            colorSelection = colorSetting,
            colorSoundSelection = colorButtonSetting,

        };

        //data.nameTextGame = nameText;

        //data.bestScoreGame = bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "//saveSettingsFile.json", json); //Lo guardará en la ruta por defecto "C:\Users\<user>\AppData\LocalLow\<company name>".



    }

    //Agregamos un método para cargar el sonido y el color.
    public void LoadSettings()
    {
        Debug.Log("Llama a LOAD settings");
        string path = Application.persistentDataPath + "//saveSettingsFile.json";


        if (File.Exists(path))
        {

            string json = File.ReadAllText(path);

            SaveDataSettings data = JsonUtility.FromJson<SaveDataSettings>(json);

            // = data.soundSelection;

            SetActualColor(data.colorSelection);

            ActiveSoundBall(data.soundSelection);

            SetColorButton(data.colorSoundSelection);

        }

    }


}
