using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {
    public float speed = 10.0f;
    private float gravity = 20.0f;
    private float jumpSpeed = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    private int score;

    // Use this for initialization
    void Start () {
        score = 0;
        CharacterController controller = GetComponent<CharacterController>();
        TextMesh theText = GameObject.Find("GameOverBoard").GetComponent<TextMesh>();
        theText.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update () {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) 
    {
        // Destroy(other.gameObject);

        if (other.gameObject.CompareTag ("SpeedIncrease"))
        {
            Debug.Log("Increase Speed");
            other.gameObject.SetActive (false);
            StartCoroutine(WaitAndIncreaseSpeed(other, 3.0f));

        }
        else if (other.gameObject.CompareTag ("JumpIncrease")){
            Debug.Log("Increase Jump Speed");
            other.gameObject.SetActive (false);
            speed = 50f;
            StartCoroutine(WaitAndIncreaseJumpSpeed(other, 3.0f));
            revertSpeed();
        }
        else if (other.gameObject.CompareTag("Item3"))
        {
            other.gameObject.SetActive(false);
            score++;
            TextMesh theText = GameObject.Find("ScoreBoard").GetComponent<TextMesh>();
            theText.text = "Score: " + score.ToString();
            StartCoroutine(getPoints(other, 3.0f));
        }
    }

    IEnumerator getPoints(Collider other, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        print("WaitAndPrint " + Time.time);
        other.gameObject.SetActive(true);
    }

    IEnumerator WaitAndIncreaseSpeed(Collider other, float waitTime) 
    {
        speed = 75f;
        yield return new WaitForSeconds(waitTime);
        print("WaitAndPrint " + Time.time);
        revertSpeed();
        other.gameObject.SetActive (true);
    }

    IEnumerator WaitAndIncreaseJumpSpeed(Collider other, float waitTime) 
    {
        jumpSpeed = 40f;
        yield return new WaitForSeconds(waitTime);
        print("WaitAndPrint " + Time.time);
        jumpSpeed = 20f;
        other.gameObject.SetActive (true);
    }

    void revertSpeed ()
    {
       speed = 25f;
    }
}
