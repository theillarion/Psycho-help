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
