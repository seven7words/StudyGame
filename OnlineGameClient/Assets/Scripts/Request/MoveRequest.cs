﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common;
using DG.Tweening;
using UnityEngine;

public class MoveRequest : BaseRequest
{
    public Transform localPlayerTransform;
    public PlayerMove localPlayerMove;
    private int syncRate = 30;
    private Transform remotePlayerTransform;
    private Animator remotePlayerAnim;
    private bool isSyncRemotePlayer = false;
    private Vector3 pos;
    private Vector3 rotation;
    private float forward;
    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.Move;
        base.Awake();
    }

    void Start()
    {
        InvokeRepeating("SyncLocalPlayer",1f,1f/syncRate);
    }

    void FixedUpdate()
    {
        if (isSyncRemotePlayer)
        {
            SyncRemotePlayer();
            isSyncRemotePlayer = false;
        }
    }
    private void SyncLocalPlayer()
    {
        SendRequest(localPlayerTransform.position.x, localPlayerTransform.position.y, localPlayerTransform.position.z,
            localPlayerTransform.eulerAngles.x, localPlayerTransform.eulerAngles.y, localPlayerTransform.eulerAngles.z,
            localPlayerMove.forward);
    }

    private void SyncRemotePlayer()
    {
        remotePlayerTransform.position = pos;
        remotePlayerTransform.eulerAngles = rotation;
        remotePlayerAnim.SetFloat("Forward",forward);
    }
    public MoveRequest SetLocalPlayer(Transform localPlayerTransform, PlayerMove localPlayerMove)
    {
        this.localPlayerTransform = localPlayerTransform;
        this.localPlayerMove = localPlayerMove;
        return this;

    }

    public MoveRequest SetRemotePlayer(Transform remotePlayerTransform)
    {
        this.remotePlayerTransform = remotePlayerTransform;
        this.remotePlayerAnim = this.remotePlayerTransform.GetComponent<Animator>();
        return this;
    }
    private void SendRequest(float x, float y, float z, float rotationX, float rotationY, float rotationZ,
        float forward)
    {
        //forward动画属性
        string data = string.Format("{0},{1},{2}|{3},{4},{5}|{6}", x, y, z, rotationX, rotationY, rotationZ, forward);
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        string[] strs = data.Split('|');
         pos = UnityTools.ParseVector3(strs[0]);
         rotation = UnityTools.ParseVector3(strs[1]);
         forward = float.Parse(strs[2]);
        isSyncRemotePlayer = true;
    }

    
}
