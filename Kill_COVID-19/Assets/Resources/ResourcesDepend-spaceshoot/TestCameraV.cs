using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraV : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Vector2 viewportSize = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));

        //var pos = Vector3.zero;
        //pos.x = viewportSize.x;
        //pos.z = viewportSize.y;
        //pos.y = 0;
        //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = pos;
        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)); //calculating mouse position in the worldspace
        //mousePosition.z = -Camera.main.transform.position.z;
        //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = mousePosition;

        GetScreenPointInGround(new Vector3(Screen.width * 0.5F, Screen.height * 0.5F, 0));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 GetScreenPointInGround(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        Vector3 groundPoint = UFrame.Math_F.GetIntersectWithLineAndGround(ray.origin, ray.direction);
#if UNITY_EDITOR
        GameObject.CreatePrimitive(PrimitiveType.Capsule).transform.position = groundPoint;
#endif
        return groundPoint;
    }
}
