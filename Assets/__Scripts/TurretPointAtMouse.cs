using UnityEngine;

public class TurretPointAtMouse : MonoBehaviour
{
    private Vector3 mousePosition;

    void Update()
    {
        UpdateTurretDirection();
    }

    private void UpdateTurretDirection()
    {
        Vector3 targetPosition = GetMouseWorldPosition();
        RotateTurretTowards(targetPosition);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(screenPosition);
    }

    private void RotateTurretTowards(Vector3 targetPosition)
    {
        transform.LookAt(targetPosition, Vector3.back);
    }
}
