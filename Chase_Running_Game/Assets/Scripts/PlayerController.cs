using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour,IPauseable
{ 
    public Stats stats;
    private Rigidbody m_rigid;
    private GameObject m_Elevator;
    private float m_ElevatorOffsetY;
    public Camera followCam;
    private Vector3 camPos;
    public UnityEvent onPlayLost;

    private void ReadyPlayer(){
      enabled = true;
    }
    public void OnGameStart(){
       ReadyPlayer();
    }
    void Awake(){
      stats = GetComponent<Stats>();
      m_rigid = GetComponent<Rigidbody>();
      m_ElevatorOffsetY = 0; 
      camPos = followCam.transform.position - m_rigid.position;
      enabled = false;
    } 
    void FixedUpdate()
    {
      if(transform.position.y<=-15.0f){
        onPlayLost.Invoke();
      }
      float horizontalInput = Input.GetAxis("Horizontal");
      float verticalInput = Input.GetAxis("Vertical");

      float newHorizontalPosition = horizontalInput; 
      float newverticalPosition =  verticalInput;
      Vector3 playerPos = m_rigid.position;
      Vector3 newPosition = new Vector3(
        newHorizontalPosition,
        0,
        newverticalPosition  
      ).normalized;
     // Debug.Log(newPosition.magnitude);
      newPosition.Normalize();
      if(newPosition==Vector3.zero){
        return;
      }
      Quaternion targetRotation = Quaternion.LookRotation(newPosition);
      if(m_Elevator != null){
         playerPos.y = m_Elevator.transform.position.y + m_ElevatorOffsetY;
      }
      if(Mathf.Approximately( Vector3.Dot(newPosition,Vector3.forward),-1.0f )){
          targetRotation = Quaternion.LookRotation(-Vector3.forward);
      } 
      targetRotation = Quaternion.RotateTowards(
        transform.rotation,
        targetRotation,
        360 * Time.fixedDeltaTime
      );
      m_rigid.MovePosition( playerPos + newPosition * stats.velocity * Time.fixedDeltaTime );
      m_rigid.MoveRotation(targetRotation);
    }
   private void LateUpdate(){
      followCam.transform.position = m_rigid.position + camPos;
    }
    private IEnumerator bonusSpeedCountdown(){
      yield return new WaitForSeconds(5.0f);
      stats.normalSpeed();
    }
    void OnCollisionEnter(Collision hit){
        if(hit.gameObject.CompareTag("PowerUp")){
            stats.increaseVelocity();
            StartCoroutine(bonusSpeedCountdown());
            Destroy(hit.gameObject);
        }
        if(hit.gameObject.CompareTag("Enemy") && stats.getPresentStatus()==Stats.STATUS.SPEEDUP){
            Rigidbody enemyRb = hit.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = hit.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * 150.0f,ForceMode.Impulse);
        }
    }
    void OnTriggerEnter(Collider other){
      if(other.CompareTag("Elevator")){
        //transform.parent = other.transform.parent;
        m_Elevator = other.gameObject;
        m_ElevatorOffsetY = transform.position.y - m_Elevator.transform.position.y;
      }
    } 
    void OnTriggerExit(Collider other){
      if(other.CompareTag("Elevator")){
         m_Elevator = null;
         m_ElevatorOffsetY = 0.0f;
      }
      
    }
}
