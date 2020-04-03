using UnityEngine;

public class ActionVault : IAction
{
    public void Do(ModelChar m)
    {
        if (m.vaultState == ModelChar.vaultStates.canVault)
        {
            Rigidbody rg = m.GetComponent<Rigidbody>();
            Collider col = m.GetComponent<Collider>();
            Vector3 vaultPos = m.lastVault.position;
            Vector3 vaultDirection = m.transform.forward + new Vector3(0,0.3f,0);
            Debug.DrawLine(m.transform.position, m.transform.position + vaultDirection * 1000);
            m.vaultState = ModelChar.vaultStates.isVaulting;
            Physics.IgnoreCollision(m.lastVault.GetComponent<Collider>(), col);
            rg.AddForce(vaultDirection * 25000f);
            Debug.Log(vaultDirection);
        }
    }
}
