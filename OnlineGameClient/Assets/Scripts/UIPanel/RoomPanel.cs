using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class RoomPanel : BasePanel
{
    private Text localPlayerUsername;
    private Text localPlayerTotalCount;
    private Text localPlayerWinCount;
    private Text enemyPlayerUsername;
    private Text enemyPlayerTotalCount;
    private Text enemyPlayerWinCount;
    private Transform bluePanel;
    private Transform redPanel;
    private Transform startButton;
    private Transform exitButton;
    private CreateRoomRequest crRequest;
    private UserData ud = null;
    void Start()
    {
        localPlayerUsername = transform.Find("BluePanel/Username").GetComponent<Text>();
        localPlayerWinCount = transform.Find("BluePanel/WinCount").GetComponent<Text>();
        localPlayerTotalCount = transform.Find("BluePanel/TotalCount").GetComponent<Text>();
        enemyPlayerUsername = transform.Find("RedPanel/Username").GetComponent<Text>();
        enemyPlayerWinCount = transform.Find("RedPanel/WinCount").GetComponent<Text>();
        enemyPlayerTotalCount = transform.Find("RedPanel/TotalCount").GetComponent<Text>();
        transform.Find("StartButton").GetComponent<Button>().onClick.AddListener(OnStartClick);
        transform.Find("ExitButton").GetComponent<Button>().onClick.AddListener(OnExitClick);
        bluePanel = transform.Find("BluePanel");
        redPanel = transform.Find("RedPanel");
        startButton = transform.Find("StartButton");
        exitButton = transform.Find("ExitButton");
        EnterAnim();
    }

    public override void OnEnter()
    {
        if(bluePanel!=null)
            base.OnEnter();
        crRequest = GetComponent<CreateRoomRequest>();
        crRequest.SendRequest();
       
    }

    public override void OnExit()
    {
        base.OnExit();
        ExitAnim();
    }

    public override void OnPause()
    {
        base.OnPause();
        ExitAnim();
    }

    public override void OnResume()
    {
        base.OnResume();
        EnterAnim();
    }

    public void SetSetLocalPlayerResSync()
    {
        ud = facade.GetUserData();
    }
    public void SetLocalPlayerRes(string username, string winCount, string totalCount)
    {
        localPlayerUsername.text = username;
        localPlayerTotalCount.text = "总场数："+totalCount;
        localPlayerWinCount.text = "胜利："+winCount;
    }
    private void SetEnemyPlayerRes(string username, string winCount, string totalCount)
    {
        enemyPlayerUsername.text = username;
        enemyPlayerTotalCount.text = "总场数：" + totalCount;
        enemyPlayerWinCount.text = "胜利：" + winCount;
    }

    public void ClearEnemyPlayerRes()
    {
        enemyPlayerUsername.text = "";
        enemyPlayerTotalCount.text = "等待玩家加入...";
        enemyPlayerWinCount.text = "";
    }
    private void ClearLocalPlayerRes()
    {
        localPlayerUsername.text = "";
        localPlayerTotalCount.text = "";
        localPlayerWinCount.text = "";
    }
    private void OnStartClick()
    {
        
    }

    private void OnExitClick()
    {
        
    }

    private void EnterAnim()
    {
        gameObject.SetActive(true);
        bluePanel.localPosition = new Vector3(-1000,0,0);
        bluePanel.DOLocalMoveX(-190, 0.4f);
        redPanel.localPosition = new Vector3(1000, 0, 0);
        redPanel.DOLocalMoveX(190, 0.4f);
        startButton.localScale = Vector3.zero;
        startButton.DOScale(1, 0.4f);
        exitButton.localScale = Vector3.zero;
        exitButton.DOScale(1, 0.4f);

    }

    private void ExitAnim()
    {
        bluePanel.DOLocalMoveX(-1000, 0.4f);
        redPanel.DOLocalMoveX(1000, 0.4f);
        startButton.DOScale(0, 0.4f);
        exitButton.DOScale(0, 0.4f).OnComplete(() => gameObject.SetActive(false));
    }

    void Update()
    {
        if (ud != null)
        {
            SetLocalPlayerRes(ud.Username,ud.WinCount.ToString(),ud.TotalCount.ToString());
            ClearEnemyPlayerRes();
            ud = null;
        }
    }
}
