using UnityEngine;

public class Bullet : MonoBehaviour
{
    private static Transform bulletAnchor;
    
    private static Transform BulletAnchor
    {
        get
        {
            if (bulletAnchor == null)
            {
                var anchorObject = new GameObject("BulletAnchor");
                bulletAnchor = anchorObject.transform;
            }
            return bulletAnchor;
        }
    }

    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 2f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        SetBulletParent();
        ScheduleDestruction();
        SetBulletVelocity();
    }

    private void SetBulletParent()
    {
        transform.SetParent(BulletAnchor, true);
    }

    private void ScheduleDestruction()
    {
        Invoke(nameof(DestroyBullet), lifetime);
    }

    private void SetBulletVelocity()
    {
        rb.velocity = transform.forward * speed;
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
