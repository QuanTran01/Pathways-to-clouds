using UnityEngine;

public class SHARK : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float patrolRange = 5f;
    public float changeDirectionInterval = 2f;
    public Transform player;
    public bool isChestOpened = false;

    public float waterMinX; 
    public float waterMaxX; 
    public float waterMinY; 
    public float waterMaxY; 

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float changeDirectionTimer;

    void Start()
    {
        initialPosition = transform.position;
        SetNewRandomTarget();
        changeDirectionTimer = changeDirectionInterval;
    }

    void Update()
    {
        if (isChestOpened)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        changeDirectionTimer -= Time.deltaTime;

        if (changeDirectionTimer <= 0f || !IsInWater(targetPosition))
        {
            SetNewRandomTarget();
            changeDirectionTimer = changeDirectionInterval;
        }

        MoveTowards(targetPosition, patrolSpeed);
    }

    void SetNewRandomTarget()
    {
        float randomX = Random.Range(-patrolRange, patrolRange);
        float randomY = Random.Range(-patrolRange, patrolRange);
        targetPosition = new Vector3(initialPosition.x + randomX, initialPosition.y + randomY, initialPosition.z);

        targetPosition = ClampToWaterBounds(targetPosition);
    }

    void ChasePlayer()
    {
        if (IsInWater(player.position))
        {
            MoveTowards(player.position, chaseSpeed);
        }
        else
        {
            SetNewRandomTarget();
        }
    }

    void MoveTowards(Vector3 target, float speed)
    {
        Vector3 direction = target - transform.position;
        direction.z = 0;
        direction.Normalize();

        Vector3 nextPosition = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.position = ClampToWaterBounds(nextPosition);

        UpdateRotation(direction);
    }
    bool IsInWater(Vector3 position)
    {
        return position.x >= waterMinX && position.x <= waterMaxX && position.y >= waterMinY && position.y <= waterMaxY;
    }
    Vector3 ClampToWaterBounds(Vector3 position)
    {
        float clampedX = Mathf.Clamp(position.x, waterMinX, waterMaxX);
        float clampedY = Mathf.Clamp(position.y, waterMinY, waterMaxY);
        return new Vector3(clampedX, clampedY, position.z);
    }
    void UpdateRotation(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (angle > 90 || angle < -90)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                angle += 180;
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Saw"))
        {
            Destroy(gameObject);
        }
    }
}
