using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public float moveSpeed = 3f; // Velocidade de perseguição
    public float detectionRange = 5f; // Raio de detecção
    public float attackRange = 1.5f; // Distância para ataque
    public float attackCooldown = 2f; // Tempo entre ataques
    private Animator animator;
    private Transform heroi; // Referência ao jogador
    private float lastAttackTime; // Último tempo de ataque
    [Header("Configurações de Vida")]
    public int vidaMaxima = 3; // Vida máxima do inimigo
    private int vidaAtual;
    private void Start()
    {
        vidaAtual = vidaMaxima;
        animator = GetComponent<Animator>();
        // Encontra o jogador pelo nome "heroi"
        GameObject playerObject = GameObject.Find("heroi");
        if (playerObject != null)
        {
            heroi = playerObject.transform;
        }
        else
        {
            Debug.LogError("Jogador chamado 'heroi' não foi encontrado!");
        }
    }

    private void Update()
    {
        if (heroi == null) return; // Evita erros caso o jogador não tenha sido encontrado

        // Calcula a distância até o jogador
        float distanceToPlayer = Vector2.Distance(transform.position, heroi.position);

        if (distanceToPlayer < detectionRange && distanceToPlayer > attackRange)
        {
            // Persegue o jogador
            ChasePlayer();
            animator.SetInteger("estado", 1); // Estado movendo
        }
        else if (distanceToPlayer <= attackRange && Time.time > lastAttackTime + attackCooldown)
        {
            // Ataca o jogador
            AttackPlayer();

        }else
        {
            animator.SetInteger("estado", 0); // Estado movendo
        }
    }
    public void ReceberDano(int dano)
    {
        vidaAtual -= dano; // Reduz a vida pelo dano recebido
        Debug.Log("Inimigo recebeu dano: " + dano + ". Vida restante: " + vidaAtual);

        if (vidaAtual <= 0)
        {
            Morrer(); // Chama o método de morte se a vida for menor ou igual a 0
        }
    }

    private void Morrer()
    {
        Debug.Log("Inimigo morreu!");
        Destroy(gameObject); // Remove o inimigo da cena
    }
    
    private void ChasePlayer()
    {

        // Move-se em direção ao jogador
        transform.position = Vector2.MoveTowards(transform.position, heroi.position, moveSpeed * Time.deltaTime);

        // Inverte o inimigo para olhar na direção do jogador
        if (heroi.position.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1); // Olha para a esquerda
        else
            transform.localScale = new Vector3(1, 1, 1); // Olha para a direita
    }
    
    private void AttackPlayer()
    {
        animator.SetTrigger("ataque");
    }
}
