<<<<<<< HEAD
﻿ProyectoFinalSMV – Documentación

# Configuración
- Dependencias instaladas
- .NET 10.0 SDK
- Selenium WebDriver
- SeleniumExtras.WaitHelpers
- NUnit

# Desarrollo del Proyecto

# Pages/

# CartPage.cs
- IsCartVisible(): Valida que el contenedor del carrito esté presente en la página cart.html.
- Checkout(): Localiza y hace clic en el botón Checkout para iniciar el flujo de compra.
- RemoveProduct(): Usa un selector dinámico para encontrar el botón Remove de un producto específico y lo elimina del carrito.
- GetCartItems(): Devuelve la lista de productos actualmente en el carrito, útil para validaciones.

# CheckoutPage.cs
- FillFormFromJson(): Rellena el formulario de checkout con datos externos desde un archivo JSON.
- Continue(): Avanza a la página de resumen (checkout-step-two.html).
- IsSummaryDisplayed(): Valida que el resumen de la orden esté visible.
- GoToOverviewAndValidate(): Ejecuta Continue() y valida que el resumen se muestre correctamente.
- Finish(): Completa la orden.
- IsOrderComplete(): Valida que la orden se haya completado, buscando el encabezado con el texto Thank you for your order!

# CheckoutData.json
- Ubicación: Carpeta TestData/.
- Propósito: Contiene los datos de prueba para rellenar el formulario de checkout.
- Uso: El método FillFormFromJson() de CheckoutPage.cs lee este archivo y completa automáticamente los campos.

# LoginPage.cs
- Constructor: Recibe IWebDriver y configura esperas explícitas.
- Selectores: Usa By.Id y By.CssSelector.
- Métodos de acción: Encapsulan la interacción con los elementos (usuario, contraseña, clic).
- Login(): Combina los pasos para simplificar las pruebas.
- Validaciones: Comprueba si aparece un mensaje de error y obtiene su texto.

# ProductsPage.cs
- IsVisible(): Valida si el contenedor de inventario está presente.
- AddProductToCart(): Localiza el botón con XPath y espera el badge.
- IsProductInCart(): Devuelve true si hay al menos un producto en el carrito.
- RemoveProductFromCart(): Apunta al botón dinámico Remove.
- GetCartCount(): Devuelve el número del badge, manejando el caso de que no exista.
- GoToCart(): Navega al carrito con el ícono.

# Tests/WebTest/

# LoginTests.cs
- Setup(): Inicializa el driver y la página de login.
- LoginValido(): Valida login exitoso con credenciales correctas.
- LoginInvalido(): Valida login fallido con usuario bloqueado.
- TearDown(): Captura screenshot y lo adjunta al reporte NUnit.

# CheckoutTests.cs
- Setup(): Inicializa driver y páginas necesarias.
- CheckoutExitoso_ConDatosValidos(): Flujo completo de checkout con datos válidos desde JSON.
- CheckoutConDatosIncompletos(): Flujo de checkout con datos incompletos, valida mensaje de error.
- TearDown(): Captura screenshot y lo adjunta al reporte NUnit.

# ProductsTests.cs
- Setup(): Inicializa driver y páginas, realiza login y limpia carrito.
- AddProductsToCar(): Agrega tres productos y valida contador.
- RemoveProductsToCar(): Agrega y elimina producto, valida carrito vacío.
- ValidarOrdenamientoPorPrecio(): Valida ordenamiento ascendente por precio.
- TearDown(): Captura screenshot y lo adjunta al reporte NUnit.
   
   
# Documentación BDD y Utilidades
# BDDTest/

# Features
- Compra.feature.cs: Contiene los escenarios en Gherkin para el flujo completo de compra en SauceDemo.
- Escenarios parametrizados con Scenario Outline.
- Uso de Given-When-Then en español (es-ES).

# Hooks/Hooks.cs
- Setup(): Se ejecuta antes de cada escenario.
- Inicializa el driver con espera implícita de 5 segundos.
- Guarda el driver en ScenarioContext para que los steps lo reutilicen.
- TearDown(): Se ejecuta después de cada escenario.
- Captura screenshot del estado final.
- Guarda la imagen en la carpeta Screenshots con nombre seguro (limpiando caracteres especiales y añadiendo GUID corto).
- Adjunta la imagen al reporte NUnit y libera el driver.

# Steps/CompraSteps.cs
- DadoQueEstoyEnLaPaginaDeLogin(): Inicializa la página de login y navega a la URL principal.
- CuandoInicioSesion(usuario, contraseña): Ejecuta login con credenciales y prepara la página de productos.
- CuandoAgregoProducto(productoNombre): Traduce nombre de producto a su identificador interno y lo agrega al carrito.
- CuandoProcedoCheckout(nombre, apellido, codigoPostal): Inicia checkout, llena formulario, valida resumen y finaliza compra.
- EntoncesDeberiaVerMensaje(mensajeEsperado): Verifica que la orden se complete y se muestre el mensaje esperado.

# Utilities/

# DriverFactory.cs
- InitializeDriver: Recibe nombre de navegador (Chrome o Firefox) y lo inicializa.
- ImplicitWait: Configura espera implícita de 10 segundos.
- QuitDriver: Cierra navegador y limpia instancia.
- Maximize: Abre ventana en pantalla completa.
- Incluye opciones para desactivar popup de contraseñas inseguras.
- Recomendación: usar modo incógnito (options.AddArgument("--incognito")) para mayor visibilidad.

# ConfigHelper.cs
- Centraliza la lectura de .
- Devuelve valores según el ambiente activo ().
- Maneja errores si la clave no existe o si el ambiente está mal configurado.

# TestData/
**checkoutData.json**
Archivo de datos para pruebas de checkout.
- Campos:
- firstName: Nombre del cliente.
- lastName: Apellido del cliente.
- postalCode: Código postal.
- Ejemplo:
{
  "firstName": "Silvia",
  "lastName": "Muñoz",
  "postalCode": "50601"
}

# Screenshots/
- Generación: Captura en TearDown de pruebas y Hooks de Reqnroll.
- Formato: PNG.
- Nomenclatura: <NombreTest>_<Timestamp>_[GUID].png
- Ejemplo: CheckoutExitoso_ConDatosValidos_20260410_212700_a1b2c3.png
- Propósito:
- Evidencia visual de resultados.
- Adjuntar en reportes NUnit.
- Documentar errores o estados inesperados.
- La carpeta se crea automáticamente si no existe.

# Reqnroll.json
Archivo de configuración para pruebas BDD.
- language.feature: es-ES → escenarios escritos en español.
- unitTestProvider.name: NUnit → framework de ejecución.
- bindingCulture.name: es-ES → enlaza steps escritos en español con CompraSteps.cs.

# Configuración por ambientes (appsettings.json)

- Se agregó soporte para múltiples ambientes: QA, Staging, Prod.
- Cada ambiente define:
- BaseUrl
- ApiUrl
- User
- Password


## ** ProyectoFinalSMV – Documentación API**

# Endpoints/

# ApiEndpoints.cs
Clase estática que centraliza las rutas base y los endpoints de la API.
- BaseUrl: https://jsonplaceholder.typicode.com
- Users: Endpoint users
- Método: GET
- Propósito: Obtener lista de usuarios
- Posts: Endpoint posts
- Método: POST
- Propósito: Crear recurso (post)
- SinglePost: Endpoint posts/{id}
- Métodos: PUT / DELETE
- Propósito: Actualizar o eliminar recurso por ID

# TestData/

# ApiDeleteTests.cs
**Prueba DELETE en la API.**
- Delete_Post_Should_Return_OK(): Ejecuta DELETE en /posts/{id}.
- Valida respuesta con ApiAssertions.AssertDeleted(response).
- Guarda evidencia en Evidence/Status.
- Cleanup(): Libera recursos del cliente RestSharp.

# ApiGetTests.cs
**Prueba GET en la API.**
- Get_Users_Should_Return_OK(): Ejecuta GET en /users.
- Valida respuesta con ApiAssertions.AssertGetUsers(response).
- Guarda evidencia en Evidence/Json.
- Cleanup(): Libera recursos del cliente RestSharp.

# ApiPostTests.cs
**Prueba POST en la API.**
- Post_CreatePost_Should_Return_Created(): Ejecuta POST en /posts.
- Envía título "Nuevo título", contenido "Contenido de prueba", userId = 1.
- Valida respuesta con ApiAssertions.AssertPostCreated(response, "Nuevo título").
- Guarda evidencia en Evidence/Json.
- Cleanup(): Libera recursos del cliente RestSharp.

# ApiPutTests.cs
**Prueba PUT en la API.**
- Put_UpdatePost_Should_Return_OK(): Ejecuta PUT en /posts/{id}.
- Envía título "Título actualizado", contenido "Contenido actualizado", userId = 1.
- Valida respuesta con ApiAssertions.AssertPostUpdated(response, "Título actualizado").
- Guarda evidencia en Evidence/Json.
- Cleanup(): Libera recursos del cliente RestSharp.

# Utilities/
# ApiAssertions.cs
Validaciones centralizadas para respuestas de API.
- AssertGetUsers(response): Valida código 200 OK y lista no vacía.
- AssertPostCreated(response, expectedTitle): Valida 201 Created y título correcto.
- AssertPostUpdated(response, expectedTitle): Valida 200 OK y título actualizado.
- AssertDeleted(response): Valida 200 OK en DELETE.

# ApiRequests.cs
Construcción de solicitudes REST.
- GetClient(): Devuelve cliente RestSharp con URL base.
- GetUsers(): Solicitud GET /users.
- CreatePost(title, body, userId): Solicitud POST /posts con JSON.
- UpdatePost(id, title, body, userId): Solicitud PUT /posts/{id} con JSON.
- DeletePost(id): Solicitud DELETE /posts/{id}.

# EvidenceHelper.cs
Gestión de evidencia de pruebas API.
- SaveJson(response, testName):
- Carpeta Evidence/Json.
- Archivo .json con contenido de respuesta y timestamp.
- SaveStatus(response, testName):
- Carpeta Evidence/Status.
- Archivo .txt con código de estado HTTP y timestamp.

## Ejecución de pruebas Web y API
• 	WebTests (Selenium + NUnit).-- dotnet test --filter TestCategory=Web
• 	ApiTests (RestSharp + NUnit).--dotnet test --filter TestCategory=Api

# Evidencias de pruebas
El proyecto genera evidencias de ejecución en dos formatos distintos según el tipo de prueba:
	
	# API Tests:
  - Las evidencias se guardan en la carpeta Evidence/.
  - Incluyen archivos de estado, logs y resultados de validaciones

		 # Web Tests
- Las evidencias se generan como screenshots.
- Se guardan en la carpeta Screenshots/ independiente de Evidence.
- Cada screenshot se captura en el TearDown de los tests y se adjunta al reporte NUnit.

# Resumen funcional:

	# Pruebas Web (Selenium + NUnit)

El framework debe implementar al menos las siguientes 7 pruebas funcionales:
- Login válido con credenciales correctas.
- Login inválido y validación de mensaje de error.
- Agregar producto al carrito y validar contador.
- Eliminar producto del carrito.
- Completar checkout exitoso con datos válidos (data-driven desde JSON).
- Intentar checkout con datos incompletos y validar mensaje de error.
- Validar ordenamiento de productos (por precio o nombre).

	# Pruebas BDD (Reqnroll + Gherkin)

- Escenarios escritos en Gherkin (.feature) para cubrir el flujo completo de compra.
- Uso de Given-When-Then.
- Hooks para inicialización y cierre de navegador.
- Parametrización de escenarios.

	# Pruebas de API (RestSharp)

Se deben implementar pruebas CRUD contra una API pública:
- GET → consulta de recursos.
- POST → creación de recurso.
- PUT → actualización de recurso.
- DELETE → eliminación de recurso

# Evidencias/ Reportería de Pruebas

- API Tests → carpeta Evidence/ (logs y resultados).

		# Ubicación de Reportes y Evidencias:

-Reports/ → Carpeta fija en la raíz del proyecto donde se genera el reporte HTML (TestReport.html) con ExtentReports.
-Screenshots/ → Carpeta fija en la raíz del proyecto donde se guardan las capturas de pantalla de cada prueba.
-bin/Debug/net10.0/ → Carpeta de salida de compilación donde NUnit guarda resultados internos y ensamblados (ProyectoFinalSMV.dll).




=======
﻿ProyectoFinalSMV – Documentación

# Configuración

## Requisitos previos
- .NET 6 SDK o superior
- NUnit
- Selenium WebDriver
- Navegador compatible (Chrome recomendado)
- Git instalado

##  Instalación
1. Clonar el repositorio:
   bash git clone https://github.com/smunozv-pixel/ProyectoFinalSMV-automation-framework.git
2. Entrar al directorio:
cd ProyectoFinalSMV-automation-framework
3. Restaurar dependencias:
dotnet restore
4. Ejecutar las pruebas:
dotnet test
4.1. Para pruebas API:
dotnet test ProyectoFinalSMV.ApiTests
4.2. Para pruebas Web
dotnet test ProyectoFinalSMV.WebTests

# Desarrollo del Proyecto

# Pages/

# CartPage.cs
- IsCartVisible(): Valida que el contenedor del carrito esté presente en la página cart.html.
- Checkout(): Localiza y hace clic en el botón Checkout para iniciar el flujo de compra.
- RemoveProduct(): Usa un selector dinámico para encontrar el botón Remove de un producto específico y lo elimina del carrito.
- GetCartItems(): Devuelve la lista de productos actualmente en el carrito, útil para validaciones.

# CheckoutPage.cs
- FillFormFromJson(): Rellena el formulario de checkout con datos externos desde un archivo JSON.
- Continue(): Avanza a la página de resumen (checkout-step-two.html).
- IsSummaryDisplayed(): Valida que el resumen de la orden esté visible.
- GoToOverviewAndValidate(): Ejecuta Continue() y valida que el resumen se muestre correctamente.
- Finish(): Completa la orden.
- IsOrderComplete(): Valida que la orden se haya completado, buscando el encabezado con el texto Thank you for your order!

# CheckoutData.json
- Ubicación: Carpeta TestData/.
- Propósito: Contiene los datos de prueba para rellenar el formulario de checkout.
- Uso: El método FillFormFromJson() de CheckoutPage.cs lee este archivo y completa automáticamente los campos.

# LoginPage.cs
- Constructor: Recibe IWebDriver y configura esperas explícitas.
- Selectores: Usa By.Id y By.CssSelector.
- Métodos de acción: Encapsulan la interacción con los elementos (usuario, contraseña, clic).
- Login(): Combina los pasos para simplificar las pruebas.
- Validaciones: Comprueba si aparece un mensaje de error y obtiene su texto.

# ProductsPage.cs
- IsVisible(): Valida si el contenedor de inventario está presente.
- AddProductToCart(): Localiza el botón con XPath y espera el badge.
- IsProductInCart(): Devuelve true si hay al menos un producto en el carrito.
- RemoveProductFromCart(): Apunta al botón dinámico Remove.
- GetCartCount(): Devuelve el número del badge, manejando el caso de que no exista.
- GoToCart(): Navega al carrito con el ícono.

# Tests/WebTest/

# LoginTests.cs
- Setup(): Inicializa el driver y la página de login.
- LoginValido(): Valida login exitoso con credenciales correctas.
- LoginInvalido(): Valida login fallido con usuario bloqueado.
- TearDown(): Captura screenshot y lo adjunta al reporte NUnit.

# CheckoutTests.cs
- Setup(): Inicializa driver y páginas necesarias.
- CheckoutExitoso_ConDatosValidos(): Flujo completo de checkout con datos válidos desde JSON.
- CheckoutConDatosIncompletos(): Flujo de checkout con datos incompletos, valida mensaje de error.
- TearDown(): Captura screenshot y lo adjunta al reporte NUnit.

# ProductsTests.cs
- Setup(): Inicializa driver y páginas, realiza login y limpia carrito.
- AddProductsToCar(): Agrega tres productos y valida contador.
- RemoveProductsToCar(): Agrega y elimina producto, valida carrito vacío.
- ValidarOrdenamientoPorPrecio(): Valida ordenamiento ascendente por precio.
- TearDown(): Captura screenshot y lo adjunta al reporte NUnit.
   
   
# Documentación BDD y Utilidades
# BDDTest/

# Features
- Compra.feature.cs: Contiene los escenarios en Gherkin para el flujo completo de compra en SauceDemo.
- Escenarios parametrizados con Scenario Outline.
- Uso de Given-When-Then en español (es-ES).

# Hooks/Hooks.cs
- Setup(): Se ejecuta antes de cada escenario.
- Inicializa el driver con espera implícita de 5 segundos.
- Guarda el driver en ScenarioContext para que los steps lo reutilicen.
- TearDown(): Se ejecuta después de cada escenario.
- Captura screenshot del estado final.
- Guarda la imagen en la carpeta Screenshots con nombre seguro (limpiando caracteres especiales y añadiendo GUID corto).
- Adjunta la imagen al reporte NUnit y libera el driver.

# Steps/CompraSteps.cs
- DadoQueEstoyEnLaPaginaDeLogin(): Inicializa la página de login y navega a la URL principal.
- CuandoInicioSesion(usuario, contraseña): Ejecuta login con credenciales y prepara la página de productos.
- CuandoAgregoProducto(productoNombre): Traduce nombre de producto a su identificador interno y lo agrega al carrito.
- CuandoProcedoCheckout(nombre, apellido, codigoPostal): Inicia checkout, llena formulario, valida resumen y finaliza compra.
- EntoncesDeberiaVerMensaje(mensajeEsperado): Verifica que la orden se complete y se muestre el mensaje esperado.

# Utilities/

# DriverFactory.cs
- InitializeDriver: Recibe nombre de navegador (Chrome o Firefox) y lo inicializa.
- ImplicitWait: Configura espera implícita de 10 segundos.
- QuitDriver: Cierra navegador y limpia instancia.
- Maximize: Abre ventana en pantalla completa.
- Incluye opciones para desactivar popup de contraseñas inseguras.
- Recomendación: usar modo incógnito (options.AddArgument("--incognito")) para mayor visibilidad.

# ConfigHelper.cs
- Centraliza la lectura de .
- Devuelve valores según el ambiente activo ().
- Maneja errores si la clave no existe o si el ambiente está mal configurado.

# TestData/
**checkoutData.json**
Archivo de datos para pruebas de checkout.
- Campos:
- firstName: Nombre del cliente.
- lastName: Apellido del cliente.
- postalCode: Código postal.
- Ejemplo:
{
  "firstName": "Silvia",
  "lastName": "Muñoz",
  "postalCode": "50601"
}

# Screenshots/
- Generación: Captura en TearDown de pruebas y Hooks de Reqnroll.
- Formato: PNG.
- Nomenclatura: <NombreTest>_<Timestamp>_[GUID].png
- Ejemplo: CheckoutExitoso_ConDatosValidos_20260410_212700_a1b2c3.png
- Propósito:
- Evidencia visual de resultados.
- Adjuntar en reportes NUnit.
- Documentar errores o estados inesperados.
- La carpeta se crea automáticamente si no existe.

# Reqnroll.json
Archivo de configuración para pruebas BDD.
- language.feature: es-ES → escenarios escritos en español.
- unitTestProvider.name: NUnit → framework de ejecución.
- bindingCulture.name: es-ES → enlaza steps escritos en español con CompraSteps.cs.

# Configuración por ambientes (appsettings.json)

- Se agregó soporte para múltiples ambientes: QA, Staging, Prod.
- Cada ambiente define:
- BaseUrl
- ApiUrl
- User
- Password


## ** ProyectoFinalSMV – Documentación API**

# Endpoints/

# ApiEndpoints.cs
Clase estática que centraliza las rutas base y los endpoints de la API.
- BaseUrl: https://jsonplaceholder.typicode.com
- Users: Endpoint users
- Método: GET
- Propósito: Obtener lista de usuarios
- Posts: Endpoint posts
- Método: POST
- Propósito: Crear recurso (post)
- SinglePost: Endpoint posts/{id}
- Métodos: PUT / DELETE
- Propósito: Actualizar o eliminar recurso por ID

# TestData/

# ApiDeleteTests.cs
**Prueba DELETE en la API.**
- Delete_Post_Should_Return_OK(): Ejecuta DELETE en /posts/{id}.
- Valida respuesta con ApiAssertions.AssertDeleted(response).
- Guarda evidencia en Evidence/Status.
- Cleanup(): Libera recursos del cliente RestSharp.

# ApiGetTests.cs
**Prueba GET en la API.**
- Get_Users_Should_Return_OK(): Ejecuta GET en /users.
- Valida respuesta con ApiAssertions.AssertGetUsers(response).
- Guarda evidencia en Evidence/Json.
- Cleanup(): Libera recursos del cliente RestSharp.

# ApiPostTests.cs
**Prueba POST en la API.**
- Post_CreatePost_Should_Return_Created(): Ejecuta POST en /posts.
- Envía título "Nuevo título", contenido "Contenido de prueba", userId = 1.
- Valida respuesta con ApiAssertions.AssertPostCreated(response, "Nuevo título").
- Guarda evidencia en Evidence/Json.
- Cleanup(): Libera recursos del cliente RestSharp.

# ApiPutTests.cs
**Prueba PUT en la API.**
- Put_UpdatePost_Should_Return_OK(): Ejecuta PUT en /posts/{id}.
- Envía título "Título actualizado", contenido "Contenido actualizado", userId = 1.
- Valida respuesta con ApiAssertions.AssertPostUpdated(response, "Título actualizado").
- Guarda evidencia en Evidence/Json.
- Cleanup(): Libera recursos del cliente RestSharp.

# Utilities/
# ApiAssertions.cs
Validaciones centralizadas para respuestas de API.
- AssertGetUsers(response): Valida código 200 OK y lista no vacía.
- AssertPostCreated(response, expectedTitle): Valida 201 Created y título correcto.
- AssertPostUpdated(response, expectedTitle): Valida 200 OK y título actualizado.
- AssertDeleted(response): Valida 200 OK en DELETE.

# ApiRequests.cs
Construcción de solicitudes REST.
- GetClient(): Devuelve cliente RestSharp con URL base.
- GetUsers(): Solicitud GET /users.
- CreatePost(title, body, userId): Solicitud POST /posts con JSON.
- UpdatePost(id, title, body, userId): Solicitud PUT /posts/{id} con JSON.
- DeletePost(id): Solicitud DELETE /posts/{id}.

# EvidenceHelper.cs
Gestión de evidencia de pruebas API.
- SaveJson(response, testName):
- Carpeta Evidence/Json.
- Archivo .json con contenido de respuesta y timestamp.
- SaveStatus(response, testName):
- Carpeta Evidence/Status.
- Archivo .txt con código de estado HTTP y timestamp.

## Ejecución de pruebas Web y API
• 	WebTests (Selenium + NUnit).-- dotnet test --filter TestCategory=Web
• 	ApiTests (RestSharp + NUnit).--dotnet test --filter TestCategory=Api

# Evidencias de pruebas
El proyecto genera evidencias de ejecución en dos formatos distintos según el tipo de prueba:
	
	# API Tests:
  - Las evidencias se guardan en la carpeta Evidence/.
  - Incluyen archivos de estado, logs y resultados de validaciones

		 # Web Tests
- Las evidencias se generan como screenshots.
- Se guardan en la carpeta Screenshots/ independiente de Evidence.
- Cada screenshot se captura en el TearDown de los tests y se adjunta al reporte NUnit.

# Resumen funcional:

	# Pruebas Web (Selenium + NUnit)

El framework debe implementar al menos las siguientes 7 pruebas funcionales:
- Login válido con credenciales correctas.
- Login inválido y validación de mensaje de error.
- Agregar producto al carrito y validar contador.
- Eliminar producto del carrito.
- Completar checkout exitoso con datos válidos (data-driven desde JSON).
- Intentar checkout con datos incompletos y validar mensaje de error.
- Validar ordenamiento de productos (por precio o nombre).

	# Pruebas BDD (Reqnroll + Gherkin)

- Escenarios escritos en Gherkin (.feature) para cubrir el flujo completo de compra.
- Uso de Given-When-Then.
- Hooks para inicialización y cierre de navegador.
- Parametrización de escenarios.

	# Pruebas de API (RestSharp)

Se deben implementar pruebas CRUD contra una API pública:
- GET → consulta de recursos.
- POST → creación de recurso.
- PUT → actualización de recurso.
- DELETE → eliminación de recurso

# Evidencias

- API Tests → carpeta Evidence/ (logs y resultados).
- Web Tests → carpeta Screenshots/ (capturas automáticas en fallos).






>>>>>>> e8c7dd8bb42ff9b5a23c779e0e0029d116e3d4a4
