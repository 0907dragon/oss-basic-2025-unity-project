# 3D 액션 생존 게임 – 개발자가이드

## 1. 개요

이 문서는 팀 프로젝트에서 구현한 3D 액션 생존 게임의 **설계 및 구현 내용을 정리한 개발자용 문서**입니다.  
최종 보고서의 “시스템 아키텍처 / 상세 설계 / 구현 내용” 파트를 기반으로 작성되었습니다.

- 개발 언어: C#
- 엔진: Unity
- 주요 스크립트 위치: `Assets/2Scripts`
- 주요 프리팹: `Assets/1Prefabs`

---

## 2. 전체 구조(시스템 아키텍처 개요)

### 2.1 주요 오브젝트 & 책임

- **GameManager**
  - 전체 게임 상태 관리 (메뉴/게임/게임오버)
  - 스테이지 진행(시작, 종료, 적 스폰, 보스 스폰)
  - 점수, 플레이 시간, UI 업데이트
  - 최대 점수(PlayerPrefs) 저장 및 로딩

- **Player**
  - 입력 처리, 이동, 점프, 회피
  - 무기 장착/교체/공격, 수류탄 투척
  - 아이템 획득, 상점 상호작용
  - 피격 처리, 무적 시간, 사망 처리
  - 플레이어 사운드 재생(점프/구르기/피격/발자국/무기 교체 등)

- **Weapon**
  - 무기 타입 (근접/원거리) 정의
  - 피해량, 발사 속도, 탄약 관리
  - 근접 공격 시 콜라이더/트레일 활성화
  - 원거리 공격 시 탄환 프리팹 생성
  - 무기별 발사 사운드 재생

- **Enemy / Boss**
  - 플레이어 추적, 공격, 피격 및 사망 처리
  - GameManager에 남은 적 수 보고
  - Boss는 체력 바 UI와 연동

- **EnemyManager**
  - 현재 씬 내의 모든 Enemy Transform을 리스트로 관리
  - (필요 시) 미니맵/화면 밖 적 위치 표시용으로 확장 가능

- **Item**
  - 아이템 타입(Ammo, Coin, Grenade, Heart, Weapon) 정의
  - 바닥과 충돌 시 Rigidbody 비활성화로 멈추게 처리
  - Player와 충돌 시 효과 적용 후 삭제

- **Shop**
  - 상점 UI 열기/닫기
  - Player의 coin, ammo, health를 조정
  - GameManager와는 느슨한 결합(필요한 정보만 Player 통해 접근)

---

## 3. 입력 및 플레이어 로직 상세

### 3.1 Player 입력 흐름

`Player.cs`에서 `Update()` 루프는 다음 순서로 동작합니다.

```csharp
void Update()
{
    GetInput();
    Move();
    Turn();
    Jump();
    Grenade();
    Dodge();
    Swap();
    Interaction();
    Attack();
    Reload();
}
