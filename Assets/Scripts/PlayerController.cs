using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    public float speed;
    CharacterController cc;
    [SerializeField]
    private Vector3 move;

    GameController gc;
    public AudioClipPlayer footsteps;
    float stepTimer = 0;
    float stepInterval;

    void Awake()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        cc = GetComponent<CharacterController>();
        move = new Vector3();
        stepInterval = speed / 3;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0)
        {
            stepTimer += Time.deltaTime;
            if(stepTimer >= stepInterval)
            {
                stepTimer = 0;
                footsteps.PlayRandom();
            }
        }

        float lastY = move.y;
        move.x = Input.GetAxis("Horizontal");
        move.y = 0;
        move.z = Input.GetAxis("Vertical");

        move = transform.TransformDirection(move);
        move.x *= speed;
        move.z *= speed;
        move.y = lastY;
        cc.Move(move * Time.deltaTime);
    }
}
