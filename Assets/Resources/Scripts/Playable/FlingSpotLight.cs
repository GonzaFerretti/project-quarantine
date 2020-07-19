using UnityEngine;

public class FlingSpotLight : Model
{
    public ModelPlayable modelPlayable;
    Light _myLight;
    public Vector3 point;
    public ControllerWrapper controller;
    public GameObject noiseRangeIndicatorPrefab;
    public GameObject flingRangeIndicatorPrefab;
    public RangeIndicator noiseRangeIndicator;
    public RangeIndicator flingRangeIndicator;

    private void Awake()
    {
        modelPlayable = GetComponentInParent<ModelPlayable>();
        _myLight = GetComponent<Light>();
        _myLight.range = modelPlayable.myAttributes.strength;
        controller.SetController();
        controller.myController.AssignModel(this);
        SetIndicator();
    }

    private void Update()
    {
        controller.myController.OnUpdate();
    }
    
    void SetIndicator()
    {
        GameObject newRangeIndicator = Instantiate(noiseRangeIndicatorPrefab, null);
        newRangeIndicator.transform.rotation = noiseRangeIndicatorPrefab.transform.localRotation;
        noiseRangeIndicator = newRangeIndicator.GetComponent<RangeIndicator>();

        GameObject newFlingRange = Instantiate(flingRangeIndicatorPrefab, null);
        flingRangeIndicator = newFlingRange.GetComponent<RangeIndicator>();
        flingRangeIndicator.gameObject.transform.rotation = flingRangeIndicatorPrefab.transform.localRotation;
    }
}
