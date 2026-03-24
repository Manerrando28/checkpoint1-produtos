# Checkpoint 1 – Sistema de Produtos em C#

## 📌 Integrantes
- Nome: Gabriel  
- RM: [558638]  
- Turma: [3ESHP]  

---

## 📖 Descrição
Aplicação de console em **C#** para gerenciamento de produtos.  
O sistema permite cadastrar, listar, buscar, atualizar estoque, calcular valor total, excluir produtos, gerar relatórios, ordenar e importar/exportar dados em **CSV**.  

---

## 🚀 Como executar
1. Certifique-se de ter o **.NET 8 SDK** instalado.  
   - Verifique com:  
     dotnet --list-sdks
2. Clone ou extraia o projeto.  
3. No terminal, dentro da pasta do projeto, execute:  
   dotnet run

---

## 📂 Estrutura do projeto
- Program.cs → contém o menu e todas as funcionalidades.  
- Produto.cs → classe que define os atributos dos produtos.  
- produtos.csv → arquivo de dados persistidos.  
- Evidencias.docx → documento com prints e explicações das funcionalidades.  

---

## 🛠️ Funcionalidades
1. Cadastrar produto (valida ID único e não permite valores negativos).  
2. Listar produtos (mostra todos os itens carregados do CSV).  
3. Buscar produto (por ID ou Nome).  
4. Atualizar estoque (não permite valores negativos).  
5. Calcular valor total em estoque (preço × quantidade).  
6. Excluir produto (remove da lista).  
7. Relatório geral (subtotal por produto, quantidade total e valor total).  
8. Ordenar produtos (por nome ou preço).  
9. Exportar para CSV (gera arquivo para abrir no Excel).  
10. Importar de CSV (carrega dados do arquivo).  
0. Sair (salva automaticamente em CSV).  

---

## ✨ Diferenciais
- Persistência em CSV (compatível com Excel).  
- Validações de entrada (sem valores negativos ou IDs duplicados).  
- Relatório consolidado com totais.  
- Ordenação por nome ou preço.  

---

## 📷 Evidências
As evidências estão documentadas no arquivo **Evidencias.docx**, com prints e legendas de cada funcionalidade.
