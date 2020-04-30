using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundParallax : MonoBehaviour
{
    public Transform[] backgrounds; //背景层数组
    public float parallaxScale = 0.5f;  //相机移动时，背景相对移动的比例

    public float layerScale = 0.5f; //层间的运动比例, 使层级1 2 3..乘上小于1的数，使层级导致的差别不会太大
    public float smooth = 5f;   //运动的平滑量  用来*deltaTime

    private Transform CamTransform; //相机的transform
    private Vector3 previousCamPos; //相机上一帧的位置

    private void Awake()
    {
        //CamTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        CamTransform = Camera.main.transform;   //Camera是个特别的类，可用其自带方法
    }

    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = CamTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float parallax = (previousCamPos.x - CamTransform.position.x) * parallaxScale;  //背景移动距离
        if (parallax != 0)
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                float targetX = backgrounds[i].position.x + parallax * (1 + i * layerScale);
                Vector3 targetPos = new Vector3(targetX, backgrounds[i].position.y, backgrounds[i].position.z);
                backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, targetPos, smooth * Time.deltaTime);
                previousCamPos = CamTransform.position;
            }
        }
    }
}
