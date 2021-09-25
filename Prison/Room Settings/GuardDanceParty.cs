using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardDanceParty : MonoBehaviour
{
    [SerializeField]
    FieldOfView[] guardFOV;
    [SerializeField]
    Material[] materials;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PartyTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PartyTime()
    {
        yield return new WaitForSeconds(10.5f);
        while (true)
        {
            foreach (var item in guardFOV)
            {
                item.GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Length - 1)];
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
