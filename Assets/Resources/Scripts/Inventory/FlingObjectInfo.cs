using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlingObjectInfo
{
    public Mesh _mesh;
    public Material _material;
    public Vector3 _originalScale;
    public SoundClip _missSound;
    public List<ActionWrapper> _collisionActions;
    public FlingObjectInfo(GameObject goToCopy)
    {
        _mesh = goToCopy.GetComponent<MeshFilter>().mesh;
        _material = goToCopy.GetComponent<ItemWrapper>().standardMaterial;
        _originalScale = goToCopy.transform.localScale;
        _missSound = (goToCopy.GetComponent<ItemWrapper>().item as FlingableItem).missSound;
        _collisionActions = (goToCopy.GetComponent<ItemWrapper>().item as FlingableItem).collisionActions;
    }
}
