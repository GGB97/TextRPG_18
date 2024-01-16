# Text RPG
C#/.NET8.0 기반 콘솔 텍스트 게임

# 팀원
  길기범 : 퀘스트 선택/완료, 보상, 저장, 스탯 분배
  
  신은지 : 스킬, 치명타, 회피, UI수정
  
  이서현 : 캐릭터 생성, 직업 선택, UI수정
  
  정현우 : 몬스터 추가, 소모품 기능, 스킬, 몬스터 레벨 스케일링

  나머지는 공동작업

# 구현 기능
<details> 
<summary>상태 보기, 레벨업, 스탯 분배</summary>
  
  #### 상태 보기
  <img src = "https://github.com/GGB97/TextRPG_18/assets/78461967/f09dd9c3-93c1-48f4-880d-e91035ea1716">
  
  #### 레벨업
  <img src = "https://github.com/GGB97/TextRPG_18/assets/78461967/02df108d-f46a-4237-8b74-de765e3166a9">

  #### 스탯 분배
    ----- 스탯 포인트 사용시 (+원하는 스탯),(-스탯포인트 차감) -----
  <img src = "https://github.com/GGB97/TextRPG_18/assets/78461967/83f43f00-4b38-4b08-9465-1ca7aea8a275">   

  
</details>


<details> 
<summary>캐릭터 생성, 직업 선택</summary>
  
  #### 캐릭터 생성
  <img src = "https://github.com/GGB97/TextRPG_18/assets/154600776/36e2993c-8e3f-4fb6-93b3-309d79c20984">
  
  #### 직업 선택
  <img src = "https://github.com/GGB97/TextRPG_18/assets/154600776/cc1a042a-24dc-4e3e-adbf-c09e1b1c17f1">
</details>

<details> 
<summary>전투, 스킬, 치명타, 회피, 몬스터 추가, 몬스터 레벨 스케일링</summary>

  #### 전투
  <img src = "https://github.com/GGB97/TextRPG_18/assets/154600776/c3303dd9-204f-4af6-bea3-52f379b41c64" width="800" height="250">
  <img src = "https://github.com/GGB97/TextRPG_18/assets/154600776/3f6f25b1-5a7a-40fe-a922-ab694c644f2a" width="800" height="500">
  <img src = "https://github.com/GGB97/TextRPG_18/assets/154600776/5016e821-978e-4f9c-8b11-7c0716a9b3a1" width="800" height="500">
  
  #### 스킬
  <img src = "https://github.com/GGB97/TextRPG_18/assets/154600776/92f19b11-ded6-4f02-85bd-d7347b820941" width="800" height="250">
  <img src = "https://github.com/GGB97/TextRPG_18/assets/154600776/65c2f070-11e2-4986-87f4-ec0f9e4c54f7" width="800" height="500">
  
  #### 치명타
  <img src = "https://github.com/GGB97/TextRPG_18/assets/128718414/921e7f15-e0de-4f74-8b76-26438019d3a1">   
  <img src = "https://github.com/GGB97/TextRPG_18/assets/128718414/67923f52-0b92-44ca-8791-3483f0585122"> 
    
  #### 회피
  <img src = "https://github.com/GGB97/TextRPG_18/assets/128718414/93158f17-1065-484b-9a28-3b77730805dc">
  <img src = "https://github.com/GGB97/TextRPG_18/assets/128718414/b1daf177-f864-4848-ae0c-e8f109a7938c">
    
  #### 몬스터 추가
    추가한 몬스터 종류: 고블린 / 오크 / 리자드맨 / 고블린 사제 / 트롤 / 흡혈 박쥐
    -각 몬스터는 랜덤 확률로 고유의 스킬을 사용한다.
    -고블린 : 두 개의 스킬을 가진다. 1.'고블린' 또는 '고블린 사제'를 하나 전투에 불러온다. 2. 대기한다.
    -오크 : 자신의 ATK*0.5 만큼 자해하고 플레이어에게 ATK의 2배 데미지를 입힌다.
    -리자드맨 : 두번 공격한다. 총합 계수는 ATK의 2배 데미지.
    -고블린 사제 : 동료 몬스터를 하나 랜덤으로 회복시킨다. 또는 죽은 동료 몬스터를 낮은 확률로 부활시킨다.
    -트롤 : 높은 확률로 대기하면서 회복한다. 체력 회복 + 최대체력 증가(대)
    -흡혈 박쥐 : 플레이어를 공격하고 공격량만큼 회복한다. 흡혈로 체력이 최대에 도달할 시 최대체력 증가(중)
    
    -고블린은 평균 하위의 능력치를 가진다.
    -오크는 평균의 공격력과 높은 체력, 낮은 회피 수치를 가진다.
    -리자드맨은 높은 공격력과 낮은 체력, 높은 회피 수치를 가진다.
    -고블린 사제는 가장 낮은 공격력과 높은 체력, 낮은 회피 수치를 가진다.
    -트롤은 가장 높은 체력과 가장 높은 공격력, 가장 낮은 회피 수치를 가진다.
    -흡혈 박쥐는 낮은 체력, 가장 높은 회피 수치를 가진다.
    
    -고블린 사제, 트롤, 흡혈 박쥐는 랜덤 확률로 포션 3종 중 하나를 드랍한다.
  
  #### 레벨 스케일링
    Player 레벨에 비례해 던전에 출현하는 몬스터가 강해진다.
    -Player 레벨에 따라 전투에 출현하는 몬스터 수의 랜덤값 최대치가 1 증가한다.
    -Player 레벨에 비례한 랜덤값의 *7만큼 몬스터의 최대 체력이 증가한다.
    -Player 레벨에 비례한 랜덤값의 *4만큼 몬스터의 공격력이 증가한다.
    -Player 레벨에 비례한 랜덤값의 *25만큼 몬스터가 드랍하는 골드량이 증가한다.
    -Player 레벨에 비례한 랜덤값의 *15만큼 몬스터가 드랍하는 경험치량이 증가한다.
    -Player 레벨에 비례한 랜덤값의 1/2만큼 몬스터의 회피치가 증가한다.
</details>

<details> 
<summary>퀘스트</summary>
  
  ### 기본 상태
  ![Quest_Default](https://github.com/GGB97/TextRPG_18/assets/99232361/00aee39b-2b33-4fa2-bcbd-dd76d6164a06)
  
  #### 퀘스트 수락
  ![Quest_Accept](https://github.com/GGB97/TextRPG_18/assets/99232361/41297573-bce4-4550-a613-13ab71a0acdf)
    
  #### 퀘스트 포기
  ![Quest_Abandon](https://github.com/GGB97/TextRPG_18/assets/99232361/4a772edd-e7d8-4813-80d4-587335f17c27)

  #### 퀘스트 완료
  ![Quest_Clear1](https://github.com/GGB97/TextRPG_18/assets/99232361/89a1ab16-aeb1-4721-9e24-e9aae514a7d6)
  ![Quest_Clear2](https://github.com/GGB97/TextRPG_18/assets/99232361/ff7a53fc-a091-410a-af69-fbb5b0bfe15c)

  
</details>

<details> 
<summary>아이템/상점, 휴식</summary>
  
  #### 장비 장착/해체
  <img src = "https://github.com/GGB97/TextRPG_18/assets/99232361/42e6b0d5-6a50-4c43-bc06-d55025f0e837">
  
  #### 소모품 사용
  <img src = "https://github.com/GGB97/TextRPG_18/assets/99232361/42e6b0d5-6a50-4c43-bc06-d55025f0e837">

  #### 아이템 구매/판매
  <img src = "https://github.com/GGB97/TextRPG_18/assets/99232361/42e6b0d5-6a50-4c43-bc06-d55025f0e837">

  #### 휴식
  <img src = "https://github.com/GGB97/TextRPG_18/assets/99232361/42e6b0d5-6a50-4c43-bc06-d55025f0e837">
</details>

<details> 
<summary>저장</summary>
  
  #### 저장
  <img src = "https://github.com/GGB97/TextRPG_18/assets/99232361/42e6b0d5-6a50-4c43-bc06-d55025f0e837">
  
  #### 불러오기
  <img src = "https://github.com/GGB97/TextRPG_18/assets/99232361/42e6b0d5-6a50-4c43-bc06-d55025f0e837">
</details>


