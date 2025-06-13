using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    //Array de tipo string que contiene los nombres.
    public string[] names = {"Joe", "Peter", "Lisa", "Maggie", "Bart"};

    //Variable de tipo Array para guardar los nombres de los jugadores.
    public Text[] namePlayers = new Text[5];

    //Array de tipo int que contiene las puntuaciones.
    public int[] scores = { 8, 2, 5, 7, 9 };
    
    //Variable de tipo Array para guardar las puntuaciones de los jugadores.
    public Text[] scorePlayers = new Text[5];


    public string namePlayer;

    public int actualScore;


    // Start is called before the first frame update
    void Start()
    {

        SetPlayerNames();

        SetPlayerScores();



        //Condición para compartir datos (nombre) entre escenas.
        if (DataManager.Instance != null)
        {

            DataManager.Instance.LoadNameScore();

           ComparePlayerBestScore(DataManager.Instance.nameText, DataManager.Instance.bestScore);

        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método para mostrar los nombres de prueba.
    public void SetPlayerNames()
    {

        for(int i=0; i<namePlayers.Length; i++)
        {


            namePlayers[i].text = names[i];

            
        }

    }
    //Método para mostrar las puntuaciones de prueba.
    public void SetPlayerScores()
    {

        Array.Sort(scores);

        Array.Reverse(scores);

        for (int i=0; i<scorePlayers.Length; i++)
        {

            scorePlayers[i].text = "       " + scores[i].ToString();

        }     

    }



    //Método para comparar las puntuaciones y guardar al jugador en su correspondiente posición.
    public void ComparePlayerBestScore(string name, int score)
    {

        actualScore = DataManager.Instance.bestScore;

        name = DataManager.Instance.nameText;

        namePlayer = name;


        if (actualScore > scores[0])
        {
            
            namePlayers[0].text = name;

            scorePlayers[0].text = actualScore.ToString();

            SaveNamesScores();

        }


        if(actualScore == scores[0] && actualScore > scores[1])
        {

            namePlayers[1].text = name;

            scorePlayers[1].text = actualScore.ToString();

            SaveNamesScores();

        }

        if (actualScore == scores[1] && actualScore > scores[2])
        {

            namePlayers[2].text = name;

            scorePlayers[2].text = actualScore.ToString();

            SaveNamesScores();


        }


        if (actualScore == scores[2] && actualScore > scores[3])
        {

            namePlayers[3].text = name;

            scorePlayers[3].text = actualScore.ToString();

            SaveNamesScores();


        }

        if (actualScore == scores[3] && actualScore > scores[4])
        {

            namePlayers[4].text = name;

            scorePlayers[4].text = actualScore.ToString();

            SaveNamesScores();


        }


    }

    //Añadir una clase serializable para guardar los datos.
    [System.Serializable]

    public class SaveHighScores
    {

        public string nameHighScore;

        public int scoreHighScore;

    }

    
    //Método para guardar los nombres y la puntuación.
    public void SaveNamesScores()
    {

        Debug.Log("LLama a SaveNamesScores");

        SaveHighScores data = new SaveHighScores
        {

            nameHighScore = namePlayer,

            scoreHighScore = actualScore,

        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "//saveHighScoresFile.json", json); //Lo guardará en la ruta por defecto "C:\Users\<user>\AppData\LocalLow\<company name>".


    }

    //Agregamos un método para cargar el sonido y el color.
    public void LoadHighScores()
    {
        Debug.Log("Llama a LOAD settings");
        string path = Application.persistentDataPath + "////saveHighScoresFile.json";


        if (File.Exists(path))
        {

            string json = File.ReadAllText(path);

            SaveHighScores data = JsonUtility.FromJson<SaveHighScores>(json);

            // = data.soundSelection;

            //SetActualColor(data.colorSelection);

            //ActiveSoundBall(data.soundSelection);

            //SetColorButton(data.colorSoundSelection);

        }

    }


}
