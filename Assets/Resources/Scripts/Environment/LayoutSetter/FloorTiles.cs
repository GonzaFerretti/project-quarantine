using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTiles : MonoBehaviour
{
    public float distance;
    public GameObject wall;

    private void Start()
    {
        StartCoroutine(CastRayCast(new Vector3(1, -1, 0), new Vector3(-0.5f, 1 + wall.transform.localScale.y / 2, 0), 90));
        StartCoroutine(CastRayCast(new Vector3(-1, -1, 0), new Vector3(0.5f, 1 + wall.transform.localScale.y / 2, 0), 90));
        StartCoroutine(CastRayCast(new Vector3(0, -1, 1), new Vector3(0, 1 + wall.transform.localScale.y / 2, -0.5f), 0));
        StartCoroutine(CastRayCast(new Vector3(0, -1, -1), new Vector3(0, 1 + wall.transform.localScale.y / 2, 0.5f), 0));
        StartCoroutine(DestroyScript());
    }

    IEnumerator DestroyScript()
    {
        yield return new WaitForFixedUpdate();
        Destroy(this);
    }

    IEnumerator CastRayCast(Vector3 location, Vector3 adjustment, float rotation)
    {
        yield return new WaitForFixedUpdate();
        RaycastHit hit;
        Physics.Raycast(transform.position + location, transform.up, out hit, 1 + distance);

        if (!hit.collider)
        {
            GameObject newWall = Instantiate(wall);
            newWall.transform.position = transform.position;
            newWall.transform.parent = transform.parent;
            newWall.transform.position = transform.position + location + adjustment;
            newWall.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }
}
