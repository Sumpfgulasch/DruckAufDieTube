using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using System.Threading.Tasks;

public class RelayMatchmaking : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _joinCodeText;
    [SerializeField] private TMP_InputField _joinInputField;

    [SerializeField] private Button _joinButton;
    [SerializeField] private Button _createButton;

    private UnityTransport _transport;
    private const int maxPlayers = 2;

    private async void Awake()
    {
        _transport = FindObjectOfType<UnityTransport>();
        await Authenticate();

        print("Authenticated");

    }
    private static async Task Authenticate() {

        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void CreateGame() 
    {
        _joinButton.enabled = false;
        _createButton.enabled = false;
       
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxPlayers);
        _joinCodeText.text = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        _transport.SetHostRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port, allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData);
        NetworkManager.Singleton.StartHost();


        _joinButton.enabled = true;
        _createButton.enabled = true;

    }
    public async void JoinGame()
    {
        _joinButton.enabled = false;
        _createButton.enabled = false;

        string joinCode = _joinInputField.text;

        Debug.Log(joinCode);
        Debug.Log(joinCode.Length);

        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(_joinInputField.text);
        _transport.SetClientRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port, allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);
        NetworkManager.Singleton.StartClient();

        _joinButton.enabled = true;
        _createButton.enabled = true;


    }


}
