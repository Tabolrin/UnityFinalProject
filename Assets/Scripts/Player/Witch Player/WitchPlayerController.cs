using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;


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
    [SerializeField] SceneHandler sceneHandler;
    [SerializeField] Button pauseButton;
    
    //constant
    const string speedX = "SpeedX";
    const string speedY = "SpeedY";
    const string shoot = "Shoot";
    const string enemyTag = "Enemy";
    const float deathAnimationTime = 1;

    //Fields
    Vector2 moveDirection = Vector2.zero;
    float lookAngle = 0;
    bool isAlive = true;
    

    void Awake()
    {
        Application.targetFrameRate = 1000;
        SpikeTrap.onSpikeTouched += TakeDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if ( hp.health <= 0)
            Death();
            
        Move();
        ChangeAnimationSpeed();
    }

    private void Move()
    {
        if (!isAlive)
            return;
        if (Time.timeScale == 0)
            return;
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

    public void TakeDamage(int damage)
    {
        AudioManager.Instance.PlaySound(AudioManager.SoundClips.PlayerHurtSfx);
        hp.TakeDamage(damage);
    }
    
    private void DealDamage(EnemyController enemy)
    {
        enemy.TakeDamage(fireboltDamage);
    }
    
    private void DealDamageToBoss(BossController boss)
    {
        boss.TakeDamage(fireboltDamage);
    }

    private void OnCollisionEnter(Collision collision)
    {
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
        if (Time.timeScale == 0)
            return;
        anim.SetTrigger(shoot);
        AudioManager.Instance.PlaySound(AudioManager.SoundClips.PlayerAttackSfx);
        FireBoltScript newFirebolt = Instantiate(firebolt, firePoint.transform.position, Quaternion.identity).GetComponent<FireBoltScript>();
        newFirebolt.SetDirection(PlayerModel.transform.forward, bulletSpeed);
        newFirebolt.hitAnEnemy.AddListener(DealDamage);
        newFirebolt.hitBoss.AddListener(DealDamageToBoss);
    }
    private void OnPause()
    {
        pauseButton.onClick.Invoke();
    }

    private void Death()
    {
        StartCoroutine(DeathSequence());
    }
    
    private IEnumerator DeathSequence()
    {
        isAlive = false;
        anim.SetBool("IsAlive", false);
        anim.SetLayerWeight(1, 0);
        
        
        yield return new WaitForSeconds(deathAnimationTime);
        
        sceneHandler.Load("LoseScene");
    }
}
