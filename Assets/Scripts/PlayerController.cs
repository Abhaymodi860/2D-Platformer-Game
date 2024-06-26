using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private float speed;
    [SerializeField] private float jump;

    private void Awake()
    {
        // Debug.Log("Player Controller Awake");
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");

        MoveCharacter(horizontal, vertical);

        PlayeMovementAnimation(horizontal, vertical);
    }

    void MoveCharacter(float horizontal, float vertical)
    {
        // moving horizontal
        Vector3 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform.position = position;

        // moving vertical
        if(vertical > 0)
        {
            rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
        }
    }

    void PlayeMovementAnimation(float horizontal, float vertical)
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        Vector3 scale = transform.localScale;
        if (horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

       /* 
        * bool isJumping = false;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }
        animator.SetBool("Jump", isJumping);
       */

        // new jump
        if(vertical > 0)
        {
            animator.SetBool("Jump", true);
        } else
        {
            animator.SetBool("Jump", false);
        }

        bool isCrouching = false;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
        }
        animator.SetBool("Crouch", isCrouching);
    }
}
