using UnityEngine;

public class ModelChar : MonoBehaviour
{
    public ControllerWrapper controller;
    public float currentSpeed;
    public float walkSpeed;
    public float runSpeed;
    public bool isDucking;
    public vaultStates vaultState = vaultStates.cantVault;
    public Transform lastVault;

    public enum vaultStates
    {
        cantVault = 0,
        isVaulting = 1,
        canVault = 2,
    }

    protected virtual void Start()
    {
        currentSpeed = walkSpeed;
        controller.SetController();
        controller.myController.AssignModel(this);
    }

    protected virtual void Update()
    {
        isDucking = false;
        controller.myController.OnUpdate();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "TerrainObsLow" && vaultState != vaultStates.canVault)
        {
            vaultState = vaultStates.canVault;
            lastVault = collision.transform;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Terrain" && vaultState == vaultStates.isVaulting)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            vaultState = vaultStates.cantVault;
            Physics.IgnoreCollision(lastVault.GetComponent<Collider>(), GetComponent<Collider>(), false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "TerrainObsLow" && vaultState == vaultStates.canVault)
        {
            vaultState = vaultStates.cantVault;
        }
    }
}