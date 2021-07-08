using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    Transform m_groundCheck;
    public float m_groundRadius = 0.5f;
    bool m_isGrounded;
    public LayerMask m_whatIsGround;
    

    Animator m_anim;
    Rigidbody2D m_body;

    bool m_FacingRight = true;

    public float m_Speed = 100f;
    public float m_jumpForce = 300f;
    bool m_jump;

    
    int m_jumps;
    // Start is called before the first frame update
    void Start()
    {
        m_groundCheck = transform.Find("GroundCheck");

        m_anim = GetComponent<Animator>();
        m_body = GetComponent<Rigidbody2D>();

        m_jumps = 0;
        m_jump = false;
    }

    private void FixedUpdate()
    {
        m_isGrounded = false;
        Collider2D[]colliders = Physics2D.OverlapCircleAll(m_groundCheck.position, m_groundRadius, m_whatIsGround);
        foreach (Collider2D collider in colliders) {
            m_isGrounded = true;
            m_jumps = 0;
        }
        if (m_anim.GetBool("Ground") != m_isGrounded) {
            m_anim.SetBool("Ground", m_isGrounded);
        }

        if (m_body.velocity.y == 0)
        {
            m_anim.SetFloat("vSpeed", 0);
        }
        else
        {
            m_anim.SetFloat("vSpeed", m_body.velocity.y / Mathf.Abs(m_body.velocity.y));
        }

        float h = Input.GetAxis("Horizontal");
        if (m_jumps == 0)
        {
            m_jump = Input.GetButtonDown("Jump");
        }
        else if (m_jumps == 1) {
            if (m_body.velocity.y <= 0) {
                m_jump = Input.GetButtonDown("Jump");
            }
        }
        

        Move(h,m_jump);
        m_jump = false;
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    private void Move(float h,bool jump)
    {
        Vector2 v = m_body.velocity;

        if (h > 0)
        {
            v.x = h / Mathf.Abs(h) * m_Speed * Time.deltaTime;
            if (m_FacingRight)
            {
                Flip();
            }
            
            if (!m_anim.GetBool("run"))
            {
                m_anim.SetBool("run", true);
            }
            //move

        }
        else if (h < 0)
        {
            v.x = h / Mathf.Abs(h) * m_Speed * Time.deltaTime;
            if (!m_FacingRight)
            {
                Flip();
            }
            
            //move
            if (!m_anim.GetBool("run"))
            {
                m_anim.SetBool("run", true);
            }
        }
        else
        {
            v.x = 0;
            if (m_anim.GetBool("run"))
            {
                m_anim.SetBool("run", false);
            }
        }


        if (jump)
        {

            m_jumps++;
            v.y = m_jumpForce * Time.deltaTime;
        }
        m_body.velocity = v;
    }

    private void Flip()
    {
        
        m_FacingRight = !m_FacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
//46:11


