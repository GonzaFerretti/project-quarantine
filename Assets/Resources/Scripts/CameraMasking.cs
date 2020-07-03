using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMasking : MonoBehaviour
{
    //public List<GameObject> lastMaskedBuildings = new List<GameObject>();
    public float checkDistanceDelta;
    public float perpendicularLength;
    public int buildingLayerId;
    public int maskedBuildingLayerId;
    private WideCamMaskCheck wideCheckScript;
    public ModelPlayable player;

    public BoxCollider longCollider;
    public BoxCollider wideCollider;

    private void Start()
    {
        longCollider = GetComponent<BoxCollider>();
        wideCheckScript = wideCollider.GetComponent<WideCamMaskCheck>();
        wideCheckScript.buildingLayerId = buildingLayerId;
        wideCheckScript.maskedBuildingLayerId = maskedBuildingLayerId;
        longCollider.center = new Vector3(0, 0, checkDistanceDelta / 2);
        StartCoroutine(FindPlayerWithDelay());
    }

    IEnumerator FindPlayerWithDelay()
    {
        yield return new WaitUntil(() => FindObjectOfType<ModelPlayable>() != null);
        player = FindObjectOfType<ModelPlayable>();
    }

    void Update()
    {
        if (player != null)
        { 
        UpdateCheckDistance();
        }
        //CheckCoveringBuilding();
    }

    void UpdateCheckDistance()
    {
        float newDistance = (transform.position - player.transform.position).magnitude;
        longCollider.size = new Vector3(longCollider.size.x, longCollider.size.y, newDistance - checkDistanceDelta);
        longCollider.center = new Vector3(0, 0, (newDistance -checkDistanceDelta) / 2f);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == buildingLayerId)
        {
            other.gameObject.layer = maskedBuildingLayerId;
            wideCheckScript.SwitchPosition();
            wideCollider.enabled = true;
            wideCollider.size = new Vector3(perpendicularLength, 0.25f, 0.25f);
            //wideCollider.center = new Vector3(0, 0, checkDistance);
            wideCollider.transform.position = other.transform.position;
            wideCollider.transform.right = other.GetComponent<BuildingMaskSettings>().maskDirection;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == maskedBuildingLayerId)
        {
            other.gameObject.layer = buildingLayerId;
            wideCollider.enabled = false;
            wideCheckScript.SwitchPosition();
        }
    }
    /*
    private void CheckCoveringBuilding()
    {
        RaycastHit[] hitsBuilding = Physics.RaycastAll(transform.position, transform.forward, checkDistance, 1 << buildingLayerId);
        RaycastHit[] hitsMasked = Physics.RaycastAll(transform.position, transform.forward, checkDistance, 1 << maskedBuildingLayerId);
        if (lastMaskedBuildings.Count > 0)
        { 
            List<GameObject> maskedButNotHitBuildings = new List<GameObject>(lastMaskedBuildings);
            for (int i = 0; i < hitsMasked.Length; i++)
            {
                RaycastHit hit = hitsMasked[i];
                if (maskedButNotHitBuildings.Contains(hit.transform.gameObject))
                {
                    maskedButNotHitBuildings.Remove(hit.transform.gameObject);
                }
            }
            foreach (GameObject building in maskedButNotHitBuildings)
            {
                building.transform.gameObject.layer = buildingLayerId;
            }
        }
        for (int i = 0; i<hitsBuilding.Length; i++)
        {
            RaycastHit hit = hitsBuilding[i];
            hit.collider.gameObject.layer = maskedBuildingLayerId;
            if (!lastMaskedBuildings.Contains(hit.transform.gameObject))
            {
                lastMaskedBuildings.Add(hit.transform.gameObject);
            }
        }

        Debug.DrawLine(transform.position, transform.position + transform.forward * checkDistance, Color.red, Time.deltaTime);
    }
    */
}
