# MMO_Unity
C# Unity 입문

## Transform



### Position

오브젝트의 위치(x,y,z)를 나타내는 값
유니티는 로컬좌표, 글로벌(월드)좌표로 2가지가 존재한다.

![image](https://github.com/user-attachments/assets/aaf3d6ba-fe32-4b3a-a247-f41006d6bcc1)

로컬은 오브젝트기준 시점의 좌표

![image](https://github.com/user-attachments/assets/2dc87f85-b28f-4d39-9c79-4fa557322a21)

글로벌좌표는 유니티 공간의 기준 좌표

로컬에서 월드로 변환하는 함수는 TransformDirection(Vector3 방향)
월드에서 로컬로 변환하는 함수는 InverseTransformDirection(Vector3 방향)

로컬좌표기준

transform.Translate(Vector3.forward * Time.deltaTime * _speed)  

월드좌표기준

transform.position += Vector3.forward * Time.deltaTime * _speed 



### Vector3

벡터는 위치벡터, 방향벡터 2가지로 나타낸다.

Vector3.magnitude() 함수를 사용하면 힘의 크기를 구할 수 있다.
noramlized 함수를 사용하면 단위벡터를 구할 수 있다.(단위벡터는 길이가 1인 방향벡터)

오브젝트를 특정 위치로 이동시키고 싶을 때 단위벡터를 사용하면 좋다.
Vector3.forward, back, right, left..는 단위벡터를 사용한 함수들



### Rotation

오브젝트의 회전값
position때 와는 다르게 
단순히 transform.rotation = new Vector3(0.0f, 0.0f, 0.0f)로 입력하면 오류가 발생한다.
오브젝트를 회전시키고 싶을 때는 Quateranion과 Euler angle을 이용해야한다.
  
월드값

transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f)

transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f))

로컬값
transform.Rotate(new Vector3(0.0f, Time.deltaTime * _speed, 0.0f))


Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f) //(시작점, 목표점, 회전속도)



### Prefab

프리팹은 Hierachy에 있는 오브젝트를 Project(Assets)쪽으로 드래그로 옮기면 만들 수 있다.
프리팹을 이용하면 같은 오브젝트를 하나하나 새로 만들어 낼 필요없이 재사용할 수 있고, 프리팹에 값을 수정하면 다른 프리팹 인스턴스들에 값도 다같이 변경이 된다. 하지만 오버라이드를 통해 개별적 수정도 가능하다.

이미 프리팹으로 만든 오브젝트도 다시 드래그로 옮기고 prefab variant를 선택하여서 이 전 프리팹을 상속받을 수 있다.

여러 프리팹을 하나의 오브젝트로 묶어주고 다시 그 오브젝트를 프리팹을 만들면 nested prefab을 만들 수 있다.


### Collision

Collider 컴포넌트를 추가하면 오브젝트의 충돌 이벤트를 발생시킬 수 있다.
충돌이벤트가 일어나려면 몇가지 조건이 있는데 최소한의 조건으로는 2가지 정도이다.
1) 두 오브젝트가 Collider 컴포넌트를 가지고 있어야 한다.
2) 둘 중 하나의 오브젝트는 RigidBody 컴포넌트가 있어야 한다. (Is Kinematic off)

이 외에도 Is Trigger를 꺼놔야한다.
Trigger는 오브젝트의 범위에 존재하는가에 대한 이벤트라고 생각하면 편하다.
Collider와 비슷하지만 Trigger는 충돌이 일어나도 현실에서처럼 부딫히는게 아니라 관통하게 된다.


### RayCasting

RayCasting은 말 그대로 레이저를 쏜다라는 개념이다. 이 기술을 이용해서 특정 오브젝트가 앞으로 레이저를 쏴서 특정 오브젝트의 충돌하면 그 오브젝트를 인식한다거나
마우스를 클릭했을 때 그 위치로 오브젝트를 움직이게 하거나 할 수 있다.

### Layer, Tag



### Animation






