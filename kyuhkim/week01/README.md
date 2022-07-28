# 1주차: 1부 유니티 준비하기 전체 (32p - 123p)
    ## 1장 유니티 준비하기
    ## 2장 유니티 인터페이스 둘러보기
    ## 3장 유니티 엔진이 동작하는 원리
    ## 과제
        1. 2장 중심으로 실제 유니티를 설치하고 사용하면서 영상을 제작해서 올려보기
        2. 3장에서 논의하고 싶은 부분 1개 이상 작성하고 서로 얘기하고 이해하는 시간


---
# 과제 2 
## 3장에서 논의하고 싶은 부분 1개 이상 작성하고 서로 얘기하고 이해하는 시간
---

컴포넌트 패턴에서 말하는 기능의 조립은, OOP에서 말하는 ‘책임’와 ‘협력’과 동일해보인다.

이들 컴포넌트는 인터페이스를 통해 구현 된것처럼, 조합을 통해 게임오브젝트를 쉽게 구성할 수 있다는 내용을 설명하는 부분에서는 거의 oop와 동일한 내용을 다루고 있는 것처럼 보인다.

각 컴포넌트들은 다른 컴포넌트에 대해서 관심이 없다고 설명하는 부분에서는 역시 OOP에서 ‘독립적/자율적’으로 기능을 구성할 것을 요구하는것과 같은 의미로 보인다.

다만, broadcasting에 대해서는 아직 어떤 패턴인지는 모르겠으나, 모든 게임오브젝트에 메시지를 전달한다는 것을로 봤을때, 저렴한 비용으로 처리할수 있는것은 아닐것 같다. (streaming이 이와 유사하지 않을까.. 추측하지만, 아직 streaming에 대해서 올바로 알지 못하므로 속단할수 없다)
이런 방식을 소개만 하는 것인지, 권장하는것인지는 앞으로의 내용을 더 살펴보면서 파악해야 할 부분이라 생각된다.

처음부터 OOP의 개념을 일부 소개하는 것으로 봐서는, 앞으로 어떤식으로 내용을 끌고갈지 내심 기대된다 :)

추가로… orc -> orc fighter -> orc chief 같은 방식으로 설명했을때, 이런 모습에서는 상속이 매우 합리적인 방법일 것이라는 생각이 들기도 했고, oop에서는 상속을 사용하지 않을것을 권장하는것으로 알고 있지만..
interface로 만들어진 orc를 orc fighter가 상속 받는 행위는, interface를 상속받는 것이므로…
중복 구현에 대한 부담을 덜어주는 것이 되어(orc fighter는 orc의 모든 속성을 동일한 구현 방식으로 가지고 있다는 전제 하에서이며, override할때는 base.Method()를 통한 기존 기능 호출과, 추가 기능 구현도 가능하므로) 무조건적 배척이 옳은것인가? 라는 생각도 하게된다.
oop에서 상속을 권장하지 않는 이유는, 불필요한 기능까지 상속받게 되어 객체가 외부와 협력하는 방식이 잘못되거나, 불필요한 메시지 인터페이스를 외부에 노출하게 되어, 이 객체를 사용하는 다른 객체에게 기능적 오해를 야기시킬수 있기 때문이 아닐까.. 생각된다.