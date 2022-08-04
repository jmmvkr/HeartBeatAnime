# HeartBeatAnime
A WPF demo for Heart Beat Anime

# Demo Video
https://github.com/jmmvkr/HeartBeatAnime/tree/main/HeartBeatAnime/demo

# Pseudo Code for Arduino Application
```
// sample animation frames: int arr[] = { 70, 192, 255, 240, 235, 215, 195, 175, 155, 135, 115, 95, 75 };
const int MAX_TIMELINE = 30;
const int MAX_FRAME = 10;
int iFrame = 0;
int arrFrame[];
int heartOnOff;

showHeartLight()
{
  if ((iFrame >= MAX_FRAME) || (heartOnOff <= 0))
  {
    color = arrFrame[iFrame];
  }
  else
  {
    color = 0;
  }
  setPwmColor(color);
  iFrame = (1 + iFrame) % MAX_TIMELINE;
}

updateHeartSwitch(int nOnOff)
{
   heartOnOff = nOnOff;
   iFrame = 0;
}

loop()
{
  showHeartLight()
  if(gameOver())
  {
    updateHeartSwitch(0)
  }
}
```
