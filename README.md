# **Unity Reusable Solutions**
Этот репозиторий предоставляет инструменты и решения, которые можно повторно использовать в проектах на Unity.

## Быстрая установка
Скачайте файл [unitypaсkage](Unity_Reusable_Solutions.unitypackage) и импортируйте в корневую папку Assets.
> *Для избежания ошибок при импорте оставьте все галочки, т.к. внутри пакета есть взаимозависимые модули.*

![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/52f6d795-3b15-40c6-89a8-ee96806b699e)

# Effects
## Shaking Effect
Компонент, реализующий эффект тряски объекта, к которому он прикреплён.
> *Имеет зависимость от модуля [Events](README.md#events)*

<details>
<summary>Пример использования</summary>

* Добавьте компонент на камеру в сцене.
* Назначьте событие-триггер для включения и настройте параметры силы и продолжительности эффекта:
   ![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/fa8fa778-999e-49fe-a160-917babb95c0e)

* Эффект будет воспроизводиться при срабатывании триггера: 
  ![CameraShakingExample](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/7b0f57e0-b37f-4080-bfc1-53fcff14bf86)

> С более наглядным примером можно ознакомиться в исходном проекте, открыв сцену "CameraShakingExample"
</details>

# Events
## Scriptable Event
Событие, не привязанное к конкретному типу.
Позволяет создать гибкую систему взаимодействия между объектами, в которой у издателей и подписчиков нет нужды знать друг о друге.

<details>
<summary>Пример использования</summary>
  
* Допустим, есть скрипт псевдо-персонажа. Персонаж должен реагировать на некое событие извне, чтобы менять своё поведение. Это событие будет объявлено членом его класса, чтобы управлять подпиской-отпиской: 
```csharp
using Devolvist.UnityReusableSolutions.Events;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private ScriptableEvent _somethingHappened;

    private void OnEnable()
    {
        _somethingHappened.Subscribe(OnSomethingHappaned);
    }

    private void OnDisable()
    {
        _somethingHappened.Unsubscribe(OnSomethingHappaned);
    }

    private void OnSomethingHappaned()
    {
        // Handle.
    }
}
```
>*Если вы случайно подпишитесь на одно событие дважды, вторая подписка не будет засчитана.*


* Есть скрипт окружения в игровом мире, которое инициирует определённые события в определённый момент:
```csharp
using Devolvist.UnityReusableSolutions.Events;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] private ScriptableEvent _somethingEvent;
    [SerializeField] private ScriptableEvent _anotherSomethingEvent;

    private void Start()
    {
        _somethingEvent.Publish();
        _anotherSomethingEvent.Publish();
    }
}
```
* Создадим объекты-события в папке проекта через CreateAssetMenu, кликнув правой кнопкой мыши по папке:
![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/ac9b13b8-487d-48ef-bec5-ab302a1790f2)


* Установим соответствующие имена для объектов событий:  
 ![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/48a3d168-bf77-4127-b1c3-8d813ef2e380)

> *Имена файлов ни на что не влияют, кроме удобства восприятия и читаемости.*

* Далее, нужно установить ссылки на соответствующие события в инспекторе скрипта окружения:
![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/84807530-249c-4379-b93b-58dd0d95a65f)


* Затем, тоже самое нужно проделать в инспекторе скрипта персонажа:
![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/c7ced7e2-ad27-4c73-8e38-f1d5c86f6ff7)

* Готово. При запуске сцены персонаж будет реагировать на событие в момент его иницииации (публикации).

## В чём преимущество этой системы в отличие от стандартных event-членов в C#?
Если бы событие было обычным членом класса Environment, нам пришлось бы назначать ссылку на скрипт Environment в инспекторе Character.
"И что?" - скажете вы, "В данном случае зависимость от Environment поменялась на зависимость от объектов-событий".
Вы будуте правы, но ScriptableEvent позволяет гибко менять зависимости от них прямо в инспекторе, без внесения изменений в код.

К примеру, можно с лёгкостью поменять события местами как у издателя, так и подписчика:
![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/a402ac4a-fb9b-4891-9b63-4722321eab57)
![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/4efa2ccb-5046-42c6-9c59-68d52f99b27f)

Этот подход позволяет геймдизайнерам более гибко и быстро настраивать поведение игры.

И, наконец, данные события не привязаны к конкретной сцене, и существуют перманентно в проекте независимо от наличия издателей и подписчиков.

> С более наглядным примером можно ознакомиться в исходном проекте, открыв сцену "EventsExample"

</details>

# Inspector Fields Null-Validation
Часто ли бывает так, что вы объявляете public-поля или с атрибутом [SerializeField], запускаете PlayMode и видите в консоли ошибки, которые досадно напоминают вам о том что вы забыли назначить нужные ссылки в инспекторе?
Или случаи, когда вы переименовали поле и ссылка в инспекторе "слетает", вынуждая искать все места в проекте, где нужно заново её присвоить?

Данное решение предоставляет простой и быстрый способ обнаруживать null-поля в инспекторах активной сцены (включая сцену префаба), а также во всех Scriptable Object в папке проекта.

<details>
<summary>Пример использования</summary>

* Пометьте скрипты, в которых вы используете сериализуемые поля атрибутом [InspectorFieldsNullValidation]:
 ```csharp
using UnityEngine;
using Devolvist.UnityReusableSolutions.InspectorFieldsNullValidation;

[InspectorFieldsNullValidation]
public class Cube : MonoBehaviour
{
    [SerializeField] private CubeData _data;
}

// Another script.
using UnityEngine;
using Devolvist.UnityReusableSolutions.InspectorFieldsNullValidation;

[CreateAssetMenu()]
[InspectorFieldsNullValidation]
public class CubeData : ScriptableObject
{
    [SerializeField] private Material _material;
}
  ```

* Оставьте поля в инспекторе без назначенных ссылок:
  ![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/0352d555-dc91-4d93-9322-490639944297)
  ![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/ddf3a95f-e985-4523-8004-3c0642466ce1)

* Откройте специальное окно, найти которое можно через главное меню редактора:
  ![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/76c78a64-888c-4308-a620-1ba8d89ea123)

* Нажмите на кнопку:
  ![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/91b736a2-7b26-408b-99ce-9b7728fdce52)

* В этом окне будет выведен список кнопок, на которых указана информация об инспекторах с null-ссылками. Кликните по нужной вам кнопке, чтобы Unity выделил найденный объект в интерфейсе редактора:
![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/06f32c40-4c9a-40d2-b467-db7aa9547429)

* Выберете выделенный объект и назначьте недостающую ссылку в инспекторе. Проделайте то же со всеми объектами, у которых были найдены null-ссылки в инспекторах:
![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/005c7646-fac5-4507-8208-73eb21f2d0c6)

* После того, как все найденные null-ссылки будут назначены, нажмите "Check inspectors for null-fields" ещё раз:
  ![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/e5733e22-2044-4f81-86cd-47dfaec73a4a)

* Поздравляю! Теперь ваша жизнь разработчика стала немного проще :)

*При наличии большого кол-ва найденных инспекторов с null-ссылками, их кнопки помещаются в ScrollView, которое можно прокручивать:*

![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/30f09fa3-654c-45d6-bfb0-4cfaa0821048)

</details>

# Internet
## Internet Connection
Singleton, предоставляющий интерфейс для проверки соединения с интернетом здесь и сейчас.

>*Имеет зависимость от модуля [Singleton](README.md#singleton)*

<details>
<summary>Как использовать</summary>
  
* Прикрепите компонент InternetConnection к любому GameObject.
  
* Обратитесь к его открытому методу проверки из другого скрипта, передав аргумент делегата для получения обратного вызова с результатом проверки:

```csharp
InternetConnection.Instance.IsAvailable(result => Debug.Log($"Internet connection status: {result}"));
```
> С более наглядным примером можно ознакомиться в исходном проекте, открыв сцену "InternetConnectionCheckingExample"
</details>

# Math
## Interpolation
Глобально доступный класс, предоставляющий методы для получения интерполированных значений.

<details>
<summary>Пример использования GetRange()</summary>

```csharp
using UnityEngine;
using Devolvist.UnityReusableSolutions.Math;

public class Example : MonoBehaviour
{
    private void Start()
    {
        int[] interpolatedRange =
             Interpolation.GetRange(
                 minValue: 0,
                 maxValue: 100,
                 valuesCount: 10);

        for (int i = 0; i < interpolatedRange.Length; i++)
            Debug.Log(interpolatedRange[i]);
    }
}
```

* Вывод в консоли:
  
![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/e3b82132-56f6-4609-8ec0-719f31e3ec86)

</details>

## Percentage Calculator
Глобально доступный класс, предоставляющий методы получения процентных значений.

<details>
<summary>Пример использования GetPercentageValue()</summary>

```csharp
using UnityEngine;
using Devolvist.UnityReusableSolutions.Math;

public class Example : MonoBehaviour
{
    private void Start()
    {
        int percents = PercentageCalculator.GetPercentageValue(
            currentValue: 110,
            minValue: 100,
            maxValue: 1000);

        Debug.Log(string.Concat(percents.ToString() + '%'));
    }
}
```

* Вывод в консоль:
  
![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/61c6a016-4097-456b-a065-dfeb8e1da3da)

</details>

<details>
<summary>Пример использования GetValueFromPercentage()</summary>

```csharp
using UnityEngine;
using Devolvist.UnityReusableSolutions.Math;

public class Example : MonoBehaviour
{
    private void Start()
    {
        int value = PercentageCalculator.GetValueFromPercentage(
             percentage: 50,
             minValue: 50,
             maxValue: 100);

        Debug.Log(value);
    }
}
```

* Вывод в консоль:
  
![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/b0464a4f-df40-4c77-af13-cddc3559a521)

</details>

# Save-Load
Простая система сохранения-загрузки.

> *Имеет зависимость от модуля [Singleton](README.md#singleton)*

<details>
<summary>Пример использования</summary>
   
* Возьмём скрипт псевдо-персонажа, у которого есть поле со значением здоровья, и реализуем интерфейс ISavable:

```csharp
using UnityEngine;
using Devolvist.UnityReusableSolutions.SaveLoad;

public class Character : MonoBehaviour, ISavable
{
    private int _health;

    public void Load() { }

    public void Save() { }

    public void DeleteSaves() { }

    public void ResetToDefault() { }
}
```

* Создадим в сцене GameObject с компонентом SaveLoadService:

![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/745810ec-377f-48e5-80b8-62a5691147b8)

* Добавим реализацию сохранения-загрузки здоровья через взаимодействие с глобальным сервисом:

```csharp
using UnityEngine;
using Devolvist.UnityReusableSolutions.SaveLoad;

public class Character : MonoBehaviour, ISavable
{
    private const int DEFAULT_HEALTH = 100;

    private int _health = DEFAULT_HEALTH;

    public void Load()
    {
        int loadedHealth = SaveLoadService.Instance.LoadData<int>(id: "Health");
        _health = Mathf.Clamp(loadedHealth, 0, DEFAULT_HEALTH);
    }

    public void Save()
    {
        SaveLoadService.Instance.SaveData(id: "Health", data: _health);
    }

    public void DeleteSaves()
    {
        SaveLoadService.Instance.DeleteSavedData(id: "Health");
    }

    public void ResetToDefault()
    {
        _health = DEFAULT_HEALTH;
    }
}
```

* Добавим реализацию изменения здоровья с последующим сохранением данных:

```csharp

    public void ApplyDamage(int value)
    {
        _health = Mathf.Clamp(_health - value, 0, DEFAULT_HEALTH);

        Save();
    }

```

* Добавим реализацию загрузки данных здоровья при запуске скрипта:

```csharp

    private void Start()
    {
        Load();
    }

```

* Можно передать управление вызовами методов интерфейса ISavable, зарегистрировав персонажа в сервисе сохранения-загрузки:

```csharp

    private void Start()
{
    SaveLoadService.Register(this);

    Load();
}

```

>*Для внешнего Вызова методов ISavable нужно реализовать внешний контроллер, который будет сохранять данные раз в n-времени у всех ISavable-сущностей, или инициировать загрузку, к примеру, при запуске сцены.*
 ```csharp

 public class Autosave : MonoBehaviour
 {
    private int _timeInterval = 10;

    private void Start() => StartCoroutine(PerformSaving());

    private IEnumerator PerformSaving()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(_timeInterval);

            SaveLoadService.SaveRegisteredSavableObjects();
        }
    }
 }
 ```

> **С более наглядным примером можно ознакомиться в исходном проекте, открыв сцену "SaveLoadExample".**

</details>

<details>
<summary>Настройка параметров локальных сохранений</summary>

* В папке модуля SaveLoad есть ScriptableObject, в котором можно указать имя папки для сохранений и указать тип обработки данных (BinaryFormatter или PlayerPrefs)
![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/9152131f-78ad-4ab8-aad2-b8cd26d28bc5)

</details>

<details>  
<summary>Вызов некоторых функций из главного меню редактора</summary>

* В специальном меню на верхней панели редактора можно открыть папку в проводнике, в которой в настоящий момент находятся сохранённые данные. Или запросить удаление данных из этой папки без взаимодействия с ней.
![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/80bf1d0f-dbc9-480d-8d42-071953e05db2)

>*Если папки с указанным в конфигурации названием нет на ПК, в консоль будет выведено сообщение о том что локальные сохранения отсутствуют.*

</details>

# Singleton
## Mono Singleton
Реализация паттерна Singleton, которую можно применить для объектов MonoBehaviour.

<details>
<summary>Пример использования</summary>

* Создадим свой синглтон, унаследовав его от базового варианта и указав конкретный тип наследника:
```csharp
using Devolvist.UnityReusableSolutions.Singleton;

public class SingletonExample : MonoSingleton<SingletonExample>
{
    protected override void InitializeOnAwake()
    {
        // Initialization implementation...
    }

    public void DoSomething()
    {
        // Behavior...
    }
}
```
> *Для корректной инициализации всегда выполняйте её логику в переопределённом методе InitializeOnAwake(). Не используйте обычный Awake() в конкретных синглтонах.*

* Для взаимодействия с глобальным экземпляром синглтона, нужно обращаться к его методам через Instance:
```csharp
public class Client : MonoBehaviour
{
    private void Start()
    {
        SingletonExample.Instance.DoSomething();
    }
}
```

> *Созданный синглтон можно опционально сделать объектом, который будет переходить между сценами:*
> ![image](https://github.com/Devolvist/Unity-Reusable-Solutions/assets/97983639/fa9b3e86-2531-4c41-b8ff-26abba6fc7a6)
> 
> *Если убрать галочку, Instance будет существовать только в пределах сцены, в которой он был создан.*
</details>

# States Management
Предоставляет простую реализацию паттерна State, позволяющую управлять переходами между конкретными состояниями объекта.

<details>  
<summary>Пример использования</summary>

* Создадим классы состояний персонажа, реализующие IState:
  
```csharp
public class WalkingState : IState
{
    public WalkingState(Animator animator)
    {
        _animator = animator;
    }

    private readonly Animator _animator;

    public void Enter()
    {
        _animator.Play("Walking");
    }

    public void Exit()
    {
        _animator.StopPlayback();
    }
}

public class RunningState : IState
{
    public RunningState(Animator animator)
    {
        _animator = animator;
    }

    private readonly Animator _animator;

    public void Enter()
    {
        _animator.Play("Running");
    }

    public void Exit()
    {
        _animator.StopPlayback();
    }
}

public class JumpingState : IState
{
    public JumpingState(Rigidbody rigidbody, float jumpForce)
    {
        _rigidbody = rigidbody;
        _jumpForce = jumpForce;
    }

    private readonly Rigidbody _rigidbody;
    private readonly float _jumpForce;

    public void Enter()
    {
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    public void Exit()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}
```

* Создадим класс персонажа с полями состояний, машины состояний и параметров в инспекторе:

```csharp
using Devolvist.UnityReusableSolutions.StatesManagement;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _jumpForce;

    private IState _walkingState;
    private IState _runningState;
    private IState _jumpingState;
    private StateMachine _stateMachine;
}
```

* Реализуем инициализацию состояний и переходы между ними в зависимости от ввода:

```csharp
    private void Awake()
    {
        _walkingState = new WalkingState(_animator);
        _runningState = new RunningState(_animator);
        _jumpingState = new JumpingState(_rigidbody, _jumpForce);

        _stateMachine = new StateMachine(defaultState: _walkingState);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            _stateMachine.ChangeState(_walkingState);

        if (Input.GetKeyDown(KeyCode.LeftShift))
            _stateMachine.ChangeState(_runningState);

        if (Input.GetKeyDown(KeyCode.Space))
            _stateMachine.ChangeState(_jumpingState);
    }
```

* Таким образом, мы делегировали конкретное поведение персонажа в зависимости от ввода объектам-состояниям, внутри которых это поведение реализовано.
Если бы мы реализовывали методы шага, бега и прыжка внутри персонажа, его код в перспективе стал бы слишком запутанным. Особенно, при необходимости расширять существующее поведение и добавляя новое при разных внешних факторах.

> Реализация состояний в данном примере упрощена и описана для понимания их сути.
Автор даёт себе отчёт в том, что персонажу нужно время для приземления и перехода в состояние бега, чтобы не воспроизводить анимацию бега по воздуху.
Преимущество состояний как раз в том, чтобы инкапсулировать эту проверку условий и управлять возможностью перехода в другое состояние внутри другого.
Состояния можно расширить до того, чтобы они могли сами управлять переходами в другие состояния в зависимости от контекста.

</details>

# String Utilities

# Time
