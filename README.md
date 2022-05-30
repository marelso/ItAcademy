# It Academy - Gestor de medicamentos
O projeto consiste em uma solução para armazenar, ler e exibir os dados referentes aos medicamentos que foram importados pelo arquivo “.csv”.

# Pré Requisitos
A solução foi desenvolvida em torno da plataforma .net, mais especificamente, em sua versão 6. Portanto é necessário possuir o runtime que pode ser encontrado no link abaixo.
  * [dotnet.microsoft.com](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).

# Instalação
Todos os arquivos necessários para execução foram fornecidos em conjunto a este documento. Existem algumas etapas de configuração para perfeita utilização do sistema, estes seguem listados abaixo.

  * Persistência dos dados
Todos os dados gerados pela solução serão incluídos em uma pasta chamada “DB” no diretório raiz do sistema. Para isso, garanta que a pasta esteja presente no caminho “C:\”.
Para trabalhar com os dados, a aplicação utiliza banco de dados (especificamente Sqlite). Os dados são lidos pela aplicação e, quando válidos, são passados para o banco. Por isso, é importante colocar o arquivo fornecido na pasta criada anteriormente. Apenas para fins de teste, a pasta “DB - Clean” possui um banco sem dados populados.

# Execução
Após finalizar a configuração do ambiente e da persistência, o executável da solução se encontra dentro da pasta “App”. Basta executar o arquivo “ItAcademy.exe” para ter acesso às funcionalidades da solução.

  * Importação do arquivo “.csv” com os registros.
Antes de iniciar as etapas de consulta, o arquivo contendo os registros de medicamentos deve ser carregado no sistema. Ao fim da importação será possível visualizar uma janela informando a conclusão. Caso existam algumas ocorrências (registros inválidos e/ou que sofreram correção na etapa de inclusão) será exibida uma segunda janela informando o ocorrido e a localização do log de execução.


  * Consulta de medicamento.
Para pesquisar por medicamentos é necessário escolher qual a forma de filtro (por nome ou por código). Após selecionar o filtro de sua preferência basta pressionar o botão de localizar que o sistema trará todos os registros com determinada característica selecionada.
É importante ressaltar que a pesquisa por nome retornará apenas os registros comercializados em 2020, ao contrário da busca por código que retornará todos os produtos localizados com aquele código.

  * Consulta de concessão de crédito.
Para consultar os índices de concessão de crédito basta realizar a consulta pressionando o botão de “Consultar”. Assim, o sistema retornará os índices informando seus percentuais.
