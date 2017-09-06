using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class PlayerManager : BaseManager {

    public PlayerManager(GameFacade facade):base(facade)
    {
        
    }

    private RoleType currentRoleType;
    private GameObject currentRoleGameObject;
    private GameObject playerSyncRequest;
    private GameObject remoteRoleGameObject;
    public void SetCurrentRoleType(RoleType rt)
    {
        currentRoleType = rt;
    }
    private Transform rolePositions;
    private UserData userData;
    private ShootRequest shootRequest;
    private AttackRequest attackRequest;
    private Dictionary<RoleType,RoleData> roleDataDict = new Dictionary<RoleType, RoleData>();
    public UserData UserData
    {
        set { userData = value; }
        get { return userData; }
    }

    private void InitRoleDataDict()
    {
        roleDataDict.Add(RoleType.Blue, new RoleData(RoleType.Blue, "Prefabs/Hunter_BLUE", "Prefabs/Arrow_BLUE", rolePositions.GetChild(0), "Prefabs/Explosion_BLUE"));
        roleDataDict.Add(RoleType.Red, new RoleData(RoleType.Red, "Prefabs/Hunter_RED", "Prefabs/Arrow_RED", rolePositions.GetChild(1), "Prefabs/Explosion_RED"));

    }

    public override void OnInit()
    {
        rolePositions = GameObject.Find("RolePositions").transform;
        InitRoleDataDict();
    }

    public void SpawnRoles()
    {

        foreach (RoleData rd in roleDataDict.Values)
        {
            GameObject go =  GameObject.Instantiate(rd.RolePrefab, rd.SpawnPosition, Quaternion.identity);
            go.tag = "Player";
            if (rd.RoleType == currentRoleType)
            {
                currentRoleGameObject = go;
                currentRoleGameObject.GetComponent<PlayerInfo>().isLocal = true;
            }
            else
            {
                remoteRoleGameObject = go;
            }
        }
    }

    public GameObject GetCurrentRoleGameObject()
    {
        return currentRoleGameObject;
    }

    private RoleData GetRoleData(RoleType rt)
    {
        RoleData rd = null;
        roleDataDict.TryGetValue(rt,out rd);
        return rd;
    }
    public void AddControlScript()
    {
        currentRoleGameObject.AddComponent<PlayerMove>();
        PlayerAttack playerAttack =   currentRoleGameObject.AddComponent<PlayerAttack>();
        RoleType rt = currentRoleGameObject.GetComponent<PlayerInfo>().RoleType;
        RoleData rd = GetRoleData(rt);
        playerAttack.arrowPrefab = rd.ArrowPrefab;
        playerAttack.SetPlayerManager(this);
    }

    public void CreateSyncRequest()
    {
        playerSyncRequest = new GameObject("PlayerSyncRequest");
        playerSyncRequest.AddComponent<MoveRequest>()
            .SetLocalPlayer(currentRoleGameObject.transform, currentRoleGameObject.GetComponent<PlayerMove>())
            .SetRemotePlayer(remoteRoleGameObject.transform);
    shootRequest =     playerSyncRequest.AddComponent<ShootRequest>();
       shootRequest.PlayerManager = this;
        attackRequest = playerSyncRequest.AddComponent<AttackRequest>();
    }

    public void Shoot(GameObject arrowPrefab, Vector3 pos, Quaternion rotation)
    {
        facade.PlayNormalSound(AudioManager.Sound_Timer);
        GameObject.Instantiate(arrowPrefab, pos, rotation).GetComponent<Arrow>().isLocal = true;
        shootRequest.SendRequest(arrowPrefab.GetComponent<Arrow>().RoleType,pos,rotation.eulerAngles);
    }

    public void RemoteShoot(RoleType rt, Vector3 pos, Vector3 rotation)
    {
        GameObject arrowPrefab = GetRoleData(rt).ArrowPrefab;
     Transform transform =    GameObject.Instantiate(arrowPrefab).transform;
        transform.position = pos;
        transform.eulerAngles = rotation;

    }

    public void SendAttack(int damage)
    {
        attackRequest.SendRequest(damage);
    }

    public void GameOver()
    {
        GameObject.Destroy(currentRoleGameObject);
        GameObject.Destroy(playerSyncRequest);
        GameObject.Destroy(remoteRoleGameObject);
        shootRequest = null;
        attackRequest = null;

    }
}
