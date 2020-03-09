using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    public float horizonralInput;
    public float speed = 10.0f;
    public float maxHealth = 100;
    public float healthDecreaseSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            if (maxHealth >= 75)
            {
                jumpForce = 10.0f;
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
            }
            if (maxHealth > 50 && maxHealth < 75)
            {
                jumpForce = 7.0f;
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
            }
            if (maxHealth < 50)
            {
                jumpForce = 5.0f;
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
            }
        }

        horizonralInput = Input.GetAxis("Horizontal");
        if (!gameOver)
        {
            transform.Translate(Vector3.right * horizonralInput * Time.deltaTime * speed);
        }

        maxHealth -= Time.deltaTime * healthDecreaseSpeed;
        if (maxHealth == 0)
        {
            gameOver = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            maxHealth -= 25;
            Destroy(GameObject.FindWithTag("Obstacle"));
        }
        else if (collision.gameObject.CompareTag("Item"))
        {
            maxHealth += 25;
            Destroy(GameObject.FindWithTag("Item"));
        }
        else if (collision.gameObject.CompareTag("Fire"))
        {
            gameOver = true;
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), $"Health: {maxHealth}%");
    }
}