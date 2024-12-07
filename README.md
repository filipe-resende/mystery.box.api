<h1 align="center">Mystery GiftCard API 👋</h1>

<h3 align="left">📖 Sobre o Projeto</h3>

<p align="left">O <strong>Mystery GiftCard API</strong> é uma aplicação dedicada à venda online de gift cards misteriosos para jogos. Este projeto combina tecnologias modernas e boas práticas de engenharia de software para fornecer uma solução robusta e escalável.</p>

<ul>
  <li><strong>Tecnologias e padrões utilizados:</strong></li>
  <ul>
    <li><strong>C#</strong> (.NET 6)</li>
    <li><strong>ASP.NET Core Web API</li>
    <li><strong>Entity Framework Core</strong> (Code First e Migrations)</li>
    <li><strong>SQL Server</strong> (banco de dados relacional)</li>
    <li><strong>MediatR</strong> (implementação do padrão Mediator)</li>
  </ul>
  <li>Diversos padrões de projeto, incluindo:</li>
  <ul>
    <li>Dependency Injection (DI)</li>
    <li>Repository Pattern</li>
    <li>CQRS (Command Query Responsibility Segregation)</li>
  </ul>
</ul>

<h3 align="left">🔧 Configuração e Instalação</h3>

<ol>
  <li><strong>Instale as dependências necessárias:</strong>
    <ul>
      <li>Visual Studio 2022</li>
      <li>SQL Server</li>
    </ul>
  </li>
  <li><strong>Clone o repositório:</strong>
    <pre><code>git clone &lt;URL_DO_REPOSITORIO&gt;</code></pre>
  </li>
  <li><strong>Configure o banco de dados:</strong>
    <p>Abrir o arquivo <code>appsettings.json</code> na pasta <code>src/MysteryGiftCard.API</code>.</p>
    <p>Insira sua string de conexão:</p>
    <pre><code>
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=GiftCardDB;User Id=SEU_USUARIO;Password=SUA_SENHA;"
}
    </code></pre>
  </li>
  <li><strong>Compile e aplique as migrações:</strong>
    <p>No Visual Studio, compile pressionando <strong>Ctrl + Shift + B</strong>.</p>
    <p>No PMC (Package Manager Console), selecione <code>MysteryGiftCard.Infrastructure</code> como projeto padrão e execute:</p>
    <pre><code>Update-Database</code></pre>
  </li>
  <li><strong>Execute o projeto:</strong>
    <p>No Visual Studio, pressione <strong>F5</strong>.</p>
  </li>
</ol>

<h3 align="left">💡 Como Usar</h3>

<p align="left">Após iniciar a aplicação, você pode acessar a documentação Swagger no endpoint <code>/swagger</code>. A API inclui endpoints para gerenciar e comprar gift cards misteriosos.</p>

<h3 align="left">🛠 Tecnologias e Ferramentas</h3>

<div align="left">
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" height="40" alt="csharp logo" />
  <img width="12" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dot-net/dot-net-plain-wordmark.svg" height="40" alt="dot-net logo" />
  <img width="12" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/sqlserver/sqlserver-plain-wordmark.svg" height="40" alt="sql server logo" />
  <img width="12" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/docker/docker-plain-wordmark.svg" height="40" alt="docker logo" />
</div>

<h3 align="left">📫 Contato</h3>

<p align="left">Desenvolvido por Filipe de Castro.<br>
- <strong>Email:</strong> filipedecastroresende@gmail.com<br>
- <strong>GitHub:</strong> <a href="https://github.com/filipe-resende">GitHub</a><br>
- <strong>LinkedIn:</strong> <a href="https://www.linkedin.com/in/filipe-resende">LinkedIn</a></p>

<h3 align="left">📝 Licença</h3>

<p align="left">Este projeto está licenciado sob os termos da Licença Apache 2.0.</p>
