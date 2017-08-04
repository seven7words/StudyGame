using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    private static GameFacade _instance;

    public static GameFacade Instance
    {
        get { return _instance; }
    }

    private UIManager uiManager;

    private AudioManager audioManager;

    private PlayerManager playerManager;

    private CameraManager cameraManager;

    private RequestManager requestManager;

    private ClientManager clientManager;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
    }
	// Use this for initialization
	void Start () {
		InitManager();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void InitManager()
    {
        uiManager = new UIManager(this);
        audioManager = new AudioManager(this);
        playerManager = new PlayerManager(this);
        cameraManager =  new CameraManager(this);
        requestManager = new RequestManager(this);
        clientManager = new ClientManager(this);
        uiManager.OnInit();
        audioManager.OnInit();
        playerManager.OnInit();
        cameraManager.OnInit();
        requestManager.OnInit();
        clientManager.OnInit();
    }

    private void DestroyManager()
    {
        uiManager.OnDestroy();
        audioManager.OnDestroy();
        playerManager.OnDestroy();
        cameraManager.OnDestroy();
        requestManager.OnDestroy();
        clientManager.OnDestroy();
    }

    void OnDestroy()
    {
        DestroyManager();
    }
    public void AddRequest(RequestCode requestCode, BaseRequest request)
    {
        requestManager.AddRequest(requestCode, request);
    }

    public void RemoveRequest(RequestCode requestCode)
    {
        requestManager.RemoveRequest(requestCode);
    }

    public void HandleResponse(RequestCode requestCode, string data)
    {
        requestManager.HandleResponse(requestCode,data);
    }

    public void ShowMessage(string msg)
    {
        uiManager.ShowMessage(msg);
    }
}
