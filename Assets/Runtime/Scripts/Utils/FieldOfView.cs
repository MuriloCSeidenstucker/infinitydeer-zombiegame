using UnityEngine;

public static class FieldOfView
{
    public static bool CanSeeTarget(float radius, float angle, Transform origin, Transform target)
    {
        if (origin == null || target == null) return false;

        Vector3 toTarget = (target.position - origin.position);
        if (toTarget.sqrMagnitude > radius * radius) return false;

        float dot = Vector3.Dot(toTarget, origin.forward);
        if (dot < 0) return false;

        float cosHalfAngleToTarget = dot / (toTarget.magnitude * origin.forward.magnitude);
        float halfAngleToTarget = Mathf.Acos(cosHalfAngleToTarget) * Mathf.Rad2Deg;

        if (float.IsNaN(halfAngleToTarget)) return true;

        return halfAngleToTarget <= (angle * 0.5f);
    }
}
