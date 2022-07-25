using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    public float speed;
    public Rigidbody2D body;
    public Vector2 direction;
    public int maxHealth = 3;
    int currentHealth;
    public Animator animator;
    public Transform enemyAttack;
    public float attackRange = 0.5f;
    public LayerMask player;
    private int frame;
    public int attackDamage = 1;



   void Start(){
        currentHealth = maxHealth;
        body = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }//end start

    public void TakeDamage(int damage){
        currentHealth -= damage;

        if(currentHealth <= 0 ){
            Die();
        }
    }

    void Die(){
        Destroy(gameObject, .5f);
    }

    void Update(){
        if(frame % 222 == 0){
            ChangeDirection();
        }
         
            if(!(body.velocity.x >3 || body.velocity.y >3 || body.velocity.x < -3 || body.velocity.y < -3)){
                body.velocity += direction; 
        }//end if hor != and ver !=

        frame++; //updating the frame

         if(frame % 222 == 0 && Random.Range(0,8) == 3){
            Attack();
        }
    }//end update

    public void ChangeDirection(){
        int newDirection  = Random.Range(0,8);
        switch(newDirection){
            case 0:
                direction = new Vector2(speed, 0);
                break;
            case 1:
                direction = new Vector2(0.5f * speed, 0.5f * speed);
                break;
            case 2:
                direction = new Vector2(0, speed);
                break;
            case 3:
                direction = new Vector2(-0.5f * speed, 0.5f * speed);
                break;
            case 4:
                direction = new Vector2(-speed, 0);
                break;
            case 5:
                direction = new Vector2(-0.5f * speed, -0.5f * speed);
                break;
            case 6:
                direction = new Vector2(0, -speed);
                break;
            case 7:
                direction = new Vector2(0.5f * speed, -0.5f * speed);
                break;
        }//end switch
    }// end Change Direction
    
    void Attack(){
        // //attack animation
        // animator.SetTrigger("Attack");

        //dececting enemies:
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(enemyAttack.position, attackRange, player);

        //damage them
        foreach(Collider2D player in hitPlayer){
            player.GetComponent<PlayerCharacter>().TakeDamage(attackDamage);
            Debug.Log("Hit" + player.gameObject);
            // Debug.Log("Hit" + (PlayerCharacter)(player.gameObject).hitPoints);
        }
    }//end attack
    private void OnDrawGizmosSelected(){
        if(enemyAttack == null){
            return;
        }
        Gizmos.DrawWireSphere(enemyAttack.position, attackRange); 
    }//end Draw
}//end enemy class
