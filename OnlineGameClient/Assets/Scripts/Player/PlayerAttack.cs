using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private Transform leftHandTrans;
    public GameObject arrowPrefab;
	// Use this for initialization
	void Start ()
	{
	    anim = GetComponent<Animator>();
	    leftHandTrans = transform.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 L Clavicle/Bip001 L UpperArm/Bip001 L Forearm/Bip001 L Hand");

    }

    // Update is called once per frame
    void Update () {
	    if (anim.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
	    {
	        if (Input.GetMouseButton(0))
	        {
	            Ray ray=    Camera.main.ScreenPointToRay(Input.mousePosition);
	            RaycastHit hit;
	            bool isCollider =    Physics.Raycast(ray, out hit);
	            if (isCollider)
	            {
	                Vector3 point = hit.point;
                    anim.SetTrigger("Attack");
                    Shoot(point);
	            }

	        }
	    }
	}

    private void Shoot(Vector3 tartgetPoint)
    {
        tartgetPoint.y = transform.position.y;
        Vector3 dir = tartgetPoint - transform.position;
        GameObject.Instantiate(arrowPrefab, leftHandTrans.position, Quaternion.LookRotation(dir));

    }
}
