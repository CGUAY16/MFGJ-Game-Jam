using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    [SerializeField] Transform objectTransform;
    [SerializeField] float obSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        objectTransform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * obSpeed);
    }

}
