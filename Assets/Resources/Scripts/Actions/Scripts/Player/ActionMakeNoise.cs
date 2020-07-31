public class ActionMakeNoise : IAction
{
    public virtual void Do(Model m)
    {
        if(m is IMakeNoise)
        {
            EventManager.TriggerLocEvent("Noise", m);
        }
    }
}