<h1 align="center">Login Seguro!</h1>

<p align="center">Noções gerais sobre APIs e segurança com C# 🔒</p>
<h1 align="center"><img src="https://media.discordapp.net/attachments/970409780604706926/1134647493058101418/image.png?width=798&height=468" width="800px"></h1>
<p align="center"><i>Representação final da tabela</i></p>


## 🤓 Sobre o Projeto

Esse é um projeto que iniciei com o intuito de aprofundar meus conhecimentos em Programação Orientada a Objetos e C#. A ideia foi desenvolver uma API de login utilizando banco de dados, armazenando os dados recebidos de forma segura.

Durante o processo de desenvolvimento, pude entender melhor e botar em prática bons conhecimentos sobre design patterns, principalmente SOLID. Além de entender um pouco mais sobre criptografia e segurança na hora de armazenar dados.

Além dessa documentação, o código é comentado nas partes mais importantes, faz parte do estudo.


| ***Saiba mais!***
Usei como referências os seguintes conteúdos:
- [Store Passwords Securely in Database using SHA256](https://juldhais.net/secure-way-to-store-passwords-in-database-using-sha256-asp-net-core-898128d1c4ef)
- [PBKDF2 Hashing Algorithm](https://nishothan-17.medium.com/pbkdf2-hashing-algorithm-841d5cc9178d)
- [Salt & Pepper: Spice up your hash!](https://medium.com/@berto168/salt-pepper-spice-up-your-hash-b48328caa2af)
- [Alura - C# com Orientação a Objetos](https://cursos.alura.com.br/formacao-c-sharp-orientacao-objetos)
- [Orientação a Objetos e SOLID para Ninjas - Mauricio Aniche](https://www.amazon.com.br/Orienta%C3%A7%C3%A3o-Objetos-SOLID-para-Ninjas-ebook/dp/B019OU0G5U)


##  🌊 Requisitos
Quer ver com seus próprios olhos? Veja o que precisa:
- **Visual Studio**: https://visualstudio.microsoft.com/pt-br/ 🎀
- **Git**: https://git-scm.com/book/pt-br/v2/ 🐈
- **MySQL Workbench** (Opcional): https://www.mysql.com/products/workbench/ 🐬

Lembre-se de instalar os Workloads de **.NET Desktop Development** e **ASP.NET and Web Development** para melhor aproveitamento.

Aliás, o Workbench é opcional, mas o MySQL é essencial para o funcionamento! 😃

## 🤔 Por onde eu começo?

Começa clonando o projeto com o Git (cê instalou o Git, né? 😡 ) 

```bash
   $ git clone https://github.com/vergonha/login_seguro
   $ cd login_seguro
``` 

Depois disso, você deve abrir o arquivo chamado `appsettings.json`, é nele que vamos colocar nossa Connection String do MySQL e escolher definir um Pepper para nossas senhas. 🔐

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "UserConnection": "server=localhost;database=users;user=root;password=root"
  },
  "Secrets": {
    "Pepper": "UmVkIEhvdCBDaGlsaSBQZXBwZXJz"
  }
}
```

Seu arquivo após preencher deve ficar parecido com esse. O Pepper no JSON é uma string de sua escolha convertida em Base64. 😊

[Mas o que é Pepper de verdade? Para que ele serve?](https://www.makeuseof.com/what-is-peppering-how-does-it-work/) 🌶️

Onde pesquisei, há quem diga que o Pepper deve variar de acordo com a senha, outros dizem que apenas o Salt deve variar. Nesse caso, manti o Pepper fixo (logicamente, após criptografado, o Hash vai ser diferente para cada senha no banco de dados). 👍

### Migrations no Banco de Dados 🐬

Você pode usar o NuGet Console indo por esse caminho no seu Visual Studio:
`Tools > NuGet Package Manager > Packet Manager Console` 

Ou você também pode utilizar seu Powershell para executar os seguintes comandos: 

```bash
Add-Migration NOME_DA_MIGRATION
Update-Database
```

É importante estar com o serviço do MySQL rodando, e também ter configurado corretamente a sua string connection. Sinta-se a vontade para me mandar uma mensagem se tiver dúvidas. 🙏

Depois disso, seu banco de dados vai estar configurado com as tabelas e colunas corretas. 🏢


## 📑 Documentação da API

#### Retorna todos os itens

```http
  POST /user/register
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `username` | `string` | **Obrigatório**. Seu nome de usuário |
| `email` | `string` | **Obrigatório**. Seu email |
| `password` | `string` | **Obrigatório**. Sua senha |

#### Registra seu usuário. Retorna ele mesmo. 😚
 
```http
  POST /user/login
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `username` | `string` | **Obrigatório**. Seu nome de usuário |
| `password` | `string` | **Obrigatório**. Sua senha |

#### Retorna uma indicação se a combinação é válida. 😉


---
