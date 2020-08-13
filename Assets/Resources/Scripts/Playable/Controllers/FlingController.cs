using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Player/Fling Controller")]
public class FlingController : PlayerController
{
    ModelPlayable _mp;
    public FlingableItem rock;
    public FlingableItem bottle;

    public override void OnUpdate()
    {
        base.OnUpdate();
        CheckFlingables();
        //_mp.transform.forward = (new Vector3(_mp.flingSpotlight.transform.position.x, 0, _mp.flingSpotlight.transform.position.z) - new Vector3(_mp.transform.position.x, 0, _mp.transform.position.z)).normalized;
    }

    void CheckFlingables()
    {
        if (_mp == null) _mp = _model as ModelPlayable;
        List<FlingableItem> currentlySelectedFlingableItems = new List<FlingableItem>();
        List<FlingableItem> flingableItems = new List<FlingableItem>();

        for (int i = 0; i < _mp.inv.items.Count; i++)
        {
            if (_mp.inv.items[i] is FlingableItem)
            {
                flingableItems.Add(_mp.inv.items[i] as FlingableItem);
                if (_mp.inv.items[i] == _mp.inv.currentlySelectedItem)
                {
                currentlySelectedFlingableItems.Add(_mp.inv.items[i] as FlingableItem);
                }
            }
        }

        if (flingableItems.Count == 0)
        {
            if (_mp.inv.currentlySelectedItem == bottle)
            {
                _mp.inv.currentlySelectedItem = rock;
                _mp.inv.UpdateUI(rock, false, false, true, true);
                _mp.inv.UpdateUI(bottle, false, false, false, true);
            }
            else
            {
                _mp.inv.currentlySelectedItem = bottle;
                _mp.inv.UpdateUI(rock, false, false, false, true);
                _mp.inv.UpdateUI(bottle, false, false, true, true);
            }
            DisableFling();
        }
    }

    public void DisableFling()
    {
        (_mp.flingSpotlight.controller as FlingSpotlightController)._curveDrawer.Hide();
        _mp.flingSpotlight.noiseRangeIndicator.gameObject.SetActive(false);
        _mp.rangeIndicator.gameObject.SetActive(false);
        _mp.controller = _mp.usualController;
        //_mp.flingObject.trailren.enabled = false;
        _mp.flingSpotlight.gameObject.SetActive(false);
    }
}
