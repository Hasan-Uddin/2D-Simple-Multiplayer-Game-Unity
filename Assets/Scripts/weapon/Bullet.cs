using Unity.Netcode;
using UnityEngine;

public sealed class Bullet : NetworkBehaviour
{
    public float speed = 100f;
    public float lifeTime = 1f;
    private Vector2 direction = Vector2.right; // Store the direction the bullet should travel

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            Invoke(nameof(DestroyBullet), lifeTime);
        }
    }

    // Called by server to set bullet direction
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }


    void FixedUpdate()
    {
        if (!IsServer) return;

        transform.Translate(direction * speed * Time.fixedDeltaTime, Space.World);
    }

    void DestroyBullet()
    {
        GetComponent<NetworkObject>().Despawn();
    }
}
