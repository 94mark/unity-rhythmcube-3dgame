# unity-rhythm-cube-3dgame
리듬큐브 게임 제작

https://user-images.githubusercontent.com/90877724/162144096-e8eaddc3-c55a-4b7e-87e9-6ed9009bffca.mp4

## 1. 프로젝트 개요
### 1-1. 개발 인원/기간 및 포지션
- 1인, 총 7일 소요
### 1-2. 개발 환경
- Unity 2020.3.16f
- 언어 : C#
- OS : Window 10
- server : 뒤끝서버

## 2. 핵심 구현 내용
### 2-1. 노트 생성/파괴 및 판정
- 노트 생성 position에서 Vector3.Right 방향으로 note 이동
- OnTriggerExit2D() 메서드를 사용하여 collider 충돌 시 파괴
- 60s / bpm = 1 beat 시간을 의미(예 : 120bpm은 0.5beat로 0.5초 간격으로 노트 생성)
```c#
currentTime += Time.deltaTime;

if(currentTime >= 60d / bpm)
{
  GameObject t_note = Instantiate(goNote, tfNoteAppear.position, Quaternion.identity);
  currentTime -= 60d / bpm;
}
```
- perfect/ cool/ good/ bad 판정 box 생성
- 각 rect 별 판정 범위는 최소값(중심 - rect/2) ~ 최댓값(중심 + rect/2), perfect = 가장 좁은 판정 / bad = 가장 넓은 판정
```c#
timingBoxs = new Vector2[timingRect.Length];

for(int i = 0; i < timingRect.Length; i++)
   {
       timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2, Center.localPosition.x + timingRect[i].rect.width / 2);
   } 
```
- space 키 입력 시 판정이 연출되도록 CheckTiming() 메서드 생성, 애니메이션 이펙트 추가
```c#
public bool CheckTiming()
    {
        for(int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;

            for(int x = 0; x < timingBoxs.Length; x++)
            {
                if(timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {                    
                    boxNoteList[i].GetComponent<Note>().HideNote();
                    boxNoteList.RemoveAt(i);
                    theEffect.JudgementEffect(x);
                }
            }
        }
    }
```
- perfect / cool / good 일 때만 effect가 실행되고 bad 판정 시 effect가 실행되지 않도록 코드 구현
- peferct -> cool -> good -> bad 순으로 도는 for문을 실행하고 -1 해주어 bad는 돌지 않도록 함 
```c#
if (x < timingBoxs.Length - 1) 
      theEffect.NoteHitEffect(); 
```
### 2-2. 스코어
- 점수 시스템 구현
- 콤보 시스템 구현
### 2-3. 플레이어 이동 및 카메라 액션
### 2-4. 스테이지
- 다중 스테이지 구현
- 골인 지점 구현
- 낭떠러지 추락
### 2-5. 게임 UI
-  체력 및 실드 시스템 구현
-  결과창, 메뉴 UI
### 2-6. 서버 

## 3. 문제 해결 내용
### 3-1. 노트 판정 딜레이 문제
- 60s / bpm = 1 beat 시간으로 세팅값 설정(예 : bpm이 120이면 0.5초당 note 하나 생성)
- 노트 생성 후 `currentTime = 0`으로 초기화했는데 시간이 지날수록 박자 딜레이가 생기는 문제가 발생
- 게임이 프레임 단위로 흘러가기 때문에 currentTime이 정확히 0.5가 아닌, 0.51005551..로 미세한 시간 오차가 발생
- 시간 오차가 적용된 상태로 초기화된 것이기 때문에 시간차가 누적되어 노트 생성과 bpm 박자 불일치
- `currentTime -= 60d / bpm;` 으로 초기화하여 문제 해결
### 3-2. 노트 판정 시 음악 재생 문제
- 노트가 centerframe(collider 영역)지날 때 배경 음악 재생하도록 설정
- 하지만 노트가 centerframe 전에 판정받아 destroy되면 음악이 재생되지 않는 문제 발생, 첫 노트부터 음악이 계속 재생되어야 함
- 처음 노트가 판정을 받을 때 destroy가 아닌 enabled하여 이미지만 비활성화되고 노트는 centerframe을 지나도록 하여 문제 해결
- `boxNoteList[i].GetComponent<Note>().HideNote();` HideNnote() 메서드는 `noteImage.enabled = false`로 해줌 
### 3-3. 노트를 놓치지 않았음에도 miss 애니메이션이 실행되는 문제
- 노트 판정 이미지 애니메이션(perfect/cool/good/bad/miss) 실행 과정에서 miss 애니메이션만 계속 실행되는 문제 발생
- 기존에 음악 재생 문제 해결을 위해 note의 이미지만 비활성화 했기 때문에 노트 객체는 계속 miss 콜라이더와 충돌하는 원인 파악
- note 이미지의 enabled 여부를 판단하는 GetNoteFlag() bool 타입 메서드를 만들어 note 이미지가 활성화되있는 경우에만 miss 애니메이션을 연출하도록하여 해결
```c#
if(collision.GetComponent<Note>().GetNoteFlag())
  theEffectManager.JudgementEffect(4);
```
