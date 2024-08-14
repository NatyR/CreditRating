# 🚀 CreditRating Project


## 📝 Descrição

O projeto **CreditRating** é uma solução para o gerenciamento de propostas de crédito e emissão de cartões de crédito. Ele é composto por três APIs principais:

- **Customer.API**: Gerencia as informações dos clientes;
- **Proposal.API**: Gerencia as propostas de crédito, realizando a análise a partir dos dados dos clientes;
- **Card.API**: Emite cartões de crédito para propostas aprovadas.


## 🛠️ Tecnologias Utilizadas

- **.NET 8**: Plataforma principal para o desenvolvimento das APIs.
- **RabbitMQ**: Utilizado para comunicação assíncrona entre as APIs.
- **Swagger**: Utilizado para documentação;
- **xUnit**: Framework de testes utilizado para garantir a qualidade do código.
- **Moq**: Biblioteca para criação de mocks nos testes unitários.

## ⚙️ Instalação

1. Clone o repositório:

   ```bash
   git clone https://github.com/NatyR/CreditRating.git
   cd creditrating

2. Para esse projeto, utilizei um Rabbit criado a partir do Docker:

   ```bash
    docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management


3. Instalação de pacotes e execução do projeto:

   ```bash
    docker restore
    dotnet run


3. Acesso ao Rabbit:

   ```bash
    http://localhost:15672    
    Login: guest
    Pass: guest


4. Filas necessárias:
   
![image](https://github.com/user-attachments/assets/e37d7ccf-8671-40e1-a728-52e2af55dcd8)
   

Feito com ❤️ por Renata Felix 🚀

    

