using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModelChar : MonoBehaviour
{
    public ControllerWrapper controller;
    public float currentSpeed;
    public float walkSpeed;
    public float runSpeed;
    public bool isDucking;
    public vaultStates vaultState = vaultStates.cantVault;
    private Vector3 vaultObjetive, vaultStartPoint;
    private float vaultStart;
    public float vaultDuration, vaultHeight;
    public Transform lastVault;
    public List<KeyCode> movementKeys = new List<KeyCode>();

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
        if (vaultState == vaultStates.isVaulting)
        {
            moveTowardsVaultPoint();
        }
    }


    private void moveTowardsVaultPoint()
    {
        float vaultProgress = (Time.time - vaultStart) / (vaultDuration);
        if (vaultProgress < 1)
        {
            //The arc movement is described seperately lerping the current progress between 0 and 2PI on a Sine function. The distance travelled is lerped between the starting point and the objective.
            float vaultHeightIndex = Mathf.Lerp(0, Mathf.PI, vaultProgress);
            float yPosition = vaultStartPoint.y + Mathf.Sin(vaultHeightIndex) * vaultHeight;
            Vector3 XandZposition = Vector3.Lerp(vaultStartPoint, vaultObjetive, vaultProgress);
            transform.position = new Vector3(XandZposition.x, yPosition, XandZposition.z);
        }
        else
        {
            transform.position = vaultObjetive;
            vaultState = vaultStates.cantVault;
            Physics.IgnoreCollision(lastVault.GetComponent<Collider>(), GetComponent<Collider>(), false);
        }
    }

    public void startVault(Vector3 objetive)
    {
        vaultStart = Time.time;
        vaultStartPoint = transform.position;
        vaultObjetive = objetive;
        vaultState = vaultStates.isVaulting;
        Physics.IgnoreCollision(lastVault.GetComponent<Collider>(), GetComponent<Collider>(), true);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "TerrainObsLow" && vaultState != vaultStates.canVault)
        {
            vaultState = vaultStates.canVault;
            lastVault = collision.transform;
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