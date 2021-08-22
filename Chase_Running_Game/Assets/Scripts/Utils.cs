using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectEfx
{
    public static void DrawCircle(this GameObject container,float radius ,float width){
      var segment = 360;
      var line = container.AddComponent<LineRenderer>();
      line.useWorldSpace =  false;
      line.startWidth =width;
      line.endWidth = width;
      line.positionCount = segment + 1;
      var points = new Vector3[line.positionCount];
      for(int i= 0;i<points.Length;i++){
        var rad = Mathf.Deg2Rad * i;
        points[i] = new Vector3(Mathf.Cos(rad) * radius,0,Mathf.Sin(rad) * radius);
      }
      line.SetPositions(points);
    }
}