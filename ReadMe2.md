# 코드리뷰
**캐릭터 컨트롤 로직**   
- 캐릭터는 DoAction을 통해 전달받은 데이터를 통해 Action을 수행합니다.
- Action은 Ability, Movement, CharacterState 세가지의 동작으로 이루어져 있습니다.

![image](https://github.com/1506022022/Platformer/assets/88864717/6a16584f-4d81-4d02-924e-1053fd65dc7d)
- [Controller.cs](https://github.com/1506022022/Platformer/blob/main/Character/Controller/PlayerCharacterController.cs)   
- [Character.cs](https://github.com/1506022022/Platformer/blob/main/Core/Character/Character.cs)   
- [AbilityAgent.cs](https://github.com/1506022022/Platformer/blob/main/Core/Combat/AbilityAgent.cs)   
- [HitBoxGroup.cs](https://github.com/1506022022/Platformer/blob/main/Core/HitBox/HitBoxGroup.cs)   
- [HitBoxCollider.cs](https://github.com/1506022022/Platformer/blob/main/Core/HitBox/HitBoxCollider.cs)   
- [MovementComponent.cs](https://github.com/1506022022/Platformer/blob/main/Character/Movement/MovementComponent.cs)   
- [MovementAction.cs](https://github.com/1506022022/Platformer/blob/main/Character/Movement/MovementAction.cs)   
- [CharacterAnimation.cs](https://github.com/1506022022/Platformer/blob/main/Core/Character/Animation/CharacterAnimation.cs)   
***

**충돌 처리 로직**
- 충돌은 공격의 주체와 피격의 주체의 정보를 가지고 처리합니다.
- 충돌이 발생하면 파이프라인을 통해 필수 이벤트와 추가된 이벤트를 실행합니다.
  - 필수 이벤트에는 부여된 Ability를 발동하는 AttackEvent와, 충돌 사실을 알려주는 HitCallback, 유니티 에디터에서 할당하는 FixedHitEvent가 있습니다.
  - 추가된 이벤트에는 디버그에 사용되는 로그출력 등의 기능이 포함됩니다.

![image](https://github.com/1506022022/Platformer/assets/88864717/5126588d-1710-4e26-b3d1-21599db23039)
- [HitBoxCollider.cs](https://github.com/1506022022/Platformer/blob/main/Core/HitBox/HitBoxCollider.cs)   
- [Ability.cs](https://github.com/1506022022/Platformer/blob/main/Core/Combat/CombatAction/Ability.cs)   
***

**능력**
- Ability 클래스는 ScriptableObject를 상속받았습니다. 메서드들이 자원으로 사용됩니다.
- 능력은 충돌 처리 로직에서 생성된 CollisionData에서 공격의 주체와 피격의 주체를 읽어와 수행됩니다.
- Ability.DoActivation()가 호출되면 파이프라인이 실행됩니다.
  - 파이프라인의 첫 단계로 필수적인 기능들이 실행됩니다.
  - 두번째 단계로는 추가적인 기능들이 실행됩니다. 예를 들어 데미지를 처리하는 기능이 추가될 수 있습니다.

![image](https://github.com/1506022022/Platformer/assets/88864717/de8b605f-fe84-4db5-8d1d-8fde1c65974e)
- [Ability.cs](https://github.com/1506022022/Platformer/blob/main/Core/Combat/CombatAction/Ability.cs)
***

**게임 로드 로직**   
- 기본적으로 GameMaanger의 Awake 이벤트 때 GameManager.LoadGame()을 실행합니다.
- 플레이중 GoaltPortal에 도착하면 Contents.SetLoaderType()을 통해 로딩방식을 결정하고 GameManager.LoadGame()을 실행합니다.
- GameManager는 GameManager.LoadGame()이 호출되면 Contents.LoadNextLevel()을 실행합니다.
- Contents는 설정된 타입에 따른 형태로 Contents.LoadNextLevel()을 수행합니다.

![image](https://github.com/1506022022/Platformer/assets/88864717/39c3ea28-e56f-48ff-9fad-eba48031be68)
- [GoalPortal.cs](https://github.com/1506022022/Platformer/blob/main/Contents/Portal/GoalPortal.cs)   
- [Portal.cs](https://github.com/1506022022/Platformer/blob/main/Contents/Portal/Portal.cs)   
- [Contents.cs](https://github.com/1506022022/Platformer/blob/main/Core/Contents/Contents.cs)   
- [GameManager.cs](https://github.com/1506022022/Platformer/blob/main/GameManager/GameManager.cs)   
