public class HidingPlace : InteractableObject
{
    public bool full;

    public ActionWrapper hideAction;
    public ActionWrapper unhideAction;
    protected override void Start()
    {
        base.Start();
        requiredAction = hideAction;
    }
    
}
