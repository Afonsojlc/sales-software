# 🚀 Gestor Comercial & Automação de Encomendas

![Status do Projeto](https://img.shields.io/badge/Status-Em_Desenvolvimento-yellow)
![.NET](https://img.shields.io/badge/.NET-Windows_Forms-purple)
![Language](https://img.shields.io/badge/Linguagem-C%23-blue)
![Database](https://img.shields.io/badge/Database-SQL_Server-red)

## 📖 Sobre o Projeto

Este projeto está a ser desenvolvido no âmbito do **CTeSP de Tecnologias de Programação de Sistemas de Informação** do **IPMAIA** (1.º Ano).

O objetivo é criar uma solução de software real para apoiar a atividade de um **Comercial de Vendas** (focado na área de material elétrico). A aplicação visa substituir o preenchimento manual de notas de encomenda, agilizar o envio de informação para a empresa e fornecer métricas financeiras pessoais ao vendedor.

### 🎯 Problema a Resolver
Atualmente, o processo de venda envolve preenchimento manual de papelada e cálculos manuais de comissões. Este projeto digitaliza todo o fluxo, desde a escolha do produto no catálogo até ao envio do email final.

---

## ✨ Funcionalidades Principais

* **⚡ Gestão de Encomendas:** Interface intuitiva para seleção de Clientes e Produtos (baseada em catálogo real), com cálculo automático de totais e IVA.
* **📄 Geração de PDF (Nota de Encomenda):** A aplicação preenche automaticamente os dados da venda sobre o modelo oficial da nota de encomenda da empresa, respeitando o layout original.
* **📧 Automação de Email:** Envio automático da encomenda finalizada para a sede da empresa via SMTP.
* **💰 Dashboard de Comissões:**
    * Cálculo automático dos ganhos do vendedor com base na percentagem de comissão definida.
    * Visualização do total vendido no dia e previsão de ganhos mensais.
* **🗄️ Base de Dados:** Histórico completo e persistente de Clientes, Catálogo de Produtos e Vendas passadas.

---

## 🛠️ Tecnologias Usadas

* **Linguagem:** C# (.NET Framework)
* **Interface Gráfica:** Windows Forms (WinForms)
* **Base de Dados:** Microsoft SQL Server
* **IDE:** Visual Studio 2022 Enterprise
* **Modelagem:** Dia Portable (Diagramas E-R)

---

## 🗂️ Estrutura da Base de Dados

O projeto segue um modelo relacional normalizado, estruturado da seguinte forma:

1.  **Clientes:** Gestão de dados pessoais, NIF (único), moradas e contactos.
2.  **Tipo_Produto:** Categorização do material (ex: Iluminação, Cablagem, Aparelhagem).
3.  **Material:** Catálogo de produtos com Referência, Descrição, Preço Unitário, Unidade de Venda e Stock.
4.  **Encomenda:** Cabeçalho da transação (Cliente, Data, Valor Total, Estado).
5.  **Linha_Encomenda:** Detalhes da venda (Produto, Quantidade, Preço de Venda, Descontos).

---

## 🚀 Como Executar (Em Breve)

1.  **Clonar o repositório:**
    ```bash
    git clone [https://github.com/Afonsojlc/SalesManager.git](https://github.com/Afonsojlc/SalesManager.git)
    ```
2.  **Base de Dados:**
    * Importar o script SQL (disponível na pasta `/Database`) para o SQL Server Management Studio.
    * Configurar a *Connection String* no ficheiro `App.config`.
3.  **Executar:**
    * Abrir a solução `.sln` no Visual Studio 2022 e iniciar o projeto.

---

## 🔮 Futuras Melhorias

* [ ] Implementação de sistema de Login.
* [ ] Filtros avançados de pesquisa de produtos por referência.
* [ ] Exportação de relatórios mensais em Excel.

---

## 👨‍💻 Autor

**Afonso**
* Estudante do IPMAIA - CTeSP TPSI
* GitHub: [https://github.com/Afonsojlc](https://github.com/Afonsojlc)