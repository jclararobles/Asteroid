using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShip : MonoBehaviour
{
    public float shipSpeed = 10f;
    
    private static PlayerShip _S;
    public static PlayerShip S
    {
        get
        {
            return _S;
        }
        private set
        {
            _S = value;
        }
    }

    private Rigidbody rigid;
    public GameObject bulletPrefab;

    void Awake()
    {
        if (_S == null)
        {
            _S = this;
        }

        rigid = GetComponent<Rigidbody>();
    }

    public void OnFire()
    {
        Fire();
    }

    public void OnMove(InputValue value)
    {
        Vector2 inputVelocity = value.Get<Vector2>();
        MoveShip(inputVelocity);
    }

    private void MoveShip(Vector2 velocity)
    {
        rigid.velocity = velocity * shipSpeed;
    }

    private void Fire()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        SpawnBullet(worldPosition);
    }

    private void SpawnBullet(Vector3 targetPosition)
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.transform.LookAt(targetPosition);
    }
}
