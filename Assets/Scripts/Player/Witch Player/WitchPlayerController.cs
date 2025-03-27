using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class WitchPlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float speed;
    [SerializeField] float bulletSpeed;
    [SerializeField] int fireboltDamage;
    [SerializeField] public float knockbackPower;
    [SerializeField] float cameraSpeed;
    [SerializeField] float animationDampenTime;
        
    [Header("Component Refrences")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] PlayerHealth hp;

    [Header("Other Object Refrences")]
    [SerializeField] GameObject PlayerModel;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject firebolt;
    [SerializeField] GameObject firePoint;
    
    //constant strings
    const string speedX = "SpeedX";
    const string speedY = "SpeedY";
    const string shoot = "Shoot";
    const string enemyTag = "Enemy";

    //Fields
    Vector2 moveDirection = Vector2.zero;
    float lookAngle = 0;

    void Awake()
    {
        Application.targetFrameRate = 1000;
        SpikeTrap.onSpikeTouched += TakeDamage;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ChangeAnimationSpeed();
    }

    private void Move()
    {
        //position movement
        Vector3 movementForward = mainCamera.transform.forward * moveDirection.y;
        Vector3 movementRight = mainCamera.transform.right * moveDirection.x;
        Vector3 directionVector = movementForward + movementRight;
        directionVector.y = 0;
        Vector3 movement = directionVector.normalized * speed;
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);

        //rotation
        //Vector3 look = new Vector3(lookDirection.x, 0, lookDirection.y);
        PlayerModel.transform.rotation = Quaternion.Euler(new Vector3(0, lookAngle, 0));
    }
    private void ChangeAnimationSpeed()
    {
        anim.SetFloat(speedX, moveDirection.x, animationDampenTime, Time.deltaTime);
        anim.SetFloat(speedY, moveDirection.y, animationDampenTime, Time.deltaTime);

    }

    private void TakeDamage(int damage)
    {
        hp.TakeDamage(damage);
    }
    private void DealDamage(EnemyController enemy)
    {
        enemy.TakeDamage(fireboltDamage);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if(collision.gameObject.tag == enemyTag)
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            TakeDamage(enemy.ContactDamage);

            //calculate knockback
            Vector3 knockbackVect = (enemy.transform.position - transform.position).normalized * knockbackPower;
            enemy.CallKnockBack(knockbackVect);
        }
    }

    //New Input Systems
    private void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }
    
    private void OnLook(InputValue value)
    {
        Vector2 deltaMovement = value.Get<Vector2>();
        deltaMovement.y = 0;
        lookAngle += deltaMovement.x * cameraSpeed;
    }
    
    private void OnFire()
    {
        anim.SetTrigger(shoot);
        FireBoltScript newFirebolt = Instantiate(firebolt, firePoint.transform.position, Quaternion.identity).GetComponent<FireBoltScript>();
        newFirebolt.SetDirection(PlayerModel.transform.forward, bulletSpeed);
        newFirebolt.hitATarget.AddListener(DealDamage);
    }
        
}
