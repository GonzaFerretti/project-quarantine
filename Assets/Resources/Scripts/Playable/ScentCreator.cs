using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScentCreator : MonoBehaviour
{
    ModelPlayable _model;
    public int scentTrailAmount;
    public float scentMovementTimer;
    public ModelScentTrail scent;
    public List<ModelScentTrail> scentObjects;
    int _currentScent;

    void Start()
    {
        _model = GetComponentInParent<ModelPlayable>();
        scentObjects = new List<ModelScentTrail>();
        for (int i = 0; i < scentTrailAmount; i++)
        {
            ModelScentTrail newScent = Instantiate(scent);
            newScent.transform.position = _model.transform.position;
            DontDestroyOnLoad(newScent);
            scentObjects.Add(newScent);
        }
        _currentScent = scentObjects.Count-1;
        StartCoroutine(ScentMovement());
    }

    IEnumerator ScentMovement()
    {
        yield return new WaitForSeconds(scentMovementTimer);
        if (scentObjects[_currentScent] != null)
        {
            scentObjects[_currentScent].transform.position = _model.transform.position;
            if (_currentScent > 0)
                _currentScent--;
            else _currentScent = scentObjects.Count - 1;

            StartCoroutine(ScentMovement());
        }
    }
}