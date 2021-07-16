# 에마플레이어(EmaPlayer)

![image](https://drive.google.com/uc?export=view&id=1Yfst2tgn3E248mmLO5A5KpT1dbJUpYRS)

사운드 호라이즌 에마에게 소원을!을 틀어놓고 작업하고 싶은데 선택지 선택이 귀찮아서 만든 플레이어입니다.
초기 버전이라 버그가 많습니다.

Loop player for Sound Horizon Ema ni Negai wo!. Automatically select the choice.

## 설치방법(How to install)
[링크](https://github.com/Yongjun042/EmaPlayer/releases)에서 최신 Emaplayer.7z을 다운받아 압축을 해제합니다.

Download Newest emaplayer from link.

[링크](https://sourceforge.net/projects/mpv-player-windows/files/libmpv/) 에서 mpv-dev-x86_64....7z을 다운받습니다.

Download mpv-dev-x86_64....7z from link.

해당 압축파일 안에 있는 mpv-1.dll파일을 조금전에 압축해제한 EmaPlayer 폴더에 넣습니다.

move mpv-1.dll from zip file to EmaPlyaer folder.

![image](https://drive.google.com/uc?export=view&id=1XdJdCh8JC_tLJpfjyya0MjKgHr_sQ9JM)

EmaPlayer.exe를 실행합니다. .Net Core의 설치가 필요할 수 도 있습니다.

Run EmaPlayer.exe You might need to install .Net Core.

![image](https://drive.google.com/uc?export=view&id=1P-Yz-n0gY95Lm6_eOe3x8z6FK5WGO0NV)
![image](https://drive.google.com/uc?export=view&id=1nOPU3x2oJEqAyHGDduMnDbzMHUW7vXMr)


##사용방법(How to use)

좌측 상단의 폴더 - 폴더 열기 를 한 후 에마에게 소원을! 재생파일(.m2ts)이 있는 폴더를 선택합니다.

(예 SOUND_HORIZON_EMANINEGAHIWO\BDMV\STREAM)

그러면 자동으로 재생이 됩니다.

Click File - Load Folder and select the folder of ema playing file(.m2ts).

(EX SOUND_HORIZON_EMANINEGAHIWO\BDMV\STREAM)

![image](https://drive.google.com/uc?export=view&id=10rB6bp5c9wMKiuAMBCELl_oBbAz_xbtA)

[MakeMkv](https://www.makemkv.com/)를 이용해서 블루레이를 복사하면 재생에 필요한 파일을 얻을 수 있습니다.

MakeMKV실행 후 좌상단의 File-Backup을 이용하시면 됩니다.(Decrypt video files 가 꼭 체크되어 있어야 합니다!)

You can use MakeMkv to get a file. Use File-Backup(Decrypt video files).

![image](https://drive.google.com/uc?export=view&id=1GJHgYk1dHGoS4vf9GpvuO-h69TDtbb1r)

인트로와 타이틀 부분은 스킵됩니다.

비석 스킵, 크레딧 스킵을 체크해 놓으면 선택지와 엔딩 크레딧을 스킵할 수 있습니다.

소원 선택지가 재생될 경우 정지상태(일시정지가 아닙니다.)가 아닐 경우 무조건 약 5초후에 다음 영상이 재생됩니다. 주의해주세요.

Intro and title will be skipped.

You can skip stone select, and Ending credit.

If the Choice scene get played, it will play next scene in any circumstances(except STOP) about 5 seconds after. Be aware.

![image](https://drive.google.com/uc?export=view&id=1SmJt4xsoc3NXXeKxvYLz0TslptRLbzFq)

![image](https://drive.google.com/uc?export=view&id=1yqVSVUVGZkdMEdluhDBLdOCL7THRZ1Jt)

선택지 루트를 미리 정하거나 히드 트랙의 확률을 조정할 수 있습니다.

You can choose route or adjust the probability of hidden track.

엔터 버튼을 눌러 전체화면과 창모드로 변경할 수 있고, H버튼을 눌러 UI를 숨기거나 표시할 수 있습니다.

Press Enter for FullScreen/Window Mode, H for hide/show UI.

## 알려진 버그(Known issue)

###
아이콘 제작자 [Freepik](https://www.freepik.com) from [Flaticon](https://www.flaticon.com/kr/)
Mpv.Net.Wpf 사용
