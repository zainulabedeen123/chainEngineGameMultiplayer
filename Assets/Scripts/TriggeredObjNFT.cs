using ChainEngine.Shared.Exceptions;
using ChainEngine.Actions;
using ChainEngine.Model;
using ChainEngine.Types;
using ChainEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;

public class TriggeredObjNFT : MonoBehaviourPunCallbacks
{
    private ChainEngineSDK client;
    public GameObject Sword, Pistol;
    public Text WalletAdressPlayer;
    private void Start()
    {
        client = ChainEngineSDK.Instance();
        WalletAdressPlayer.text = ChainEngineSDK._instance._player.WalletAddress;
    }

    private void OnEnable()
    {
        ChainEngineActions.OnWalletAuthSuccess += OnWalletAuthSuccess;
        ChainEngineActions.OnWalletAuthFailure += OnWalletAuthFailure;
    }

    private void OnDisable()
    {
        ChainEngineActions.OnWalletAuthSuccess -= OnWalletAuthSuccess;
        ChainEngineActions.OnWalletAuthFailure -= OnWalletAuthFailure;
    }

    public async void CreateOrFetchPlayer()
    {
        const string walletAddress = "";
        //const string walletAddress = "0x3Ad426944b0E302781c222C791c3d78ef5b77586";
        //const string walletAddress = "0xb86d5053eE5260B6A4AFc2D65f485f468B635e80";

        Player player = await client.CreateOrFetchPlayer(walletAddress);

        Debug.Log("Player: " +
                  $"gameId {player.GameId}\n" +
                  $"apiKey {player.APIKey}\n" +
                  $"walletAddress {player.WalletAddress}");
    }

    public void WalletLogin()
    {
        client.WalletLogin();
    }

    public void TrustWalletLogin()
    {
        client.WalletLogin(WalletProvider.TrustWallet);
    }

    public void MetamaskLogin()
    {
        client.WalletLogin(WalletProvider.Metamask);
    }

    public void CoinbaseLogin()
    {
        client.WalletLogin(WalletProvider.Coinbase);
    }

    public async void GetPlayerNFTs()
    {
        var nfts = await client.GetPlayerNFTs();

        foreach (var nft in nfts.Items())
        {
            Debug.Log($"NFT: {nft.Metadata.Name}\nChain ID: {nft.OnChainId}\nID: {nft.Id}");
        }
    }

    public async void GetNFT()
    {
        var nft = await client.GetNFT("0f7b9337-1b64-483c-80d7-c94052be6c40");
        //var nft = await client.GetNFT("fbf72fb5-377f-418f-be76-52854d1a8e47");

        Debug.Log($"NFT: {nft.Metadata.Name}\nChain ID: {nft.OnChainId}\nID: {nft.Id}");
    }


    //   private void OnTriggerEnter(Collider other)
    //   {
    //       if (other.gameObject.name == "Player")
    //      {
    //If the GameObject's name matches the one you suggest, output this message in the console
    //           Debug.Log("Do something here");
    //       }
    //   }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("sword"))
        {
            GetPlayerNFTs();
            Sword.SetActive(true);
            if (photonView.IsMine)
            {
                print(ChainEngineSDK._instance._player.WalletAddress);
            }
           // string myWallet = ChainEngineSDK._instance.CreateOrFetchPlayer()
           //     ._player(WalletAddress);
        }

        if (collision.gameObject.CompareTag("pistol"))
        {
            Pistol.SetActive(true);
            GetPlayerNFTs();
        }

            //  CreateOrFetchPlayer();
            //const string walletAddress = "";
            // Player player = await client.CreateOrFetchPlayer(walletAddress);

            //Debug.Log("Wallet: " + $"walletAddress {player.WalletAddress}");

            ////const string walletAddress = "";
            //const string walletAddress = "0x3Ad426944b0E302781c222C791c3d78ef5b77586";
            //const string walletAddress = "0xb86d5053eE5260B6A4AFc2D65f485f468B635e80";
            //     GetNFT();

            //Player player;

        //    string walletAddress = "";
            //client.CreateOrFetchPlayer(walletAddress);
            //print("My Wallet Address: " + walletAddress);

       
        //    Player player = await client.CreateOrFetchPlayer(walletAddress);

            //Debug.Log("Player: " +
            //          $"gameId {player.GameId}\n" +
            //          $"apiKey {player.APIKey}\n" +
            //          $"walletAddress {player.WalletAddress}");


        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("sword"))
        {
            Sword.SetActive(false);
          //  GetPlayerNFTs();
        }

        if (other.gameObject.CompareTag("pistol"))
        {
            Pistol.SetActive(false);
          //  GetPlayerNFTs();
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    //Check for a match with the specified name on any GameObject that collides with your GameObject
    //    if (collision.gameObject.name == "Player")
    //    {
    //        //If the GameObject's name matches the one you suggest, output this message in the console
    //        Debug.Log("Do something here");
    //    }

    //    //Check for a match with the specific tag on any GameObject that collides with your GameObject
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        //If the GameObject has the same tag as specified, output this message in the console
    //        Debug.Log("Do something else here");
    //    }
    //}



    private void OnWalletAuthSuccess(Player player)
    {
        SceneManager.LoadScene("Menu");

        Debug.Log("Player: " +
                  $"gameId {player.GameId}\n" +
                  $"apiKey {player.APIKey}\n" +
                  $"walletAddress {player.WalletAddress}");


    }

    private void OnWalletAuthFailure(WalletAuthenticationError error)
    {
        Debug.Log(error);
    }

}
