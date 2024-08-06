using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Vida do inimigo
    public float health = 100f;

    // Velocidade de movimento do inimigo
    public float moveSpeed = 5f;

    // Tamanho da �rea circular de persegui��o
    public float pursuitRadius = 10f;

    // Refer�ncia ao player
    private Transform player;

    // Dano do tiro
    public float bulletDamage = 10f;

    // Refer�ncia ao componente Rigidbody do inimigo
    private Rigidbody enemyBody;

    void Start()
    {
        // Encontrar o objeto Player na cena
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Obter o componente Rigidbody do inimigo
        enemyBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Verificar se o player est� dentro da �rea circular de persegui��o
        if (Vector3.Distance(transform.position, player.position) <= pursuitRadius)
        {
            // Movimentar o inimigo em dire��o ao player
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        // Calcular a dire��o do player em rela��o ao inimigo
        Vector3 direction = (player.position - transform.position).normalized;

        // Movimentar o inimigo na dire��o do player
        enemyBody.velocity = direction * moveSpeed;
    }

    // Fun��o chamada quando o inimigo � atingido por um tiro do player
    void OnTriggerEnter(Collider other)
    {
        // Verificar se o objeto que colidiu � uma bala do player
        if (other.gameObject.CompareTag("bullet"))
        {
            // Tomar dano do tiro
            TakeDamage(10f);

            //Destruir tiro do player
            Destroy(other.gameObject);
        }
    }

    // Fun��o que aplica dano ao inimigo
    void TakeDamage(float damage)
    {
        // Reduzir a vida do inimigo
        health -= damage;

        // Verificar se o inimigo morreu
        if (health <= 0f)
        {
            Die();
        }
    }

    // Fun��o chamada quando o inimigo morre
    void Die()
    {
        // Destruir o inimigo
        Destroy(gameObject);
    }

    // Gizmo para desenhar a �rea circular de persegui��o
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pursuitRadius);
    }
}