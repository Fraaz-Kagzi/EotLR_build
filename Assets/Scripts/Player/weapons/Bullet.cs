using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Bullet : MonoBehaviour
{
    //Assignables
    public Rigidbody rb;
    public LayerMask whatIsEnemies;

    //Stats
    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity;

    //Damage
    public int explosionDamage;
    public float explosionRange;
    public float explosionForce;

    //Lifetime
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;

    int collisions;
    PhysicMaterial physics_mat;

    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        //When to explode:
        if (collisions > maxCollisions) Explode();

        //Count down lifetime
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Explode();
    }

    private void Explode()
    {
        //Debug.Log("Explode");


        //Check for enemies 
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        for (int i = 0; i < enemies.Length; i++)
        {
            // Get the component of the collided object
            Enemy enemyComponent = enemies[i].GetComponent<Enemy>();
            Ork orkComponent = enemies[i].GetComponent<Ork>();
            Guardian guardComponent = enemies[i].GetComponent<Guardian>();
            DragonBoss dragonComponent = enemies[i].GetComponent<DragonBoss>();
            OrkClone orkCloneComponent = enemies[i].GetComponent<OrkClone>();
            Shadowmancer shadowComponent = enemies[i].GetComponent<Shadowmancer>();

            // Check if it's a general enemy and has the Enemy component
            if (enemyComponent != null)
            {
                // Call TakeDamage on the Enemy component
                enemyComponent.TakeDamage(explosionDamage);
            }
            else if (guardComponent != null)
            {

                guardComponent.TakeDamage(explosionDamage);
                Debug.Log("Guardian Hit!");
            }
            // Check if it's an Ork enemy and has the Ork component
            else if (orkComponent != null)
            {

                orkComponent.TakeDamage(explosionDamage);
                Debug.Log("Ork Hit!");
            }
            else if (dragonComponent != null)
            {

                dragonComponent.TakeDamage(explosionDamage);
                Debug.Log("dragon Hit!");
            }
            else if (orkCloneComponent != null)
            {

                orkCloneComponent.TakeDamage(explosionDamage);
                Debug.Log("Ork Clone Hit!");
            }
            else if (shadowComponent != null)
            {

                shadowComponent.TakeDamage(explosionDamage);
                Debug.Log("Shadowmancer Hit!");
            }
        }

        //Add a little delay, just to make sure everything works fine
        Invoke("Delay", 0.05f);

    }
    private void Delay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Don't count collisions with other bullets
        if (collision.collider.CompareTag("Bullet")) return;

        //Count up collisions
        collisions++;

        //Explode if bullet hits an enemy directly and explodeOnTouch is activated
        if (collision.collider.CompareTag("Enemy") && explodeOnTouch) Explode();
        else if (collision.collider.CompareTag("Ork") && explodeOnTouch) Explode();
        else if (collision.collider.CompareTag("Guardian") && explodeOnTouch) Explode();
        else if (collision.collider.CompareTag("Dragon") && explodeOnTouch) Explode();
        else if (collision.collider.CompareTag("OrkClone") && explodeOnTouch) Explode();
        else if (collision.collider.CompareTag("Shadowmancer") && explodeOnTouch) Explode();
    }

    private void Setup()
    {
        //Create a new Physic material
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        //Assign material to collider
        GetComponent<SphereCollider>().material = physics_mat;

        //Set gravity
        rb.useGravity = useGravity;
    }

}
