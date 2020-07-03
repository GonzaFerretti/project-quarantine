using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Controller/AI/SearchAI")]
public class SearchAI : ControllerWrapper, IController
{
    public float goalDistance;
    public float minDistance;
    public Vector2 range;
    ModelPatrol _model;
    Vector3 _newGoal;
    NavMeshAgent _agent;

    public void AssignModel(Model model)
    {
        _model = model as ModelPatrol;
        _agent = model.GetComponent<NavMeshAgent>();
    }

    public void OnUpdate()
    {
        if (_agent.isStopped) _agent.isStopped = false;
        if (Vector3.Distance(_model.transform.position, _newGoal) > goalDistance)
           _agent.SetDestination(_newGoal);
        else GenerateNewGoal();
    }

    public void GenerateNewGoal()
    {
        RaycastHit hit;
        Vector3 _location = new Vector3(_model.transform.position.x + Random.Range(-range.x, range.x), _model.transform.position.y, _model.transform.position.z + Random.Range(-range.y, range.y));
        Physics.Raycast(_location, Vector3.down, out hit);
        if (hit.collider)
        {
            if (Vector3.Distance(_model.transform.position, hit.point) > minDistance)
            {
                _newGoal = hit.point;
            }
            else GenerateNewGoal();
        }
        else GenerateNewGoal();
    }

    public override void SetController()
    {
        myController = this;
    }

    public override ControllerWrapper Clone()
    {
        SearchAI clone = CreateInstance("SearchAI") as SearchAI;
        clone.range = range;
        clone.minDistance = minDistance;
        clone.goalDistance = goalDistance;
        return clone;
    }
}
