using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    public float velocidade = 0.25f;
    public GameObject objetoTiro;
    public GameObject objetoInimigo;
    public float tempoNascer = 2;
    private float tempoNascerAux;
    public float distanciaNascerMin = 10;
    public float distanciaNascerMax = 20;

    public TextMesh score;
    public TextMesh scoreInimigo;
    public static int scoreCount;
    public static int scoreInimigoCount;

    // Use this for initialization
    void Start()
    {
        tempoNascerAux = tempoNascer;
        scoreCount = 0;
        scoreInimigoCount = 0;        
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score Jogador: " + scoreCount;

        //Game Over
        if (scoreInimigoCount > 0)
        {
            scoreInimigo.characterSize = 2;
            Destroy(this);
        }
        
        //Aumenta o nivel de dificuldade
        if ( (scoreCount > 20) && (tempoNascer > 0.7f))
        {
            tempoNascer = 0.7f;
            tempoNascerAux = tempoNascer;
        }
        else if ((scoreCount > 30) && (tempoNascer > 0.6f))
        {
            tempoNascer = 0.6f;
            tempoNascerAux = tempoNascer;
        }
        else if ((scoreCount > 40) && (tempoNascer > 0.5f))
        {
            tempoNascer = 0.5f;
            tempoNascerAux = tempoNascer;
        }
        else if ((scoreCount > 50) && (tempoNascer > 0.3f))
        {
            tempoNascer = 0.3f;
            tempoNascerAux = tempoNascer;
        }

        //posição do mouse
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = transform.position.z;

        //Movimentar
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left.normalized * velocidade;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right.normalized * velocidade;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up.normalized * velocidade;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down.normalized * velocidade;
        }

        //Atirar
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 vetorTiro = (mouse - transform.position).normalized;
            vetorTiro = transform.position + vetorTiro;

            Instantiate(objetoTiro, vetorTiro, Quaternion.identity);
        }

        //Criar inimigo
        tempoNascer -= Time.deltaTime;
        if (tempoNascer < 0)
        {
            Vector3 nascer = new Vector3(posicaoInimigo(transform.position.x),
                                         posicaoInimigo(transform.position.y),
                                         0);

            Instantiate(objetoInimigo, nascer, Quaternion.identity);
            tempoNascer = tempoNascerAux;
        }

        //Girar jogador de acordo com a posição do mouse
        float angulo = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;        
        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }

    float posicaoInimigo(float posicao)
    {
        float distancia = Random.Range(distanciaNascerMin, distanciaNascerMax);
        return (posicao > 0 ? posicao + distancia : posicao + (distancia * -1));
    }
    
}
