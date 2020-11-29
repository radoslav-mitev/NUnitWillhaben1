Feature: WillhabenLoginErrorMessages

  Background:
    Given an user opens www.willhaben.at in a chrome browser

  Scenario: Login using incorrect credentials
    When trying to log into account using non-valid credentials <eMail> and <password>
      | eMail              | password  |
      | test11111@gmail.at | password1 |      
    Then user should see [Der Benutzername oder das Passwort konnten nicht erkannt werden.]


  Scenario Outline: Login test using correct e-mail format but without a password
    When trying to log into accoung using correct "<eMail>" format but without a "<password>"
    Then user should see [Bitte gib dein Passwort ein]

    Examples:
      | eMail              | password |
      | test22222@gmail.at |          |
      | test33333@gmail.at |          |
