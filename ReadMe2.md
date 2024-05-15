# 코드리뷰
**게임 로드 로직**   
- 기본적으로 GameMaanger의 Awake 이벤트 때 GameManager.LoadGame()을 실행합니다.
- 플레이중 GoaltPortal에 도착하면 Contents.SetLoaderType()을 통해 로딩방식을 결정하고 GameManager.LoadGame()을 실행합니다.
- GameManager는 GameManager.LoadGame()이 호출되면 Contents.LoadNextLevel()을 실행합니다.
- Contents는 설정된 타입에 따른 형태로 Contents.LoadNextLevel()을 수행합니다.

![image](https://github.com/1506022022/Platformer/assets/88864717/39c3ea28-e56f-48ff-9fad-eba48031be68)
- [Portal.cs](https://github.com/1506022022/Platformer/blob/main/Contents/Portal/Portal.cs)   
- [GoalPortal.cs](https://github.com/1506022022/Platformer/blob/main/Contents/Portal/GoalPortal.cs)   
- [Contents.cs](https://github.com/1506022022/Platformer/blob/main/Core/Contents/Contents.cs)   
- [GameManager.cs](https://github.com/1506022022/Platformer/blob/main/GameManager/GameManager.cs)   
***
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

![image](https://github.com/1506022022/Platformer/assets/88864717/5126588d-1710-4e26-b3d1-21599db23039)
- [HitBoxCollider.cs](https://github.com/1506022022/Platformer/blob/main/Core/HitBox/HitBoxCollider.cs)   
- [Ability.cs](https://github.com/1506022022/Platformer/blob/main/Core/Combat/CombatAction/Ability.cs)   

![image](https://github.com/1506022022/Platformer/assets/88864717/de8b605f-fe84-4db5-8d1d-8fde1c65974e)
- [Ability.cs](https://github.com/1506022022/Platformer/blob/main/Core/Combat/CombatAction/Ability.cs)   
