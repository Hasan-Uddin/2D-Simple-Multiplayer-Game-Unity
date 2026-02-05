using Unity.Netcode;
using UnityEngine;

public class SpawnManager : NetworkBehaviour
{
    public Transform spawnPointA;
    public Transform spawnPointB;

    private int playerCount = 0;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return; // only server can subscribe

        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }
	
	public override void OnNetworkDespawn()
	{
		if (!IsServer) return;

		NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
	}

    private void OnClientConnected(ulong clientId)
    {
        playerCount++;

        NetworkObject player =
            NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;

        if (playerCount == 1)
        {
            player.transform.position = spawnPointA.position;
        }
        else if (playerCount == 2)
        {
            player.transform.position = spawnPointB.position;
        }
    }
}