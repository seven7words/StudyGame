using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class RequestManager : BaseManager {

    public RequestManager(GameFacade facade):base(facade)
    {

    }
    private Dictionary<RequestCode,BaseRequest> requestDict = new Dictionary<RequestCode, BaseRequest>();

    public void AddRequest(RequestCode requestCode, BaseRequest request)
    {
        requestDict.Add(requestCode,request);
    }

    public void RemoveRequest(RequestCode requestCode)
    {
        requestDict.Remove(requestCode);
    }

    public void HandleResponse(RequestCode requestCode, string data)
    {
       BaseRequest request =  requestDict.TryGet<RequestCode,BaseRequest>(requestCode);
        if (request == null)
        {
            Debug.LogWarning("无法得到RequestCode["+requestCode+"]对应的Request类");
            return;
        }
        
        request.OnResponse(data);
    }
}
