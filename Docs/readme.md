![ClassDiagram](https://user-images.githubusercontent.com/119236151/232856755-67ca6ed0-893e-4418-a79e-e92121324541.png)
![ClassDiagram](https://user-images.githubusercontent.com/119236151/232856759-e758fb88-8ac7-4f29-b415-a9ae9c25b7e8.png)
![ClassDiagram](https://user-images.githubusercontent.com/119236151/232856763-6e7ac023-88b5-4fb8-a6d4-5bfde14638a2.png)
![ClassDiagram](https://user-images.githubusercontent.com/119236151/232856765-4a3e1fc7-c18c-4040-aa9a-a02f5ab37de5.png)






























@startuml
class DbUser {
  - IdUserRole: uint
  - Login: string
  - HashPassword: byte[]
  - FirstName: string
  - SecondName: string
  - DateBirthday: DateTime
  - IsBlocked: bool

  + DbUser()
  + DbUser(idUserRole: uint, login: string, hashPassword: byte[], firstName: string, secondName: string, dateBirthday: DateTime, isBlocked: bool)
  + ToString(): string
}
enum UserRole {
   User
    Psychologist,
    CopyWriter,
    Admin,
    SuperAdmin
}

class HashedValue {
  - Value: byte[]

  + HashedValue(value: byte[])
  + ToString(): string
}
interface IDbUser {
  + IdUserRole: uint
  + Login: string
  + HashPassword: byte[]
  + FirstName: string
  + SecondName: string
  + DateBirthday: DateTime
  + IsBlocked: bool
}
interface IDbService {
  + ExistsUser(login: string): bool
  + IsBannedUser(login: string): bool
  + GetHashPassword(login: string): string
  + AddUser(user: DbUser): AddUserResult
  + GetDataUserByLogin(login: string): DataRow?
  + AddLog(login: string, loggingType: LoggingType): AddLoggingResult
  + GetTable(nameTable: string): DataTable?
}
class DbSettingsService {
    - const FileSettings = "SettingsMySql.json"
    + LoadDbSettings(): DbConnectionStringBuilder
    + DbSettingsFileExists(): bool
    + RemoveDbSettings(): void
}
class AdminPanel {
-readonly IDbService _dbService
-const string TitlePage
+AdminPanel(IDbService dbService)
+static Create() : AdminPanel
}

AdminPanel --> IDbService
AdminPanel --> dbTable
class AuthRegistration {
    - IDbService _dbService
    - const string TitlePageAuth = "Authentication"
    - const string TitlePageReg = "Registration"
    + AuthRegistration(IDbService dbService)
    - void SetError(string? message)
    - void RemoveEmptyFields()
    + void loginTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    + void passTextBox_PreviewMouseLeftButtonDown(object sender: MouseButtonEventArgs e)
    + async void loginClick(object sender, RoutedEventArgs e)
    + async void regRegistrationClick(object sender, RoutedEventArgs e)
    + void employeeLoginButton_Click(object sender, RoutedEventArgs e)
}

AuthRegistration --> IDbService
@enduml
