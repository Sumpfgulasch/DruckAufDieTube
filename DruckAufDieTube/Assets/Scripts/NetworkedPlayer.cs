using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class NetworkedPlayer : NetworkBehaviour
{
	// Start is called before the first frame update
	public override void OnNetworkSpawn()
	{
		base.OnNetworkDespawn();
	}

	public override void OnNetworkDespawn()
	{
		base.OnNetworkDespawn();
	}

	void Update()
    {
        if (!IsOwner) return;
        
    }
}
