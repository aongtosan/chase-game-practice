using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
     public enum STATUS{
        NEUTURAL,SPEEDUP
    };
    public float velocity;
    private float default_velocity;
    private STATUS state;
    void Awake(){
        default_velocity = velocity ;
        state = STATUS.NEUTURAL;
    }
    public void increaseVelocity(){
        velocity+=10;
        state = STATUS.SPEEDUP;
    }
    public void normalSpeed(){
        velocity = default_velocity;
        state = STATUS.NEUTURAL;
    }
    public STATUS getPresentStatus(){
        return state;
    }
}
