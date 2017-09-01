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

    private bool isEnterPlay = false;
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
		UpdateManager();
	    if (isEnterPlay)
	    {
	        EnterPlaying();
	        isEnterPlay = false;
	    }
	}
    void OnDestroy()
    {
        DestroyManager();
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

    private void UpdateManager()
    {
        uiManager.Update();
        audioManager.Update();
        playerManager.Update();
        cameraManager.Update();
        requestManager.Update();
        clientManager.Update();
    }
   
    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requestManager.AddRequest(actionCode, request);
    }

    public void RemoveRequest(ActionCode actionCode)
    {
        requestManager.RemoveRequest(actionCode);
    }

    public void HandleResponse(ActionCode actionCode, string data)
    {
        requestManager.HandleResponse(actionCode, data);
    }

    public void ShowMessage(string msg)
    {
        uiManager.ShowMessage(msg);
    }

    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        clientManager.SendRequest(requestCode,actionCode,data);
    }
    public void PlayBgSound(string soundName)
    {
       audioManager.PlayBgSound(soundName);
    }

    public void PlayNormalSound(string soundName)
    {
        audioManager.PlayNormalSound(soundName);
    }

    public void SetUserData(UserData ud)
    {
        playerManager.UserData = ud;
    }

    public UserData GetUserData()
    {
        return playerManager.UserData;
    }
    public void SetCurrentRoleType(RoleType rt)
    {
        playerManager.SetCurrentRoleType(rt);
    }
    public GameObject GetCurrentRoleGameObject()
    {
        return playerManager.GetCurrentRoleGameObject();
    }

    private void EnterPlaying()
    {
        playerManager.SpawnRoles();
        cameraManager.FollowRole();
    }

    public void EnterPlayingSync()
    {
        isEnterPlay = true;
    }

    public void AddControlScript()
    {
        playerManager.AddControlScript();
    }


}
