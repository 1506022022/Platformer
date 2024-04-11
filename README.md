# 과제

<b>~ 24/04/12</b>

<https://youtu.be/19IBKkHH6m4>


<b>~ 24/04/06</b>

<https://youtu.be/bY9U0UJOR3U>


# 문서 설명
이 문서에서 RPG(Role-playing game)는 플레이어블 캐릭터를 조작하여 몬스터와 전투하며 성장하는 역할 수행게임입니다.

- RPG의 프레임워크에 대해 제가 어떠한 고민을 했는지 기록하고 되돌아보기 위해
- 팀 프로젝트를 진행할 경우에 팀원들과 코딩 규칙을 맞춰가는 참고자료로 활용하기 위해

해당 문서를 기록하고 있습니다.

# RPG Base System
![RPG 구조](https://github.com/1506022022/RPG/assets/88864717/0a29b29a-7889-44b9-8f37-1a902c046c1c)


<details><summary> <b>업데이트 히스토리</b></summary>

<details><summary><b>2024.04.06</b></summary>
  
- **입력 노드 -> 컨트롤러 노드** : 수행하는 역할이 컨트롤러에 더 맞다고 판단해 의미를 명확하게 전달할수 있도록 변경했습니다.
- **상호작용 노드 -> 인터렉션 노드** : 콘텐츠 하위 노드인 인터렉터블 노드와의 일관성을 위해서 변경하였습니다.
- **코딩 원칙** : 노드 이름들을 영어나 한글 둘 중 하나로 통일하려 했지만 몇몇 단어가(인터렉터블 = 상호작용가능?) 부자연스러울 수 있을 것 같아 자연스러운 쪽을 택하기로 결정했습니다.
- **코딩 원칙** : 동일한 레벨로 연결되어 있는 노드 간에는 접근 가능하도록 하는 원칙이었지만 모듈성이 깨지기 때문에 최대한 지양하는 것이 좋을 것 같아 해당 문구를 추가했습니다.
- **코딩 원칙** : 하위에서 상위의 레벨로 옵저버를 통해 정보를 전달할 때 반환값을 통한 전달을 누락했었기에 추가했습니다.
  
![RPG 구조](https://github.com/1506022022/RPG/assets/88864717/827fc4f7-dee2-4911-876f-e1eda7164f95)
</details>
<!-- 00 -->
<details><summary><b>2024.04.03</b></summary>
  
![RPG](https://github.com/1506022022/MyPortfolio/assets/88864717/a97cabde-f8af-4a33-9ed3-b5d3569609ec)
</details>
</details>

# C#
※포프님의 C# 코딩 스탠다드를 참고했습니다.

<https://docs.popekim.com/ko/coding-standards/csharp>

[기본 원칙]

가독성을 최우선으로 삼는다. (대부분의 경우 코드는 그 자체가 문서의 역할을 해야 함)
정말 합당한 이유가 있지 않는 한, 통합개발환경(IDE)의 자동 서식을 따른다. (비주얼 스튜디오의 "Ctrl + K + D" 기능)

# I. 메인 코딩 표준

- 클래스와 구조체의 이름은 파스칼 표기법을 따른다.
```C++
class PlayerManager;
struct PlayerData;
```
- 지역 변수 그리고 함수의 매개 변수의 이름은 카멜 표기법을 따른다.
  
```C++
public void SomeMethod(int someParameter)
{
    int someNumber;
    int id;
}
```

- 메서드 이름은 기본적으로 동사(명령형)+명사(목적어)의 형태로 짓는다.
```C++
public uint GetAge();
```

단, 단순히 부울(boolean) 상태를 반환하는 메서드의 동사 부분은 최대한 Is, Can, Has, Should를 사용하되 그러는 것이 부자연스러울 경우에는 상태를 나타내는 다른 3인칭 단수형 동사를 사용한다.
```C++
public bool IsAlive(Person person);
public bool HasChild(Person person);
public bool CanAccept(Person person);
public bool ShouldDelete(Person person);
public bool Exists(Person person);
```

- 메서드의 이름은 파스칼 표기법을 따른다.
```C++
public uint GetAge();
```

- 상수의 이름은 모두 대문자로 하되 밑줄로 각 단어를 분리한다.
```C++
const int SOME_CONSTANT = 1;
```

- 상수로 사용하는 개체형 변수에는 static readonly를 사용한다.
```C++
public static readonly MyConstClass MY_CONST_OBJECT = new MyConstClass();
```

- 상수로 사용하는 static readonly 변수는 모두 대문자로 하되 밑줄로 각 단어를 분리한다.


- 초기화 후 값이 변하지 않는 변수는 readonly로 선언한다.
```C++
 public class Account
 {
     private readonly string mPassword;
    
     public Account(string password)
     {
         mPassword = password;
     }
 }
```
- 네임스페이스의 이름은 파스칼 표기법을 따른다.
```C++
 namespace System.Graphics
```
- 네임스페이스의 구성 요소는 마침표로 구분한다(Microsoft.Office.PowerPoint).
- 서로 다른 회사의 네임스페이스가 동일한 이름을 갖는 것을 방지하려면 네임스페이스 이름 앞에 회사 이름을 붙인다.
- 네임스페이스 이름의 두 번째 수준에서는 안정적이고 버전 독립적인 제품 이름을 사용한다.
- 부울(boolean) 변수는 앞에 b를 붙인다.
```C++
 bool bFired;                // 지역변수
 private bool mbFired;       // private 멤버변수
```
- 부울 프로퍼티는 앞에 Is, Has, Can, Should 중에 하나를 붙인다.
```C++
 public bool IsFired { get; private set; }
 public bool HasChild { get; private set; }
 public bool CanModal { get; private set; }
 public bool ShouldRedirect { get; private set; }
```
- 인터페이스를 선언할 때는 앞에 I를 붙인다.
```C++
 interface ISomeInterface;
```
- 열거형의 이름은 파스칼 표기법을 따른다.
```C++
 public enum Direction
 {
     North,
     South
 }
```
- 구조체의 이름은 파스칼 표기법을 따른다.
```C++
 public struct UserID;
```
- private 멤버 변수명은 앞에 m을 붙이고 파스칼 표기법을 따른다.
```C++
 public class Employee
 {
     public int DepartmentID { get; set; }
     private int mAge;
 }
```
- 값을 반환하는 함수의 이름은 무엇을 반환하는지 알 수 있게 짓는다.
```C++
 public uint GetAge();
```
- 단순히 반복문에 사용되는 변수가 아닌 경우엔 i, e 같은 변수명 대신 index, employee 처럼 변수에 저장되는 데이터를 한 눈에 알아볼 수 있는 변수명을 사용한다.

- 뒤에 추가적인 단어가 오지 않는 경우 줄임말은 모두 대문자로 표기한다.
```C++
 public int OrderID { get; private set; }
 public int HttpCode { get; private set; }
```
- getter와 setter 대신 프로퍼티를 사용한다.

틀린 방식:
```C++
 public class Employee
 {
     private string mName;
     public string GetName();
     public string SetName(string name);
 }
```
올바른 방식:
```C++
 public class Employee
 {
     public string Name { get; set; }
 }
```
- 지역 변수를 선언할 때는 그 지역 변수를 사용하는 코드와 동일한 줄에 선언하는 것을 원칙으로 한다.

- double이 반드시 필요한 경우가 아닌 이상 부동 소수점 값에 f를 붙여준다.
```C++
 float f = 0.5f;
```
- switch 문에 언제나 default: 케이스를 넣는다.
```C++
 switch (number)
 {
     case 0:
         ... 
         break;
     default:
         break;
 }
```
- switch 문에서 default: 케이스가 절대 실행될 일이 없는 경우, default: 안에 Debug.Fail()을 추가한다.
```C++
 switch (type)
 {
     case 1:
         ... 
         break;
     default:
         Debug.Fail("unknown type");
         break;
 }
```
- 코드를 작성하면서 세운 모든 가정에 Debug.Assert()를 넣는다.

- 재귀 함수는 이름 뒤에 Recursive를 붙인다.
```C++
 public void FibonacciRecursive();
```
- 클래스 안에서 멤버 변수와 메서드의 등장 순서는 다음을 따른다.
멤버 변수
프로퍼티 (단, 프로퍼티와 대응하는 private 멤버변수는 프로퍼티 바로 위에 적음)
생성자
메서드 (public -> private 순서로)

- 클래스 안에서 연관 있는 메서드끼리 그룹을 짓는다. 멤버 변수도 마찬가지이다.

- 매개변수 자료형이 범용적인 경우, 함수 오버로딩을 피한다.

올바른 방식:
```C++
 public Anim GetAnimByIndex(int index);
 public Anim GetAnimByName(string name);
```
틀린 방식:
```C++
 public Anim GetAnim(int index);
 public Anim GetAnim(string name);
```
- 클래스는 각각 독립된 소스 파일에 있어야 한다. 단, 작은 클래스 몇 개를 한 파일 안에 같이 넣어두는 것이 상식적일 경우 예외를 허용한다.

- 파일 이름은 대소문자까지 포함해서 반드시 클래스 이름과 일치해야 한다.
  
PlayerAnimation.cs
```C++
 public class PlayerAnimation;
```
 
- 여러 파일이 하나의 클래스를 이룰 때(즉, partial 클래스), 파일 이름은 클래스 이름으로 시작하고, 그 뒤에 마침표와 세부 항목 이름을 붙인다.

  
 Human.Head.cs
 
 Human.Body.cs
 
 Human.Arm.cs
```C++
public partial class Human;
```

- 특정 조건이 반드시 충족되어야 한다고 가정(assertion)하고 짠 코드 모든 곳에 assert를 사용한다. assert는 복구 불가능한 조건이다.(예: 대부분의 함수는 다음과 같은 assert를 가질 수도… Debug.Assert(매개변수의 null 값 검사) )

- 비트 플래그 열거형은 이름 뒤에 Flags를 붙인다.
```C++
 [Flags]
 public enum EVisibilityFlags
 {
     None = 0,
     Character = 1 << 0,
     Terrain = 1 << 1,
     Building = 1 << 2,
 }
```
- 디폴트 매개 변수 대신 함수 오버로딩을 선호한다.

- 디폴트 매개 변수를 사용하는 경우, null이나 false, 0 같이 비트 패턴이 0인 값을 사용한다.

- 변수 가리기(variable shadowing)는 허용되지 않는다. 외부 변수가 동일한 이름을 사용중이라면 내부 변수에는 다른 이름을 사용한다.
```C++
 public class SomeClass
 {
     public int Count { get; set; }
     public void Func(int count)
     {
         for (int count = 0; count != 10; ++count)
         {
             // count를 사용
         }
     }
 }
```
- 언제나 System.Collections에 들어있는 컨테이너 대신에 System.Collections.Generic에 들어있는 컨테이너를 사용한다. 순수 배열을 사용하는 것도 괜찮다.

- var 키워드를 사용하지 않는다. 단, 데이터형이 중요하지 않은 경우에는 예외를 허용한다. IEnumerable에 var를 사용하거나 익명 타입(anonymous type)을 사용할 때가 좋은 예이다.

- 싱글턴 패턴 대신에 정적(static) 클래스를 사용한다.

- async void 대신에 async Task를 사용한다. async void가 허용되는 유일한 곳은 이벤트 핸들러이다.

- 외부로부터 들어오는 데이터의 유효성은 외부/내부 경계가 바뀌는 곳에서 검증(validate)하고 문제가 있을 경우 내부 함수로 전달하기 전에 반환해 버린다. 이는 경계를 넘어 내부로 들어온 모든 데이터는 유효하다고 가정한다는 뜻이다.
  
따라서 내부 함수에서 예외(익셉션)를 던지지 않으려 노력한다. 예외는 경계에서만 처리하는 것을 원칙으로 한다.

위 규칙의 예외: enum 형을 switch 문에서 처리할 때 실수로 처리 안 한 enum 값을 찾기 위해 default: 케이스에서 예외를 던지는 것은 허용.
```C++
switch (accountType)
 {
     case AccountType.Personal:
         return something;
     case AccountType.Business:
         return somethingElse;
     default:
         throw new NotImplementedException($"unhandled switch case: {accountType}");
 }
```
- 함수의 매개변수로 null을 허용하지 않는 것을 추구한다. 특히 public 함수일 경우 더욱 그러하다.

- null 값을 허용하는 매개변수를 사용할 경우 변수명 뒤에 OrNull를 붙인다.
```C++
 public Anim GetAnim(string nameOrNull);
```
- 함수에서 null을 반환하지 않는 것을 추구한다. 특히 public 함수일 경우 더욱 그러하다. 그러나 때로는 예외를 던지는 것을 방지하기 위해 그래야 할 경우도 있다. 함수에서 null을 반환할 때는 함수 이름 뒤에 OrNull을 붙인다.
```C++
 public string GetNameOrNull();
```
- 인라인 람다는 한 줄짜리 짧은 코드만 담을 수 있다.

- 개체 초기자(object initializer)를 사용하지 않으려고 노력한다. 단, required 한정자(C# 11.0)와 초기화 전용 setter(C# 9.0)와 같이 사용할 때는 괜찮다.

- 함수에 전달하는 out 매개변수는 별도의 라인에 선언한다. 즉, 인자 목록 안에서 선언하지 않는다.

- null 병합 연산자(C# 7.0)의 사용을 금한다.

- using 선언(C# 8.0)의 사용을 금한다. 대신 using 문을 사용한다.

- new 키워드 뒤에 반드시 명시적으로 자료형을 적어준다. (즉, C# 9.0의 new() 사용 금지) 단, 함수 내부에서 익명 타입(anonymous type)을 사용하기 위해 그럴 때는 허용한다.

- 프로퍼티에 private init(C# 9.0)을 최대한 사용한다.

- 파일 범위 namespace 선언(C# 10.0)을 사용한다

- 범용적인 자료형을 강타입(strong type)으로 만들 때는readonly record struct(C# 10.0)를 사용한다.

  
# II. 소스 코드 포맷팅

- 탭(tab)은 비주얼 스튜디오 기본값을 사용하며, 비주얼 스튜디오를 사용하지 않을 시 띄어쓰기 4칸을 탭으로 사용한다.

- 중괄호( { )를 열 때는 언제나 새로운 줄에 연다.

- 중괄호 안( { } )에 코드가 한 줄만 있더라도 반드시 중괄호를 사용한다.
```C++
if (bSomething)
{
    return;
}
```
- 한 줄에 변수 하나만 선언한다.
  
틀린 방식:
```C++
int counter = 0, index = 0;
```
올바른 방식:
```C++
int counter = 0;
int index = 0;
```

# III. 프로젝트 설정 관련

- 배포(release) 빌드에서 컴파일러 경고(warning)를 오류(error)로 처리하게 설정한다.

- implicit global using(C# 10.0)을 사용하지 않는다.
