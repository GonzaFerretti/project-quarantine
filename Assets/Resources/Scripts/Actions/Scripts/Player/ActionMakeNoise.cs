public class ActionMakeNoise : IAction
{
    public void Do(Model m)
    {
        EventManager.TriggerLocEvent("Noise", m);
    }
}