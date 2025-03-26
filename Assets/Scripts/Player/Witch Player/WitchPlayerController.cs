using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class WitchPlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float speed;
    [SerializeField] float bulletSpeed;
    [SerializeField] int fireboltDamage;
    [SerializeField] public readonly float knockbackPower;
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
    //string Parameters
    const string speedX = "SpeedX";
    const string speedY = "SpeedY";
    const string shoot = "Shoot";
    const string enemyTag = "Enemy";

    //Fields
    Vector2 moveDirection = Vector2.zero;
    Vector2 lookDirection = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
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
        Vector3 movement = new Vector3(moveDirection.x, 0, moveDirection.y) * speed;
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);

        //rotation
        Vector3 look = new Vector3(lookDirection.x, 0, lookDirection.y);
        PlayerModel.transform.rotation = Quaternion.LookRotation(look);
    }
    private Vector2 CalculateCorrectRotation(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Vector3 worldPos = hit.point;

        Vector2 direction = new Vector2(worldPos.x - transform.position.x, worldPos.z - transform.position.z);
        return direction.normalized;
    }
    private void ChangeAnimationSpeed()
    {
        //calculating the angle between the look direction and the world axis
        float lookAngle = Mathf.Atan(lookDirection.y / lookDirection.x);
        lookAngle = Mathf.Rad2Deg * lookAngle;
        if(lookDirection.x < 0 && lookDirection.y >0)
        {
            lookAngle = lookAngle + 180;
        }
        else if(lookDirection.x < 0 && lookDirection.y < 0)
        {
            lookAngle = lookAngle - 180;
        }
        lookAngle = -(lookAngle-90); //Additional calculations to make it match with the y axis of the player's up = world up

        //rotating the move direction to be relative to the facing direction and then sending it to the animator
        Vector2 alteredMoveDirection = Quaternion.AngleAxis(lookAngle, Vector3.forward) * moveDirection;

        anim.SetFloat(speedX, alteredMoveDirection.x, animationDampenTime, Time.deltaTime);
        anim.SetFloat(speedY, alteredMoveDirection.y, animationDampenTime, Time.deltaTime);

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
        if(collision.gameObject.tag == enemyTag)
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            TakeDamage(enemy.ContactDamage);

            //calculate knockback
            Vector3 knockbackVect = (enemy.transform.position - transform.position).normalized * knockbackPower;
            StartCoroutine(enemy.Knockback(knockbackVect));
        }
    }

    //New Input Systems
    private void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }
    private void OnMousePosition(InputValue value)
    {
        lookDirection = CalculateCorrectRotation(value.Get<Vector2>());
    }
    private void OnFire()
    {
        anim.SetTrigger(shoot);
        FireBoltScript newFirebolt = Instantiate(firebolt, firePoint.transform.position, Quaternion.identity).GetComponent<FireBoltScript>();
        newFirebolt.SetDirection(lookDirection, bulletSpeed);
        newFirebolt.hitATarget.AddListener(DealDamage);
    }
        
}
