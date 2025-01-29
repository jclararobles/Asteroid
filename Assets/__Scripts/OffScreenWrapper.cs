using UnityEngine;

public class OffScreenWrapper : MonoBehaviour
{
    private void OnTriggerExit(Collider collider)
    {
        if (!isActiveAndEnabled) 
        {
            return;
        }

        var screenBounds = collider.GetComponent<ScreenBounds>();
        if (screenBounds == null)
        {
            return;
        }

        PerformScreenWrap(screenBounds);
    }

    private void PerformScreenWrap(ScreenBounds screenBounds)
    {
        Vector3 localPosition = screenBounds.transform.InverseTransformPoint(transform.position);

        if (Mathf.Abs(localPosition.x) > 0.5f)
        {
            localPosition.x = -localPosition.x;
        }

        if (Mathf.Abs(localPosition.y) > 0.5f)
        {
            localPosition.y = -localPosition.y;
        }

        transform.position = screenBounds.transform.TransformPoint(localPosition);
    }
}
