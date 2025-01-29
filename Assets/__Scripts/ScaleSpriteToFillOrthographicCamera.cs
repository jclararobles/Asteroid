using UnityEngine;

public class ScaleSpriteToFillOrthographicCamera : MonoBehaviour
{
    public Camera camToMatch;

    void Start()
    {
        if (camToMatch == null || !camToMatch.orthographic)
        {
            return;
        }

        ResetScale();
        AdjustScaleToCamera();
    }

    private void ResetScale()
    {
        transform.localScale = Vector3.one;
    }

    private void AdjustScaleToCamera()
    {
        Renderer renderer = GetComponent<Renderer>();
        Vector3 spriteSize = renderer.bounds.size;

        Vector3 cameraSize = spriteSize;
        cameraSize.y = camToMatch.orthographicSize * 2;
        cameraSize.x = cameraSize.y * camToMatch.aspect;

        Vector3 finalScale = cameraSize.ComponentDivide(spriteSize);

        transform.localScale = finalScale;
    }
}
