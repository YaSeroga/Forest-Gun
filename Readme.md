## Table Of Contents

  - [Introduction](#introduction)
  - [How To Use](#how-to-implement)
  	- [Setup scriptable objects](#setup-scriptable-objects)
  	- [Setup service class](#setup-service-class)
  - [Let\`s go deeper](#Lets-go-deeper)
  	- [Playing tracking](#playing-tracking)
  	- [Stopping clips](#stopping-clips)
  	- [Decorators](#decorators)

## Introduction
Audio system created to easier realization of audio systems in different projects. It's not complex to understand but allows you implement and extend audio systems as fast as asset as you want.

## How to implement
To start use audio system just install it into your project and do 2 simple steps:

### Setup scriptable objects
 There are only 3 scriptable objects you need. There are already configured objects for demo in `AudioSystem\Data` directory.
 - `Audio channels data` - This object contains how much there will channels which channel it will and which `AudioMixerGroup` will on it.
 - `Audio Data` - Contains path to directory with clips in `Resources` folder and names of clips with paths to files.
 - `Audio Events` - Contains events and which sound this event should call. For example Game designer can change clip name of any event and if event called somewhere according sound will called.
---
Create all of them you can by right click on project window **Create &rarr; Audio system &rarr; Choose when you want**

### Setup service class
There are 2 ready ways and 3rd if you use Zenject.
- `MonoAudioService` - To use it add component on scene and link all data references which described above.
- `AudioService` - instantiate this class and use it but remember to update it manually through `AudioService.Update()` method.
- `AudioServiceTickable` - if you using [Zenject](https://github.com/modesttree/Zenject) you can bind them [ITickable](https://github.com/modesttree/Zenject) interface to any container and use `void Tick()` to call Update method.
---
Thats all! Your audio system is ready to use, just use `AudioService.PlaySound(...)` or `AudioService.PlayEvent(...)` methods.

## Let\`s go deeper

### Playing tracking
If you need to track playing of clip, for any reason, you can use `IPlayingInfo` which returned by `AudioService.PlaySound(...)` or `AudioService.PlayEvent(...)` method. It updates in real time and allows you to see actual information about playing. But if you found this as solution to your problem, then maybe [decorators](#decorators) are right for you?

### Stopping clips
You can stop audio by 3 ways:
- Stop all clips in all channels - for this just call `AudioSystem.Stop()` method
- Stop only selected channel - For this call `AudioSystem.Stop(int index)` with index of channel you want to stop
- Stop concrete clip - For this you should save `IPlayingInfo` and use it as argument in `AudioSystem.Stop(IPlayingInfo info)` method.

### Decorators
Decorators is utility which allows you to add effects, changes, trackers and whatever your heart desires to sounds playing without changing core code. It have 4 events:
- `OnPlay` - called before playing started.
- `BeforeUpdate` - Called before processor update clip playing (volume fade unfade for example).
- `AfterUpdate` - Called after processor updated clip playing.
- `OnEnd` - Called when clip doesn't playing anymore.

To use in just create class inherited from `IAudioDecorator` and push it in any `Play` method you want as parameter. Here\`s an example

```csharp
AudioSystem.PlayEvent(myEventName, myBeautifulDecorator);
AudioSystem.PlayEvent(myClipName, myBeautifulDecorator);
```

In project already 3 realization of this interface. That\`s for position, parent, and 3d space enabling on `AudioSource`. Here\`s one of them
``` csharp
public class ParentAudioDecorator : IAudioDecorator
{
	private Transform _oldParent;
	private Transform _newParent;

	public ParentAudioDecorator(Transform newParent) => _newParent = newParent;

	public void OnPlay(IPlayingInfo info)
	{
		_oldParent = info.AudioSource.transform.parent;
		info.AudioSource.transform.SetParent(_newParent);
	}

	public void BeforeUpdate(IPlayingInfo info){}

	public void AfterUpdate(IPlayingInfo info){}

	public void OnEnd(IPlayingInfo info) => info.AudioSource.transform.SetParent(_oldParent);
}
```

By this functionality you can change pith at start or change volume depended speed of your character, or pitch of audio source, or notify anyone about events **but don't forgot to discard all your changes in `OnEnd` method** because audio source returning in pool and your changes can be in next sound playing on this `AudioSource`.

