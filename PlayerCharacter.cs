using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour{
    public float speed;
    public Rigidbody2D body;
    public int maxHealth = 3;
    int currentHealth;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemy;

    public int attackDamage = 1;

    public List<Enemy> enemies = new List<Enemy>();

    Vector2 movement;

    void Start(){
        currentHealth = maxHealth;
        body = GetComponent<Rigidbody2D>();
        animator.GetComponent<Animator>();
    }//end
    // Update is called once per frame
    void Update(){
        //inputs
        float hor = Input.GetAxis("Horizontal"); //not doing any animations
        float ver = Input.GetAxis("Vertical");
        // moving in like sqaures.
        // Debug.Log (hor + "," + ver); 
        if (hor != 0 || ver !=0){
            Vector2 move = new Vector2(speed*hor, speed*ver);
            if(!(body.velocity.x >3 || body.velocity.y >3 || body.velocity.x < -3 || body.velocity.y < -3)){
                body.velocity += move;
            }

        }
        
        if(Input.GetKeyDown(KeyCode.Space)){
            Attack();

        }
    }//end
    void Attack(){
        //attack animation
        animator.SetTrigger("Attack");

        //dececting enemies:
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemy);

        //damage them
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            Debug.Log("We Hit" + enemy.tag);
        }
    }

   public void TakeDamage(int damage){
        currentHealth -= damage;

        if(currentHealth <= 0 ){
            Die();
        }
    }

    void Die(){
        Destroy(gameObject, .5f);
        SceneManager.LoadScene("Menu");
    }

    private void OnDrawGizmosSelected(){
        if(attackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    
}//end player class
