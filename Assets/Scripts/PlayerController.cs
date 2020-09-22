using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject player;

    private Rigidbody rb;

    private float movementX;
    private float movementY;
    private float Ylocation;

    private int count;
    private Vector3 scaleChange = new Vector3(.05f, .05f, .05f);


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }


    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    void Update()
    {
        Ylocation = player.transform.position.y;
        if(Ylocation < -5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            
            player.transform.localScale += (scaleChange);

            SetCountText();
        }

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if(count >= 20)
        {
            winTextObject.SetActive(true);
        }
    }
}
