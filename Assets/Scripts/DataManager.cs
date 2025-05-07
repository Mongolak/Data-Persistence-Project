using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public string nameText; //Variable para guardar el nombre del jugador.

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
    }


}
