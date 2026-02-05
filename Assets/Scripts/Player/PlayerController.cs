using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private Rigidbody2D rb;
    private FacingDirection facingDir;
    private FacingDirection lastFacingDir;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Only the owner reads input
        if (!IsOwner) return;

        // Initialize Plyer scale/direction
        if (transform.localScale.x < 0)
        {
            facingDir = FacingDirection.Right;
            lastFacingDir = FacingDirection.Right;
        }
        else
        {
            facingDir = FacingDirection.Left;
            lastFacingDir = FacingDirection.Left;
        }

        // Read input
        moveInput = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed) moveInput.x -= 1;
            if (Keyboard.current.dKey.isPressed) moveInput.x += 1;
        }

        Vector2 moveDir = moveInput.normalized;

        // Only update flip when direction CHANGES
        if (moveInput.x < 0)
        {
            facingDir = FacingDirection.Left;
            if (lastFacingDir != facingDir) // Only send when changed
            {
                UpdateFlipServerRpc(FacingDirection.Left);
                lastFacingDir = facingDir;
            }
        }
        else if (moveInput.x > 0)
        {
            facingDir = FacingDirection.Right;
            if (lastFacingDir != facingDir) // Only send when changed
            {
                UpdateFlipServerRpc(FacingDirection.Right);
                lastFacingDir = facingDir;
            }
        }

        // Movements
        if (moveDir != Vector2.zero)
        {
            SendMovementServerRpc(moveDir);
        }
        else
        {
            SendMovementServerRpc(Vector2.zero);
        }

        // Shoot
        if (Keyboard.current.spaceKey.wasPressedThisFrame)  // One press = 1 bullet
        {
            ShootServerRpc(facingDir);
        }
    }

    [ServerRpc]
    void SendMovementServerRpc(Vector2 direction)
    {
        rb.linearVelocity = direction * moveSpeed;
    }

    [ServerRpc]
    void UpdateFlipServerRpc(FacingDirection direction)
    {
		// Update directly on server - NetworkTransform will sync it
        if (direction == FacingDirection.Left)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    [ServerRpc]
    void ShootServerRpc(FacingDirection FD)
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        // Set bullet direction based on player facing
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDirection(new Vector2((int)FD, 0));

        bullet.GetComponent<NetworkObject>().Spawn();
    }
}

enum FacingDirection
{
    Left = -1,
    Right = 1
}