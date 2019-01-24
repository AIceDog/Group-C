using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cowboy : MonoBehaviour
{
    // 移动速度
    [SerializeField] float speed;

    // 物体是否接触地面
    [SerializeField] bool isGround;

    // 上一次接触地面的法线
    [SerializeField] Vector2 oldNormal;

    // 表面重力
    [SerializeField] float gravity;

    [SerializeField] Animator playerAnimator;
    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] Transform playerTransform;
    private Vector3 right;
    private Vector3 left;

    private void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        playerAnimator = gameObject.GetComponent<Animator>();
        playerTransform = gameObject.transform;
        gravity = -1.0f;
        speed = 2.0f;
        isGround = true;
        right = new Vector3(0.061f, 0.061f, 0.061f);
        left = new Vector3(-0.061f, 0.061f, 0.061f);
    }

    private void Update()
    {
        if (Input.GetKey("a"))
        {
            playerRigidbody.velocity = Vector2.left * speed;

            playerAnimator.SetBool("Walk", true);
            playerTransform.localScale = left;
        }
        else if (Input.GetKey("d"))
        {
            playerRigidbody.velocity = Vector2.right * speed;

            playerAnimator.SetBool("Walk", true);
            playerTransform.localScale = right;
        }
        else
        {
            playerRigidbody.velocity = Vector2.zero;

            playerAnimator.SetBool("Walk", false);
        }

        if (isGround == false)
        {
            playerRigidbody.velocity = playerRigidbody.velocity + oldNormal * gravity;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGround = true;
        oldNormal = collision.GetContact(0).normal;
        playerRigidbody.AddForce(collision.GetContact(0).normal * (-1));
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }
}
