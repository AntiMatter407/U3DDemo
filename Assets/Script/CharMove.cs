using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour
{
    Vector3 speed = new Vector3(0, 0, 0);
    Vector3 Acceleration = new Vector3(0, 0, 0);
    float Direction = 0.0f;
    public float MaxForwardSpeed = 6.0f;
    public float MaxBackwardSpeed = 3.0f;
    public float Accelerator = 0.6f;
    public float decelerator = 1.6f;
    public float factor = 2.0f;

    bool isjump = false;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        GameObject actor = transform.Find("hbcm_a_06").gameObject;
        anim = actor.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal"); // -1, 0, 1
        float z = Input.GetAxisRaw("Vertical"); // -1, 0, 1
        float Mx = Input.GetAxisRaw("Mouse X");
        float My = Input.GetAxisRaw("Mouse Y");
        Vector3 move = transform.forward * z + transform.right * x;
        transform.Rotate(0, Mx * factor, 0);
        if (move.magnitude > 0)
        {
            Acceleration = move.normalized * Accelerator;
            speed += Acceleration * Time.deltaTime;
        }
        else
        {
            Acceleration = -speed.normalized * decelerator;
            Vector3 temp = Acceleration * Time.deltaTime;
            if(temp.magnitude >= speed.magnitude)
            {
                speed = new Vector3(0, 0, 0);
            }
            else
            {
                speed += Acceleration * Time.deltaTime;
            }
        }       
        
        if (Vector3.Dot(speed, transform.forward) > 0 && speed.magnitude > MaxForwardSpeed)
        {
            speed = speed.normalized * MaxForwardSpeed;
        }else if(Vector3.Dot(speed, transform.forward) <= 0 && speed.magnitude > MaxBackwardSpeed)
        {
            speed = speed.normalized * MaxBackwardSpeed;
        }
        if(speed.magnitude > 0.01f)
        {
            Direction = Vector3.Angle(transform.right, speed);
        }else
        {
            Direction = 90.0f;
        }

        transform.position += speed * Time.deltaTime;
        UpdateAnim();
    }

    void UpdateAnim()
    {
        float Speed = Vector3.Dot(speed, transform.forward) / MaxForwardSpeed;
        float Dir = 0.0f;
        if(Vector3.Dot(speed, transform.forward) == 0) Speed = 0;
        if(Vector3.Dot(speed, transform.forward) < 0) Speed = -Speed;
        anim.SetFloat("Speed", Speed);
       
        Dir = Mathf.Cos(Mathf.PI * Direction / 180);

        anim.SetFloat("Direction", Dir);

        if (Input.GetButtonDown("Jump") && !isjump)
        {
            anim.SetBool("IsJump", true);
            isjump = true;
        }
        else
        {
            isjump = false;
            anim.SetBool("IsJump", false);
        }

        Debug.Log(Speed+", "+ Dir);
     
    }
}
