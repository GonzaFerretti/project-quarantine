using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWrapper : InteractableObject
{
    public Item item;
    private ParticleSystem parts;
    public GameObject characterModel;
    protected override void Start()
    {
        //tentative;
        base.Start();
        parts = GetComponent<ParticleSystem>();
        //GameObject itemModel = Instantiate(characterModel, transform);
        //itemModel.name = "itemModel";
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
