#	Стиль кода
##	Код
*	Использование глобальных переменных **запрещено**!
*	Имена классов, методов, перечислений, публичных полей, публичных свойств, пространств имен: `PascalCase`
*	Имена локальных переменных и параметров: `camelCase`
*	Имена private, protected, internal и protected internal полей и свойств: `_camelCase`
*	Имена интерфейсов начинаются с I, например IInterface
*	Имена абстрактных классов начинаются с A, например AAbstractClass

##	Файлы
*	Имена файлов и каталогов PascalCase, например File.cs
*	По возможности имя файла должно совпадать с именем основного класса в файле, например MyClass.cs
*	По возможности в одном файле один класс

##	Организация
*	Порядок модификаторов следующий: `public protected internal private
    new abstract virtual override sealed static readonly extern unsafe volatile
    async`
*	Объявления пространств имен `using` идут в самом начале, перед любыми пространствами имен
*	Порядок членов класса:
	*	Сгруппируйте участников класса в следующем порядке:
	* 	Вложенные классы, перечисления, делегаты и события.
	* 	Статические, константные и только для чтения поля.
	* 	Поля и свойства.
	* 	Конструкторы и финализаторы.
	* 	Методы.
*	Внутри каждой группы элементы должны располагаться в следующем порядке:
  	*   Public.
	*   Internal.
	*   Protected internal.
	*   Protected.
	*   Private.

##	Формат
*	Не более одного оператора в строке
*	Отступ 4 пробела или 1 таб
*	Лимит длины строки: 120.
*	Лимит количество строк в функции/методе: 50
*	Разрыв строки перед открывающей фигурной скобкой.
*	Разрыв строки между закрывающей фигурной скобкой и else.
*	Скобки используются, даже если они необязательны.
*	Пробел после if/ for/ whileи т. д. и после запятых.
*	Нет пробела после открывающей скобки или перед закрывающей скобкой.
*	Нет пробела между унарным оператором и его операндом. Один пробел между оператором и каждым операндом всех других операторов.
*	Для определений функций и вызовов, если аргументы не помещаются в одну строку, их следует разбить на несколько строк, при этом каждая последующая строка должна быть выровнена с первым аргументом. Если для этого недостаточно места, вместо этого аргументы могут быть помещены на последующих строках с отступом в четыре пробела.

## Пример

```c#
using System;                           

namespace MyNamespace
{
	public interface IMyInterface
	{
    		public int Calculate(float value, float exp);
	}

	public enum MyEnum
	{
		Yes,
		No,
  	}

  	public class MyClass
	{                            
		public int Foo = 0;
		public bool NoCounting = false;

		private class Results
		{
			public int NumNegativeResults = 0;
			public int NumPositiveResults = 0;
		}

		private Results _results;
		public static int NumTimesCalled = 0;
		private const int _bar = 100;
		private int[] _someTable = {2, 3, 4}

		public MyClass()
		{
			_results = new Results
			{
				NumNegativeResults = 1,
				NumPositiveResults = 1,
			};
		}

		public int CalculateValue(int mulNumber)
		{
			var resultValue = Foo * mulNumber;
			NumTimesCalled++;
			Foo += _bar;

			if (!NoCounting)
			{
				if (resultValue < 0)
				{
					_results.NumNegativeResults++;
				}
				else if (resultValue > 0)
				{
					_results.NumPositiveResults++;
				}
			}
			return resultValue;
		}

		public void ExpressionBodies()
		{
			Func<int, int> increment = x => x + 1;
			Func<int, int, long> difference1 = (x, y) =>
			{
				long diff = (long)x - y;
				return diff >= 0 ? diff : -diff;
			};

			Func<int, int, long> difference2 = (x, y) =>
			{
				long diff = (long)x - y;
				return diff >= 0 ? diff : -diff;
			};

			CallWithDelegate((x, y) =>
			{
				long diff = (long)x - y;
				return diff >= 0 ? diff : -diff;
			});
		}

		void DoNothing() {}

		void AVeryLongFunctionNameThatCausesLineWrappingProblems(int longArgumentName,
									 int p1, int p2) {}

		void AnotherLongFunctionNameThatCausesLineWrappingProblems(
			int longArgumentName, int longArgumentName2, int longArgumentName3) {}

		void CallingLongFunctionName()
		{
			int veryLongArgumentName = 1234;
			int shortArg = 1;
			AnotherLongFunctionNameThatCausesLineWrappingProblems(arg,
									      veryLongArg);
			AnotherLongFunctionNameThatCausesLineWrappingProblems(
				veryLongArgumentName, veryLongArgumentName, veryLongArgumentName);
		}
  	}
}
```
