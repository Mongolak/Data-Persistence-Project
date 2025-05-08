using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    public Text playerName; //Variable para el nombre del jugador.


    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        //Condición para compartir datos (nombre) entre escenas.
        if(DataManager.Instance != null)
        {

            //ReadName(DataManager.Instance.nameText);

            DataManager.Instance.LoadNameScore();

            GetNameScore(DataManager.Instance.nameText, DataManager.Instance.bestScore);

        }


        //Mostrar el nombre al inicio de la partida.
        //GetName(DataManager.Instance.nameText);

        //DataManager.Instance.LoadNameScore();

        //Muestra el nombre del jugador durante la partida.
        //GetName(DataManager.Instance.nameText);

        //GetNameScore(DataManager.Instance.nameText, DataManager.Instance.bestScore);
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            
            DataManager.Instance.SaveNameScore();


            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            //Condicional para volver a cargar la escena del Menú principal.
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                SceneManager.LoadScene(0);

                ReadName(DataManager.Instance.nameText);

                
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        //Guardar la puntuación al finalizar la partida
        if(DataManager.Instance != null)
        {

            DataManager.Instance.bestScore = m_Points;

            DataManager.Instance.SaveNameScore();

        }

        //Muestra el nombre y la puntuación obtenida en la partida.
        playerName.text = "Best Score: " + DataManager.Instance.nameText + ": " + DataManager.Instance.bestScore;

    }

    //Método para acceder al juego (escena 1).
    public void StartNew()
    {

        SceneManager.LoadScene(1);

    }

    //Método para salir del juego en el caso, de que esté en pruebas.
    public void Exit()
    {
   

        DataManager.Instance.SaveNameScore();



#if UNITY_EDITOR

        EditorApplication.ExitPlaymode();

#else
    
    

    Applicattion.Quit();
    
    
#endif

    }

    //Método para leer el nombre del jugador, que se guardará en la variable de DataManager (nameText).
    public void ReadName(string name)
    {

        if(DataManager.Instance !=null)
        {

            DataManager.Instance.nameText = name;

            DataManager.Instance.SaveNameScore(); //Guarda el nombre inmediatamente.

            Debug.Log(DataManager.Instance.nameText);

        } 

    }

    //Método para guardar la puntuación.
    public void ReadScore(int score)
    {

        if(DataManager.Instance!=null)
        {
            score = m_Points;

            DataManager.Instance.bestScore = score;

            DataManager.Instance.SaveNameScore(); //Guarda la puntuación inmediatamente.

            Debug.Log(DataManager.Instance.bestScore);

        }

       

    }

    //Método para mostrar el nombre del jugador al cambiar de escena.
    public void GetName(string name)
    {

        playerName.text = "Best Score: " + name + ": 0 ";

    }

    //Método para mostrar el nombre del jugador y la puntuación guardad al cambiar de escena.
    public void GetNameScore(string name, int score)
    {

        if (score == 0)
        {
            
            GetName(DataManager.Instance.nameText);

        }
        else if (score > 0)
        {

            playerName.text = "Best Score: " + name + ": " + score;
           
        }


    }

    //Método para volver al menú.
    public void ReturnMenu()
    {

        if(DataManager.Instance != null)
        {

            DataManager.Instance.SaveNameScore(); //Guardar antes de cambiar de escena.

        }

        SceneManager.LoadScene(0);

    }

    //Método para mostrar la escena de puntuaciones (High Scores).
    public void ScoresScene()
    {

        SceneManager.LoadScene(2);

    }

    //Método para mostrar la escena de ajustes (Settings).

    public void SettingsSecene()
    {

        SceneManager.LoadScene(3);

    }

}
