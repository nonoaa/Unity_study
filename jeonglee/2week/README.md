# 2week 

**블로그 포스팅 내용**

*정리는 블로그에 md형식으로 정리해서 해당 내용을 복붙하였습니다.*

# 유니티 디자인 패턴 - 싱글톤  

디자인 패턴 또는 프로그래밍 패턴은 중복되는 코드를 효율적인 해결책이다.  

많은 사람들이 말하길 '효율적인 코드를 만들기위한 방법론'정도로 설명한다. 

게임에 적용되는 디자인 패턴의 종류도 매우 다양하지만 그 활용도가 매우 높아서 필수적으로 활용해야 한다고 본다.  

오늘은 그중에서도 말이 많은 **싱글톤 패턴**을 알아본다.  

싱글톤 패턴이란, 유니티에서만 한정되는 것이 아닌 하나의 프로그래밍 패턴으로 전역적으로 접근 가능한 상태의 인스턴스를 생성하여 임의의 클래스에서도 전역적으로 접근가능하게 만드는 것이다.  

전역적으로 접근이 가능하다면 게임전체적인 흐름을 관장할 수 있기 때문에 매니저의 형태로 가장 많이 사용되며 사용에 편리하다.  

> 그렇다면 필요한건 다 싱글톤으로 만들면 되는거 아닌가..?  

**아니다.** 사용은 매우 간편하지만 많은 데이터나 메서드를 넣다보면 코드 전체적인 흐름이 망가지기 마련이다.  

처음에는 이말을 잘 이해하지 못했지만 한번 경험해보니 이유를 이해하기 시작했다.  

직접 경험해보는 것이 가장 이해가 빠르기 때문에 무작정 안좋다기 보다 직접 실패를 경험해보며 사용방법을 익히는게 좋다고 생각한다.  

*게임을 제작할 때 무분별하게 사용하게 되면 이후에 덩치가 커질 경우 의존도와 전체적인 수정이 대대적으로 이루어져야 하기 때문에 조심하는 것이 좋다.*

++ 중요한 unity lifecycle을 망가트리는 주범이라고 많이 말한다.(안티패턴)  

## 유니티 싱글톤 패턴 사용법  

구조를 설명하기 앞서 위에서 설명한 것 처럼 싱글톤 패턴자체가 프로그래밍 패턴이기 때문에 MonoBehaviour를 상속받지 않고(하이어라키에 존재하지 않음) 사용도 가능하다.  

1. MonoBehaviour를 상속받아서 Hierarchy상에 존재하는 것(즉, 씬에 존재)
2. MonoBehaviour를 상속받지 않고 Hierarchy상에도 존재하지 않는 것

1번의 경우 유일객체로 보존을 보장할 수 없기 때문에(instantiate생성, 컴포넌트 부착 등..) static으로 유일특성을 예외처리 해줘야 한다.  

*싱글톤 패턴이라고 해서 코드가 다 똑같은게 아니고 살짝식 다른 점이 존재한다. (프로퍼티 선언, 삭제 방식 등등..)*

### singleton(1)

1번 방식으로 싱글톤패턴을 구현하고 테스트 해본다.  

```cs
using UnityEngine;

/// <summary>
/// MonoBehaviour를 상속받아서 Scene에 존재하는 싱글톤패턴
/// </summary>
public class Singleton : MonoBehaviour
{
    private static Singleton instance = null;

    public static Singleton Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TestSingleton()
    {
        Debug.Log("singleton Test Log");
    }
}
```

기본적인 싱글톤 패턴의 모습이다.  

1. 우선 static으로 선언해서 유일의 속성을 가지게 하고 해당 인스턴스만 존재할 수 있게 만든다. 
2. private으로 캡슐화를 미리 만들어 두고 프로퍼티로 get메서드만 구현한다(null예외)
3. Awake로 시작하자마자 자신을 할당하고 만약 다른 씬에 같은 싱글톤이 존재한다면 새로 생긴 싱글톤을 삭제한다.  

```cs
using UnityEngine;

public class Temp : MonoBehaviour
{
    private void Start()
    {
        Singleton.Instance.TestSingleton();
    }
}
```

씬에 빈 오브젝트에 부착하여 실행해보면 문제없이 돌아감을 알 수 있다.  

장점이 명확하게 존재하는 부분은 다른 코드를 단 한줄로 참조하여 다룰 수 있는 점은 정말 좋다ㅠㅠ..  

### singleton(2)

```cs
using UnityEngine;

/// <summary>
/// MonoBehaviour를 상속받지 않는 싱글톤
/// </summary>
public class SingletonType2
{
    private static SingletonType2 instance;

    public static SingletonType2 Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SingletonType2();
            }
            return instance;
        }
    }
    
    public void TestSingleton()
    {
        Debug.Log("singleton Test Log");
    }
}
```

확연히 짧아진 코드이다.  

이유는 씬 이동에 신경쓰지 않아도 되서 해당 예외처리가 빠진 것(유일이 보장됨)  

말 그대로 static 함수를 꺼내다 쓰는 동작과 같다.(Math, Mathf)  

두 함수의 차이는 크게 다르지 않기 때문에 사용방식에 따라서 취향것 고르면 될것 같다.  

## 더 나아가서  

좀 더 예외처리에 중점을 두고 만들게 되면 제네릭을 사용해서 만들 수 있다.

*동기화, 코드 일반화, 오브젝트 문제 등등..*  

```cs
using UnityEngine;

public class SingletonType3<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static T m_Instance;

    public static T Instance
    {
        get
        {
            if (m_ShuttingDown)
            {
                Debug.LogWarning($"{typeof(T)} return null");
                return null;
            }

            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    m_Instance = (T)FindObjectOfType(typeof(T));

                    if (m_Instance == null)
                    {
                        var singletonObject = new GameObject();
                        m_Instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + "(singleton)";
                        
                        DontDestroyOnLoad(singletonObject);
                    }
                }
            }

            return m_Instance;
        }
    }

    private void OnDestroy()
    {
        m_ShuttingDown = true;
    }

    private void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }
}
```

많이 발전된 형태로 코루틴의 형식 자체를 제네릭으로 만들어 필요한 매니저마다 상속받아서 사용할 수 있다.  

2번과 마찬가지로 하이어라키에 추가하여 사용하는게 아닌 코드상으로 Gameobject를 추가하여 시작할 때 인스턴스화 한다.  

```cs
using UnityEngine;

public class EtcManager : SingletonType3<EtcManager>
{
    protected EtcManager() { }
    
    public void TestSingleton()
    {
        Debug.Log("singleton Test Log");
    }
}
```

자기 자신을 제네릭으로 넘겨서 생성하고 기본 상속자를 제외하면 코루틴의 예외처리부분은 부모에 있기 때문에 위처럼 짧게 만들어서 각 매니저로 분리하여 사용가능하다.  

공부하다 보니 이렇게 유동적으로 사용할 수 있는 코루틴 형식을 보니 신기하다 각 매니저마다 만들어서 사용하는 줄 알았지만 이렇게 일반화가 되니 직접 분리하여 명확하게 적재적소에 사용한다면?(덩치가 크지 않다면) 무난하게 사용해도 좋을 것 같다.  

# 유니티 최적화

항상 생각하는.. 내가 짜는 코드가 병목현상이나.. 프로그램에 지대한 영향을 미치진 않을까? 잘못된 코드가 아닐까하는 의문이 많아서..

최적화 관련하여 궁금증이 정말 많았다..!

유니티에도 최적화를 위해 간단하게 state에서 드로우콜같이 배치를 보거나 cpu를 보는건 알았지만 최근에 **프로파일러**라는 최적화를 위해 만들어진 툴이 있다는걸 알았다.  

## 최적화에 앞서  

1. 플랫폼

게임에서 최적화란 정말 잘된 게임은 플레이에 직접적인 영향을 끼치게 된다.  

이때 제작자의 입장에서 최적화 할때 제일 먼저 파악해야 하는것은 **플랫폼**이다.  

2. 프로토타입

아직 프로토타입의 개발환경이라면 이때는 최적화(코드적인 부분 x)를 진행할 필요가 없다.  

구현에만 집중해야 하기 때문에 이후에 다시 다듬으면서 최적화를 진행해야하기 때문..!  

3. **프로파일링** 

앞 과정을 거치고 나서 가장 중요한게 바로 프로파일링..!  

프로파일링을 통해 최적화를 진행해야 한다.  

출시전에 몰아서 프로파일링을 하는게 아닌 개발 단계별로 나눠서 프로파일링을 진행하는 것이 좋다.  
*추측에 의한 프로파일링이 되지 않을려면 코드, 리소스, 사운드 등을 나눠서 진행..*

유니티 에디터의 프로파일링을 맹신하면 안된다.  

앞서 말한 타켓플랫폼의 개별적인 프로파일링도 알아볼 것  


---

[출처(유니티 공식 유튜브 최적화)](https://www.youtube.com/watch?v=d_0uDfNEOk8)

# 게임수학

게임쪽을 공부하다보니 부족한 부분을 가장 많이 만나는 곳이 영어와 수학부분이였다.  

필요한 공식이나 수식을 활용할 때 다른 사람의 코드를 참고하지만 100%이해한다고 보기 어렵고 활용도 제대로 못하는 것 같아서..

게임수학을 공부하려고 한다.  

*영어도 꼭..!*

> 깃허브에도 공부레포를 따로 만들어서 관리하는게 좋아보여서 첨부

**공부 그라운드 룰**

1. 조금이라도 궁금하거나 이해안되면 구글링 or 질문하기
2. 배우는 개념을 바로 실행해보기(마크다운에 정리 or 유니티 실행)
3. 수학의 아름다움을 느끼기..?

[<mark style='background-color: #A9BCF5'>레포 주소</mark>](https://github.com/fkdl0048/unity_gamemath)  

> 블로그 포스팅 내용은 아래 강의를 보고 작성한 것입니다.  

[<mark style='background-color: #A9BCF5'>강의 주소</mark>](https://www.udemy.com/course/math-gamedev/)  

- 연한 파랑으로 변경

## 기본연산

뒤에서 다루게 될 벡터, 확통, 대수학 등 다양한 내용을 다루기 위해선 기반이 탄탄해야 한다고 생각한다.  

### 덧셈과 뺄셈

수가 커지거나 작아질 수 있는 수단으로 수직선상에서 가운데를 `0`이라고 본다면 오른쪽으로 증가하면 덧셈 왼쪽으로 증가하면 뺄셈이라고 할 수 있다.  

인간이 상상으로 덧셈 뺄셈의 기준은 무한하지만 컴퓨터는 그렇지 않다.  

```cs
using UnityEngine;

public class IntegerOverflow : MonoBehaviour
{
    private int _bigNumber = 2147483640;
    void Update()
    {
        Debug.Log(_bigNumber);
        _bigNumber++;
    }
}
```

코드를 실행하면 값이 증가하다 음수가 되어 버린다.  

![이미지](../../../../assets/images/Unity_img/unity_math/un_math_01.png)  

integer를 기준으로 20억을 조금 넘는 수를 저장할 수 있기 때문이다.

따라서 우리는 컴퓨터로 수학을 할 때는 모든 수가 같은 유형이 아니라는 사실을 기억해야한다.  

또한 덧셈은 순서가 중요하지 않지만 뺄셈은 순서에 유의해야 한다.  

> 1 + 2 = 2 + 1 
> 1 - 2 != 2 - 1

### 어림수  

반올림은 (0, 1, 2, 3, 4)수는 버림을 하고 (5, 6, 7, 8, 9)는 올림을 한다.  

> 9.99 소수점 첫째 자리에서 반올림을 한다면?

결과값은 10.0이다. (10이 아님)

컴퓨터의 기준으로 앞에서 설명한 숫자의 형식이 다르기 때문을 명심해야한다.  

**내림**과 **올림**은 기준으로 숫자를 상관하지 않고 내림과 올림을 하는 것이다.  

게임에서 적절한 어림수로 표현할 때가 많기 때문에 이러한 기능을 잘 사용해야한다..!

*내부적으로는 실수를 다루지만 사용자에겐 정수만 보여주거나 내부적으로도 정수로 변환해야하는 상황이 오기 때문*  

이러한 상황에서 중요한 점은 어림수를 계산할 때는 마지막에 해줘야 하는 점이다.  

계산 도중에 어림수 계산을 하게 된다면 사소한 오차들이 계속 발생하게 되어서 결과값이 매우 차이나는 상황이 초래된다.  

이러한 문제점은 꽤 심각한 오류로 자리잡기 때문에 항상 어림수 계산은 마지막에..!  

```cs
public class RoundNumber : MonoBehaviour
{
    void Start()
    {
        Debug.Log(Math.Round(3.67f, 1)); // 반올림
        Debug.Log(Math.Ceiling(3.67f)); // 올림
        Debug.Log(Math.Floor(3.67f)); // 내림
    }
}
```

> *대부분의 수학관련은 Math클래스에 static메서드로 존재한다.*  

### 곱셈

게임을 만들다보면 곱셉을 정말 정말 많이 사용하게 된다. (속도, 거리 등)

수직선상에서 곱셉을 다루면 생각보다 이해가 쉽게 된다.  

> 2 * 3 = 6
> 2 * -3 = -6
> -2 * 3 = -6
> -2 * -3 = 6

마이너스 곱하기 마이너스가 플러스가 되는 이유는 여러가지 논리적, 과학적인 증명방법이 있기 때문에 한번 참고해보는 것도 좋다.  

*이해가 1순위*  

### 나눗셈  

곱하기가 더하기를 여러번 한다는 개념이라면 나눗셈은 수를 분리한다는 개념이다.  

컴퓨터를 사용한 작업을 할 때 가장 흔히하는 실수는 1만큼 차이가 나는 것이다.  

*0에서 시작하는 경우*

숫자 8을 수직선 상에 놓고 4로 나눈다고 했을 때 필요한 분리선은 3개인것과 같은 개념이다.  

간단하게 아래 수식을 제대로 이해한다.

> 2/8 / 1 = 2/8
> 2/8 / 2/2 = 2/8
> (2/2)/(8/2) = 1/4 = 2/8

뺄셈에서는 계산을 반대로 하면 부호가 달라진다면 나눗셈은 **크기**가 달라진다.

또한 곱하기와 마찬가지로 음수 나누기 음수는 양수가 된다.  

### 나머지  

간단한 나머지 연산으로 5를 2로 나눈다면 몫은 2이고 나머지는 1이다 라는 계산이 나온다.  

몫은 같은 그룹에 속한다는 것이고 나머지는 말 그대로 속하지 못한 그룹이다.  

여기서 나오는 개념이 짝수와 홀수의 정의이다.  

게임에서 예를 들자면 가장 많이 사용하는 2가지의 경우로 나누는 경우.  

1. 랜덤 숫자를 뽑는다.
2. 2로 나눈 나머지를 구한다.
3. 0이라면 짝수
4. 1이라면 홀수

간단하게 이분법으로 두가지형태로 분류할 수 있기 때문에 확률 계산에 있어서 유리하다.  

> 단적인 예로 동전뒤집기의 경우가 있다.  

시계의 초를 60 -> 1분도 나머지의 개념이다.  

코드상으로는 나머지 연산자가 존재하기 때문에 쉽게 계산이 가능하다.  

### BODMAS

인터넷에서 쉽게 볼 수 있는 문제가 있다.  

> $$ 6 \div 2 (1 + 2) $$

위의 식은 대부분의 컴퓨터에서 정확하게 식을 사용할 수 없다.  

> $$ 6 / 2 * (1 + 2) $$

이런식으로 적어야 컴퓨터의 계산으로 사용할 수 있다는 점.  

따라서 정확한(원하는) 값을 얻고 싶다면 컴퓨터가 따를 수 있는 명령을 내려야 한다.  

그렇다고 해서 계산식을 명확하게 한다고 괄호를 무지막지하게 늘리는 것 또한 불필요하고 악순환이다.  

즉, 계산식에 대한 명확한 이해가 수반되어야 한다는 것.  

**BODMAS** 보드메스란. 컴퓨터에서 계산이 될 때의 우선 순위를 나타낸다.

*PEMDAS라는 다른 규약도 있지만 나눗셈과 곱셈의 우선순위 차이가 있다.*

* B(bracket) 괄호로 가장 우선 시 된다.
* O(Order) 위수(지수)이다. 제곱이나 제곱근
* D(Division) 나누기
* M(Multiplication) 곱하기
* A(Addition) 더하기
* S(Substraction) 빼기 

위 순서로 다시 식 계산한다.  

> $$ 6 / 2 * (1 + 2) $$
> $$ 6 / 2 * (3) $$
> $$ 3 * (3) $$
> $$ 3 * 3 $$
> $$ 9 $$

*이제 비슷한 문제들에 대해 명확한 답을 낼 도구를 찾았다..*

## 제곱연산  

게임수학이기 때문에 계산식과 같이 너무 어렵게는 들어가지 않고 활용방안, 이해정도만 하고 넘어간다.  

### 거듭제곱  

제곱이란, 어떤 수를 제곱한다고 했을 때 그 수를 정사각형이 되도록 회전시킨다고 생각하면 이해가 빠르다.  

4의 제곱은 16(4 * 4)의 계산이므로 1칸 짜리 블록을 가로4 세로4의 기준으로 채우는 것이다.  

역으로, 9칸짜리 정사각형의 수의 제곱근은 한변의 칸수이다. 즉, 3이 제곱근이다.(3 *3 = 9)

같은 개념으로 세제곱의 경우는 3차원 정사각형을 생각하면 된다.  

정면의 경우를 제곱이라고 보고 뒤로 정면 4개를 더 그려넣는다면 3차원 정사각형이 모양이 되기 때문에

> 4 * 4 * 4 = 64

64칸으로 계산된다. (제곱의 경우 16)  

간단한 지식으로 2의 24승은 16777216으로 색을 24비트 즉, RGB를 각각 8비트식 가져가서 색을 표현하기 때문에  

**16777216** 총 나올 수 있는 색의 개수이다. 

```cs
Debug.Log($"계산 결과입니다. {Math.Pow(2,24)}"); 
// 유니티 Math.Pow사용
```

### 소수의 제곱  

위의 개념을 그대로 활용하여 칸수를 사용해 2의 제곱을 구한다고 하면 2을 회전시켜 정사각형을 채워 4칸이 채워진다.  

> [ ][ ]  
> [ ][ ]

만약 2.5개라면?

>  \+ \+ .  
> [ ][ ]+  
> [ ][ ]+

반블록이 존재하게 되고 그대로 정사각형을 만들게 되면 반블록 4개 + 반의반블록 1개가 추가되어서 정사각형을 만든다.  

> $$ 2.5^2 = 4 + 4 * \frac{1}{2} + \frac{1}{4} $$
> $$ 4 + 2 + \frac{1}{4} $$
> $$ 6\frac{1}{4} $$
> $$ 6.25 $$

이해하기 어렵다면 회전을 시켜 정사각형을 만들어 본다면 확실하게 쉬워진다.  

**3.5의 제곱**

> $$ 3.5^2 = 9 + 6 * \frac{1}{2} + \frac{1}{4} $$
> $$ 12.25 $$

이걸 게임에 적용하는 방식은 면적, 색조의 양, 픽셀의 양등 많은 사용방법이 있을 것이다.  

대부분 면적을 어림잡아 계산하기에 유용하다.  

```cs
Debug.Log($"계산 결과입니다. {Math.Pow(2.5,2)}"); 
// 유니티 Math.Pow사용
```

### 제곱근  

4096칸의 3차원 정사각형이 있다고 했을 때 한변의 칸수가 몇일까?  

3차원 정사각형이니 x의 세제곱으로 나타낼 수 있다.  

> $$ x * x * x = x^3 (4096)$$

그렇다면 10의 세제곱은 ?

> $$ 10 * 10 * 10 = 1000$$
> $$ 20 * 20 * 20 = 8000$$  

위와 같이 어림잡아서 내려가며 구하는 방법도 있으며 이러한 방법을 써야할 때도 있지만 **분석적 해법**을 통해 쉽게 구할 수 있다.  

> $$ (a^b)^c = a^b*^c$$

이러한 규칙을 통해 

> $$x^3*^\frac{1}{3} = 4096^\frac{1}{3}$$  

으로 표시되고 

> $$ 4096^{0.33..} $$

을 계산기로 어림 잡아서 계산해보면 어림 16으로 나온다. 

실제로 16을 세제곱하면 4096의 값임을 알 수 있다.  

> $$ ^3\sqrt{4096} $$

## 그래프  

그래프는 게임에서 정말 많이 사용된다. 수치를 명확하게 시작적으로 보여줄 수 있기 때문에 이해할 수 있어야 한다.  

### 차트 그리고 그래프  

그래프를 그릴 줄 안다면 다른 사람들의 그래프를 쉽게 해석하고 이해할 수 있다.  
따라서 그래프를 직접 그려보는 경험이 중요하다.  

그래프를 그릴 때 두가지 축을 기준으로 레이블을 정해야 한다.  

1. 속도, 힘 과 같은 수치로 레이블을 만들고 단위를 정한다. 


**시네머신, shape profile, MVC패턴도 이후에 포스팅 예정**