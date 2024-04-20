using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;


public class player_mvt : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveInput;

    public float dashSpeed;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public float dashCounter;
    public Text cooldownText;
    bool isDashing = false;
    bool canDash = true;

    public Animator animator;


    public Slider hp;

    public static int chest = 0;

    public int poison = 100;


    public GameObject damagecircleee;


    public Transform bosslocation;
    public GameObject damagecircle;
    void Start()
    {
        animator.SetBool("right", true);
        animator.SetBool("up", false);
        animator.SetBool("idle", true);
        animator.SetBool("run", false);
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput.x > 0.1)
        {
            animator.SetBool("right", true);
        }
        else if (moveInput.x < -0.1)
        {
            animator.SetBool("right", false);
        }

        if (moveInput.y > 0.1)
        {
            animator.SetBool("up", true);
        }
        else if (moveInput.y < -0.1)
        {
            animator.SetBool("up", false);
        }

        if (math.abs(moveInput.y) > 0.1 || math.abs(moveInput.x) > 0.1)
        {
            animator.SetBool("run", true);
            animator.SetBool("idle", false);
        }
        else if (math.abs(moveInput.y) < 0.1 && math.abs(moveInput.x) < 0.1)
        {
            animator.SetBool("run", false);
            animator.SetBool("idle", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash && (moveInput.x != 0 || moveInput.y != 0))
        {
            StartCoroutine(Dash());
        }


        if (Input.GetKeyDown(KeyCode.U))
        {
            hp.value -= 1;
            Debug.Log(chest);
        }


        if(Input.GetKeyDown(KeyCode.R))
        {
            poison--;
            hp.value++;
            Debug.Log(poison);
        }

        moveInput.Normalize();
        cooldownText.text = "Dash Cooldown: " + (canDash ? "READY" :"STANA");

        if (hp.value >= 2)
        {
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.color = Color.white;
        }
        else
        {
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.color = Color.red;
        }
    }


    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        Vector2 dashDirection = moveInput.normalized;
        float dashTime = 0f;

        while (dashTime < dashDuration)
        {
            rb.velocity = dashDirection * dashSpeed;
            dashTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("projectile"))
        {
            hp.value--;

            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("damagcircle"))
        {
            hp.value--;
        }


        if (collision.CompareTag("boss"))
        {
            Debug.Log("ib3ed");
            Invoke("spawndamagincircle" ,1f);
        }
    }


    void spawndamagincircle()
    {
        Instantiate(damagecircle, bosslocation.position, quaternion.identity);
    }
}



