using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresNames : MonoBehaviour
{

    //Variables para mostrar los nombres.
    public Text player1;
    public Text player2;
    public Text player3;
    public Text player4;
    public Text player5;

    //Variables para mostrar las puntuaciones.
    public Text score1;
    public Text score2;
    public Text score3;
    public Text score4;
    public Text score5;

    

    // Start is called before the first frame update
    void Start()
    {

        if (DataHighScoresManager.Instance != null)
        {
      

            //Llamar al método para mostrar los nombres y las puntuaciones.
            ShowPlayerNameScore();

            //Debug.Log("Muestra los nombres y las puntuaciones");
        }


    }

    // Update is called once per frame
    void Update()
    {
        

    }

    //Método para mostrar en pantalla los nombres y las puntuaciones de los jugadores.
    public void ShowPlayerNameScore()
    {
        DataHighScoresManager.Instance.LoadNameHighScore();

        //Nombres de los jugadores.
        player1.text = DataHighScoresManager.Instance.namePlayer1;
        player2.text = DataHighScoresManager.Instance.namePlayer2;
        player3.text = DataHighScoresManager.Instance.namePlayer3;
        player4.text = DataHighScoresManager.Instance.namePlayer4;
        player5.text = DataHighScoresManager.Instance.namePlayer5;

        //Puntuaciones de los jugadores.
        score1.text = "     " + DataHighScoresManager.Instance.scorePlayer1.ToString();
        score2.text = "     " + DataHighScoresManager.Instance.scorePlayer2.ToString();
        score3.text = "     " + DataHighScoresManager.Instance.scorePlayer3.ToString(); ;
        score4.text = "     " + DataHighScoresManager.Instance.scorePlayer4.ToString(); ;
        score5.text = "     " + DataHighScoresManager.Instance.scorePlayer5.ToString(); ;


    }


}
