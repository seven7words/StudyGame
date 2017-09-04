using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int speed = 5;

    private Rigidbody rgd;

    public RoleType RoleType;
    public bool isLocal = false;
    public GameObject explosionEffect;
	// Use this for initialization
	void Start ()
	{
	    rgd = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rgd.MovePosition(transform.position+ transform.forward * speed * Time.deltaTime);


	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameFacade.Instance.PlayNormalSound(AudioManager.Sound_ShootPerson);
            if (isLocal)
            {
                bool playerIsLocal = other.GetComponent<PlayerInfo>().isLocal;
                if (isLocal != playerIsLocal)
                {
                    GameFacade.Instance.SendAttack(Random.Range(10,20));
                }
            }
        }
        else
        {
            GameFacade.Instance.PlayNormalSound(AudioManager.Sound_Miss);
        }
        GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        GameObject.Destroy(this.gameObject);
    }
}
