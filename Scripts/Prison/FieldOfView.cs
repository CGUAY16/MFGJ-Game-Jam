using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    //Sets how circular the field of view renders. The higher the number, the more processing required
    [SerializeField]
    int rayCount = 20;
    //Sets the fov angle
    [SerializeField]
    float fov = 90f;
    //Sets view distance of the guard's fov
    [SerializeField]
    float viewDistance = 10f;
    //Sets what the raycast will hit
    [SerializeField]
    LayerMask layerMask;
    //Sets first angle for FOV mask
    [SerializeField]
    float startingAngle;

    //Placeholder bool to indicate if player is spotted
    //public bool playerSpotted = false;
    bool hit = false;

    //Variables for mesh
    private Vector3 origin;
    private Mesh mesh;
    MeshRenderer mRenderer;
    [SerializeField]
    Material spottedMaterial;

    // Start is called before the first frame update
    void Start()
    {
        //Sets mesh for field of view at (0, 0, 0.5)
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mRenderer = GetComponent<MeshRenderer>();
        origin = new Vector3 (0f, 0f, 0f);
    }

    void LateUpdate()
    {
        //Sets the angles of the mask
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;

        //Builds mask lists
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        //Calculates vertices
        vertices[0] = origin;
        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 vertex;
            //Casts a ray to obscure scenery and spot player
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            if (raycastHit2D.collider == null)
            {
                //Didn't hit anything
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                //hit something

                if (raycastHit2D.collider.CompareTag("Player"))
                {
                    if (!hit)
                    {
                        StartCoroutine(PlayerSpotted());
                    }

                    //Stops player impeding camera field of view indicator
                    vertex = origin + GetVectorFromAngle(angle) * viewDistance;
                }

                else
                {
                    //Stops mask at the scenery it hit
                    vertex = raycastHit2D.point;
                }

                //Resets computer
                if (raycastHit2D.collider.CompareTag("Computer") && Vector2.Distance(origin, raycastHit2D.collider.transform.position) < 0.64f)
                {
                    raycastHit2D.collider.SendMessage("GuardActivate");
                }


            }



            vertices[vertexIndex] = vertex;
            //Builds triangles in mask
            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;


                triangleIndex += 3;
            }


            vertexIndex++;

            angle -= angleIncrease;
        }

        //Sets calculated mask values 
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    //math shortcut
    public Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    //Sets new origin for moving guards
    public void SetOrigin (Vector3 origin)
    {
        this.origin = origin;
    }

    //Sets new direction for moving cameras
    public void SetAimDirection(Vector3 aimDirection)
    {
        Vector3 dir = aimDirection;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
        {
            n += 360;
        }

        startingAngle = n - fov / 2f;
    }

    //Sets new direction for moving guards. Separate due to weird x/y flipping.
    public void SetGuardAimDirection(Vector3 aimDirection)
    {
        Vector3 dir = aimDirection;
        float n = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        if (n < 0)
        {
            n += 360;
        }

        startingAngle = n - fov / 2f;
    }

    public IEnumerator PlayerSpotted()
    {
        hit = true;
        GameManager.Instance.paused = true;

        mRenderer.material = spottedMaterial;
        //Insert alarm sound here
        Music.Instance.PlayTrack(0);


        yield return new WaitForSeconds(1);

        GameManager.Instance.playerSpotted = true;
        //Music.Instance.PlayTrack(0);


    }
}
