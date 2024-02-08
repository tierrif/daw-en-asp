Tierri Ferreira - 22897
DAW - Época Normal - ASP.NET

Ambiente de desenvolvimento usado:
- Visual Studio 2022 (inicialmente), depois JetBrains Rider
- MSSQLLocalDB
- ASP.NET Core 6.0
- EntityFramework 6.0.26

Comandos executados para criação da base de dados:
- dotnet ef migrations add InitialCreate
- dotnet ef database update

NOTA: As classes criadas não refletem exatamente a estrutura da base de dados dada.
	  Esta estrutura é respeitada assim que a base de dados é criada automaticamente pela migração com os comandos acima.
	  
O teste foi realizado na sua totalidade. Houveram alguns problemas na geração de vistas automaticamente, sendo necessário
a realização de algumas partes como as dropdownbox manualmente, o que foi difícil na quantidade de tempo dada.
