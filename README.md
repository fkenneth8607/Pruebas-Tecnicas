# Pruebas-Tecnicas
Pruebas Tecnicas

Contenido: 
-----------------------------------------------------------------------------------------------------------
1 Proyecto API Rest con pruebas Unitarias usando xUnit,
2 Script para el examen sobre sql
3 Dentro de la carpeta BD se encuentra la BD y las tablas para probar el API


Desarrollo
-----------------------------------------------------------------------------------------------------------
El Api fue desarrollada en Net Core 2.2 en vs 2019 ya que tuve algunos inconvenientes con el 2.1 aqui  tenemos 3 proyectos

1. Web Api: este contiene los crud del modelo user ademas si se corre como proyecto de inicio abre la ventana de Swagger.

  1.1 los metodos son asincronos
 
  1.2 Una de las caracteristicas que tiene es que cree una Interfase para tener metodos comunes de Crud como GetId, Create, Update entre otros,
   y cada Clase que le implemente podra reusar estos metodos
   
  1.3 Al servicio tambien le implemente una interfase por si hay logica de negocio que cambie dependiendo del cliente
  
  
  
2. ApiConnection: cree una clase para las conexiones a BD, Entidades y construcciones de cadenas de conexion
  
  2.1 Usamos las cadenas de conexion a travez de un archivo de configuracion 
  2.2 Usamos Dapper para las operaciones de BD, la mayoria Async
  
3. Api Test: Este pequeno proyecto nos permite probar lo que el Api hace a travez de sus metodos
 Nota: no implemente mock por que no encotre como inyectar la dependencia en la parte de crear la conexion
 

 3.1 se crea una peticion por cada operacion del controlador.
 
Instrucciones
-----------------------------------------------------------------------------------------------------------
1. Se ejecuta el Script.sql dentro de la carpeta BD
2. Se abre el proyecto en vs 2019 y debes tener el core 2.2 , se compila luego tenemos 2 opciones
 a Podemos correr las pruebas Unitarias y verificar que todo este correcto
 b Si ponemos como proyecto de Inicio ePortal Api nos abre la ventana de swagger para probar las peticiones API
 
 



