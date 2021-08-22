using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public Stats stats;
    private Rigidbody enemyRigidBody;
    private GameObject markFollowingTarget;
    private bool enemyRechargeGauge;
    public float pushRadius;
    void Awake(){
        AddCircle();
        stats = GetComponent<Stats>();
        enemyRigidBody = GetComponent<Rigidbody>();
        enemyRechargeGauge = true;
    }
    void Start()
    {
        markFollowingTarget = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 follow = (markFollowingTarget.transform.position - transform.position).normalized;
        enemyRigidBody.AddForce(follow*stats.velocity); 
        if(transform.position.y<-15.0f){
            Destroy(gameObject);
        }
        if(Mathf.Abs(follow.magnitude)<=pushRadius && enemyRechargeGauge){
           enemyRechargeGauge = false;
           enemyRigidBody.AddForce(follow*stats.velocity*1.5f,ForceMode.Impulse);
           Invoke(nameof(recharge),2.5f);
        }
    }
    void recharge(){
        enemyRechargeGauge = true;
    }
    void AddCircle(){
        GameObject circle = new GameObject{
        name = "Circle"
      };
      circle.transform.parent = transform;
      circle.transform.localPosition = new Vector3 (0,-0.49f,0);   
      circle.DrawCircle(pushRadius,0.1f);
    }
}
