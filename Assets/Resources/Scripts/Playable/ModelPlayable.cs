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
    public ControllerWrapper talkController;
    public ControllerWrapper lossController;
    public FlingSpotLight flingSpotlight;
    public bool isHidden = false;
    public Item currentlySelectedItem;

    public Dictionary<KeyCode, movementKeysDirection> movementKeys = new Dictionary<KeyCode, movementKeysDirection>();

    public GameObject baseFlingObject;

    protected override void Start()
    {
        DontDestroyOnLoad(this);
        controller = usualController;
        base.Start();
        SetAttributes(myAttributes);
        flingController.SetController();
        flingController.myController.AssignModel(this);
        if (!flingObject)
        {
            GameObject newFlingObject = Instantiate(baseFlingObject, transform.parent);
            newFlingObject.SetActive(false);
            flingObject = newFlingObject.GetComponent<FlingObject>();
            newFlingObject.gameObject.SetActive(false);
        }
        talkController.SetController();
        talkController.myController.AssignModel(this);

        for (int i = 0; i < gainedActions.Count; i++)
        {
            gainedActions[i].SetAction();
        }

        if (controller is PlayerController)
        {
            (controller as PlayerController).StartFunction();
        }

        lossController.SetController();
        lossController.myController.AssignModel(this);

        inv = inv.cloneInvTemplate();
        inv.initializeInventory(this);
        EventManager.SubscribeToEvent("Loss", LossBehavior);
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
        InitModel(ref animator, attributes.characterModel, attributes.animations);
    }

    void LossBehavior()
    {
        controller = lossController;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
