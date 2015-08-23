using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    public float speed, chargeSpeed, chargeDuration, chargeCooldown;
    public Image chargeIcon;
    CharacterController cc;
    [SerializeField]
    private Vector3 move;

    GameController gc;
    public AudioClipPlayer footsteps;
    float stepTimer = 0;
    float stepInterval;

    bool canCharge = true;
    bool charging = false;
    float chargeTimer;
    float chargeCooldownTimer;

    void Awake()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        cc = GetComponent<CharacterController>();
        move = new Vector3();
        
    }

    // Update is called once per frame
    void Update()
    {
        stepInterval = 1.5f/(charging ? chargeSpeed : speed);

        if(Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0 || charging)
        {
            stepTimer += Time.deltaTime;
            if(stepTimer >= stepInterval)
            {
                stepTimer = 0;
                footsteps.PlayRandom();
            }
        }

        if(charging)
        {
            chargeTimer -= Time.deltaTime;
            if(chargeTimer <= 0)
            {
                charging = false;
                
            }
        }

        if(!canCharge)
        {
            chargeCooldownTimer -= Time.deltaTime;
            chargeIcon.fillAmount = 1 - chargeCooldownTimer / chargeCooldown;
            if(chargeCooldownTimer <= 0)
            {
                canCharge = true;
                
            }
        }

        if(Input.GetAxis("Fire2") > 0 && canCharge)
        {
            charging = true;
            canCharge = false;
            chargeTimer = chargeDuration;
            chargeCooldownTimer = chargeCooldown;
            chargeIcon.fillAmount = 0;
        }

        float lastY = move.y;
        move.x = Input.GetAxis("Horizontal");
        move.y = 0;
        move.z = charging ? 1 : Input.GetAxis("Vertical");

        move = transform.TransformDirection(move);
        move.x *= charging ? chargeSpeed : speed;
        move.z *= charging ? chargeSpeed : speed;
        move.y = lastY;
        cc.Move(move * Time.deltaTime);
    }
}
