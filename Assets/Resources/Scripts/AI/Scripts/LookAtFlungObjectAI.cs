using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Controller/AI/LookAtFlungObjectAI")]
public class LookAtFlungObjectAI : ControllerWrapper, IController, INeedTargetLocation
{
    public float targetThreshold;
    public float returnToIdleTime;
    public float lerpFloat;
    Vector3 _walkTarget;
    Vector3 _lookTarget;
    ModelPatrol _model;
    NavMeshAgent _agent;
    float _multiplier = 1;
    bool _activatedCoroutine;



    public void AssignModel(Model model)
    {
        _model = (model as ModelPatrol);
        _agent = model.GetComponent<NavMeshAgent>();
    }

    public override ControllerWrapper Clone()
    {
        LookAtFlungObjectAI clone = CreateInstance("LookAtFlungObjectAI") as LookAtFlungObjectAI;
        clone.targetThreshold = targetThreshold;
        clone.returnToIdleTime = returnToIdleTime;
        clone.lerpFloat = lerpFloat;
        return clone;
    }

    public void OnUpdate()
    {
        if (Distance())
        {
            if (_agent.pathStatus != NavMeshPathStatus.PathInvalid)
            {
                _multiplier = 1;
                _agent.SetDestination(_walkTarget);
            }
            else
            {
                Debug.Log("AAAAA");
                _multiplier += 1;
                _walkTarget = Vector3.Lerp(_lookTarget, _model.target.transform.position, lerpFloat * _multiplier);
            }
        }
        else
        {
            _model.animator.SetBool("isIdle", true);
            Vector3 clampForward = _model.transform.forward;
            Vector3 currentgoal = Vector3.RotateTowards(clampForward, (_lookTarget - _model.transform.position).normalized, _model.currentSpeed * Time.deltaTime, 0);
            _model.transform.rotation = Quaternion.LookRotation(currentgoal);
            if (!_activatedCoroutine)
            {
                _model.StartCoroutine(SwapController());
                _activatedCoroutine = true;
            }
        }
        //Debug.Log(Vector3.Distance(_model.transform.position, _walkTarget));
    }

    IEnumerator SwapController()
    {
        yield return new WaitForSeconds(returnToIdleTime);
        _model.GetComponent<NavMeshAgent>().SetDestination(_model.node.transform.position);
        _model.controller = _model.standardController;
    }

    bool Distance()
    {
        return Vector3.Distance(_model.transform.position, _walkTarget) > targetThreshold;
    }

    public override void SetController()
    {
        myController = this;
    }

    public INeedTargetLocation SetTarget(Vector3 target)
    {
        _lookTarget = target;
        _activatedCoroutine = false;
        _walkTarget = Vector3.Lerp(_lookTarget,_model.target.transform.position,lerpFloat);
        return this;
    }
}