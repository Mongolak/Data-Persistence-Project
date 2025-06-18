using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static UnityEditor.SceneView;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public string nameText; //Variable para guardar el nombre del jugador, si la puntuación es más alta.

    public int bestScore; //Variable para guardar la puntuación del jugador, que se mostrará en la escena "main".

    public string actualName; //Variable para guardar el nombre del jugador.


    //Variables para poder mostrar los nombres y las puntuaciones en la escena "HighScore".
    public int newScore; //Variable que servirá para guardar la puntuación actual.

    public string newName; //Variable para guardar el nombre del jugador actual.
    

    //Código para acceder desde cualquier otro script.
    public static DataManager Instance; 

    private void Awake()
    {

        if(Instance != null)
        {

            Destroy(gameObject);

            return;

        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        LoadNameScore();//Carga los datos de la partida anterior entre escenas.


    }

   


    //Añadir una clase Serializable para guardar los datos.
    [System.Serializable]
    class SaveData
    {

        public string nameTextGame;

        public int bestScoreGame;


    }

    //Agregamos un método para guardar el nombre.
    public void SaveNameScore()
    {

        SaveData data = new SaveData
        {

            nameTextGame = nameText,
            bestScoreGame = bestScore,

        };

        //data.nameTextGame = nameText;

        //data.bestScoreGame = bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "//savefile.json", json); //Lo guardará en la ruta por defecto "C:\Users\<user>\AppData\LocalLow\<company name>".

        

    }

    //Agregamos un método para cargar el nombre.
    public void LoadNameScore()
    {

        string path = Application.persistentDataPath + "//savefile.json";


        if (File.Exists(path))
        {

            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            nameText = data.nameTextGame;

            bestScore = data.bestScoreGame;


        }

    }


    //Método para leer el nombre del jugador, que se guardará en la variable nameText.
    //Se accede desde el InputField de la escena Start Menu.
    public void SetActualName(string name)
    {

        actualName = name;

        Debug.Log("Ahora actual name es : " + name);
        

    }

}
