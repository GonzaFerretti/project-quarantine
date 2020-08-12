using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelNodeUsingEnemy : ModelEnemy
{
    public PatrolNode node;
    public ControllerWrapper scoutController;
    public ControllerWrapper guardController;
    public List<GameObject> fovCones = new List<GameObject>();
    public GameObject fovConePrefab;
    public Vector3 lastSight;

   protected IEnumerator FoVConeIitialization()
    {
        yield return new WaitForSeconds(0.5f);

        InitFOVCone(enemyAttributes.angle, _suspectRange, true);
        InitFOVCone(enemyAttributes.angle, alertRange, false);
        InitFOVCone(360, GetComponent<SphereCollider>().radius, false);
    }

    protected void InitFOVCone(float angle, float range, bool isSuspect)
    {
        GameObject cone = Instantiate(fovConePrefab, null);
        cone.GetComponent<FieldOfView>().Init(angle, range, gameObject, isSuspect);
        fovCones.Add(cone);
    }

    public void SetFOVcones(bool isOn)
    {
        foreach (GameObject fovCone in fovCones)
        {
            fovCone.SetActive(isOn);
        }
    }
}