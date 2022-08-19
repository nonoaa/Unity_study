# Unity 01WEEK

## BOOK : 레트로의 유니티 게임프로그래밍

### Reviewer : junlee2

## To Do
- 1주차: 2부 C#프로그래밍 시작하기(126p - 217p)
    - 4장 C#프로그래밍 시작하기
    - 5장 5장 게임 오브젝트 제어하기
    - 과제
        - 4장, 5장의 예제 스크립트를 작성하고 github에 pull request로 제출
        - 4장 내용 중 자신이 기존에 배웠던 언어와 차이점이 뭔지 찾아서 정리하기
            - C#이 처음인 사람은 C# 언어에 대한 첫인상
        - 객체지향에 대한 이해도 정리

## 4장 | C#프로그래밍 시작하기
- 변수에 대한 기초적인 설명
    - 변수의 선언, 변수의 자료형
- 함수(메서드)
    - 함수의 정의, 인자, 반환값에 대한 설명
    - 같거나 혹은 비슷한 명령어를 하나로 묶어 반복되는 코드를 줄일 수 있다
    - 예)나무를 이동시키는 코드가 100개 있을때, 기능 추가시 함수로 작성하면 1개의 코드만 수정하면 된다
- 스크립트 작성하기
    - 새 C#파일 만들기
        - 프로젝트 창에서 우클릭 -> C# Script를 선택
        - 파일명 ex) hello.cs 을 항상 클래스 명과 동일하게 유지해야 한다. ex) hello : MonoBehaviour
    - 스크립트의 구성
        - using
            - using 키워드를 통하여 라이브러리를 불러온다
            - c의 #include?
        - start() 메서드
            - 코드 실행이 시작되는 시발점을 제공한다.
            - 유니티 이벤트 메서드 중 하나로 게임이 시작될 때 자동으로 한번 실행이 된다.
    - 주석
        - 컴퓨터가 처리하지 않는 부분이다.
        - 주로 코드의 기능에 대한 메모로 사용한다.
        - c와 동일하게 // 또는 /**/를 사용한다.
    - 변수의 자료형
        - 각각의 자료형으로 변수를 만들고 이를 이용해 출력.
        - Debug.Log("" + 변수명); 으로 출력 가능
        - float형은 32비트를 사용하여 소수점 7자리 까지 정확함
        - bool 은 c와 달리 true/false 으로 처리함
    - 위의 것들을 활용하여 GetDistance() 메서드(함수)를 만들어보기
        - 각각의 a와 b의 x,y 좌표를 가져온 후 피타고라스의 정리를 이용하여 거리를 계산한다.
        - 제곱근은 Mathf.Sqrt(n); 의 반환값을 이용한다.
- 제어문
    - if(조건)
        - 조건이 true(bool 형 또는 비교 연산자) 이면 스코프 안의 코드를 실행한다.
    - 비교 연산자 ( "<" , ">" , "=" , "!" )
    - "<", ">" 대소 비교
        - "=" 동일할때
        - "!" 부정
    - else (else if)
        - if문과 함께 사용한다.
        - if문이 아니였을때 스코프의 내용이 실행된다
        - else if는 if가 아니면서 (조건)이 참일 때 실행된다
    - 논리연산자 ( "&&" , "||" )
        - && AND(그리고)연산자
            - 조건 2개 모두 참일때 참으로 나타난다.
        - || OR(또는)연산다
            - 조건 2개 중 하나라도 참이면 참으로 나타난다.
    - for문 c에서의 for문과 동일함
        - for(변수 초기화; 종료 조건; 변수 증가)
    - while문 if처럼 조건문이 있고 참일시 반복
        while(조건)

## 5장 | 게임 오브젝트 제어하기
- 클래스와 오브젝트
    - 클래스
        - 클래스는 표현하고 싶은 대상을 추상화(묘사)하여 대상과 관련된 변수와 메서드를 정의하는 틀이라고 한다.
    - 오브젝트
        - 클래스가 설계도라고 하면 오브젝트는 물건이다.
        - HUMAN 클래스를 만들면 HUMAN은 오브젝트가 아니다, HUMAN 클래스를 이용하여 HUMAN : 철수, HUMAN : 영희 오브젝트르르 만들 수 있다.
        - 위의 과정을 인스턴스화 라고 하며 생성된 오브젝트를 인스턴스 라고 한다.
        - 철수와 영희 모두 HUMAN 클래스 이지만 서로 다른 독립적인 오브젝트 이다.
- C# 의 클래스
    - Animal 클레스를 만들기
        - Animal 클레스에는 name, sound 변수와 PlaySound()메서드를 가지고 있다.
        - 클레스만으로는 아무일도 일어나지 않는다.
    - Animal 클레스로 동물을 만들기
        - MonoBehavior를 이용하여 zoo스크립트를 만든다.
        - zoo의 Start()메서드에서 Animal 클레스를 이용하여 tom을 생성한다.
        - Animal tom = new Animal();
        - 오브젝트에 zoo 스크립트를 추가하면 작동한다.
        - MonoBehavior 클레스는 new를 통하지 않는다. -> 작동하지 않음.
    - 접근 제한자
        - public : 클래스 외부에서 멤버에 접근 가능
        - private : 클래스 내부에서만 멤버에 접근 가능
        - protected : 클래스 내부와 파생 클래스에서만 멤버에 접근 가능
        - 아무 언급이 없으면 private 으로 생성된다.
    - 나만의 정리 : c#의 클래스는 c의 구조체의 진화형? 이라고 생각해 볼 수 있을수도. (물론 변수 이외의 메서드들을 포함한)
        - Animal tom = new Animal();
        - struct s_animal *tom = malloc(sizeof(s_animal));
    - 만약 tom 과 jerry를 만들고 tom = jerry를 하면 c의 포인터처럼 두 변수? 클래스? 가 하나(jerry)를 가르키게 된다.
    - 아무도 가르키지 않는 오브젝트는 c와 달리(freeㅠㅠ) c#의 가비지 컬렉터가 자동으로 파괴하여 정리한다.
- 참조타입
    - int, float등의 c#내장 변수와 struct타입(Vector3, Color)들은 값 타입처럼 작동하고
    - class, 유니티의 모든 컴포넌트, MonoBehavior를 상속받는 클래스 는 참조(포인터) 타입처럼 작동한다.
    - 예)Rigidbody
    
- 소감
    - 클래스라는 것이 생소하였지만 활용 가능성들이 보이면서 앞으로의 책이 기대가 됩니다.
    - 참조와 값타입의 명확한 구분 (c에서는 *)이 없어서 예전에 많이 했갈렸던 기억이 있는데, 이번 과에서 정리가 되었습니다.