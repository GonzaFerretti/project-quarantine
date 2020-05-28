public class ActionMakeNoise : IAction
{
    public void Do(Model m)
    {
        if(m is IMakeNoise)
        EventManager.TriggerLocEvent("Noise", m);
    }
}