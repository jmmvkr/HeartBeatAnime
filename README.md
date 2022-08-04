# HeartBeatAnime
A WPF demo for Heart Beat Anime

# Demo Video
https://github.com/jmmvkr/HeartBeatAnime/tree/main/HeartBeatAnime/demo

# Pseudo Code for Arduino Application
```
int MAX_TIMELINE;
int MAX_FRAME;
int arrFrame[];
int heartOnOff;
int iFrame;

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
