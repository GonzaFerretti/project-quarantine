using UnityEngine;

public class FlingSpotLight : Model
{
    public ModelPlayable modelPlayable;
    Light _myLight;
    public Vector3 point;
    public ControllerWrapper controller;
    public GameObject rangeIndicatorPrefab;
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
        GameObject newRangeIndicator = Instantiate(rangeIndicatorPrefab, null);
        newRangeIndicator.transform.rotation = rangeIndicatorPrefab.transform.localRotation;
        noiseRangeIndicator = newRangeIndicator.GetComponent<RangeIndicator>();

        GameObject newFlingRange = Instantiate(rangeIndicatorPrefab, null);
        flingRangeIndicator = newFlingRange.GetComponent<RangeIndicator>();
        flingRangeIndicator.gameObject.transform.rotation = rangeIndicatorPrefab.transform.localRotation;
    }
}
