using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadingText : MonoBehaviour
{

    [SerializeField] TMP_Text text;
    [SerializeField] GameObject introObjects;
    [SerializeField] GameObject mainmenuObjects;
    [SerializeField] TMP_Text[] mainmenuText; 

    Color32 defaultColor;
    Color32 fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = new Color32(152, 2, 2, 255);
        fadeOut = new Color32(152, 2, 2, 0);

        foreach (var item in mainmenuText)
        {
            //item.alpha = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.faceColor = Color32.Lerp(defaultColor, fadeOut, Mathf.Sin(Time.time));

        if (Input.anyKeyDown)
        { 
            text.CrossFadeAlpha(0f, 1f, false);
            mainmenuObjects.SetActive(true);
        }

        if (text.alpha == 0f)
        {
            introObjects.SetActive(false);
        }
    }
}
