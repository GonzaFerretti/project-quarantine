using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableVaultPreview : InteractablePreviewTextBase
{
    private float vaultHeight;
    private float vaultCheckDistance;
    private float objectiveOffset;
    private float distanceModifierMin;
    public InteractableVaultPreview(float _vaultHeight, float _vaultCheckDistance, float _objectiveOffset, float _distanceModifierMin)
    {
        vaultHeight = _vaultHeight;
        vaultCheckDistance = _vaultCheckDistance;
        objectiveOffset = _objectiveOffset;
        distanceModifierMin = _distanceModifierMin;
    }

    public override void Do(Model m, TextMeshProUGUI tb, InteractableObject obj, bool hasRequiredAction)
    {
        base.Do(m, tb, obj, hasRequiredAction);
        ModelHumanoid mh = m as ModelHumanoid;
        if (!mh.isVaulting)
        {
            mh.vaultHeight = vaultHeight;
            Collider obsCol = obj.GetComponent<Collider>();
            Vector3 rayCastOrigin = (m as ModelChar).GetRayCastOrigin();
            float vaultMaxDist = LongestPossibleRoute(obsCol) + vaultCheckDistance;
            Vector3 vaultCastStartPoint = (m as ModelChar).GetRayCastOrigin() + m.transform.forward * vaultMaxDist;
            //Throw a ray from the longest possible distance the object can be vaulted, but in the opposite direction the player is facing, so we can find where the other end should be.
            Ray endLocationRay = new Ray(vaultCastStartPoint, -(m.transform.forward));
            RaycastHit[] hits = Physics.RaycastAll(endLocationRay, vaultMaxDist);
            Vector3 objectivePoint = (m as ModelChar).GetRayCastOrigin();
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.gameObject == obj.gameObject)
                {
                    objectivePoint = hit.point;
                    break;
                }
            }
            float distanceFromVaultCoefficient = Vector3.Distance((m as ModelChar).GetRayCastOrigin(), obj.transform.position) / vaultCheckDistance;
            float finalCoefficient = Mathf.Lerp(distanceModifierMin, 1, distanceModifierMin);
            float objectivePointOffset = m.GetComponent<Collider>().bounds.extents.x + objectiveOffset;
            Vector3 finalPoint = objectivePoint + m.transform.forward * objectivePointOffset;
            (mh as ModelPlayable).vaultCurveDrawer.Hide();
            if (isVaultValid(rayCastOrigin, finalPoint, obj.gameObject))
            {
                (mh as ModelPlayable).vaultCurveDrawer.UpdateLine(mh.transform, mh.transform.position, finalPoint, mh.vaultHeight + (mh as ModelPlayable).standingBodyHeight);
            }

        }
    }

    private bool isVaultValid(Vector3 startPoint, Vector3 finalPoint, GameObject vault)
    {
        bool isThereAnObstacle = false;

        RaycastHit[] obstacleHits = Physics.RaycastAll(startPoint, (finalPoint - startPoint).normalized, (finalPoint - startPoint).magnitude);
        foreach (RaycastHit hit in obstacleHits)
        {
            if (hit.collider && hit.collider.gameObject != vault)
            {
                isThereAnObstacle = true;
                break;
            }
        }
        RaycastHit groundHit = new RaycastHit();
        bool hasGroundBeneath = false;
        Physics.Raycast(finalPoint, Vector3.down, out groundHit, float.MaxValue, 1 << LayerMask.NameToLayer("Ground"));
        if (groundHit.collider)
        {
            hasGroundBeneath = true;
        }
        return !(isThereAnObstacle) && hasGroundBeneath;
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
