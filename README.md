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
- [@costaangelo](https://github.com/costaangelo) 

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

1. Mude a variável "connectionString" do método "ObterClientes" do arquivo TransacionadoresRepository.cs em SendGen.Repository/SaborColonialRepositories/ para a conexão com a database desejada.

2. Verifique caso a condição sendo usada para formar a lista em "List<Transacionadores> lista" está adequada para sua aplicação (a database deverá conter colunas com os mesmos nomes que estão na condição para o programa funcionar corretamente).

3. Acesse a página "ObterClientes/Processar" com o programa compilado para a importação ser feita.

## Funcionalidades 🛠️

- Cadastro de usuário/login
- CRUD clientes
- Envio de mensagem via template Opa Suite
- Importação de Database
- Agendamento de Mensagens

## Screenshots 🖼️

![Login](https://github.com/infogenunoesc/SendGen/assets/124840306/accff980-20c2-4927-81d9-6bb5c6e653a8)
![Clientes](https://github.com/infogenunoesc/SendGen/assets/124840306/dd66db93-b43b-4275-92db-1c839534fbcc)
![Filtros](https://github.com/infogenunoesc/SendGen/assets/124840306/5556265b-f0f7-4e48-a4bf-693938903c49)
![Agendamentos](https://github.com/infogenunoesc/SendGen/assets/124840306/53f5d212-5cca-421e-9e27-293fe88a3219)


