
    Grenade();
    Dodge();
    Swap();📘 관리자 가이드(Administrator Guide)

OSS Basic 2025 Unity 3D Action Survival Game
관리자용 운영·유지보수 지침서

📌 1. 이 문서의 목적

이 문서는 본 Unity 3D 액션 생존 게임 프로젝트를 처음 접하는 관리자도 바로 개발 환경을 구축하고, 콘텐츠를 수정하고, 게임을 운영·보수할 수 있도록 설명하는 매뉴얼이다.

이 문서만 따라 하면 다음을 모두 할 수 있다.

개발 환경(Windows + Unity + GitHub) 준비

Unity 프로젝트 열기 및 빌드

게임 데이터(사운드, 무기, 아이템, 적 스폰 등) 수정

코드 구조 파악 및 기능 확장

GitHub 저장소 관리 및 협업 시 주의사항

문제 발생 시 해결 방법

📂 2. 프로젝트 개요 요약
▶ 게임 장르

3D 액션 생존 게임 (Stage 기반 Endless Survival)

▶ 핵심 목표

플레이어가 여러 적을 처치하며 최대한 오래 생존

적을 처치하여 얻은 코인으로 상점에서 무기/탄약/체력을 구매

높은 기록 달성

▶ 주요 기능 구성

플레이어 이동/점프/공격/피격

무기 시스템 (근접/원거리)

적 AI / 스폰 시스템

상점 시스템 (코인 소비)

사운드 시스템 (player + weapon + explosion 등)

UI (HP, Ammo, Coin, Stage Progress 등)

🖥️ 3. 개발 환경 구축 방법
✅ 3.1. 필수 설치 프로그램

아래 프로그램들이 설치되어 있어야 한다:

1) Unity Hub

https://unity.com/download

2) Unity Editor (권장 버전: Unity 6.2)

프로젝트가 제작된 버전과 맞아야 정상 실행됨.

3) Visual Studio Code 또는 Visual Studio

C# 스크립트 편집을 위해 필요.

4) Git

https://git-scm.com/

GitHub 연동 및 프로젝트 버전 관리를 위해 필수.

✅ 3.2. GitHub에서 프로젝트 내려받기

관리자는 GitHub 저장소에서 다음 명령어로 프로젝트를 가져올 수 있다:

git clone https://github.com/사용자명/저장소명.git


예시:

git clone https://github.com/0907dragon/oss-basic-2025-unity-project.git

✅ 3.3. Unity로 프로젝트 열기

Unity Hub 실행

Open Project 클릭

내려받은 폴더 선택

로딩 완료되면 Scene을 열기:

Assets/Scenes/SampleScene.unity

🕹️ 4. 관리자 주요 기능 및 설정 매뉴얼

여기서는 관리자가 자주 다루게 되는 구성 요소를 중심으로 설명한다.

📌 4.1. 플레이어(Player) 관리
▶ Player.cs 역할

이동 속도

점프 처리

피격 처리

소리 재생

무기 장착/교체

UI 반영

▶ 플레이어 파라미터 위치

Hierarchy → Player
Inspector 오른쪽 창에서 수정 가능.

찐 관리자용 Tip:

Speed : 이동 속도

Health : 체력

HasWeapons : 시작 무기 개수

Equip Weapon : 현재 장착 무기

📌 4.2. 무기(Weapon) 관리
▶ 무기 구성 요소

각 무기 prefab은 다음을 포함함:

Weapon.cs 스크립트

BoxCollider / TrailRenderer (근접 공격용)

Bullet prefab 연결 (원거리 공격용)

fireSound (사운드)

damage, rate(공격 쿨타임), ammo 등이 설정됨

▶ 무기 데이터 수정 방법

Hierarchy 또는 Prefab에서 무기를 선택하고 Inspector에서 다음을 조절할 수 있다:

damage → 공격력

curAmmo / maxAmmo → 탄창 수

rate → 공격 속도

fireSound → 발사/공격 소리 연결

📌 4.3. 아이템(Item) 관리
▶ Item.cs 역할

드롭 아이템 인식

타입(Ammo/Coin/Weapon/Heart/Grenade) 구분

플레이어와 충돌 시 효과 적용

▶ 아이템 Prefab에 존재하는 요소

Rigidbody

Sphere Collider

Item Script (Type, Value 변경 가능)

관리자가 새 아이템을 추가하려면 기존 Prefab을 복사해 Type과 Value를 변경하면 된다.

📌 4.4. 적 및 스폰 시스템 관리
▶ EnemyManager.cs 역할

스테이지마다 적 생성

적 난이도 상승

최대 적 수 관리

Boss 스폰

▶ 적 조절 방법

EnemyManager 오브젝트 선택 후 Inspector에서 다음 조절:

maxEnemy (최대 적 수)

spawnTime (적 등장 간격)

spawnPoint (스폰 위치 목록)

🔊 5. 사운드 시스템 관리
▶ 사운드가 들어가는 구성 요소

Player (jump / hit / footstep / swap weapon 등)

Weapon (fire / melee swing)

Explosion (bomb.wav 등)

UI (선택 시 효과음 가능)

▶ 사운드 추가하는 방법

Assets/sounds 폴더에 .wav 또는 .mp3 추가

Player 또는 Weapon Inspector에서 AudioClip에 드래그

필요 시 코드에서 PlayOneShot 사용

▶ 주의사항

파일 이름 변경 시 meta 파일이 깨져 prefab 연결이 풀릴 수 있음

반드시 Unity Editor 안에서만 파일 이동/삭제할 것

🧩 6. 코드 구조 요약
▶ Scripts 위치:

Assets/2Scripts/

핵심 스크립트들:
스크립트	역할
Player.cs	플레이어 조작/소리/무기 조작
Weapon.cs	근접/원거리 무기 시스템
Enemy.cs	적 이동/피격/사망
EnemyManager.cs	스테이지 스폰 시스템
GameManager.cs	전체 게임 흐름
Item.cs	드롭 아이템 관리
Shop.cs	상점 시스템
Bullet.cs	총알 이동/피격 처리
🔧 7. 빌드(Build) 방법

Unity 메뉴 상단
File → Build Settings

Platform: PC, Mac & Linux Standalone

Architecture: x86_64

Add Open Scenes (SampleScene이 추가되어 있어야 함)

Build 클릭 → 출력 폴더 지정

🔐 8. GitHub 관리 방법(관리자 필수)

관리자는 GitHub 저장소를 통해 버전을 관리해야 한다.

▶ 8.1. 로컬 변경사항 올리는 기본 명령어
git add .
git commit -m "변경 내용"
git push

▶ 8.2. 팀원과 협업 시 주의사항

main 브랜치는 되도록 "완성된 기능만" 올릴 것

기능 개발은 feature 브랜치에서 진행

pull 하지 않고 push 시 충돌날 수 있음

강제 push는 팀 협업 중에는 절대 금지

▶ 8.3. 강제 덮어쓰기(관리자 전용)

프로젝트를 고쳐야 하거나 오류가 심각해 되돌릴 방법이 없을 때만 사용:

git push -u origin main --force

🛠 9. 자주 발생하는 문제 & 해결법(FAQ)
❓ Unity에서 prefab 연결이 갑자기 없어짐

→ 사운드 파일 또는 스크립트 파일 이름을 OS에서 변경했을 때 주로 발생
→ 반드시 Unity 내부에서 Rename해야 함

❓ GitHub에 push할 때 “rejected” 오류 발생

→ 원격 저장소의 내용과 충돌
→ 해결:

git pull


또는 관리자라면:

git push --force

❓ 실행했는데 화면이 새하얗고 아무것도 안 나옴

→ Scene이 추가되지 않은 경우
→ Build Settings → Add Open Scenes 실행

❓ 스크립트는 정상인데 공격이 안 됨

→ 무기 prefab의 Collider 또는 trailEffect 연결 누락
→ Weapon.cs에서 fireSound가 null일 수도 있음

🧱 10. 관리자가 알아야 할 내부 설계 요약
▶ Stage 진행 구조

EnemyManager가 적 웨이브 생성

Wave 완료 시 보상 지급

플레이어는 상점에서 무기/회복 구매

체력이 0이 되면 GameManager가 GameOver 처리

▶ UML 간단 설명

관리자가 보기 쉽게 핵심 흐름만 요약:

Player → Weapon → Bullet
       ↓
   Enemy ← EnemyManager
       ↓
    ItemDrop

🏁 11. 결론: 관리자가 알아야 할 핵심 5가지

Assets와 Scripts는 Unity에서만 수정할 것

무기/사운드/스폰 설정은 모두 Prefab에서 해결 가능

게임 진행은 GameManager와 EnemyManager가 담당

GitHub는 반드시 add → commit → push 순서로 관리

문제 발생 시 가장 먼저 콘솔(오류 메시지)을 확인

필요하면:

개발자 가이드(dev guide) 더 확장 버전

UML 다이어그램 이미지 제작

보고서용 그림/설명 구성

최종보고서 그대로 만들어주는 버전

이것도 작성할 수 있어.

나의 말:
개발자 가이드(dev guide) 더 확장 버전 바로 복붙 가능하도록
ChatGPT의 말:

아래는 Unity 기반 3D 액션 생존 게임 프로젝트의 “개발자 가이드(Developer Guide)” 확장 버전이다.
GitHub에 developer_guide.md 또는 개발자가이드.md 로 그대로 올릴 수 있도록 작성해두었어.

문서 구조는 다음 기준을 충족함:

처음 보는 개발자도 즉시 개발 시작 가능

프로젝트 구조와 흐름을 완전히 이해할 수 있도록 상세

코드가 어떤 방식으로 연동되는지 명확한 설명

새로운 기능을 추가할 수 있도록 확장성 위주 설명

직접 복붙 가능하도록 Markdown 형식으로 정리

📘 개발자 가이드 (Developer Guide)

OSS Basic 2025 Unity 3D Action Survival Game
개발자를 위한 설계·구현·확장 지침서

📂 1. 개발 환경 구축
✔ 1.1 필수 설치 프로그램

본 프로젝트는 다음 환경에서 개발됨:

Unity Editor 6.2

Unity Hub

Visual Studio Code / Visual Studio (C# 지원 필수)

Git

Windows 10/11 64bit

Unity 버전이 반드시 맞아야 오류 없이 실행됨.

✔ 1.2 GitHub Clone
git clone https://github.com/사용자명/저장소명.git
cd 저장소명


로컬에서 열기:

Unity Hub → Open

프로젝트 폴더 선택

📂 2. 프로젝트 구조
Assets/
 ├─ 1Prefabs/           # 플레이어 / 적 / 무기 / 아이템 프리팹
 ├─ 2Scripts/           # 모든 C# 스크립트
 │   ├─ Player.cs
 │   ├─ Weapon.cs
 │   ├─ Enemy.cs
 │   ├─ EnemyManager.cs
 │   ├─ GameManager.cs
 │   ├─ Item.cs
 │   ├─ Shop.cs
 │   └─ Bullet.cs
 ├─ 3Animation/         # 애니메이션 파일
 ├─ sounds/             # 모든 오디오 파일
 ├─ Materials/
 ├─ Scenes/
 │   └─ SampleScene.unity
ProjectSettings/
Packages/

📂 3. 핵심 시스템 구조 이해

아래는 본 게임의 전체 구조를 개발자 관점에서 해석한 것이다.

🔹 3.1 Player 시스템 구조

Player.cs 담당 기능:

이동 (WASD)

점프

공격 입력 처리

무기 교체

피격 처리 및 체력 감소

사운드 출력

UI 반영

플레이어는 Weapon.cs 를 통해 공격 로직을 위임하는 구조다.

흐름도:
Player Input → Player.cs → 현재 무기(Weapon.cs) 호출 → 무기 실행

🔹 3.2 Weapon 시스템 구조

Weapon.cs 기능:

근접/원거리 타입 구분

공격 속도, 데미지, 탄약 관리

사운드 출력

근접 공격 시 TrailRenderer / Collider ON/OFF

원거리 공격 시 Bullet 생성

무기 실행 흐름
Player.cs → currentWeapon.Use()  
               ↓  
     (근접) Swing Coroutine  
               ↓  
     TrailRenderer 활성화 → Collider 판정 → 비활성화  

     (원거리) Shot Coroutine  
               ↓  
          Bullet 생성 → rigidbody.forward 이동  


모든 무기는 Prefab 기반으로 관리되며 Inspector에서 damage/rate/sound 를 조절한다.

🔹 3.3 Bullet 시스템 구조

Bullet.cs 담당 기능:

Forward 방향으로 이동

Enemy와 충돌 시 데미지 전달

일정 시간 후 자동 파괴

🔹 3.4 Enemy + EnemyManager 시스템 구조
Enemy.cs 역할

플레이어 추적

애니메이션

피격 처리

사망 시 코인/아이템 드랍

EnemyManager.cs 역할

일정 간격으로 적 생성

Stage 증가에 따라 난이도 자동 상승

적 최대 수 관리

Boss 스폰 관리

적 생성 흐름
EnemyManager.Update()  
      ↓  
spawnTimer 증가  
      ↓  
spawnPoint 랜덤 선택  
      ↓  
Enemy Instantiate  

🔹 3.5 Item 시스템 구조

Item.cs 기능:

아이템 타입 관리
(Ammo, Coin, Grenade, Heart, Weapon)

플레이어와 충돌 시 효과 적용

바닥에 닿으면 Collider 비활성화(굴러다니지 않도록)

아이템 획득 처리 흐름
플레이어 충돌감지(Player.cs OnTriggerEnter)
 ↓
Item.cs Type에 따라 효과 분기
 ↓
Ammo 증가 / Coin 증가 / 무기 획득 등 반영

🔹 3.6 GameManager 시스템 구조

GameManager.cs 기능:

게임 전체 상태 관리(Start → Play → GameOver)

점수/시간/코인 등 저장

UI 업데이트

Stage 클리어 시 보상 처리

씬 전환

🔹 3.7 Sound 시스템 구조

사운드는 3단계 구조로 구성되어 있음.

1) Player 내부 AudioSource

점프

피격

발자국

무기 교체

2) Weapon 내부 AudioSource

근접 공격

총기 발사

3) 폭발/효과음

Grenade Explosion

사운드 적용 방식:
audioSource.PlayOneShot(fireSound);

📂 4. 코드 설계 상세
🔥 4.1 Player.cs 주요 동작 구조
// 공격 처리
if (Input.GetButtonDown("Fire1")) {
    weapons[equipWeaponIndex].Use();
    PlayWeaponSound();
}


코드 특징:

Player는 직접 공격하지 않는다.

선택된 Weapon을 실행시켜 공격 기능을 위임한다.

단일 책임 원칙(SRP)에 부합하도록 분리되어 있음.

🔥 4.2 Weapon.cs 주요 동작
근접 공격 Swing()
trailEffect.enabled = true;
meleeArea.enabled = true;

yield return new WaitForSeconds(0.2f);

meleeArea.enabled = false;
trailEffect.enabled = false;

원거리 공격 Shot()
GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);
bullet.GetComponent<Rigidbody>().velocity = bulletPos.forward * 50f;

🔥 4.3 EnemyManager.cs 주요 로직
if (curEnemy < maxEnemy)
{
    StartCoroutine(SpawnEnemy());
}


스폰 지점 목록(spawnPoints)에서 랜덤으로 선택하는 방식이다.

🔥 4.4 Item.cs 구조

아이템 효과는 Value 값으로 유동적으로 변경 가능.

예:

Ammo: +30

Coin: +10

Heart: +20

Weapon: 새로운 무기 언락

📂 5. 개발자가 알아야 할 Prefab 구조

Prefab 구조는 다음과 같다:

▶ Player Prefab
Player
 ├─ Mesh Object
 ├─ BulletPos
 ├─ JumpSound (AudioSource)
 ├─ WeaponHammer
 ├─ WeaponHandgun
 ├─ WeaponSMG
 ├─ WeaponGrenade


각 무기들은 비활성 상태로 플레이어가 장착 시 활성화됨.

▶ Enemy Prefab
Enemy
 ├─ Mesh Object
 ├─ LookTarget
 ├─ Health UI

▶ Weapon Prefab
WeaponX
 ├─ Model
 ├─ BoxCollider
 ├─ Trail Renderer
 ├─ AudioSource(fireSound)
 └─ Weapon.cs

📂 6. 기능 확장 방법
🧩 6.1 새로운 무기 추가하기

기존 무기 Prefab을 복사

damage / rate 등 조정

fireSound 연결

Weapon.cs에 bulletPrefab 또는 melee 설정

Player.cs의 무기 배열에 등록

🧩 6.2 새로운 적 추가하기

Enemy Prefab 복사

Enemy.cs에서 HP, speed 조정

EnemyManager.cs의 enemyPrefab 배열에 추가

🧩 6.3 새로운 아이템 추가하기

Item Prefab 복사

Type 설정

Value 설정

🧩 6.4 스테이지 확장

EnemyManager.cs에서 spawn 속도, 최대 적 수, stage 상승 규칙을 변경 가능.

📂 7. 빌드 가이드

File → Build Settings

Platform: PC/Mac/Linux Standalone

Add Open Scenes

Build

게임 실행 파일이 생성된다.

📂 8. GitHub 개발자 워크플로우
✔ 기본 워크플로우
git pull
git add .
git commit -m "message"
git push

✔ 충돌 방지를 위해 권장되는 브랜치 전략
main : 완성된 빌드
dev  : 개발 중 기능
feature/기능명 : 개별 기능 개발

✔ 강제 덮어쓰기는 금지(관리자만 허용)
git push --force

📂 9. 자주 발생하는 오류 및 해결
❗ 무기 소리가 안 난다

→ Weapon prefab의 fireSound가 null일 확률 높음
→ AudioSource가 Prefab에 존재하는지 확인

❗ Player가 공격을 못 한다

→ Player.cs → weapons 배열 연결 누락
→ Weapon prefab의 Collider가 disabled 상태

❗ 적이 너무 많이 나온다

→ EnemyManager → maxEnemy 조정

❗ 상점이 동작하지 않는다

→ Shop.cs의 Player와 GameManager 레퍼런스 확인

❗ Prefab 연결이 풀림

→ 파일 이름을 OS에서 변경했을 가능성
→ 반드시 Unity Editor 안에서 Rename/Move 수행

📂 10. 결론: 개발자가 반드시 알아야 할 핵심 5가지

Player는 Weapon에게 공격 기능을 위임하는 구조

대부분의 설정은 Prefab으로 관리되며 코드 수정 없이 변경 가능

EnemyManager가 게임 난이도와 스테이지 제어의 핵심

사운드 시스템은 Player/Weapon/Explosion 세 분리 구조

Unity에서는 파일 이동·삭제를 반드시 Editor 내부에서 수행
    Interaction();
    Attack();
    Reload();
}
