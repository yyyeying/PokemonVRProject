# 游戏全局变量
## 简介
由于本游戏包含动画、UI等不同部分，各个部分之间需要同步游戏状态，故设置全局变量。

比如说一只宝可梦由于检测到和水果（武器）发生碰撞而触发了战斗，那么他就可以通过修改名为`status`的变量，使得游戏UI等其他部分通过读取变量得知目前进入了新的状态，从而做自己该做的事（比如UI从巡游状态切换至战斗状态）。

全局变量名称为`Global`，脚本为`Global.cs`，继承了`MonoBehavior`类并绑定在FPC上。由于FPC在游戏中不会被销毁，所以这样做应该是安全的。

## 变量介绍
### 游戏状态
游戏状态采用了枚举类型，游戏目前分为玩家巡游状态和战斗状态，之后若需要添加新的类型则继续添加。
```
public GameStat status;
```
```
public enum GameStat
    {
        WALK,
        BATTLE
    }
```

### Pokemon Ball 数量
初始化为0。
```
public int pokemonBallCount;
```

### 血槽
初始化为100。
```
public int hp;
```

## 如何读取及修改全局变量
首先在自己的脚本中声明两个变量，一个是`player`，一个是`global`。
```
private GameObject player;
private Global global;
```
然后在`Awake()`或`Start()`中通过查找进行初始化。
```
player = GameObject.Find("Player");
global = player.GetComponent<Global>();
```
然后就可以通过`global.xxx`访问和修改任何一个全局变量了。

## 用例
在控制宝可梦的脚本中，需要根据当前游戏状态，使宝可梦播放不同的动画。可以通过读取`global.status`实现。
```
void Update()
    {
        switch(global.status)
        {
            case Global.GameStat.WALK:
                // Some code...
                break;
            case Global.GameStat.BATTLE:
                // Some code...
                break;
        }        
    }
```