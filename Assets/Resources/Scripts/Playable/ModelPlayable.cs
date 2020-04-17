public class ModelPlayable : ModelChar
{
    public ActionWrapper[] availableActions;
    public PlayerController controller;
    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < availableActions.Length; i++)
        {
            availableActions[i].SetAction();
        }
        controller.SaveControllerKeys();
    }
}
