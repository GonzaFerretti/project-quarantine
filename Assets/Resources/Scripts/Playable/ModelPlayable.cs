using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModelPlayable : ModelHumanoid
{
    float _sneakSpeed;    
    public Inventory inv;
    public CharacterAttributes myAttributes;
    public ControllerWrapper usualController;
    public ControllerWrapper redirectController;
    public ControllerWrapper hideController;
    public ControllerWrapper flingController;
    public FlingSpotLight flingSpotlight;
    public bool isHidden = false;

    protected override void Start()
    {
        DontDestroyOnLoad(this);
        controller = usualController;
        base.Start();
        SetAttributes(myAttributes);
        flingController.SetController();
        flingController.myController.AssignModel(this);

        for (int i = 0; i < gainedActions.Count; i++)
        {
            gainedActions[i].SetAction();
        }

        if (controller is PlayerController)
        {
            (controller as PlayerController).StartFunction();
        }

        inv = inv.cloneInvTemplate();
        inv.initializeInventory(this);
    }

    void SetAttributes(CharacterAttributes attributes)
    {
        standardSpeed = attributes.walkSpeed;
        runSpeed = attributes.runSpeed;
        _sneakSpeed = attributes.sneakSpeed;
        strength = attributes.strength;
        currentSpeed = standardSpeed;

        gainedActions = new List<ActionWrapper>();
        for (int i = 0; i < attributes.innateActions.Length; i++)
        {
            gainedActions.Add(attributes.innateActions[i]);
            if (controller is PlayerController && !(controller as PlayerController).actionKeyLinks.Contains(attributes.innateActions[i].actionKey) && attributes.innateActions[i].actionKey)
            { 
            gainedActionKeyLinks.Add(attributes.innateActions[i].actionKey);
            }
        }
        GameObject myPlayerCharacter = Instantiate(attributes.characterModel, transform);
        myPlayerCharacter.GetComponent<Animator>().runtimeAnimatorController = attributes.animations;
        myPlayerCharacter.name = "characterModel";
        animator = myPlayerCharacter.GetComponent<Animator>();
    }
}
