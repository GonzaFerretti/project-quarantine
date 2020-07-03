using UnityEngine;

[CreateAssetMenu(menuName ="Controller/MapManager/Requisites")]
public class MapActionAddRequisites : ActionMapWrapper, IActionMap
{
    public override ActionMapWrapper Clone()
    {
        MapActionAddRequisites clone = CreateInstance("MapActionAddRequisites") as MapActionAddRequisites;
        return clone;
    }

    public void Do()
    {
        ModelNPC[] myNpcs = FindObjectsOfType<ModelNPC>();
        for (int i = 0; i < myNpcs.Length; i++)
        {
            ResourceManager.AddToResourceDict(myNpcs[i].currentResource.resourceName, Mathf.RoundToInt(myNpcs[i].currentAmountRequired), ref ResourceManager.requiredResources);
            ResourceManager.AddToResourceDict(myNpcs[i].currentResource.resourceName, 0, ref ResourceManager.currentResources);
        }
    }

    public override void SetAction()
    {
        myMapAction = this;
    }
}