using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Controller/AI/Exit Indoors AI")]
public class ExitIndoorAI : ControllerWrapper, IController
{
    ModelPatrol _model;
    NavMeshAgent _agent;
    public float tresholdDistance;

    public void AssignModel(Model model)
    {
        _model = model as ModelPatrol;
        _agent = _model.GetComponent<NavMeshAgent>();
    }

    public override ControllerWrapper Clone()
    {
        ExitIndoorAI newExitDoorAI = CreateInstance("ExitIndoorAI") as ExitIndoorAI;
        newExitDoorAI.tresholdDistance = tresholdDistance;
        return newExitDoorAI;
    }

    public void OnUpdate()
    {
        //if (Vector3.Distance(_model.transform.position, _model.spawner.transform.position-_model.spawner.transform.forward) < tresholdDistance)
        //  Destroy(_model.transform);
        //else 
        _agent.SetDestination(_model.spawner.transform.position);
    }

    public override void SetController()
    {
        myController = this;
    }
}
