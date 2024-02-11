This application is one of my earliest works when I was still getting a grasp on programming. As such... It sucks. I wouldn't suggest using it or relying on it too much. It just needs a rewrite.

#BadModHunter

First of all, I’d like to re-iterate. The Hunter does NOT under any circumstance write or modify your files in any way. It reads them and that is all. The hunter is also not perfect and it may give you false positives or it may miss other errors entirely. If you suspect a false positive or encounter it missed an error it should’ve caught, let me know so I can continue improving the hunter.

The Hunter at the moment is capable of showing many of the bad practices within mods that were previously unknown to the user and ignored by the game. In doing so, it might give users a sense that all of the given errors are critical, but quite a few are in fact ignorable.

Most critical errors are indeed critical except for some select few errors when the hunter fails to read a model file. These are in-fact errors but the game handles them so they don’t cause any issues. Regardless, these can be fixed by simply opening the file in the CM3d2-Converter (The blender plugin) and resaving the model file. Or they can be very simply ignored. But sometimes the issue is indeed critical and should be resolved immediately.
Errors in yellow are usually warnings and can be more or less safely ignored. They’re more suggestions than actual immediate stability threats.

Sometimes a model can also be missing a mod file (typically a texture) and not cause any problems if and WHEN a mate file is being applied into a specific slot. In these cases you can ignore model files not finding an error, otherwise, you will get errors and this should be remediated immediately.
