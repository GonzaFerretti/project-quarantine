using System.Collections;
using UnityEngine;

public class ActionBite : BaseAction
{
    float _timeRooted;

    public ActionBite(float timeRooted)
    {
        _timeRooted = timeRooted;
    }
    
    public override void Do(Model m)
    {
        (m as ModelDog).target.currentSpeed = 0;
        (m as ModelDog).target.animator.SetTrigger("trip");
        m.StartCoroutine(RegainSpeed((m as ModelDog).target));
    }

    IEnumerator RegainSpeed(ModelPlayable p)
    {
        yield return new WaitForSeconds(_timeRooted);
        p.currentSpeed = p.standardSpeed;
    }
}