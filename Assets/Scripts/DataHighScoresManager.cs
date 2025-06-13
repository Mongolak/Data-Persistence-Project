using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataHighScoresManager : MonoBehaviour
{
    
    //Variables para guardar los nombres.
    public string namePlayer1;
    public string namePlayer2;
    public string namePlayer3;
    public string namePlayer4;
    public string namePlayer5;

    //Variables para guardar las puntuaciones.
    public int scorePlayer1;
    public int scorePlayer2;
    public int scorePlayer3;
    public int scorePlayer4;
    public int scorePlayer5;


    //Código para acceder desde cualquier otro script. Patrón singleton.
    public static DataHighScoresManager Instance;

    private void Awake()
    {

        if (Instance != null)
        {

            Destroy(gameObject);

            return;

        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        LoadNameHighScore();
    }

    //Método para seleccionar en el listado, el nombre y la puntuación del jugador.

    public void SetRankingScores(string namePlayer, int scorePlayer)
    {


        Debug.Log("SetRankingScores params: " + namePlayer+" "+scorePlayer.ToString());
        LoadNameHighScore();


        //Variables para guardar los nombres y las puntuaciones guardadas.
        string name1 = namePlayer1;
        string name2 = namePlayer2;
        string name3 = namePlayer3;
        string name4 = namePlayer4;
        string name5 = namePlayer5;

        int score1 = scorePlayer1;
        int score2 = scorePlayer2;
        int score3 = scorePlayer3;
        int score4 = scorePlayer4;
        int score5 = scorePlayer5;


        if (scorePlayer > scorePlayer1)
        {

            scorePlayer1 = scorePlayer;

            scorePlayer5 = score4;
            scorePlayer4 = score3;
            scorePlayer3 = score2;
            scorePlayer2 = score1;


            namePlayer1 = namePlayer;

            namePlayer5 = name4;
            namePlayer4 = name3;
            namePlayer3 = name2;
            namePlayer2 = name1;

            SaveNameHighScore();

        }
        else if ((scorePlayer <= scorePlayer1) && (scorePlayer > scorePlayer2))
        {

            scorePlayer2 = scorePlayer;

            scorePlayer5 = score4;
            scorePlayer4 = score3;
            scorePlayer3 = score2;
            scorePlayer1 = score1;

            namePlayer2 = namePlayer;

            namePlayer5 = name4;
            namePlayer4 = name3;
            namePlayer3 = name2;
            namePlayer1 = name1;

            SaveNameHighScore();

        }
        else if ((scorePlayer <= scorePlayer2) && (scorePlayer > scorePlayer3))
        {

            scorePlayer3 = scorePlayer;

            scorePlayer5 = score4;
            scorePlayer4 = score3;
            scorePlayer2 = score2;
            scorePlayer1 = score1;

            namePlayer3 = namePlayer;

            namePlayer5 = name4;
            namePlayer4 = name3;
            namePlayer2 = name2;
            namePlayer1 = name1;

            SaveNameHighScore();

        }
        else if ((scorePlayer <= scorePlayer3) && (scorePlayer > scorePlayer4))
        {

            scorePlayer4 = scorePlayer;

            scorePlayer5 = score4;
            scorePlayer3 = score3;
            scorePlayer2 = score2;
            scorePlayer1 = score1;

            namePlayer4 = namePlayer;

            namePlayer5 = name4;
            namePlayer3 = name3;
            namePlayer2 = name2;
            namePlayer1 = name1;

            SaveNameHighScore();

        }
        else if ((scorePlayer <= scorePlayer4) && (scorePlayer > scorePlayer5))
        {

            scorePlayer5 = scorePlayer;

            scorePlayer4 = score4;
            scorePlayer3 = score3;
            scorePlayer2 = score2;
            scorePlayer1 = score1;

            namePlayer5 = namePlayer;

            namePlayer4 = name4;
            namePlayer3 = name3;
            namePlayer2 = name2;
            namePlayer1 = name1;

            SaveNameHighScore();

        }

        Debug.Log("Compara");

    }


    //Añadir una clase Serializable para guardar los datos.
    [System.Serializable]
    class SaveData
    {
        //Variables para guardar los nombres de los jugadores.
        public string nameTextGame1;
        public string nameTextGame2;
        public string nameTextGame3;
        public string nameTextGame4;
        public string nameTextGame5;

        //Variables para guardar las puntuaciones de los jugadores.
        public int bestScoreGame1;
        public int bestScoreGame2;
        public int bestScoreGame3;
        public int bestScoreGame4;
        public int bestScoreGame5;


    }

    //Agregamos un método para guardar los datos (nombres y puntuaciones).
    public void SaveNameHighScore()
    {
        

        SaveData data = new SaveData
        {

            //Utilizar las siguientes variables para resetear los nombres y las puntuaciones.
            /*nameTextGame1 = "Joe",
            nameTextGame2 = "Peter",
            nameTextGame3 = "Kyle",
            nameTextGame4 = "Anne",
            nameTextGame5 = "John",

            bestScoreGame1 = 25,
            bestScoreGame2 = 15,
            bestScoreGame3 = 10,
            bestScoreGame4 = 5,
            bestScoreGame5 = 1,*/


            //Utilizar las siguientes variables para actualizar los nombres y las puntuaciones.
            nameTextGame1 = namePlayer1,
            nameTextGame2 = namePlayer2,
            nameTextGame3 = namePlayer3,
            nameTextGame4 = namePlayer4,
            nameTextGame5 = namePlayer5,

            bestScoreGame1 = scorePlayer1,
            bestScoreGame2 = scorePlayer2,
            bestScoreGame3 = scorePlayer3,
            bestScoreGame4 = scorePlayer4,
            bestScoreGame5 = scorePlayer5,

        };


        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "//saveScoresfile.json", json); //Lo guardará en la ruta por defecto "C:\Users\<user>\AppData\LocalLow\<company name>".



    }

    //Agregamos un método para cargar los datos (nombres y puntuaciones).
    public void LoadNameHighScore()
    {

        string path = Application.persistentDataPath + "//saveScoresfile.json";


        if (File.Exists(path))
        {

            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            namePlayer1 = data.nameTextGame1;
            namePlayer2 = data.nameTextGame2;
            namePlayer3 = data.nameTextGame3;
            namePlayer4 = data.nameTextGame4;
            namePlayer5 = data.nameTextGame5;

            scorePlayer1 = data.bestScoreGame1;
            scorePlayer2 = data.bestScoreGame2;
            scorePlayer3 = data.bestScoreGame3;
            scorePlayer4 = data.bestScoreGame4;
            scorePlayer5 = data.bestScoreGame5;


        }

    }
    

}
