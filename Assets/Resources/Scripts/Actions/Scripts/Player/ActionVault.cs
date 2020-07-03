using UnityEngine;

public class ActionVault : IAction
{
    private float vaultHeight;
    private float vaultCheckDistance;
    private float objectiveOffset;
    private float distanceModifierMin;
    public ActionVault(float _vaultHeight, float _vaultCheckDistance, float _objectiveOffset, float _distanceModifierMin)
    {
        vaultHeight = _vaultHeight;
        vaultCheckDistance = _vaultCheckDistance;
        objectiveOffset = _objectiveOffset;
        distanceModifierMin = _distanceModifierMin;
    }

    public void Do(Model m)
    {
        ModelHumanoid mh = m as ModelHumanoid;

        if (!mh.isVaulting)
        {
            Ray directionFacingRay = new Ray((m as ModelChar).GetRayCastOrigin(), m.transform.forward);
            RaycastHit[] directionHits = Physics.RaycastAll(directionFacingRay, vaultCheckDistance);
            bool checkVault = false;
            GameObject closestVault = null;
            foreach (RaycastHit Dhit in directionHits)
            {
                checkVault = Dhit.transform.gameObject.GetComponent<InteractableVaultWrapper>();
                if (checkVault)
                {
                    closestVault = Dhit.transform.gameObject;
                    break;
                }
            }
            if (checkVault)
            {
                mh.vaultHeight = vaultHeight;
                Collider obsCol = closestVault.GetComponent<Collider>();
                float vaultMaxDist = LongestPossibleRoute(obsCol) + vaultCheckDistance;
                Vector3 vaultCastStartPoint = (m as ModelChar).GetRayCastOrigin() + m.transform.forward * vaultMaxDist;
                //Throw a ray from the longest possible distance the object can be vaulted, but in the opposite direction the player is facing, so we can find where the other end should be.
                Ray endLocationRay = new Ray(vaultCastStartPoint, -(m.transform.forward));
                RaycastHit[] hits = Physics.RaycastAll(endLocationRay, vaultMaxDist);
                Vector3 objectivePoint = (m as ModelChar).GetRayCastOrigin();
                foreach (RaycastHit hit in hits)
                {
                    if (hit.transform.gameObject == closestVault)
                    {
                        objectivePoint = hit.point;
                        //Gizmos.DrawSphere(objectivePoint, 1);
                        break;
                    }
                }
                // Add a multiplier to modify the duration depending on how close the character is from the vault object.
                float distanceFromVaultCoefficient = Vector3.Distance((m as ModelChar).GetRayCastOrigin(), closestVault.transform.position)/vaultCheckDistance;
                float finalCoefficient = Mathf.Lerp(distanceModifierMin, 1, distanceModifierMin);
                // Add an offset equal to half the size of the collider so it doesn't rely on the physics to pop it out of the obstacle in an unnatural manner.
                float objectivePointOffset = m.GetComponent<Collider>().bounds.extents.x + objectiveOffset;
                //Debug.DrawLine((m as ModelChar).GetRayCastOrigin(), objectivePoint, Color.red, 3);
                mh.startVault(objectivePoint + m.transform.forward * objectivePointOffset,obsCol, finalCoefficient);
            }
        }
    }

    private float LongestPossibleRoute(Collider objectCol)
    {
        //Calculates the longest possible distance to traverse an object, its collider's bounding box hypotenuse.
        float sizeX = objectCol.bounds.size.x;
        float sizeZ = objectCol.bounds.size.z;
        float result = Mathf.Sqrt(Mathf.Pow(sizeX, 2) + Mathf.Pow(sizeZ, 2));
        return result;
    }
}
