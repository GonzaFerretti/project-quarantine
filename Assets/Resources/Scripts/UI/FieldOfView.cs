using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    private int rayCount;
    private Mesh mesh;
    [SerializeField] private float fov;
    [SerializeField] private float viewDistance;
    [SerializeField] private float heightOffset;
    [Range(1, 100)]
    [SerializeField] private int curveSharpness;
    [SerializeField] private LayerMask layerFilter;
    [SerializeField] public GameObject model;
    private Vector3 origin;
    [SerializeField] private float startingAngle;
    public Material suspectMaterial;
    public bool isVisible = true;
    public Material detectionMaterial;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public bool isObjectChildOfModel(Collider col)
    {
        Transform trans = col.transform;
        while (trans.parent != null)
        {
            trans = trans.parent;
        }
        return trans.gameObject == model;
    }

    private void OnBecameInvisible()
    {
        isVisible = false;
    }

    private void OnBecameVisible()
    {
        isVisible = true;
    }

    private void LateUpdate()
    {
        if (isVisible)
        {
            startingAngle = GetAngleFromVectorFloat(model.transform.forward);
            Vector3 rayCastOrigin = new Vector3(model.transform.position.x, model.GetComponent<ModelPatrol>().headHeight, model.transform.position.z);
            transform.position = model.transform.position + heightOffset * Vector3.up;
            rayCount = Mathf.RoundToInt(fov / curveSharpness);
            origin = Vector3.zero;
            float angle = startingAngle + fov / 2;
            float angleIncrease = fov / rayCount;

            Vector3[] vertices = new Vector3[rayCount + 1 + 1];
            Vector2[] uv = new Vector2[vertices.Length];
            int[] triangles = new int[rayCount * 3];

            vertices[0] = origin;

            int vertexIndex = 1;
            int triangleIndex = 0;
            for (int i = 0; i <= rayCount; i++)
            {
                Vector3 vertex;
                RaycastHit hit = new RaycastHit();
                RaycastHit[] hits = Physics.RaycastAll(rayCastOrigin, GetVectorFromAngle(angle), viewDistance, layerFilter.value);
                foreach (RaycastHit possibleHit in hits)
                {
                    if (!possibleHit.collider.isTrigger)
                    {
                        hit = possibleHit;
                        break;
                    }
                }

                if (hit.collider == null)
                {
                    // No hit
                    vertex = origin + GetVectorFromAngle(angle) * viewDistance;
                }
                else
                {
                    // Hit object
                    vertex = hit.point - rayCastOrigin;
                }

                //Debug.DrawLine(rayCastOrigin, rayCastOrigin + GetVectorFromAngle(angle) * viewDistance, Color.red, Time.deltaTime);
                vertices[vertexIndex] = vertex;

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


            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            mesh.bounds = new Bounds(origin, Vector3.one * viewDistance);
        }
    }

    public void Init(float fov, float viewDistance, GameObject model, bool isSuspect)
    {
        this.fov = fov;
        this.viewDistance = viewDistance;
        this.model = model;
        name = name + "(" + model.gameObject.name + ")";
        GetComponent<MeshRenderer>().material = (isSuspect) ? suspectMaterial : detectionMaterial;
    }

    Vector3 GetVectorFromAngle(float angle)
    {
        // angle = 0 -> 360
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad));
    }

    float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
