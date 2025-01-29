using UnityEngine;

public static class Vector3Extensions
{
    static public Vector3 ComponentDivide(this Vector3 v0, Vector3 v1)
    {
        Vector3 result = v0;
        DivideComponent(ref result.x, v0.x, v1.x);
        DivideComponent(ref result.y, v0.y, v1.y);
        DivideComponent(ref result.z, v0.z, v1.z);

        return result;
    }

    private static void DivideComponent(ref float componentResult, float componentValue, float divisor)
    {
        if (divisor != 0)
        {
            componentResult = componentValue / divisor;
        }
    }
}
