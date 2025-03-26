using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class WitchPlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float speed;
    [SerializeField] float bulletSpeed;
    [SerializeField] float animationDampenTime;
    
    [Header("Refrences")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject PlayerModel;
    [SerializeField] Animator anim;
    [SerializeField] GameObject firebolt;
    [SerializeField] GameObject firePoint;
    //Animator Parameters
    readonly string speedX = "SpeedX";
    readonly string speedY = "SpeedY";
    readonly string shoot = "Shoot";

    //Fields
    Vector2 moveDirection = Vector2.zero;
    Vector2 lookDirection = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
    }
        
}
