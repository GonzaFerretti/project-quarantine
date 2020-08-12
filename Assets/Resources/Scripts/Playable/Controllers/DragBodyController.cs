using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Controller/Player/DragBody Controller")]
public class DragBodyController : ControllerWrapper, IController
{
    ModelPlayable _model;
    public Transform guardPivotPoint;
    public Rigidbody handPivot;
    public Vector3 lastPos;
    public ActionKeyLinks[] actionLinks;

    public void AssignModel(Model model)
    {
        _model = model as ModelPlayable;
        handPivot = FindObjectOfType<AnimCheck>().rightArm;
    }

    public override ControllerWrapper Clone()
    {
        return new DragBodyController();
    }

    public void OnUpdate()
    {
        for (int i = 0; i < actionLinks.Length; i++)
        {
            if (Input.GetKey(actionLinks[i].key))
                if (actionLinks[i].myAction.action == null)
                    actionLinks[i].myAction.SetAction();
                else actionLinks[i].myAction.action.Do(_model);
        }
        _model.draggingEnemy.transform.right = -_model.transform.forward;
        _model.draggingEnemy.transform.position = _model.transform.position + _model.transform.rotation * _model.offsetDragEnemy;
        _model.draggingEnemy.ragdoll.transform.position = _model.transform.position + _model.transform.rotation * _model.offsetDragEnemy;
        /*
        if (lastPos != Vector3.one * Mathf.PI)
        {
            draggedGuard.transform.position += handPivot.position - lastPos;
        }
        guardPivotPoint.position = handPivot.position;
        lastPos = handPivot.position;*/

    }

    public override void SetController()
    {
        myController = this;
    }
}
