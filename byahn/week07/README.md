# Week07

## 7주차 내용
- 7주차: 6부 좀비 서바이버 16장, 17장 (686p - 813p)
    - 16장 좀비 서바이버: 생명과 좀비 AI
    - 17장 좀비 서바이버: 최종 완성과 포스트 프로세싱
    - 과제
        - 6부 좀비 서바이서 완성 프로젝트를 github에 올리고 실행 영상 올리기

## 과제 1
- 과제
    - 6부 좀비 서바이서 완성 프로젝트를 github에 올리고 실행 영상 올리기
        - https://drive.google.com/file/d/1_ivGz9Ia0xFIOrCAAigDtghmZ_dr52bI/view?usp=sharing

## 내용 정리
### 16장 좀비 서바이버
#### 다루는 내용
	- 다형성을 사용해 여러 타입을 하나의 타입으로 다루기
	- 오버라이드를 사용해 부모 클래스의 기존 메서드 확장하기
	- 이벤트를 사용해 견고한 커플링을 해소하고 코드를 간결하게 만들기
	- UI 슬라이더 사용하기
	- 게임 월드 내부에 UI 배치하기
	- 내비게이션 시스템을 사용해 인공지능 구현하기

- 오버라이드
	```cs
		public class Monster : MonoBehaviour {
			public virtual void Attack() {
				Debug.Log("공격");
			}
		}

		public class Orc : Monster {
			public override void Attack() {
				base.Attack();
				Debug.Log("크왕");
			}
		}
	```
	- base 키워드
		- 부모 클래스를 지칭한다.
		- 오버라이드 되기 전의 원형 메서드로 접근할 수 있다.
		- C++에서는 다중 상속을 지원하기 때문에 모호함을 없애기 위해서 자바의 super 나 C#의 base 키워드가 존재하지 않는다.
	
- Action 타입
	- 입력과 출력이 없는 메서드를 가리킬 수 있는 델리게이트(메서드를 값으로 할당 받을 수 있는 타입)
	```cs
		Action onClean;

		void Start() {
			onClean += CleaningRoomA;
			onClean += CleaningRoomB;
		}

		void Update() {
			onClean();
		}

		void CleaningRoomA(){
			Debug.Log("A 청소");
		}

		void CleaningRoomB(){
			Debug.Log("B 청소");
		}
	```
	- Action 타입의 변수에는 +=을 사용해 메서드를 등록할 수 있다.

- 이벤트 
	- C#에서 이벤트를 구현하는 대표적인 방법은 델리게이트를 클래스 외부로 공개하는 것이다.
	- 이벤트가 발동되면 이벤트에 등록된 메서드들이 모두 실행된다.
	- 델리게이트 타입의 변수는 event 키워드를 붙여 선언할 수 있다. event로 선언하면 클래스 외부에서는 해당 델리게이트를 실행할 수 없다.

- 내비게이션 시스템
	- 유니티는 실시간으로 장애물을 피하며 다른 위치로의 경로를 계산하고 이동하는 인공지능을 만드는 내비게이션 시스템을 제공한다.
	- 내비게이션 시스템에 사용되는 오브젝트
		- 내비메시(NavMesh) : 에이전트가 걸어 다닐 수 있는 표면
		- 내비메시 에이전트(NavMesh Agent) : 내비메시 위에서 경로를 계산하고 이동하는 캐릭터 또는 컴포넌트
		- 내비메시 장애물(NavMesh Obstacle) : 에이전트의 경로를 막는 장애물
		- 오프메시 링크(Off Mesh Link) : 끊어진 내비메시 영역 사이를 잇는 연결 지점(뛰어넘을 수 있는 울타리나 타고 올라갈 수 있는 담벼락을 구현하는데 사용)

### 17장 좀비 서바이버
#### 다루는 내용
	- UI 버튼의 OnClick 유니티 이벤트
	- 좀비 생성기와 아이템 생성기 구현 방법
	- 익명 함수와 람다식
	- 내비메시에서 랜덤한 점 찾는 방법
	- 포스트 프로세싱으로 게임 비주얼의 퀄리티를 높이는 방법

- 익명 함수
	- 익명 함수는 미리 정의하지 않고, 인라인(실행 중인 코드 블록 내부)에서 즉석 생성할 수 있는 메서드이다.
	- 실시간으로 생성할 수 있으며, 변수에 저장할 수 있는 값이나 오브젝트로 취급되며, 생성된 익명 함수는 델리케이트 타입의 변수에 저장할 수 있다.
	- 람다표현식
		- (입력) => 내용;
	- 사용 예시
	```cs
		zombie.onDeath += () => zombies.Remove(zombie);
		zombie.onDeath += () => Destroy(zombie.gameObject, 10f);
	```

- 포스트 프로세싱(후처리)
	- 게임 화면이 최종 출력되기 전에 카메라의 이미지 버퍼에 삽입하는 추가 처리
	- 핸드폰 카메라 앱의 필터 기능과 비슷하다.
	- 적은 노력으로 뛰어난 영상미 구현이 가능하다.
	- 대부분의 포스트 프로세싱 연산은 렌더링 파이프라인의 주요 과정에서 적용되지 않고 마지막 부분에 적용된다.
	- 유니티는 포스트 프로세싱을 쉽게 사용할 수 있는 포스트 프로세싱 스택 패키지를 제공한다.

- 포워드 렌더링
	- 포워드 렌더링은 각각의 오브젝트를 그릴 때마다 해당 오브젝트에 영향을 주는 모든 라이팅도 함께 계산하는 전통적인 방식이다.
	- 메모리 사용량이 적고 저사양에서도 비교적 잘 동작한다.
	- 연산 속도가 느리고, 오브젝트와 광원이 움직이거나 수가 많아질수록 연산량이 급증한다.
	- 하나의 게임 오브젝트에 대해 최대 4개의 광원만 제대로 개별 연산한다. 나머지 '중요하지 않은' 광원과 라이팅 효과는 부하를 줄이기 위해 합쳐서 한 번에 연산한다. 따라서 라이팅 효과가 실제와 다르게 표현될 수 있다.

- 디퍼드 셰이딩
	- 디퍼드 셰이딩은 라이팅 연산을 미뤄서 실행하는 방식이다.
	- 첫 번째 패스에서는 오브젝트의 메시를 그리되 라이팅을 계산하거나 색을 채우지 않고, 오브젝트의 여러 정보를 종류별로 버퍼에 저장한다.
	- 두 번째 패스에서 첫 번째 패스의 정보를 활용해 라이팅을 계산하고 최종 컬러를 결정한다.
	- 유니티의 디퍼드 셰이딩은 개수 제한 없이 광원을 표현할 수 있고, 모든 광원의 효과가 올바르게 표현된다.
	- MSAA 같은 일부 안티앨리어싱 설정을 제대로 지원하지 않는다.