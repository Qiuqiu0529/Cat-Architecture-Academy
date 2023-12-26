using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    private Vector3 Rotion_Transform;
    private new Camera camera;

    float last = -1;
    Vector3 oldPos1, oldPos2;
    void Start()
    {
        camera = GetComponent<Camera>();
        Rotion_Transform = target.position;
    }
    void Update()
    {
        Ctrl_Cam_Move();
        Cam_Ctrl_Rotation();
    }
   
    public void Ctrl_Cam_Move()
    {
        Vector3 prepos = transform.position;
        Quaternion prerot = transform.rotation;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            transform.Translate(Vector3.forward * 1f);//�ٶȿɵ�  ���е���
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            transform.Translate(Vector3.forward * -1f);//�ٶȿɵ�  ���е���
        }

        if (Input.touchCount == 2)
        {
            float dis = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);//��ָ֮��ľ���
            if (-1 == last) 
                last = dis;
            float result = dis - last;//����һ֡�Ƚϱ仯
            if (result > 0)
            {
                transform.Translate(Vector3.forward * 1f);
            }
            else if (result < 0)
            {
                transform.Translate(Vector3.forward * -1f);
            }
            last = dis;//��¼Ϊ��һ֡��ֵ
        }

        if (transform.position.x > 10f || transform.position.x < -25f)
        {
            transform.position = prepos;
            transform.rotation = prerot;
        }
    }
    public void Cam_Ctrl_Rotation()
    {
        var mouse_x = Input.GetAxis("Mouse X");//��ȡ���X���ƶ�
        var mouse_y = -Input.GetAxis("Mouse Y");//��ȡ���Y���ƶ�
        Vector3 prepos=transform.position;
        Quaternion prerot=transform.rotation;
        if (Input.GetKey(KeyCode.Mouse1))
        {
            transform.RotateAround(Rotion_Transform, Vector3.up, mouse_x * 5);
            transform.RotateAround(Rotion_Transform, transform.right, mouse_y * 5);
           
        }

        /*if (Input.touchCount ==2)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began 
                || Input.GetTouch(1).phase == TouchPhase.Began)
            {
                //��¼��ָ�մ�����λ��
               oldPos1 = Input.GetTouch(0).position;
               oldPos2 = Input.GetTouch(1).position;
            }

            // 2ֻ��ָ�������Ͷ�Ϊ�ƶ�����
            if (Input.GetTouch(0).phase == TouchPhase.Moved 
                || Input.GetTouch(1).phase == TouchPhase.Moved )
            {
                var tempPosition1 = Input.GetTouch(0).position;
                var tempPosition2 = Input.GetTouch(1).position;

                float xOne = tempPosition1.x -oldPos1.x;
                float xTwo = tempPosition2.x -oldPos2.x;
                float yOne = tempPosition1.y - oldPos1.y;
                float yTwo = tempPosition2.y - oldPos2.y;
                if (xOne > 0 || xTwo > 0 )
                {
                    transform.RotateAround(Rotion_Transform, Vector3.up,5);
                }
                if (xOne < 0 || xTwo < 0)
                {
                    transform.RotateAround(Rotion_Transform, Vector3.up, -5);
                }
                if (yOne > 0 || yTwo > 0 )
                {
                    transform.RotateAround(Rotion_Transform, transform.right,  5);
                }
                if (yOne < 0 || yTwo < 0 )
                {
                    transform.RotateAround(Rotion_Transform, transform.right, -5);
                }
            }
        }*/

        if (transform.position.y < 0.1f)
        {
            transform.position = prepos;
            transform.rotation = prerot;
        }

    }
}
