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

    void Awake()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        cc = GetComponent<CharacterController>();
        move = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.lossyScale.x / 2);
        foreach (Collider col in colliders)
        {
            if (col.GetComponent<HeroController>())
            {
                if (gc.MinotaurHasEnough())
                {
                    Destroy(col.gameObject);
                    //TODO victory
                }
                else
                {
                    //TODO defeat
                }
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
