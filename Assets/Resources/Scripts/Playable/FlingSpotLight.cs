using UnityEngine;

public class FlingSpotLight : Model
{
    public ModelPlayable modelPlayable;
    Light _myLight;
    public Vector3 point;
    public ControllerWrapper controller;
    public GameObject noiseRangeIndicatorPrefab;
    public RangeIndicator noiseRangeIndicator;

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
    }

    public void SetIndicatorState(bool isEnabled)
    {
        gameObject.SetActive(isEnabled);
        if (isEnabled)
        { 
        noiseRangeIndicator.UpdateSize(modelPlayable.baseFlingObject.GetComponent<FlingObject>().noiseValue);
        modelPlayable.rangeIndicator.UpdateSize(modelPlayable.strength * 2 / modelPlayable.transform.localScale.x);
        }
        noiseRangeIndicator.gameObject.SetActive(isEnabled);
        modelPlayable.rangeIndicator.gameObject.SetActive(isEnabled);
    }
}
