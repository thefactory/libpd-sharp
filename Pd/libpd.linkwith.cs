using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libpd.a", LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Simulator, ForceLoad = true, Frameworks = "AVFoundation")]
