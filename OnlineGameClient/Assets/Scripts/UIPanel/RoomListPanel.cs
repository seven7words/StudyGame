using System;
using System.Collections;
using System.Collections.Generic;
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

        EnterAnim();
    }

    private void OnCreateRoomClick()
    {
        uiManager.PushPanel(UIPanelType.Room);
    }

    public override void OnEnter()
    {
        SetBattleRes();
        if (battleRes != null)
        {
            EnterAnim();
        }
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

    private void LoadRoomItem(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject roomItem = GameObject.Instantiate(roomItemPrefab);
            roomItem.transform.SetParent(roomLayout.transform);

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
    }
}
