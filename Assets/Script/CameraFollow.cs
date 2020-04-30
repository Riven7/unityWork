using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //摄像机与英雄间的距离超过某个值
    public float xDistance = 2f;
    public float yDistance = 2f;

    public float xSmooth = 5f;//每秒钟运动的距离
    public float ySmooth = 5f;

    //摄像机可以移动的最大距离
    public Vector2 maxXAndY;
    public Vector2 minXAndY;//二维向量.x and .y

    public Transform player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    bool CheckXDistance()   //检查x方向上是否超过距离
    {
        return Mathf.Abs(transform.position.x - player.position.x) > xDistance;
    }
    bool CheckYDistance()   //检查y方向上是否超过距离
    {
        return Mathf.Abs(transform.position.y - player.position.y) > yDistance;
    }
    void TrackPlayer() // 摄像机去跟随英雄
    {
        float fTargerX = transform.position.x;
        float fTargerY = transform.position.y;//获取当前（原先）摄像机的位置

        if (CheckXDistance())
        {
            //每一帧去移动一点
            fTargerX = Mathf.Lerp(transform.position.x, player.transform.position.x
                                    , Time.deltaTime * xSmooth);
            //如果fTargerX小于minX则等于minX，处与min和max中间则还是等于fTargerX
            fTargerX = Mathf.Clamp(fTargerX, minXAndY.x, maxXAndY.x);
        }
        if (CheckYDistance())
        {    
            fTargerY = Mathf.Lerp(transform.position.y, player.transform.position.y
                                    , Time.deltaTime * ySmooth);
            fTargerY = Mathf.Clamp(fTargerY, minXAndY.y, maxXAndY.y);
        }
        transform.position = new Vector3(fTargerX, fTargerY, transform.position.z);
    }
    // Update is called once per frame
    void Update()
    {
        TrackPlayer();
        //Debug.Log(Time.deltaTime);看每一帧的deltatime值
    }
}
