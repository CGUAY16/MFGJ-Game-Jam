using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField]
    Transform[] ToDestroy = new Transform[0];
    [SerializeField]
    Transform[] ToActivate = new Transform[0];

    public bool computerStatus;
    [SerializeField]
    Sprite onComputer;
    [SerializeField]
    Sprite activatedComputer;

    SpriteRenderer SRenderer;
    [SerializeField]
    AudioSource soundEffect;

    private void Awake()
    {
        SRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (computerStatus)
        {
            SRenderer.sprite = onComputer;
        }
        else
        {
            SRenderer.sprite = activatedComputer;
            ToggleObjects();
        }
    }
    
    public void Activate()
    {
        if (computerStatus)
        {
            soundEffect.Play();
            computerStatus = false;
            DestroyObjects();
            ToggleObjects();
            SRenderer.sprite = activatedComputer;
        }

        else
        {
            soundEffect.Play();
            computerStatus = true;
            ToggleObjects();
            SRenderer.sprite = onComputer;
        }
    }

    public void GuardActivate()
    {
        if (!computerStatus)
        {
            soundEffect.Play();
            ToggleObjects();
        }

        computerStatus = true;
        SRenderer.sprite = onComputer;
    }

    void DestroyObjects()
    {
        if (ToDestroy.Length != 0)
        {
            foreach (var item in ToDestroy)
            {
                Destroy(item.gameObject);
            }
        }
    }

    void ToggleObjects()
    {
        if (ToActivate.Length != 0)
        {
            foreach (var item in ToActivate)
            {
                item.gameObject.SetActive(!item.gameObject.activeSelf);
            }
        }
    }
}
