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

    public Text playerName; //Variable para el texto del jugador y la puntuación.

    public InputField actualNameInput; //Variable para el almacenar el valor del InputField.

   
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

            DataManager.Instance.LoadNameScore();

            SetNameScore(DataManager.Instance.nameText, DataManager.Instance.bestScore);

        }


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
            
            //DataManager.Instance.SaveNameScore();


            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            //Condicional para volver a cargar la escena del Menú principal.
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                SceneManager.LoadScene(0);

                
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

        int oldScore;

        //Guardar la puntuación al finalizar la partida
        if (DataManager.Instance != null)
        {

            oldScore = DataManager.Instance.bestScore;

            if (m_Points > oldScore)
            {

                DataManager.Instance.bestScore = m_Points;

                //Se añadirá el nuevo nombre a la variable "nameText" del script DataManager, para después ser guardado en JSON.
                DataManager.Instance.nameText = DataManager.Instance.actualName;

                DataManager.Instance.SaveNameScore();

                Debug.Log("Añadimos: " + DataManager.Instance.actualName + " y " + m_Points);

                SetNameScore(DataManager.Instance.actualName, m_Points);

            }

            //Código para reiniciar el contador de puntos.
             
            /*DataManager.Instance.bestScore = m_Points;

            DataManager.Instance.nameText = DataManager.Instance.actualName;

            DataManager.Instance.SaveNameScore();*/

        }


    }


    //Método para salir del juego en el caso, de que esté en pruebas.
    //Se accede desde el botón "Quit" de la escena Start Menu (escena 0).
    public void Exit()
    {
   

        DataManager.Instance.SaveNameScore();



#if UNITY_EDITOR

        EditorApplication.ExitPlaymode();

#else
    
    

    Applicattion.Quit();
    
    
#endif

    }


    //Método para mostrar el nombre del jugador al cambiar de escena.
    public void SetName(string name)
    {

        playerName.text = "Best Score: " + name + ": 0 ";

    }

    //Método para mostrar el nombre del jugador y la puntuación guardada al cambiar de escena.
    public void SetNameScore(string name, int score)
    {

        if (score == 0)
        {
            
            SetName(DataManager.Instance.nameText);

        }
        else if (score > 0)
        {

            playerName.text = "Best Score: " + name + ": " + score;
           
        }


    }

    //Método para acceder al juego (escena 1).
    //Se accede desde el botón "Start" de la escena Start Menú (escena 0).
    public void StartNew()
    {

        SceneManager.LoadScene(1);

        //Acceder al nombre actual del jugador (aún no está guardado en el JSON) y mostrarlo en consola.
        DataManager.Instance.SetActualName(actualNameInput.text);

    }

    //Método para volver al menú.
    //Se accede desde el botón "Main Menu" de la escena High Scores (escena 2) y Settings (escena 3).
    public void ReturnMenu()
    {

        if(DataManager.Instance != null)
        {

            //DataManager.Instance.SaveNameScore(); //Guardar antes de cambiar de escena. No es necesario.



        }

        SceneManager.LoadScene(0);

    }

    //Método para mostrar la escena de puntuaciones (High Scores).
    //Se accede desde el botón "High Scores" de la escena Start Menu (escena 0).
    public void ScoresScene()
    {

        SceneManager.LoadScene(2);

    }

    //Método para mostrar la escena de ajustes (Settings).
    //Se accede desde el botón "Settings" de la escena Start Menu (escena 0).
    public void SettingsScene()
    {

        SceneManager.LoadScene(3);

    }

}
