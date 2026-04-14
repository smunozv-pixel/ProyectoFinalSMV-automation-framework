Feature: Flujo de compra en SauceDemo
  Como usuario estándar
  Quiero poder iniciar sesión, agregar productos y completar la compra
  Para validar que el flujo funciona correctamente

  @web
  Scenario Outline: Compra exitosa con distintos productos
    Given que estoy en la página de login
    When inicio sesión con usuario "<usuario>" y contraseña "<contraseña>"
    And agrego el producto "<producto>" al carrito
    And procedo al checkout con datos "<nombre>", "<apellido>", "<codigoPostal>"
    Then debería ver el mensaje de confirmación "Thank you for your order!"

    Examples:
      | usuario        | contraseña   | producto                      | nombre   | apellido | codigoPostal |
      | standard_user  | secret_sauce | Sauce Labs Backpack           | Juan     | Pérez    | 12345        |
      | standard_user  | secret_sauce | Sauce Labs Bike Light         | María    | Gómez    | 67890        |