using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DataSettingsManager : MonoBehaviour
{

    public Color backgroundColorActual = Color.black; //Variable para guardar el color de fondo.

    //Variables para escoger el color.
    private Color greenColor = new Color(0.1592439f, 0.4339623f, 0.08802065f);
    private Color blueColor = new Color(0.1968672f, 0.507506f, 0.5283019f);
    private Color orangeColor = new Color(0.4716981f, 0.2625228f, 0.06452475f);
    private Color blackColor = Color.black;
    
    
    private int colorSetting; // tipo de color, 0 = verde, 1=...

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
    
    public void setActualColor(int color)
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
    //Añadir una clase Serializable para guardar los datos.
    [System.Serializable]
    class SaveDataSettings
    {

        public bool soundSelection;

        public int colorSelection;

    }

    //Agregamos un método para guardar el nombre.
    public void SaveSettings()
    {
        Debug.Log("Llama a save settings");
        SaveDataSettings data = new SaveDataSettings
        {

            soundSelection = false,
            colorSelection = colorSetting

        };

        //data.nameTextGame = nameText;

        //data.bestScoreGame = bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "//saveSettingsFile.json", json); //Lo guardará en la ruta por defecto "C:\Users\<user>\AppData\LocalLow\<company name>".



    }

    //Agregamos un método para cargar el nombre.
    public void LoadSettings()
    {
        Debug.Log("Llama a LOAD settings");
        string path = Application.persistentDataPath + "//saveSettingsFile.json";


        if (File.Exists(path))
        {

            string json = File.ReadAllText(path);

            SaveDataSettings data = JsonUtility.FromJson<SaveDataSettings>(json);

            // = data.soundSelection;

            setActualColor(data.colorSelection);

        }

    }


}
