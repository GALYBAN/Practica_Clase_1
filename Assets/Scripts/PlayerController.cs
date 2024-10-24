using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D characterRigdbody;
    private float horizontalInput;
    public static Animator characterAnimator;

    [SerializeField] private int maxHealth = 5;
    [SerializeField]private int currentHealth;

    [SerializeField]private float characterSpeed = 4.5f;
    [SerializeField]private float jumpforce = 5f;

    private bool isAttacking = false;

    [SerializeField] private Transform attackHitbox;

    [SerializeField] private float attackRadius;

    private AudioSource _audioSourcePlayer;


    //public Mimic mimicScript;

    void Awake()
    {
        characterRigdbody = GetComponent<Rigidbody2D>();

        characterAnimator = GetComponent<Animator>();
        
        _audioSourcePlayer = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // characterRigdbody.AddForce(Vector2.up * jumpforce);
        currentHealth = maxHealth;
        GameManager.instance.SetHealthBar(maxHealth);

    }
    void Update()
    {

        Movement();

        if(Input.GetButtonDown("Jump") && GroundSensor.isGrounded && !isAttacking)
        {
            Jump();
        }

        if(Input.GetButtonDown("Fire1") && GroundSensor.isGrounded && horizontalInput != 0)
        {

        }
        else if(Input.GetButtonUp("Fire1") && GroundSensor.isGrounded)
        {
            StartAttack();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            GameManager.instance.Pause();
        }

    }

    void FixedUpdate()
    {
        characterRigdbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigdbody.velocity.y);
    }

    void Movement()
    {

        if(horizontalInput == 0 && isAttacking)
        {
            horizontalInput = 0;
            return;
        }

        horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput == 0)
        {
            characterAnimator.SetBool("IsRunning", false);
        }
        else if(horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            characterAnimator.SetBool("IsRunning", true);
        }
        else if(horizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            characterAnimator.SetBool("IsRunning", true);
        }

        /*if(horizontalInput < 0)
        {
            if(!isAttacking)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            characterAnimator.SetBool("IsRunning", true);
        }

        else if(horizontalInput > 0)
        {
            if(!isAttacking)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            characterAnimator.SetBool("IsRunning", true);
        }

        else
        {
            characterAnimator.SetBool("IsRunning", false);
        }*/
    }

    void Jump()
    {
        characterRigdbody.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
    }

    /*void Attack()
    {
        StartCoroutine(AttackingAnimation());
        characterAnimator.SetTrigger("IsAttacking");
    }

    void AttackWhileMoving()
    {
        StartCoroutine(AttackingAnimation());
        characterAnimator.SetTrigger("IsAttackingWhileMoving");

    }

    IEnumerator AttackingAnimation()
    {
        
        isAttackingWhileMoving = true;

        yield return new WaitForSeconds(0.3f);

        Collider2D[] collider = Physics2D.OverlapCircleAll(attackHitbox.position, attackRadius);
         foreach(Collider2D hit in collider)
        {
            if(hit.gameObject.CompareTag("Mimic"))
            {
                Rigidbody2D enemyRigdbody = hit.gameObject.GetComponent<Rigidbody2D>();
                enemyRigdbody.AddForce(transform.right + transform.up * 2, ForceMode2D.Impulse);
                hit.gameObject.GetComponent<Mimic>().TakeDamageMimic(); 
            }
        }

        yield return new WaitForSeconds(0.5f);

        isAttackingWhileMoving = false;

    }*/

    void StartAttack()
    {
        isAttacking = true;
        characterAnimator.SetTrigger("IsAttacking");
        SoundManager.instance.PlaySFX(_audioSourcePlayer, SoundManager.instance.swordAttack[Random.Range(0, SoundManager.instance.swordAttack.Length)]);
    }


    void Attack()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(attackHitbox.position, attackRadius);
         foreach(Collider2D hit in collider)
        {
            if(hit.gameObject.CompareTag("Mimic"))
            {
                Rigidbody2D enemyRigdbody = hit.gameObject.GetComponent<Rigidbody2D>();
                enemyRigdbody.AddForce(transform.right + transform.up * 2, ForceMode2D.Impulse);
                hit.gameObject.GetComponent<Mimic>().TakeDamageMimic(); 
            }
        }

    }
    void EndAttack()
    {
        isAttacking = false;
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        GameManager.instance.UpdateHealtBar(currentHealth);

        Debug.Log(currentHealth);

        if(currentHealth <= 0)
        {
            Die();
            return;
        }

        characterAnimator.SetTrigger("IsHurt");
    }
    
    void Health(int damage)
    {
        currentHealth += damage;

        GameManager.instance.UpdateHealtBar(currentHealth);

        Debug.Log(currentHealth);

    }

     void Die()
    {
        characterAnimator.SetTrigger("IsDead");
        Destroy(gameObject, 0.45f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            Die();
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
         if(collider.gameObject.layer == 8)
        {
            TakeDamage(1);            
        }

        if(collider.gameObject.layer == 10 && currentHealth < maxHealth)
        {
            Health(1);            
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackHitbox.position, attackRadius);
    }
}
