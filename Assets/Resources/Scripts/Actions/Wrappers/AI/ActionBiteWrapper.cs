using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Bite")]
public class ActionBiteWrapper : ActionWrapper
{
    public float timeRooted;

    public override void SetAction()
    {
        action = new ActionBite(timeRooted);
    }

    public ActionBiteWrapper Clone()
    {
        ActionBiteWrapper clone = CreateInstance("ActionBiteWrapper") as ActionBiteWrapper;
        clone.timeRooted = timeRooted;
        return clone;
    }
}