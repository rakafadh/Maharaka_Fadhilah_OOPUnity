using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed;
    [SerializeField] private Vector2 timeToFullSpeed;
    [SerializeField] private Vector2 timeToStop;
    [SerializeField] private Vector2 stopClamp;
    [SerializeField] private Vector2 screenBoundOffset = Vector2.zero;

    private float minX, maxX, minY, maxY;
    private float objectWidth, objectHeight;
    private Vector2 moveDirection, moveVelocity, moveFriction, stopFriction;

    private Rigidbody2D rb;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

        moveVelocity = Vector2.zero;
        moveFriction = new Vector2(
            -2f * maxSpeed.x / (timeToFullSpeed.x * timeToFullSpeed.x),
            -2f * maxSpeed.y / (timeToFullSpeed.y * timeToFullSpeed.y)
        );
        stopFriction = new Vector2(
            -2f * maxSpeed.x / (timeToStop.x * timeToStop.x),
            -2f * maxSpeed.y / (timeToStop.y * timeToStop.y)
        );

        CalculateScreenBounds();
    }

    void CalculateScreenBounds()
    {
        if (mainCamera == null) return;

        float vertExtent = mainCamera.orthographicSize;
        float horizExtent = vertExtent * Screen.width / Screen.height;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            objectWidth = spriteRenderer.bounds.size.x / 2;
            objectHeight = spriteRenderer.bounds.size.y / 2;
        }

        float padding = 0.5f; 
        minX = -horizExtent + objectWidth + screenBoundOffset.x + padding;
        maxX = horizExtent - objectWidth - screenBoundOffset.x - padding;
        minY = -vertExtent + objectHeight + screenBoundOffset.y + padding;
        maxY = vertExtent - objectHeight - screenBoundOffset.y - padding;
    }

    void FixedUpdate()
    {
        Move();
        MoveBound();  // Memastikan batas pergerakan dipanggil di sini
    }

    public void Move()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector2 friction = GetFriction();

        HandleHorizontalMovement(friction.x);
        HandleVerticalMovement(friction.y);

        rb.velocity = moveVelocity; 

        MoveBound(); // Mengatur posisi sesuai batas layar setiap kali ada pergerakan
    }

    private void HandleHorizontalMovement(float frictionX)
    {
        if (moveDirection.x != 0)
        {
            float accelerationX = (2f * maxSpeed.x / timeToFullSpeed.x);
            moveVelocity.x += moveDirection.x * accelerationX * Time.fixedDeltaTime;
            moveVelocity.x = Mathf.Clamp(moveVelocity.x, -maxSpeed.x, maxSpeed.x);
        }
        else
        {
            ApplyFrictionToAxis(ref moveVelocity.x, stopClamp.x, frictionX);
        }
    }

    private void HandleVerticalMovement(float frictionY)
    {
        if (moveDirection.y != 0)
        {
            float accelerationY = (2f * maxSpeed.y / timeToFullSpeed.y);
            moveVelocity.y += moveDirection.y * accelerationY * Time.fixedDeltaTime;
            moveVelocity.y = Mathf.Clamp(moveVelocity.y, -maxSpeed.y, maxSpeed.y);
        }
        else
        {
            ApplyFrictionToAxis(ref moveVelocity.y, stopClamp.y, frictionY);
        }
    }

    private void ApplyFrictionToAxis(ref float velocity, float stopThreshold, float friction)
    {
        float currentSpeed = Mathf.Abs(velocity);
        if (currentSpeed < stopThreshold)
        {
            velocity = 0;
        }
        else
        {
            velocity += Mathf.Sign(velocity) * friction * Time.fixedDeltaTime;
        }
    }

    public void MoveBound()
    {
        Vector3 viewPos = transform.position;

        if (viewPos.x < minX)
        {
            viewPos.x = minX;
            moveVelocity.x = 0;
        }
        else if (viewPos.x > maxX)
        {
            viewPos.x = maxX;
            moveVelocity.x = 0;
        }

        if (viewPos.y < minY)
        {
            viewPos.y = minY;
            moveVelocity.y = 0;
        }
        else if (viewPos.y > maxY)
        {
            viewPos.y = maxY;
            moveVelocity.y = 0;
        }

        transform.position = viewPos;
    }

    public Vector2 GetFriction()
    {
        return new Vector2(
            moveDirection.x != 0 ? moveFriction.x : stopFriction.x,
            moveDirection.y != 0 ? moveFriction.y : stopFriction.y
        );
    }

    void OnValidate()
    {
        if (Application.isPlaying)
        {
            CalculateScreenBounds();
        }
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0.01f;
    }
}
