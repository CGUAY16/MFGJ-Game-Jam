using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField]
    GameObject menuItems;
    [SerializeField]
    GameObject creditsPage;

    public void ActivateCredits()
    {
        menuItems.SetActive(false);
        creditsPage.SetActive(true);
    }

    public void DeactivateCredits()
    {
        creditsPage.SetActive(false);
        menuItems.SetActive(true);
    }
}
