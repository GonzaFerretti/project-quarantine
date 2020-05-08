using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWrapper : InteractableObject
{
    public Item item;
    private ParticleSystem parts;
    public Mesh itemModel;
    protected override void Start()
    {
        //tentative;
        base.Start();
        parts = GetComponent<ParticleSystem>();
        GetComponent<MeshFilter>().mesh = itemModel;
    }

    public void activateParticles()
    {
        parts.Play();
    }

    public void disableParticles()
    {
        parts.Stop();
    }

    public bool isPlayerNearby()
    {
        return parts.isPlaying;
    }
}
