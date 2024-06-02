using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rgbd2d;
    [HideInInspector]
    public Vector3 movmentVector;
    [HideInInspector]
    public float lastHorizontalDeCoupledvector;
    [HideInInspector]
    public float lastVerticalDeCoupledvector;


    [HideInInspector]
    public float lastHorizontalCoupledvector;
    [HideInInspector]
    public float lastVerticalCoupledvector;



    [SerializeField] float speed = 3f;

    Animate animate;



    private void Awake()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        movmentVector = new Vector3();
        animate = GetComponent<Animate>();

    }

    private void Start()
    {
        lastHorizontalDeCoupledvector = -1f;
        lastVerticalDeCoupledvector = 1f;

        lastHorizontalCoupledvector = -1f;
        lastVerticalCoupledvector = 1f;

    }


    
    void Update()
    {
        movmentVector.x =  Input.GetAxisRaw("Horizontal");
        movmentVector.y = Input.GetAxisRaw("Vertical");

        if (movmentVector.x != 0 || movmentVector.y != 0)
        {
            lastHorizontalCoupledvector = movmentVector.x;
            lastVerticalCoupledvector = movmentVector.y;
        }

        if(movmentVector.x != 0) 
        {
            lastHorizontalDeCoupledvector = movmentVector.x;
        }
        if(movmentVector.y != 0) 
        {
            lastVerticalDeCoupledvector = movmentVector.y;
        }

        animate.horizontal = movmentVector.x;


        movmentVector *= speed;


        rgbd2d.velocity = movmentVector;

    }
}
