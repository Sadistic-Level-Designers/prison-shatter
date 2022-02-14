using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FigmentScript : MonoBehaviour
{
    public string text;
    public KeyCode closeKey = KeyCode.X;
    
    protected Canvas canvas;
    protected Text textbox;


    // Start is called before the first frame update
    void Start()
    {
        // find canvas & text
        canvas = transform.Find("Canvas").GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        if(!canvas.worldCamera) {
            Debug.LogError("You forgot to assign the camera to the canvas inside the Figment!");
        }

        // update text
        textbox = canvas.transform.Find("FigmentText").GetComponent<Text>();
        textbox.text = text;

        canvas.gameObject.SetActive(true);
        PlayerScript.i.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(closeKey)) {
            canvas.gameObject.SetActive(false);
            PlayerScript.i.enabled = true;
        }
    }
}
