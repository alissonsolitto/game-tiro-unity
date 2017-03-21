# Crazy galaxy
Este repositório contém um projeto simples de um jogo de tiro utilizando Unity e C#

# Sobre
Este é um jogo simples que possui algumas funções utilizando a matemática de vetores (Adição, Subtração, Produto Escalar, ...)

# Principais Características
O jogo possui 3 scripts  desenvolvidos em C#

* **Jogador**
  - Movimentação da nave entre os eixos
  - Rotação da nave acompanha a posição do mouse
  - Disparar projetil na direção do mouse
  - Criação de inimigos dinamicamente no cenário de acordo com a distância parametrizada

* **Inimigo**
  - O inimigo persegue o jogador apenas quando o jogador estiver no seu angulo de visão
  - A velocidade do inimigo aumenta quando estiver muito distante da nave
  - O inimigo rotaciona automaticamente caso a nave não esteja no seu campo de visão
  - Trigger de colisão quando o tiro acerta o inimigo, ou o inimigo atinge o jogador

* **Tiro**
  - Projetil que 
 

# Scripts

## Jogador

Movimentação do jogador

	float hor = Input.GetAxis("Horizontal");
	float vert = Input.GetAxis("Vertical");

	transform.position += (new Vector3(hor, vert, 0).normalized * velocidade); 
	
Disparar projetil
	
	Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mouse.z = transform.position.z;
	
	if (Input.GetMouseButtonDown(0))
	{
		Vector3 vetorTiro = (mouse - transform.position).normalized;
		vetorTiro = transform.position + vetorTiro;

		Instantiate(objetoTiro, vetorTiro, Quaternion.identity);
	}

Criar inimigo dinamicamente

    float posicaoInimigo(float posicao)
    {
        float distancia = Random.Range(distanciaNascerMin, distanciaNascerMax);
        return (posicao > 0 ? posicao + distancia : posicao + (distancia * -1));
    }

	tempoNascer -= Time.deltaTime;
	if (tempoNascer < 0)
	{
		Vector3 nascer = new Vector3(posicaoInimigo(transform.position.x),
									 posicaoInimigo(transform.position.y),
									 0);

		Instantiate(objetoInimigo, nascer, Quaternion.identity);
		tempoNascer = tempoNascerAux;
	}
	
Rotacionar jogador na posição do mouse

	float angulo = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;        
	transform.rotation = Quaternion.Euler(0, 0, angulo);
	

## Inimigo

Descobrindo o angulo de visão do inimigo

	float anguloRotacao = transform.eulerAngles.z;
	float eixoX = Mathf.Cos(anguloRotacao * Mathf.Deg2Rad);
	float eixoY = Mathf.Sin(anguloRotacao * Mathf.Deg2Rad);

	Vector3 playerDir = new Vector3(eixoX, eixoY, 0).normalized;
	Vector3 vetorVisao = (jogador.position - transform.position).normalized;

	float targetAngle = Vector3.Dot(playerDir, vetorVisao); //Angulo de visão

O inimigo anda caso a nave esteja no seu angulo de visão, se estiver muito distante a velocidade é aumentada
	
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

Rotaciona o inimigo automaticamente caso a nave não esteja no seu angulo de visão
	
	else //Gira o inimigo se não estiver vendo o jogador
	{
		Vector3 rotate = new Vector3(0.0f, 0.0f, (velocidadeRotacao * Time.deltaTime));
		transform.Rotate(0.0f, 0.0f, (velocidadeRotacao * Time.deltaTime));
	}  
	
# Capturas de tela do jogo

![Screen](https://github.com/alissonsolitto/game-tiro-unity/img/screen0.png)

![Screen](https://github.com/alissonsolitto/game-tiro-unity/img/screen01.png)

![Screen](https://github.com/alissonsolitto/game-tiro-unity/img/screen02.png)

# Atualizações
- 2017 Ano de Desenvolvimento
