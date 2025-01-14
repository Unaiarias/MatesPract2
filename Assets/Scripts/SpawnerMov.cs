using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SpawnerMov : MonoBehaviour
{
    public float velocidadSpawner;

    public GameObject Bala;
    public Animator EnemyAnimator;
    public float AlertRange = 10f;
    public LayerMask PlayerMask;
    public bool IsAlert;
   
    void Start()
    {
        
    }

    
    void Update()
    {
        //transform.Translate(0f, 0f, velocidadSpawner * Time.deltaTime);

        IsAlert = Physics.CheckSphere(transform.position, PlayerMask);
        

        if (IsAlert)
        {
            EnemyAnimator.SetBool("Disparo", true);
        }
        else
        {
            EnemyAnimator.SetBool("Disparo", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, AlertRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
    }


}
