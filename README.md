
![Logo](https://i.ibb.co/y61VB2b/Send-Gen-logo.png)


## Desafio do projeto 📋📌

Desenvolver um middleware que permita a comunicação do ERP da empresa, escrito em Genexus, com a plataforma omnychannel Opa Suit, Durante o uso desta API popular uma base para futuro ML armazenando os gatilhos do sistema ERP acionados com a comunicação que cada gatilho acionou.


## Empresa Parceira

Infogen Sistemas - Software de Gestão e ERP - Chapecó-SC 

https://infogen.com.br/

Somos uma empresa de tecnologia, focada no desenvolvimento de software de gestão direcionado ao seguimento do agronegócio.




## Grupo 7 - Alunos 👩‍💻👨‍💻

- [@kikovander](https://www.github.com/kikovander)
- [@ionar](https://www.github.com/ionar)
- [@morganeafb](https://github.com/morganeafb)
-

## Stack utilizadas 💻

- Microsoft DotNet
- C#
- Microsoft SQL Server
- Git Hub


## Rodando localmente 📲🖥️

1. Instale os seguintes programas


- Visual Studio 2022 ou superior
- Microsoft SQL Server 2019 ou superior
- Gerenciador SQL Server 2019 ou superior

2. Crie um banco de dados no SQL Server com o nome

```bash
  SendGen
```

3. Clone o projeto

```bash
  git clone https://github.com/infogenunoesc/SendGen.git
```

4. Entre no diretório do projeto SendGen

```bash
  cd caminho/do/seu/diretorio  
```

5. Abra o arquivo

```bash
  code SendGen.Web.sln
```

6. Execute o comando no terminal do Visual Studio (Um de cada vez, na ordem abaixo)

```bash
  Update-Database -Context ApplicationDbContext
  Update-Database -Context SendGenContexto

```
7. Pronto. Compile e execute a aplicação. 

## Configurando Importação de Database (opcional) 💾

1. Mude a variavél "connectionString" do método "ObterClientes" do arquivo TransacionadoresRepository.cs em SendGen.Repository/SaborColonialRepositories/ para a conexão com a database desejada.

2. Acesse a página "ObterClientes/Processar" com o programa compilado para a importação ser completada. 

3. Tenha em mente que apenas funcionará sem a necessidade de mudanças mais severas caso a database seguir o modelo providenciado de exemplo!

## Funcionalidades 🛠️

- Cadastro de usuário/login
- CRUD clientes
- Envio de mensagem via template Opa Suite
- Importação de Database
- Agendamento de Mensagens

## Screenshots 🖼️

![App Screenshot](https://via.placeholder.com/468x300?text=App+Screenshot+Here)

