# 🏃‍♂️ 2D 런 게임 (Unity)

![게임 스크린샷](Images/gameplay.png)  
> 간단하지만 중독성 있는 2D 런 게임! 장애물을 피하고, 아이템을 모으며 최대 거리 달성에 도전하세요.

---

## 🧩 프로젝트 소개
이 프로젝트는 **Unity 2D 엔진**으로 제작된 러닝 게임입니다.  
점프, 대시 등의 간단한 조작으로 장애물을 피하고, **패턴화된 맵과 확률적 요소**를 결합해 매번 다른 플레이 경험을 제공합니다.

- **장르:** 2D 런 게임 (Platformer / Endless Runner)  
- **개발 엔진:** Unity 2022.3.62f2  
- **개발 언어:** C#  
- **플랫폼:** PC / Mobile (Android)

---

## 🕹 주요 기능
- ✅ **플레이어 조작 시스템** – 점프, 더블 점프, 슬라이드  
- 🧠 **장애물 패턴 로직** – 스테이지마다 다른 패턴 등장  
- ⚙️ **스킬 시스템** – 캐릭터별 고유 스킬  
- 🎨 **픽셀 아트 스타일 그래픽**  
- 🔊 **효과음 및 배경음악 구현**

---

## 👥 개발자 정보
| 이름 | 담당 파트 |
|------|------------|
| 이승민 (팀장) | 게임 내부 캐릭터 및 게임 매니저 관리 |
| 김문경 | 마을 내 상점 및 인벤토리 |
| 구민지 | 마을 전체 구성 및 캐릭터 상호작용 |
| 조현일 | 게임 내부 맵 전체 구성 및 이동 |
| 황준영 | 전체 UI 구성 및 아트 |

---

# 🏡 Village Scene

![VillageScene](Images/VillageScene.png)

2D 횡스크롤 형식의 마을을 구현하였다.  
플레이어를 조작하여 여러 오브젝트와 상호작용할 수 있다.  
상점, 캐릭터 변경, 메인 게임으로의 이동 등이 포함되어 있다.

---

### 🧍 플레이어

#### Player(Object)
- **Player.cs** – 플레이어의 상호작용 감지 및 실행  
- **PlayerMovement.cs** – 플레이어의 이동 및 점프 제어  
- **AnimationClipChanger.cs** – 애니메이션 클립 저장 및 변경  

`Player.cs`는 Raycast를 통해 앞에 있는 물체의 ID를 인식하고, 해당 상호작용을 실행한다.  
`PlayerMovement.cs`는 **Input System**을 사용하여 입력을 받아 움직임을 제어한다.  
`AnimationClipChanger.cs`는 캐릭터별 애니메이션 클립을 관리하고 변경한다.

---

### 💬 상호작용 시스템

#### 매니저
- **GameActionManager(Object)** – 상호작용 전체 관리  
- **TalkManager(Object)** – 대화 내용 저장 및 전달  

#### 대상 오브젝트
- **GamePortal(Object)** – GameScene(메인 런 게임) 로드  
- **Shop(Object)** – 상점 UI 표시 (Gold로 캐릭터 구매 가능)  
- **SwitchCharacter(Object)** – 캐릭터 변경 UI 표시  

`GameActionManager`는 `Player`가 감지한 오브젝트의 ID를 전달받아,  
해당 오브젝트에 맞는 **대화**, **UI**, **이미지**를 불러와 표시한다.

---

# 🎮 Game Scene

![GameScene](Images/GameScene.png)

2D 런 형식의 실제 메인 게임이 실행되는 씬이다.  
플레이어는 `Space` 키로 점프, `Z` 키로 스킬을 사용하며 최대한 많은 스테이지를 클리어하는 것이 목표이다.  
시간이 지날수록 속도가 빨라지고 맵 패턴이 어려워진다.

---

### 🧍 플레이어

#### Player(Object)
- **GamePlayer.cs** – 캐릭터의 스탯, 스킬, 효과음, 충돌 관리  
- **GamePlayerControl.cs** – InputSystem을 통한 입력 처리  
- **GamePlayerAnimationControl.cs** – 애니메이션 파라미터 제어  

`GamePlayer.cs`는 캐릭터의 속도, 스킬, 타입, 충돌 등을 관리한다.  
`GamePlayerControl.cs`는 유저 입력을 받아 움직임을 실행한다.  
`GamePlayerAnimationControl.cs`는 행동에 따른 애니메이션 변경만 담당한다.

---

### 🌍 맵 시스템

- **MapGenerator(Object)** – 플레이어 이동에 맞춰 맵 섹션을 생성 및 이동시켜 러닝 효과 구현  
- **DifficultyManager(Object)** – 시간 경과에 따라 난이도 상승 및 맵 변경  
- **BackgroundManager(Object)** – 배경 이동 처리 (패럴랙스 효과)

---

### 🕹 게임 관리

- **RunGameManager(Object)** – 골드 증가 및 게임 종료 처리  
- **GameCharacterManager(Object)** – 캐릭터 데이터 관리 및 전달  
- **CharacterData (ScriptableObject)** – 캐릭터 변수, 애니메이션, 스킬, 스프라이트 저장  

![CharacterData](Images/CharacterData_One.png)

---

# ⚙️ DontDestroyObjects

- **LoadSceneManager** – 씬 간 이동 관리  
- **GameDataManager** – 캐릭터 및 골드 데이터 저장  
- **AudioManager** – 배경음악 및 효과음 관리  



