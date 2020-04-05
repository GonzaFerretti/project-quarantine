using UnityEngine;

public class ActionVault : IAction
{
    private float vaultDuration;
    private float vaultHeight;
    public ActionVault(float _vaultDuration, float _vaultHeight)
    {
        vaultDuration = _vaultDuration;
        vaultHeight = _vaultHeight;
    }

    public void Do(ModelChar m)
    {
        if (m.vaultState == ModelChar.vaultStates.canVault)
        {
            m.vaultDuration = vaultDuration;
            m.vaultHeight = vaultHeight;
            Collider obsCol = m.lastVault.GetComponent<Collider>();
            float vaultMaxDist = LongestPossibleRoute(obsCol);
            Vector3 vaultCastStartPoint = m.transform.position + m.transform.forward * vaultMaxDist;
            //Throw a ray from the longest possible distance the object can be vaulted, but in the opposite direction the player is facing, so we can find where the other end should be.
            Ray endLocationRay = new Ray(vaultCastStartPoint, -(m.transform.forward));
            RaycastHit[] hits = Physics.RaycastAll(endLocationRay,vaultMaxDist);
            Vector3 objectivePoint = m.transform.position;
            foreach(RaycastHit hit in hits)
            {
                if (hit.transform.gameObject == m.lastVault.gameObject)
                {
                    objectivePoint = hit.point;
                    break;
                }
            }
            // Add an offset equal to half the size of the collider so it doesn't rely on the physics to pop it out of the obstacle in an unnatural manner.
            float objectivePointOffset = m.GetComponent<Collider>().bounds.extents.x;
            Debug.DrawLine(m.transform.position, objectivePoint, Color.red, 3);
            m.startVault(objectivePoint + m.transform.forward * objectivePointOffset);
        }
    }

    private float LongestPossibleRoute(Collider objectCol)
    {
        //Calculates the longest possible distance to traverse an object, its collider's bounding box hypotenuse.
        float sizeX = objectCol.bounds.size.x;
        float sizeZ = objectCol.bounds.size.z;
        float result = Mathf.Sqrt(Mathf.Pow(sizeX,2) + Mathf.Pow(sizeZ, 2));
        return result;
    }
}
