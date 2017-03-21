using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour {

    private Transform jogador;
    public AudioClip audio;
    public float velocidade = 0.1f;
    public float distancia = 15;
    public float multiplicador = 3;
    public float viewConeAngleCosine = 0.8f;
    public float velocidadeRotacao = 50;

    // Use this for initialization
    void Start () {

        jogador = GameObject.Find("jogador").transform;
    }
    
    // Update is called once per frame
    void Update () {

        //Angulo de rotação do inimigo
        float anguloRotacao = transform.eulerAngles.z;
        float eixoX = Mathf.Cos(anguloRotacao * Mathf.Deg2Rad);
        float eixoY = Mathf.Sin(anguloRotacao * Mathf.Deg2Rad);

        Vector3 playerDir = new Vector3(eixoX, eixoY, 0).normalized;
        Vector3 vetorVisao = (jogador.position - transform.position).normalized;

        float targetAngle = Vector3.Dot(playerDir, vetorVisao); //Angulo de visão
                
        if (targetAngle > viewConeAngleCosine)
        {
            if (Vector3.Distance(jogador.position, transform.position) > distancia)
            {
                transform.position += ((jogador.position - transform.position).normalized) * velocidade * multiplicador;
            }
            else
            {
                transform.position += ((jogador.position - transform.position).normalized) * velocidade;
            }            
        }
        else //Gira o inimigo se não estiver vendo o jogador
        {
            Vector3 rotate = new Vector3(0.0f, 0.0f, (velocidadeRotacao * Time.deltaTime));
            transform.Rotate(0.0f, 0.0f, (velocidadeRotacao * Time.deltaTime));
        }        
    }
    
    void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(audio, Vector3.zero);

        if (other.name.Equals("jogador"))
        {
            Jogador.scoreInimigoCount++;
        }
        else
        {
            Destroy(other.gameObject);
        }

        Destroy(this.gameObject);
        Jogador.scoreCount++;
    }
}
