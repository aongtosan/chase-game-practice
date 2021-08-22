using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour,IPauseable
{
    private float m_TravelDistance = 0;
    private float m_MaxTravelDistance =15.0f;
    private float m_velocity = 3;
    private Coroutine m_reverseCoroutine;
    private Rigidbody m_ElevatorRigidBody;
    void Awake(){
        m_ElevatorRigidBody = GetComponent<Rigidbody>();
        enabled = false;
    }
    void FixedUpdate()
    {
        if(m_TravelDistance >= m_MaxTravelDistance){
             if(m_reverseCoroutine==null){
                    m_reverseCoroutine = StartCoroutine(nameof(reverseElevator));
                }
        }else{
           float m_DistanceStep = m_velocity * Time.fixedDeltaTime;
           m_TravelDistance += Mathf.Abs(m_DistanceStep);
           Vector3 elevatorPos = m_ElevatorRigidBody.position;
           elevatorPos.y += m_DistanceStep;
           m_ElevatorRigidBody.MovePosition(elevatorPos);
        }
    }
    IEnumerator reverseElevator(){
        yield return new WaitForSeconds(3.0f);
        m_TravelDistance =0;
        m_velocity*=-1;
        m_reverseCoroutine = null;
    }
    private IEnumerator ElevatorRun(){
        yield return new WaitForSeconds(1.0f);
        enabled = true; 
    }
    public void OnGameStart()
    {
        StartCoroutine(ElevatorRun());
    }
}
