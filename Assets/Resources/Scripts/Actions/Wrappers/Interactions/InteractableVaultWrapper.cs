using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableVaultWrapper : MonoBehaviour
{
    //Tentative
    Material _mat;
    private void Start()
    {
        _mat = Resources.Load<Material>("Art/Visual/Placeholder/Blue");
        transform.GetComponent<MeshRenderer>().material = _mat;
    }
}
