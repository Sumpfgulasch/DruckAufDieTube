using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class NetworkedPlayer : NetworkBehaviour
{
	// thes update outomatically on both sides when changes, no need for RPC
	private NetworkVariable<DateTime> localStartTime = new NetworkVariable<DateTime>(DateTime.MinValue, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
	private NetworkVariable<DateTime> localEndTime = new NetworkVariable<DateTime>(DateTime.MinValue, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

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

		// here comes the code that happen only locally on the client sid

	}


	// please use this funtion when the game is started atbthe clients side
	public void LogLocalStartTimeToServer(DateTime time) 
	{
		localStartTime.Value = time;

	}


	//please use this fuction when the end button is clicked by the player
	public void LogLocalEndTimeToServer(DateTime time)
	{
		localEndTime.Value = time;
	}

	public void LogButtonSpwenedToSever()
	{
		
	}

	[ServerRpc] // called by the client to work on the server 
	private void StartGameGlobalServerRpc( ServerRpcParams serverRpcParams) 
	{
		// peopably we need another button "START" in the lobby and then call that 
		// should probably call the StartGameLocalClientRpc(); to tell clients how to react


		// will be called on server whenever any client calls that
		// use serverRpcParams.Receive.SenderClientId to check who has send it

		// KEIN PLAN WIE MAN MIT RPCS callbacks macht
	}



	[ClientRpc] // called from the server to work on the client
	private void StartGameLocalClientRpc(ClientRpcParams clientRpcparams) { 
		// here the clients do whatever they do when the game is started
		// start the counter
		// turn on the UI
	}

	[ServerRpc]
	private void RegisterGameEndedServerRpc() { }

	[ClientRpc]
	private void EndGameLocallyClientRpc() {
		// shoudl fire when both player have hit the button or when they run out of time}
		// transition to summ up screen show results 

		// KEIN PLAN NOCH WIE MAN DATA VON ANDEREN SPIELER NIMMT 

	}

	[ServerRpc]
	private void RegisterSpawnNewButtonServerRPC() { }

	[ClientRpc]
	private void SpawnNewButtonClientRPC()
	{
		// NEED CKECKING WHICH PLAYER DID WHAT WITH PLAYER IDs ETC.... EHM. PROPBABLY WILL HAVE TO SPLIT THE SERVER CODE FROM THIS PLAYER CODE LATER
	}



}
