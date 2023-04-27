# DesafioRPA

O desafio consiste no desenvolvimento de um RPA simples que realiza uma busca
automaizada no site da Alura (https://www.alura.com.br/) e grava os resultados em um banco de dados (mais informações abaixo).
Você não precisa se preocupar com o design. Esse não é o objetivo do desafio.

## Os pré-requisitos são:
1. Que o código seja feito em C#;
2. Utilização do framework Selenium;
3. Utilização da abordagem DDD com injeção de dependência;

## Seu projeto será avaliado de acordo com os seguintes critérios:
1. Sua aplicação preenche os requisitos básicos;
2. Manutenabilidade, clareza e limpeza de código, resultado funcional, entre outros fatores;
3. Explique as decisões técnicas tomadas, as escolhas por bibliotecas e ferrramentas;
4. Fluxo da aplicação;
5. Se você tratou bem com erros e casos inesperados;
6. Se usou Webdriver;
7. Se fez uso do GitFlow;

## Você ganha mais pontos se:
1. Criar validações de erros, caso o dado não exista ou campo da busca não existir;
2. Boa documentação de código e de serviços;

## Fora isso, sinta-se a vontade para:
• Usar qualquer forma de persistência de dados;


# Como utilizar:
Através do executável gerado, pode-se executar a função de busca de duas formas:

1. **>DesafioRPA_AeC.exe [nome do curso]**
2. **>DesafioRPA_AeC.exe [nome do curso] [diretorio de saida]**

Também é possível verificar através do prompt o comando --help

1. **>DesafioRPA_AeC.exe --help**


# Como funciona:
1. O programa recebe os inputs do prompt.
2. Utiliza a biblioteca Selenium para iniciar a varredura no site da Alura através do primeiro parâmetro (nome do curso).
3. A partir do nome do curso, a pesquisa retorna uma lista de itens que é mostrada na página da Alura.
4. O programa realiza uma varredura em todos os itens que retornaram através da pesquisa.
5. Cada item será redirecionado para uma nova página, onde os detalhes serão disponibilizados e armazenados.
6. Após a varredura de todos os detalhes de cada item da pesquisa, o programa persiste as informações em um arquivo no formato JSON em um diretório.
7. Se o parâmetro do diretório não for informado, o mesmo será salvo na pasta raiz do executável, caso contrário, será salvo no diretório informado.