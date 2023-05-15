![Tests](https://user-images.githubusercontent.com/119236151/232862531-60158ac8-cbfb-43e1-97ce-f22f742f09d7.png)


1)Тесты для класса DbService, который предоставляет доступ к базе данных. Используется библиотека NUnit для написания тестов и библиотека Moq для создания мок-объектов.
Тесты проверяют различные методы класса DbService, такие как ExistsUser, IsBannedUser, GetHashPassword и GetDataUserByLogin. Каждый тест имеет три части: Arrange (настройка), Act (действие) и Assert (утверждение).
В разделе Arrange создаются мок-объекты и настраивается их поведение с помощью методов Setup. Это позволяет имитировать работу реального объекта и определить, что он должен возвращать в ответ на определенный вызов метода.
В разделе Act вызывается тестируемый метод и сохраняется его результат.
В разделе Assert проверяется, что результат тестируемого метода соответствует ожидаемому результату.
Эти тесты позволяют убедиться в правильности работы класса DbService и его методов. Они также обеспечивают защиту от ошибок при изменении кода класса в будущем.
Эти тесты покрывают различные сценарии использования методов в IDbService интерфейсе. Каждый тест проверяет поведение метода в разных условиях.
1.	TestExistsUser_WhenUserExists_ShouldReturnTrue: проверяет, что метод ExistsUser возвращает true, если пользователь с заданным логином существует.
2.	TestExistsUser_WhenUserDoesNotExist_ShouldReturnFalse: проверяет, что метод ExistsUser возвращает false, если пользователь с заданным логином не существует.
3.	TestIsBannedUser_WhenUserIsBanned_ShouldReturnTrue: проверяет, что метод IsBannedUser возвращает true, если пользователь с заданным логином заблокирован.
4.	TestIsBannedUser_WhenUserIsNotBanned_ShouldReturnFalse: проверяет, что метод IsBannedUser возвращает false, если пользователь с заданным логином не заблокирован.
5.	TestGetHashPassword_WhenUserExists_ShouldReturnHashPassword: проверяет, что метод GetHashPassword возвращает правильный хэш-пароль для заданного логина.
6.	TestGetDataUserByLogin_WhenUserExists_ShouldReturnDataRow: проверяет, что метод GetDataUserByLogin возвращает DataRow, содержащий информацию о пользователе с заданным логином.
7.	TestGetDataUserByLogin_WhenUserDoesNotExist_ShouldReturnNull: проверяет, что метод GetDataUserByLogin возвращает null, если пользователь с заданным логином не существует в базе данных.


2)Тесты для класса DbSettingsService предназначен для работы с файлом настроек базы данных. Он содержит методы для проверки наличия файла настроек, получения настроек из файла, сохранения настроек в файл и удаления файла настроек.
Метод DbSettingsFileExists() проверяет, существует ли файл настроек базы данных, и возвращает true, если файл существует, и false, если файл не существует.
Метод GetDbSettings() получает настройки базы данных из файла и возвращает объект типа DbSettings.
Метод SaveDbSettings(dbSettings) сохраняет настройки базы данных в файл.
Метод RemoveDbSettings() удаляет файл настроек базы данных.
Для тестирования класса DbSettingsService определены два теста:
1. DbSettingsFileExistsWhenFileDoesNotExistReturnsFalse() - проверяет метод DbSettingsFileExists на корректность возвращаемого значения при отсутствии файла настроек базы данных.
2. RemoveDbSettingsWhenFileDoesNotExistDoesNothing() - проверяет метод RemoveDbSettings на корректность работы при отсутствии файла настроек базы данных.

3)Класс UserTests содержит тесты для проверки корректности работы конструктора класса User. Тест ConstructorWithValidArgumentsSetsProperties() проверяет, что при передаче в конструктор корректных аргументов объект User создается с корректными свойствами.
В качестве аргументов конструктору передаются значения для свойств объекта User, такие как роль пользователя, логин, пароль, имя, фамилия, дата рождения и статус блокировки. После создания объекта User проверяется, что все его свойства соответствуют переданным значениям.
В этом тесте создаются переменные для каждого из  свойств User, затем объект User создается с использованием этих переменных и наконец проверяется , что каждое свойство объекта User установлено на правильные значения с помощью метода Assert.AreEqual().Если все свойства установлены правильно, тест пройдет успешно, иначе завершится неудачей.

![image](https://github.com/theillarion/Psycho-help/assets/119236151/9d7135da-5e0c-4334-b797-344119a75849)

Добавлены новые тесты для класса DbService:
1.  TestAddUser_WhenUserIsAdded_ShouldReturnAddUserResultSuccess проверяет, что метод AddUser возвращает AddUserResult.Success, если пользователь успешно добавлен в базу данных.
2.  TestAddUser_WhenDatabaseErrorOccurs_ShouldReturnAddUserResultDatabaseError проверяет, что метод AddUser возвращает AddUserResult.DatabaseError, если произошла ошибка при добавлении пользователя в базу данных.
3.  TestAddUser_WhenUserAlreadyExists_ShouldReturnAddUserResultUserAlreadyExists проверяет, что метод AddUser возвращает AddUserResult.UserAlreadyExists, если пользователь с таким логином уже существует в базе данных.
Для каждого теста используется Moq-объект IDbService, который эмулирует работу с базой данных. Также в каждом тесте создается пользователь и вызывается метод AddUser с этим пользователем в качестве аргумента.


Для класса DbSettingsService:
1.  LoadDbSettings_ShouldReturnDbConnectionStringBuilder_WhenSettingsFileExists - проверяет, что метод загрузки настроек базы данных возвращает объект класса DbConnectionStringBuilder, если файл с настройками существует.
2.  DbSettingsFileExists_ShouldReturnTrue_WhenSettingsFileExists - проверяет, что метод проверки существования файла настроек возвращает true, если файл существует.
3.  DbSettingsFileExists_ShouldReturnFalse_WhenSettingsFileDoesNotExist - проверяет, что метод проверки существования файла настроек возвращает false, если файл не существует.



Для класса UserTests:
1.  User_Equals_ReturnsFalseForDifferentObjects проверяет правильность реализации метода Equals для класса User.Тест проверяет, что метод Equals корректно возвращает false для двух объектов класса User, которые отличаются значением свойства UserRole, даже если остальные свойства объектов равны. Тест помогает обеспечить правильную работу метода Equals в случае, когда объекты не эквивалентны.

Результаты покрытия кода тестами находятся в файле "coverage results.txt".

