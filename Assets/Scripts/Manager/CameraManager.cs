using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : SingletonMono<CameraManager>
{
    public bool IsPositionInsideCamera(Vector2 position)
    {
        Vector2 pos = Camera.main.WorldToViewportPoint(position);

        return pos.x >= 0 && pos.x <= 1 && pos.y >= 0 && pos.y <= 1; 
    }

    private void Update() 
    {
        this.transform.position = new Vector3(PlayerController.GetInstance().gameObject.transform.position.x, PlayerController.GetInstance().gameObject.transform.position.y, -10);    
    }
}
