using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator anim;
    private float speed = 3;
	// Use this for initialization
	void Start ()
	{
	    anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    if (anim.GetCurrentAnimatorStateInfo(0).IsName("Grounded") ==false)
            return;
        float h = Input.GetAxis("Horizontal");
	    float v = Input.GetAxis("Vertical");

	    if (h!=0 || v!=0)
	    {
	        transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime, Space.World);

	        transform.rotation = Quaternion.LookRotation(new Vector3(h, 0, v));

	        float res = Mathf.Max(Mathf.Abs(h), Mathf.Abs(v));
	        anim.SetFloat("Forward", res);
        }
       
	}
}
