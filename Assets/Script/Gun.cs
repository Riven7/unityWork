using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Rigidbody2D rocket;
    public float speed = 20f;

    private PlayerControl playerCtrl;   // 为了获取hero的朝向

    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        //playerCtrl = GameObject.Find("hero").GetComponent<PlayerControl>();
        playerCtrl = transform.parent.GetComponent<PlayerControl>();    //找父类的组件
        audio = GetComponent<AudioSource>(); //获取声音
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (playerCtrl.faceRight)
            {
                audio.Play();   //播放声音
                // 在position处实例化rocket， Quaternion.Euler new Vector3 控制旋转
                Rigidbody2D bullet = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                bullet.velocity = new Vector2(speed, 0);
            }
            else
            {
                Rigidbody2D bullet = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                bullet.velocity = new Vector2(-speed, 0);       //z方向旋转180°
            }
        }
    }
}
