using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Player/Redirect Controller")]
public class RedirectController : ControllerWrapper, IController
{
    ModelPlayable _model;
    Vector3 frontGoal;
    Vector3 insideGoal;
    Vector3 forward;
    HidingPlace hidingPlace;

    public float distanceTreshold;
    public float animationDuration;
    bool isCoroutineHappening;

    public float enterAmplitude;
    public float enterSpeed;
    public float exitAmplitude;
    public float exitSpeed;

    public void AssignModel(Model model)
    {
        _model = model as ModelPlayable;
    }

    public override ControllerWrapper Clone()
    {
        RedirectController clone = CreateInstance("RedirectController") as RedirectController;
        clone.enterAmplitude = enterAmplitude;
        clone.exitAmplitude = exitAmplitude;
        clone.enterSpeed = enterSpeed;
        clone.exitSpeed = exitSpeed;
        return clone;
    }

    public void OnUpdate()
    {
        if (Vector3.Distance(_model.transform.position, frontGoal) > distanceTreshold)
        {
            Vector3 dir = (frontGoal - _model.transform.position).normalized;
            _model.transform.forward = dir - new Vector3(0, dir.y, 0);
            _model.transform.position += dir * _model.standardSpeed * Time.deltaTime;
        }
        else
        {
            if (hidingPlace.requiredAction == hidingPlace.hideAction)
            {
                (_model.hidingActionController as HidingActionController).amplitude = enterAmplitude;
                (_model.hidingActionController as HidingActionController).speed = enterSpeed;
                (_model.hidingActionController as HidingActionController).SetGoal(new Vector3(hidingPlace.transform.position.x, hidingPlace.GetComponent<BoxCollider>().bounds.max.y, hidingPlace.transform.position.z));
            }
            else
            {
                RaycastHit ray;
                Physics.Raycast(hidingPlace.transform.position + hidingPlace.transform.forward * 3, Vector3.down * 5, out ray);

                if (ray.collider)
                {
                    (_model.hidingActionController as HidingActionController).amplitude = exitAmplitude;
                    (_model.hidingActionController as HidingActionController).speed = exitSpeed;
                    (_model.hidingActionController as HidingActionController).SetGoal(ray.point);
                }
            }

            (_model.hidingActionController as HidingActionController).dur = 0;
            _model.GetComponent<CapsuleCollider>().enabled = false;
            _model.controller = _model.hidingActionController;
            _model.animator.Play("Jump");
            _model.StartCoroutine(EnterHidingPlace());
        }
    }

    IEnumerator EnterHidingPlace()
    {
        yield return new WaitForSeconds(animationDuration);
        {
            _model.transform.position = insideGoal;
            _model.GetComponent<CapsuleCollider>().enabled = true;

            HideController hc = ((_model as ModelPlayable).hideController as HideController);
            hc.AssignModel(_model);
            hc.SetController();
            _model.transform.forward = forward;

            if (hidingPlace.requiredAction == hidingPlace.hideAction)
            {
                hidingPlace.requiredAction = hidingPlace.unhideAction;
                _model.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                _model.GetComponent<Collider>().enabled = false;
                _model.controller = hc;
            }
            else
            {
                hidingPlace.requiredAction = hidingPlace.hideAction;
                _model.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                _model.GetComponent<Collider>().enabled = true;
                _model.controller = (_model as ModelPlayable).usualController;
            }

            hidingPlace.animator.SetBool("open", false);
            isCoroutineHappening = false;
        }
    }

    public RedirectController SetFrontGoal(Vector3 goal)
    {
        frontGoal = goal;
        return this;
    }

    public RedirectController SetInsideGoal(Vector3 goal)
    {
        insideGoal = goal;
        return this;
    }

    public RedirectController SetForward(Vector3 goal)
    {
        forward = goal;
        return this;
    }

    public RedirectController SetHidingPlace(HidingPlace goal)
    {
        hidingPlace = goal;
        return this;
    }

    public override void SetController()
    {
        myController = this;
    }
}