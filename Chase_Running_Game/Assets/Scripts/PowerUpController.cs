using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    // Start is called before the first frame update
    Quaternion circleLoc;
    void Awake(){
        AddCircle();
        circleLoc = new Quaternion(-16f,0f,10f,0f);
    }
    void Update()
    {
        transform.Rotate(Vector3.up * 30 * 10 * Time.deltaTime);
        GameObject circle = GameObject.Find("Circle-Power");
        circle.transform.rotation = circleLoc;
    }
    void AddCircle(){
        GameObject circle = new GameObject{
            name = "Circle-Power"
        };
      circle.transform.parent = transform;
      circle.transform.localPosition = new Vector3 (0,0,0);   
      circle.transform.rotation =  circleLoc;
      circle.DrawCircle(1.0f,0.1f);
    }
}
