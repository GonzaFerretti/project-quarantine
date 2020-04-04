public abstract class EnemyActionWrapper : ActionWrapper
{
    protected PatrolNode currentNode;
    public override void SetAction()
    {    }
    public abstract void SetParams(PatrolNode node);
}
