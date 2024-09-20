using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D characterRigdbody;
    private float horizontalInput;

    [SerializeField]private float characterSpeed = 4.5f;
    [SerializeField]private float jumpforce = 5f;

    void Awake()
    {
        characterRigdbody = GetComponent<Rigidbody2D>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // characterRigdbody.AddForce(Vector2.up * jumpforce);
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        else if(horizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if(Input.GetButtonDown("Jump") && GroundSensor.isGrounded)
        {
            characterRigdbody.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        characterRigdbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigdbody.velocity.y);
        
        

    }

}
