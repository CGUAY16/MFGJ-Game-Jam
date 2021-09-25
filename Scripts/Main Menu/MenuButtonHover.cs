using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] AudioSource audioPlayer;
    [SerializeField] TMP_Text tmptextInFocus;

    // text colour change
    Color32 orange = new Color32(243, 127, 10, 255);
    Color32 defaultWhite = new Color32(255, 255, 255, 255);

    public void OnPointerEnter(PointerEventData eventData)
    {
        //MenuAudioManager.menuAudioInstance.SetupAudioClip(0);
        //MenuAudioManager.menuAudioInstance.PlaySound();
        tmptextInFocus.color = orange;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tmptextInFocus.color = defaultWhite;
    }
}
