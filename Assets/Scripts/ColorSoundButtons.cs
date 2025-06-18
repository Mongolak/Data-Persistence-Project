using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSoundButtons : MonoBehaviour
{

    public Button soundButtonYes;

    public Button soundButtonNo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(DataSettingsManager.Instance != null)
        {

            soundButtonYes.image.color = DataSettingsManager.Instance.colorSoundButtonYes;

            soundButtonNo.image.color = DataSettingsManager.Instance.colorSoundButtonNo;

        }


    }

    //Método para acceder a cambiar el color de los botones del sonido.
    //Se accede desde el botón "Yes" y "No" de la escena "Settings", añadido en "Sound Buttons" (GameObject).
    public void SetColorSoundButton(int color)
    {

        DataSettingsManager.Instance.SetColorButton(color);

    }
}
