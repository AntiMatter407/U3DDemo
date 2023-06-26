using UnityEngine;
using System.Collections;


public class CameraFindPlayerScript : MonoBehaviour
{
    //�����������߶�
    public float m_Height = 1f;
    //��������������
    public float m_Distance = 5f;
    //��������ٶ�
    public float m_Speed = 4f;
    //Ŀ��λ��
    Vector3 m_TargetPosition;
    //Ҫ���������
    Transform follow;
    // Use this for initialization
    void Start()
    {
        // ͨ��Tag�õ����Ҫ���������
        follow = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    //���ƽ���ĸ��������ƶ�
    void LateUpdate()
    {
        //�õ����Ŀ��λ��
        Vector3 target = follow.position + Vector3.up * m_Height;
        m_TargetPosition = target - follow.forward * m_Distance;
        //���λ��
        transform.position = Vector3.Lerp(transform.position, m_TargetPosition, m_Speed * Time.deltaTime);
        //���ʱ�̿�������
        transform.LookAt(target);


    }
}
