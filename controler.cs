using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controler : MonoBehaviour
{

    private Animator animator;
    private CharacterController controller;
    public float transitionTime = 0.25f;
    public float fallSpeed;
    private float jumpSpeed = 50.0f;
    public float speed = 5.0f;
    public float rotSpeed = 100.0f;
    public bool breakable;
    public int val = 0;
    public float multiplier = 1.0f;
    public Transform camTransform;
    public bool p1 = true;
    public AudioClip clip;

    public float pushPower = 2.0F;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3F)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
    }

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        if (animator.layerCount >= 2)
        {
            animator.SetLayerWeight(1, 1);
        }
        if (gameObject.name != "Player1")
        {
            p1 = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        float accelerator = 0.05f;
        if (Input.GetKey(KeyCode.Q))
        {
            accelerator = 1.5f;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float xSpeed = 0;
        float zSpeed = 0;

        Vector3 moveDirection = Vector3.zero;

        float rotation = 0;
        if (p1)
        {
            if (Input.GetKey(KeyCode.A))
            {
                moveDirection -= camTransform.right * speed * Time.deltaTime;
                //rotation -= rotSpeed * Time.deltaTime;

            }
            if (Input.GetKey(KeyCode.D))
            {
                moveDirection += camTransform.right * speed * Time.deltaTime;
                //rotation += rotSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.W))
            {
                moveDirection += camTransform.forward * speed * Time.deltaTime;
                xSpeed += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveDirection -= camTransform.forward * speed * Time.deltaTime;
                xSpeed -= 1;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                rotation -= rotSpeed * Time.deltaTime;

            }
            if (Input.GetKey(KeyCode.E))
            {
                rotation += rotSpeed * Time.deltaTime;
            }
            if (controller.isGrounded)
            {
                animator.SetFloat("xSpeed", xSpeed, transitionTime, Time.deltaTime);
                animator.SetFloat("zSpeed", zSpeed, transitionTime, Time.deltaTime);
                animator.SetFloat("Speed", Mathf.Sqrt(h * h + v * v), transitionTime, Time.deltaTime);
            }
            if (Input.GetKeyDown(KeyCode.Z) && controller.isGrounded)
            {
                animator.SetBool("jump", true);
                fallSpeed = jumpSpeed * Time.deltaTime;
                moveDirection += new Vector3(0, fallSpeed, 0);
                print("jump");
            }
            else if (!controller.isGrounded)
            {
                animator.SetBool("jump", false);
                fallSpeed += Physics.gravity.y / 4 * Time.deltaTime;
                moveDirection += new Vector3(0, fallSpeed, 0);
            }
            else
            {
                fallSpeed = 0;
                animator.SetBool("jump", false);
                moveDirection += new Vector3(0, fallSpeed, 0);
            }
            controller.transform.Rotate(0, rotation, 0);
            controller.Move(moveDirection);
        }
        else
        {
            if (Input.GetKey(KeyCode.J))
            {
                moveDirection -= camTransform.right * speed * Time.deltaTime;
                //rotation -= rotSpeed * Time.deltaTime;

            }
            if (Input.GetKey(KeyCode.L))
            {
                moveDirection += camTransform.right * speed * Time.deltaTime;
                //rotation += rotSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.I))
            {
                moveDirection += camTransform.forward * speed * Time.deltaTime;
                xSpeed += 1;
            }
            if (Input.GetKey(KeyCode.K))
            {
                moveDirection -= camTransform.forward * speed * Time.deltaTime;
                xSpeed -= 1;
            }
            if (Input.GetKey(KeyCode.U))
            {
                rotation -= rotSpeed * Time.deltaTime;

            }
            if (Input.GetKey(KeyCode.O))
            {
                rotation += rotSpeed * Time.deltaTime;
            }
            if (controller.isGrounded)
            {
                animator.SetFloat("xSpeed", xSpeed, transitionTime, Time.deltaTime);
                animator.SetFloat("zSpeed", zSpeed, transitionTime, Time.deltaTime);
                animator.SetFloat("Speed", Mathf.Sqrt(h * h + v * v), transitionTime, Time.deltaTime);
            }
            if (Input.GetKeyDown(KeyCode.M) && controller.isGrounded)
            {
                animator.SetBool("jump", true);
                fallSpeed = jumpSpeed * Time.deltaTime;
                moveDirection += new Vector3(0, fallSpeed, 0);
                print("jump");
            }
            else if (!controller.isGrounded)
            {
                animator.SetBool("jump", false);
                fallSpeed += Physics.gravity.y / 4 * Time.deltaTime;
                moveDirection += new Vector3(0, fallSpeed, 0);
            }
            else
            {
                fallSpeed = 0;
                animator.SetBool("jump", false);
                moveDirection += new Vector3(0, fallSpeed, 0);
            }
            controller.transform.Rotate(0, rotation, 0);
            controller.Move(moveDirection);
        }
    }

    public void speedPowerUp()
    {
        val = 0;
        StartCoroutine(Increase());
    }

    public void jumpPowerUp()
    {
        val = 1;
        StartCoroutine(Increase());
    }

    public void breakPowerUp()
    {
        val = 2;
        StartCoroutine(Increase());
    }

    IEnumerator Increase()
    {
        AudioSource.PlayClipAtPoint(clip, gameObject.transform.position);
        float time = 10.0f;
        int pu = val;
        for (int i = 0; i < 1; i++)
        {
            if (pu == 0)
            {
                speed *= 2;
                rotSpeed *= 2;
            }
            else if (pu == 1)
            {
                jumpSpeed *= 1.5f;
            }
            else
            {
                breakable = true;
            }

            yield return new WaitForSeconds(time);
        }

        if (pu == 0)
        {
            speed /= 2;
            rotSpeed /= 2;
        }
        else if (pu == 1)
        {
            jumpSpeed /= 1.5f;
        }
        else
        {
            breakable = false;
        }
    }
}
