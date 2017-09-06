using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RoomListPanel : BasePanel
{
    private RectTransform battleRes;
    private RectTransform roomList;
    private Button closeButton;
    private VerticalLayoutGroup roomLayout;
    private GameObject roomItemPrefab;
    private List<UserData> udList = null;
    private ListRoomRequest listRoomRequest;
    private CreateRoomRequest crRequest;
    private JoinRoomRequest joinRoomRequest;
    private UserData ud1 = null;
    private UserData ud2 = null;
    void Awake()
    {
       
    }
    void Start()
    {

        roomItemPrefab = Resources.Load("UIPanel/RoomItem") as GameObject;
        battleRes = transform.Find("BattleRes").GetComponent<RectTransform>();
        roomList = transform.Find("RoomList").GetComponent<RectTransform>();
        transform.Find("RoomList/CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseClick);
        roomLayout = transform.Find("RoomList/ScrollRect/Layout").GetComponent<VerticalLayoutGroup>();
        transform.Find("RoomList/CreateRoomButton").GetComponent<Button>().onClick.AddListener(OnCreateRoomClick);
        transform.Find("RoomList/RefreshButton").GetComponent<Button>().onClick.AddListener(OnRefreshClick);
        listRoomRequest = GetComponent<ListRoomRequest>();
        crRequest = GetComponent<CreateRoomRequest>();
        joinRoomRequest = GetComponent<JoinRoomRequest>();
        EnterAnim();
    }

    private void OnRefreshClick()
    {
        listRoomRequest.SendRequest();
    }

    private void OnCreateRoomClick()
    {
      BasePanel panel = uiManager.PushPanel(UIPanelType.Room);
        crRequest.SetPanel(panel);
        crRequest.SendRequest();//发起创建请求
    }

    public override void OnEnter()
    {
        SetBattleRes();
        if (battleRes != null)
        {
            EnterAnim();
        }
        if (listRoomRequest == null)
        {
            listRoomRequest = GetComponent<ListRoomRequest>();
        }
        listRoomRequest.SendRequest();

    }
    
    private void OnCloseClick()
    {
        PlayClickSound();
        uiManager.PopPanel();

    }

    public override void OnExit()
    {
        HideAnim();
    }

    private void EnterAnim()
    {
        gameObject.SetActive(true);
        battleRes.localPosition = new Vector3(-1000,0);
        battleRes.DOLocalMoveX(-230, 0.5f);
        roomList.localPosition = new Vector3(1000,0,0);
        roomList.DOLocalMoveX(160, 0.5f);
    }

    private void HideAnim()
    {
        battleRes.DOMoveX(-1000, 0.2f);
        roomList.DOMoveX(1000, 0.2f).OnComplete(() => gameObject.SetActive(false));
    }

    private void SetBattleRes()
    {
        UserData ud = facade.GetUserData();
        transform.Find("BattleRes/Username").GetComponent<Text>().text = ud.Username;
        transform.Find("BattleRes/WinCount").GetComponent<Text>().text ="胜利场数：\n"+ ud.WinCount.ToString();
        transform.Find("BattleRes/TotalCount").GetComponent<Text>().text = "总场数：\n" + ud.TotalCount.ToString();

    }

    void Update()
    {
        if (udList != null)
        {
            LoadRoomItem(udList);
            udList = null;
        }
        if (ud1 != null && ud2 != null)
        {
            BasePanel panel = uiManager.PushPanel(UIPanelType.Room);
            (panel as RoomPanel).SetAllPlayerResSync(ud1, ud2);
            ud1 = null;
            ud2 = null;
        }
    }
    public void LoadRoomItemSync(List<UserData> udList)
    {
        this.udList = udList;
    }
    private void LoadRoomItem(List<UserData> udList)
    {
        RoomItem[] riArray =  roomLayout.GetComponentsInChildren<RoomItem>();
        foreach ( RoomItem ri in riArray)
        {
            ri.DestroySelf();
        }
        int count = udList.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject roomItem = GameObject.Instantiate(roomItemPrefab);
            roomItem.transform.SetParent(roomLayout.transform);
            UserData ud = udList[i];
            roomItem.GetComponent<RoomItem>().SetRoomInfo(ud.Id,ud.Username,ud.TotalCount,ud.WinCount,this);

        }
     int roomCount =   GetComponentsInChildren<RoomItem>().Length;
     Vector2 size =   roomLayout.GetComponent<RectTransform>().sizeDelta;
        roomLayout.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x,
            roomCount*roomItemPrefab.GetComponent<RectTransform>().sizeDelta.y+roomLayout.spacing);
    }

    public override void OnPause()
    {
       HideAnim();
    }

    public override void OnResume()
    {
        EnterAnim();
        listRoomRequest.SendRequest();
    }

    public void OnJoinClick(int id)
    {
        joinRoomRequest.SendRequset(id);
    }

    public void OnJoinResponse(ReturnCode returnCode, UserData ud1, UserData ud2)
    {
        switch (returnCode)
        {
                case ReturnCode.NotFound:
                uiManager.ShowMessageSync("房间被销毁无法加入");
                break;
                case ReturnCode.Fail:
                uiManager.ShowMessageSync("房间已满，无法加入");
                break;
                case ReturnCode.Success:
                    this.ud1 = ud1;
                    this.ud2 = ud2;
                break;
        }
    }
}
