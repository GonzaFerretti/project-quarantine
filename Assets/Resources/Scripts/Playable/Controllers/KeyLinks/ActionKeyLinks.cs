using UnityEngine;

[CreateAssetMenu(menuName ="Controller/Action + Key Link")]
public class ActionKeyLinks : ScriptableObject
{
    public ActionWrapper action;
    public KeyCode key;

    public void ChangeKey(KeyCode newKey)
    {
        key = newKey;
    }
}
