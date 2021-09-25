using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuClickPlay : MonoBehaviour
{
    [SerializeField]
    TMP_Text[] mainmenuText;
    [SerializeField]
    GameObject[] mainmenuObjects;
    [SerializeField]
    TMP_Text[] storytextText;
    [SerializeField]
    GameObject[] storytextObjects;

    [SerializeField]
    GameObject damian;
    [SerializeField]
    GameObject don;

    Color32 defaultColor;
    Color32 fadeOut;
    [SerializeField]
    Image background;

    [SerializeField]
    AudioSource menuAudio;

    bool next = false;

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = new Color32(255, 255, 255, 255);
        fadeOut = new Color32(255, 255, 255, 0);
    }

    public void StartGame()
    {
        StartCoroutine(OpeningCutscene());
    }

    private void Update()
    {
        next = Input.anyKeyDown;
    }

    IEnumerator OpeningCutscene()
    {
        foreach (var item in mainmenuText)
        {
            item.CrossFadeAlpha(0f, 1f, false);
        }

        don.GetComponent<SpriteRenderer>().color = Color32.Lerp(defaultColor, fadeOut, Mathf.Lerp(0f, 1f, Time.time / 3));

        yield return new WaitForSeconds(2f);

        foreach (var item in mainmenuObjects)
        {
            item.SetActive(false);
        }

        // Story object 1
        storytextObjects[0].SetActive(true);
        while (!next)
        {
            yield return null;
        }

        storytextObjects[0].SetActive(false);
        yield return new WaitForSeconds(1f);

        // Story object 2
        storytextObjects[1].SetActive(true);
        next = false;
        while (!next)
        {
            yield return null;
        }
        storytextObjects[1].SetActive(false);
        yield return new WaitForSeconds(1f);

        don.GetComponent<SpriteRenderer>().color = Color32.Lerp(fadeOut, defaultColor, Mathf.Lerp(0f, 1f, Time.time / 3));

        // Story object 3
        storytextObjects[2].SetActive(true);
        next = false;
        while (!next)
        {
            yield return null;
        }
        storytextObjects[2].SetActive(false);
        yield return new WaitForSeconds(1f);

        // Story object 4
        storytextObjects[3].SetActive(true);
        next = false;
        while (!next)
        {
            yield return null;
        }
        storytextObjects[3].SetActive(false);
        yield return new WaitForSeconds(.5f);

        var backgroundColor = background.GetComponent<Image>().color;

        while (background.color.a < 1)
        {
            backgroundColor.a += Time.deltaTime / 4;
            background.color = backgroundColor;
            menuAudio.volume -= .0010f;

        }

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(1);

        yield return new WaitForSeconds(1f);


        yield return null;
    }
}
