using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform subject;

    Vector2 startPos;
    float startZPos;

    // Properties
    Vector2 travel => (Vector2)cam.transform.position - startPos;


    float distanceFromSubject;
    Vector2 parallaxFactor;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startZPos = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos + travel * 0.9f;
    }
}
