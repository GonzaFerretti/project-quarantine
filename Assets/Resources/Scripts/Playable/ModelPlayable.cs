using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModelPlayable : ModelHumanoid
{
    float _sneakSpeed;
    public float bodyHeight;
    public float standingBodyHeight;
    public float duckingBodyHeight;
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
    public Firecracker baseFirecracker;
    public Firecracker firecracker;
    Rigidbody _rb;

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
        EventManager.SubscribeToLocationEvent("EnterLocation", LocRepositioning);

        standingBodyHeight = GetComponent<CapsuleCollider>().height / 3;
        duckingBodyHeight = standingBodyHeight / 2;
        bodyHeight = standingBodyHeight;
        _rb = GetComponent<Rigidbody>();
        InitFlingObsChecker();
    }

    public List<GameObject> GetFlingObstacleObjects()
    {
        return FlingObstacleChecker.GetCloseObstacles();
    }

    void LocRepositioning(Model m)
    {
        if (!(m is Door)) return;
        Door door = m as Door;
        transform.position = door.targetLocation;
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        StartCoroutine(Unfreeze());
    }

    IEnumerator Unfreeze()
    {
        yield return new WaitForFixedUpdate();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void CheckFlingObjectExistsOrCreate()
    {
        if (!flingObject)
        {
            GameObject newFlingObject = Instantiate(baseFlingObject, transform.parent);
            newFlingObject.SetActive(false);
            flingObject = newFlingObject.GetComponent<FlingObject>();
            newFlingObject.gameObject.SetActive(false);
        }
    }

    void SetAttributes(CharacterAttributes attributes)
    {
        standardSpeed = attributes.walkSpeed;
        runSpeed = attributes.runSpeed;
        _sneakSpeed = attributes.sneakSpeed;
        strength = attributes.strength;
        currentSpeed = standardSpeed;
        //bodyHeight = attributes.bodyheight;
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
        ActionFirecrackerWrapper gotFirecracker = null;

        List<Item> firecrackers = new List<Item>();

        for (int j = 0; j < inv.items.Count; j++)
        {
            if (inv.items[j].allowingActions.Count > 0 && inv.items[j].allowingActions[0] is ActionFirecrackerWrapper)
                firecrackers.Add(inv.items[j]);
        }

        if (firecrackers.Count > 0) inv.items.Remove(firecrackers[0]);


        for (int i = 0; i < gainedActions.Count; i++)
        {
            if (gainedActions[i] is ActionFirecrackerWrapper)
            {
                gotFirecracker = gainedActions[i] as ActionFirecrackerWrapper;

                if(firecrackers.Count == 1)
                gainedActions.Remove(gainedActions[i]);
                break;
            }
        }

        if (gotFirecracker != null)
        {
            gotFirecracker.SetAction();
            gotFirecracker.action.Do(this);
            gotFirecracker = null;
        }
        else
        {
            controller = lossController;
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            EventManager.UnsubscribeToLocationEvent("EnterLocation", LocRepositioning);
            EventManager.UnsubscribeToEvent("Loss", LossBehavior);
        }
    }
}