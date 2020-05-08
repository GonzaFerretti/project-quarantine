using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWrapper : InteractableObject
{
    public Item item;
    private ParticleSystem parts;
    protected override void Start()
    {
        //tentative;
        base.Start();
        if (item.mesh)
        {
            GetComponent<MeshFilter>().mesh = item.mesh;
        }
        parts = GetComponent<ParticleSystem>();
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
