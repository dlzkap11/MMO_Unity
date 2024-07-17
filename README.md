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


transform.Translate(Vector3.forward * Time.deltaTime * _speed);  //로컬좌표기준
transform.position += Vector3.forward * Time.deltaTime * _speed; //월드좌표기준

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
transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);
transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f));

로컬값
transform.Rotate(new Vector3(0.0f, Time.deltaTime * _speed, 0.0f));
  
Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f); //(시작점, 목표점, 회전속도)

