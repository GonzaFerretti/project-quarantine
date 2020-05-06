using UnityEngine;

[CreateAssetMenu(menuName = "Controller/AI/FaceAI")]
public class SpycamFaceAI : ControllerWrapper, IController
{
    ModelSpycam _model;
    public void AssignModel(Model model)
    {
        _model = model as ModelSpycam;
    }

    public override ControllerWrapper Clone()
    {
        return CreateInstance("SpycamFaceAI") as SpycamFaceAI;
    }

    public void OnUpdate()
    {
        if (IsInRange(_model.leftForward, _model.rightForward))
        {
            Vector3 currentGoal = Vector3.RotateTowards(_model.transform.forward, (_model.target.transform.position - _model.transform.position).normalized, _model.currentSpeed * Time.deltaTime, 0);
            _model.transform.rotation = Quaternion.LookRotation(currentGoal);
        }
    }

    public bool IsInRange(Vector3 left, Vector3 right)
    {
        if (_model.transform.forward.x < right.x && _model.transform.forward.z < right.z)
            return true;
        else return false;
    }

    public override void SetController()
    {
        myController = this;
    }
}
