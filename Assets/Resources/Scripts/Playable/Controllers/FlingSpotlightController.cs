using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Player/SpotlightController")]
public class FlingSpotlightController : ControllerWrapper, IController
{
    FlingSpotLight _model;
    public float height;
    public int groundLayerId;
    public float _upwardStrength;
    public CurveDrawer _curveDrawer;
    public GameObject go;

    public void AssignModel(Model model)
    {
        _model = model as FlingSpotLight;
        _curveDrawer = _model.GetComponentInChildren<CurveDrawer>();
    }

    public override ControllerWrapper Clone()
    {
        return CreateInstance("FlingSpotlightController") as FlingSpotlightController;
    }

    public void OnUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        Vector3 _modelDistVector = new Vector3(_model.transform.position.x, 0, _model.transform.position.z);
        ModelPlayable mp = _model.modelPlayable;
        Vector3 _playableDistVector = new Vector3(mp.transform.position.x, 0, mp.transform.position.z);

        //Debug.Log(Physics.RaycastAll(ray, float.MaxValue, 1 << groundLayerId).Length);
        RaycastHit[] groundHits = Physics.RaycastAll(ray, float.MaxValue, 1 << groundLayerId);
        if (groundHits.Length > 0)
        {
            float range =mp.strength;
            hit = groundHits[0];
            Vector3 mPos = mp.transform.position;
            Vector3 direction = new Vector3(hit.point.x - mPos.x,0, hit.point.z - mPos.z);
            Vector3 clampedDirection = Vector3.ClampMagnitude(direction, range);
            Vector3 finalPosition = new Vector3(mPos.x + clampedDirection.x, hit.point.y, mPos.z + clampedDirection.z);
            //mp.rangeIndicator.UpdatePosition(_model.transform.parent.position, range * 2);
            _model.noiseRangeIndicator.UpdatePosition(finalPosition);
            _model.transform.position = finalPosition + Vector3.up * height;
            UpdateTrajectory(_model.modelPlayable);
        }

        if (_model is FlingSpotLight) (_model as FlingSpotLight).point = hit.point;
    }

    public override void SetController()
    {
        myController = this;
    }

    public void UpdateTrajectory(Model m)
    {
        if (!go) {
            go = new GameObject();
            go.name = "test";
        }
        Vector3 dir;
        float strength;
        ModelPlayable mp = (m as ModelPlayable);
        Vector3 impactPoint = mp.GetComponentInChildren<FlingSpotLight>().point;
        Vector3 diff = new Vector3(mp.flingSpotlight.transform.position.x - mp.transform.position.x, 0, mp.flingSpotlight.transform.position.z - mp.transform.position.z);
        dir = diff.normalized;
        float dis = Vector3.Distance(mp.GetComponentInChildren<FlingSpotLight>().point, mp.GetRayCastOrigin() + Vector3.up * mp.standingBodyHeight);
        /*
        dir = new Vector3(mp.flingSpotlight.transform.position.x, 0, mp.flingSpotlight.transform.position.z).normalized;



        if (dis < mp.strength)
            strength = dis;
        else strength = mp.strength;
        
        Vector3 forceToApply = dir * strength + Vector3.up * _upwardStrength / 2;
        go.transform.position = (mp.transform.position + Vector3.up * mp.standingBodyHeight + forceToApply);
        _curveDrawer.UpdateDrawData(dir*10, mp.transform.position + Vector3.up * mp.standingBodyHeight, mp.GetComponentInChildren<FlingSpotLight>().point);
        Debug.DrawLine(mp.transform.position + Vector3.up * mp.standingBodyHeight, mp.transform.position + Vector3.up * mp.standingBodyHeight + forceToApply,Color.red,Time.deltaTime);
        */
        if (dis < mp.strength)
            strength = dis;
        else strength = mp.strength;
        _curveDrawer.UpdateDrawData(dir * strength + Vector3.up * _upwardStrength/2, mp.GetRayCastOrigin() + Vector3.up * mp.standingBodyHeight, impactPoint);
        _curveDrawer.Draw();
    }
}
