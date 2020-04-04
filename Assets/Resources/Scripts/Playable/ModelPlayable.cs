public class ModelPlayable : ModelChar
{
    public ActionWrapper[] availableActions;

    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < availableActions.Length; i++)
        {
            availableActions[i].SetAction();
        }
    }
}
