# ASP.NET Core 테스트 프로젝트
ASP.NET Core 성능 테스트 및 버전 업데이트 테스트를 위한 프로젝트

------------

## 프로젝트 실행 전 설정 가이드
### Docker 연동
1. 메뉴 막대에서 Preferences | Build, Execution, Deployment | Docker 이동
2. 좌측 상단 +(Docker) 버튼 클릭 
3. 개발 PC 환경에 맞게 "Connect to Docker daemon with:" 항목 선택 
4. Save 버튼 클릭

### Run Configurations 설정
1. 메뉴 막대에서 Run | Edit Configurations 이동 
2. 좌측 상단 +(Add New Configuration) 버튼 클릭 
3. Docker | Docker-compose 항목 선택 
4. Compose files 항목에 해당 솔루션 루트에 docker-compose.yml 파일 선택
5. docker compose up 옵션으로 타이틀 우측에 modify를 선택하여 Build | Always 추가
6. OK 버튼 클릭

### Database Data Sources 불러오기
1. database 폴더 내 dataSources.xml 파일 확인
2. dataSources.xml 파일은 아래 경로에 복사  
경로: .idea/.idea.aspnetcore-base-project/.idea/
3. Rider를 실행 후 Database 탐색기에서 Data Souces 항목이 나오는지 확인

------------

## 프로젝트 실행 방법
1. 단축키(control + option + R)를 입력
2. Run 팝업이 나오면 "Run Configurations 설정"에서 생성한 Docker-compose 항목 선택
3. 하단 Service창에서 docker-compose 설정 파일에 따라 이미지 빌드 및 컨테이너 실행

## 프로젝트 종료 방법
### Rider GUI
1. 하단 Toolbar에 Service 항목 선택
2. Service 창에서 우측 영역에 Docker 클릭하여 펼치기
3. Docker 하위에 "Docker-compose: aspnetcore-base-project"를 선택 후 오른쪽 마우스 클릭
4. Down 항목 선택하여 실행된 컨테이너 삭제

### docker cli 명령어
1. 하단 Toolbar에 Terminal 항목 선택
2. Repository 루트로 이동
3. "docker compose down" 명령어 실행하여 컨테이너 삭제

