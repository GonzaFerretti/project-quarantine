using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ModelPlayable : ModelHumanoid, IMakeNoise
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
    public ControllerWrapper hidingActionController;
    public ControllerWrapper flingController;
    public ControllerWrapper talkController;
    public ControllerWrapper lossController;
    public ControllerWrapper whistleController;
    public FlingSpotLight flingSpotlight;
    public RangeIndicator rangeIndicator;
    public GameObject rangeIndicatorPrefab;
    public float whistleStrength;
    public bool isHidden = false;
    public Item currentlySelectedItem;
    public CameraMovement mainCam;
    public CameraMovement secondaryCamera;

    public Dictionary<KeyCode, movementKeysDirection> movementKeys = new Dictionary<KeyCode, movementKeysDirection>();

    public GameObject baseFlingObject;
    public Firecracker baseFirecracker;
    public Firecracker firecracker;
    Rigidbody _rb;

    public float GetNoiseValue()
    {
        return whistleStrength;
    }

    public void InitIndicator()
    {
        GameObject newRangeIndicator = Instantiate(rangeIndicatorPrefab, transform);
        newRangeIndicator.transform.rotation = rangeIndicatorPrefab.transform.localRotation;
        newRangeIndicator.transform.localPosition = Vector3.zero;
        rangeIndicator = newRangeIndicator.GetComponent<RangeIndicator>();
        rangeIndicator.gameObject.SetActive(false);
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        StartCoroutine(FindCameras());
    }

    protected override void Start()
    {
        DontDestroyOnLoad(this);
        InitIndicator();
        controller = usualController;
        base.Start();
        SetAttributes(myAttributes);

        SetController(flingController);
        SetController(talkController);
        SetController(lossController);
        SetController(redirectController);
        SetController(hideController);
        SetController(hidingActionController);

        CheckFlingObjectExistsOrCreate();

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
        EventManager.SubscribeToEvent("Loss", LossBehavior);
        EventManager.SubscribeToLocationEvent("EnterLocation", LocRepositioning);

        standingBodyHeight = GetComponent<CapsuleCollider>().height / 3;
        duckingBodyHeight = standingBodyHeight / 2;
        bodyHeight = standingBodyHeight;
        _rb = GetComponent<Rigidbody>();
        InitFlingObsChecker();
        SceneManager.activeSceneChanged += ChangedActiveScene;
        mainCam = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        secondaryCamera = GameObject.Find("minimapCamera").GetComponent<CameraMovement>();
    }

    public IEnumerator FindCameras()
    {
        yield return new WaitForEndOfFrame();
        mainCam = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        secondaryCamera = GameObject.Find("minimapCamera").GetComponent<CameraMovement>();
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

    void SetController(ControllerWrapper c)
    {
        c.SetController();
        c.myController.AssignModel(this);
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
        whistleStrength = attributes.whistleStrength;
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