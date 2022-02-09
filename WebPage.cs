using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
// UnityWebRequest
using UnityEngine.Networking;
// HttpWebRequest
using System.Net;
// StreamReader
using System.IO;

using QuickType;
// Image
using UnityEngine.UI;

public class WebPage : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Login();
    
    [SerializeField]
    private string devAddress = "0x19cc54d721970cb9386e8953b1052b271432e8e3";

    public string userAddress {
        get;
        private set;
    }

    public string moralisApiKey;

    // public void Start() {
    //     #if UNITY_EDITOR
    //         Debug.Log("WebPage.Authorize()");
    //         LoginCallback(devAddress);
    //     #else
    //         Login();
    //     #endif
    // }

    public void TryLogin() {
        #if UNITY_EDITOR
            Debug.Log("WebPage.Authorize()");
            LoginCallback(devAddress);
        #else
            Login();
        #endif
    }

    public void LoginCallback(string user)
    {
        Debug.Log("Login Callback " + user);

        userAddress = user;

        FetchNFTs(user);
    }

    private void FetchNFTs(string address) {
        int limit = 1;

        print("address " + address);

        print("limit " + limit);

        string queryUrl = "https://deep-index.moralis.io/api/v2/"+address+"/nft?chain=eth&format=decimal&limit="+limit+"&offset=0";
        


        var request = (HttpWebRequest)WebRequest.Create(queryUrl);

        var headers = new WebHeaderCollection();
        headers.Add("X-API-KEY", moralisApiKey);

        request.Headers = headers;
        request.Method = "GET";

        var response = (HttpWebResponse)request.GetResponse();

        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

        print(responseString);

        MoralisResponse moralisResponse = MoralisResponse.FromJson(responseString);

        print(moralisResponse.Status);

        var total = moralisResponse.PageSize > limit ? limit : moralisResponse.PageSize;
        
        // GameObject.Find("Canvas").GetComponent<FlexibleGridLayout>().Test();

        for (int i = 0; i < total; i++)
        {
            if (moralisResponse.Result[i].Metadata == null) continue;

            Metadata metadata = Metadata.FromJson(moralisResponse.Result[i].Metadata);

            if (metadata.Image == null) continue;
    
            StartCoroutine(DownloadImage(metadata.Image));
        }

        // GameObject.Find("Canvas").GetComponent<FlexibleGridLayout>().Test();

        Debug.Log("FetchNFTs done");

    }

    IEnumerator DownloadImage(string url) {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url.Replace("ipfs://", "https://ipfs.moralis.io:2053/ipfs/"));

        yield return www.SendWebRequest();

        Texture2D textureNft = DownloadHandlerTexture.GetContent(www);

        GameObject.Find("PlayerOnTheLeft").GetComponent<MeshRenderer>().material.mainTexture = textureNft;

        // GetComponent<FlexibleGridLayout>().Test(textureNft);
    }
}