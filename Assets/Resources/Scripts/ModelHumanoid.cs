using UnityEngine;

public class ModelHumanoid : ModelChar
{
    public float runSpeed;
    public bool isDucking;
    public bool isVaulting = false;
    private Vector3 vaultObjetive, vaultStartPoint;
    float _vaultDuration;
    private float vaultStart;
    public float vaultDuration, vaultHeight;
    public Collider lastVault;
    public GameObject nearbyObject;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<ItemWrapper>())
        {
            nearbyObject = collider.gameObject;
            nearbyObject.GetComponent<ItemWrapper>().activateParticles();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<ItemWrapper>())
        {
            nearbyObject.GetComponent<ItemWrapper>().disableParticles();
            nearbyObject = null;
        }
    }

    protected override void Update()
    {
        base.Update();
        if (isVaulting)
        {
            moveTowardsVaultPoint();
        }
    }

    private void moveTowardsVaultPoint()
    {
        float vaultProgress = (Time.time - vaultStart) / (_vaultDuration);
        if (vaultProgress < 1)
        {
            //The arc movement is described seperately lerping the current progress between 0 and 2PI on a Sine-based function. The distance travelled is lerped between the starting point and the objective.
            float vaultHeightIndex = Mathf.Lerp(0, Mathf.PI, vaultProgress);
            float yPosition = vaultStartPoint.y + Mathf.Sin(vaultHeightIndex) * vaultHeightIndex * vaultHeight;
            Vector3 XandZposition = Vector3.Lerp(vaultStartPoint, vaultObjetive, vaultProgress);
            transform.position = new Vector3(XandZposition.x, yPosition, XandZposition.z);
        }
        else
        {
            transform.position = vaultObjetive;
            isVaulting = false;
            animator.SetBool("vault", false);
            Physics.IgnoreCollision(lastVault, GetComponent<Collider>(), false);
        }
    }

    public void startVault(Vector3 objetive,Collider vaultCollider)
    {
        vaultStart = Time.time;
        lastVault = vaultCollider;
        vaultStartPoint = transform.position;
        vaultObjetive = objetive;
        isVaulting = true;
        _vaultDuration = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(objetive.x, objetive.z)) / currentSpeed;
        Physics.IgnoreCollision(lastVault, GetComponent<Collider>(), true);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        animator.SetBool("vault",true);
    }
}

