public class ActionMakeNoise : BaseAction
{
    public override void Do(Model m)
    {
        if(m is IMakeNoise)
        {
            EventManager.TriggerLocEvent("Noise", m);
        }
    }
}