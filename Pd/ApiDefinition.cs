using System;
using MonoTouch.AudioUnit;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

namespace Pd {

    [BaseType(typeof(NSObject))]
    public partial interface PdAudioController {
        [Export("sampleRate")]
        int SampleRate { get; }

        [Export("numberChannels")]
        int NumberChannels { get; }

        [Export("inputEnabled")]
        bool InputEnabled { get; }

        [Export("mixingEnabled")]
        bool MixingEnabled { get; }

        [Export("ticksPerBuffer")]
        int TicksPerBuffer { get; }

        [Export("active")]
        bool Active { [Bind ("isActive")] get; set; }

        [Export("configurePlaybackWithSampleRate:numberChannels:inputEnabled:mixingEnabled:")]
        PdAudioStatus ConfigurePlaybackWithSampleRate(int sampleRate, int numChannels, bool inputEnabled, bool mixingEnabled);

        [Export("configureAmbientWithSampleRate:numberChannels:mixingEnabled:")]
        PdAudioStatus ConfigureAmbientWithSampleRate(int sampleRate, int numChannels, bool mixingEnabled);

        [Export("configureTicksPerBuffer:")]
        PdAudioStatus ConfigureTicksPerBuffer(int ticksPerBuffer);

        [Export("print")]
        void Print();
    }

    [BaseType(typeof(NSObject))]
    public partial interface PdAudioUnit {
        [Export("audioUnit")]
        IntPtr AudioUnit { get; }

        [Export("active")]
        bool Active { [Bind ("isActive")] get; set; }

        [Export("configureWithSampleRate:numberChannels:inputEnabled:")]
        int ConfigureWithSampleRate(double sampleRate, int numChannels, bool inputEnabled);

        [Export("print")]
        void Print();
    }

    [Model, BaseType(typeof(NSObject))]
    public partial interface PdListener {
        [Export("receiveBangFromSource:")]
        void ReceiveBangFromSource(string source);

        [Export("receiveFloat:fromSource:")]
        void ReceiveFloat(float received, string source);

        [Export("receiveSymbol:fromSource:")]
        void ReceiveSymbol(string symbol, string source);

        [Export("receiveList:fromSource:")]
        void ReceiveList(NSObject[] list, string source);

        [Export("receiveMessage:withArguments:fromSource:")]
        void ReceiveMessage(string message, NSObject[] arguments, string source);
    }

    [Model]
    public partial interface PdReceiverDelegate : PdListener {
        [Export("receivePrint:")]
        void ReceivePrint(string message);
    }

    [Model, BaseType(typeof(NSObject))]
    public partial interface PdMidiListener {
        [Export("receiveNoteOn:withVelocity:forChannel:")]
        void ReceiveNoteOn(int pitch, int velocity, int channel);

        [Export("receiveControlChange:forController:forChannel:")]
        void ReceiveControlChange(int value, int controller, int channel);

        [Export("receiveProgramChange:forChannel:")]
        void ReceiveProgramChange(int value, int channel);

        [Export("receivePitchBend:forChannel:")]
        void ReceivePitchBend(int value, int channel);

        [Export("receiveAftertouch:forChannel:")]
        void ReceiveAftertouch(int value, int channel);

        [Export("receivePolyAftertouch:forPitch:forChannel:")]
        void ReceivePolyAftertouch(int value, int pitch, int channel);
    }

    [Model]
    public partial interface PdMidiReceiverDelegate : PdMidiListener {
        [Export("receiveMidiByte:forPort:")]
        void ForPort(int byte_, int port);
    }

    [BaseType(typeof(NSObject))]
    public partial interface PdBase {
        [Static, Export("initialize")]
        void Initialize();

        [Static, Export("setDelegate:pollingEnabled:")]
        void SetDelegate(NSObject newDelegate, bool pollingEnabled);

        [Static, Export("setMidiDelegate:pollingEnabled:")]
        void SetMidiDelegate(NSObject newDelegate, bool pollingEnabled);

        [Static, Export("delegate")]
        NSObject Delegate { get; set; }

        [Static, Export("midiDelegate")]
        NSObject MidiDelegate { get; set; }

        [Static, Export("receiveMessages")]
        void ReceiveMessages();

        [Static, Export("receiveMidi")]
        void ReceiveMidi();

        [Static, Export("clearSearchPath")]
        void ClearSearchPath();

        [Static, Export("addToSearchPath:")]
        void AddToSearchPath(string path);

        [Static, Export ("openFile:path:")]
        IntPtr OpenFile (string baseName, string pathName);

        [Static, Export ("closeFile:")]
        void CloseFile (IntPtr x);

        /* [Static, Export ("dollarZeroForFile:")] */
        /* int DollarZeroForFile ([unmapped: pointer: Pointer] x); */

        [Static, Export("getBlockSize")]
        int GetBlockSize { get; }

        [Static, Export("openAudioWithSampleRate:inputChannels:outputChannels:")]
        int OpenAudioWithSampleRate(int samplerate, int inputChannels, int outputchannels);
        /* [Static, Export ("processFloatWithInputBuffer:outputBuffer:ticks:")] */
        /* int ProcessFloatWithInputBuffer ([unmapped: pointer: Pointer] inputBuffer, [unmapped: pointer: Pointer] outputBuffer, int ticks); */
        /* [Static, Export ("processDoubleWithInputBuffer:outputBuffer:ticks:")] */
        /* int ProcessDoubleWithInputBuffer ([unmapped: pointer: Pointer] inputBuffer, [unmapped: pointer: Pointer] outputBuffer, int ticks); */
        /* [Static, Export ("processShortWithInputBuffer:outputBuffer:ticks:")] */
        /* int ProcessShortWithInputBuffer ([unmapped: pointer: Pointer] inputBuffer, [unmapped: pointer: Pointer] outputBuffer, int ticks); */
        [Static, Export("computeAudio:")]
        void ComputeAudio(bool enable);
        /* [Static, Export ("subscribe:")] */
        /* [unmapped: pointer: Pointer] Subscribe (string symbol); */
        /* [Static, Export ("unsubscribe:")] */
        /* void Unsubscribe ([unmapped: pointer: Pointer] subscription); */
        [Static, Export("exists:")]
        bool Exists(string symbol);

        [Static, Export("sendBangToReceiver:")]
        int SendBangToReceiver(string receiverName);

        [Static, Export("sendFloat:toReceiver:")]
        int SendFloat(float value, string receiverName);

        [Static, Export("sendSymbol:toReceiver:")]
        int SendSymbol(string symbol, string receiverName);

        [Static, Export("sendList:toReceiver:")]
        int SendList(NSObject[] list, string receiverName);

        [Static, Export("sendMessage:withArguments:toReceiver:")]
        int SendMessage(string message, NSObject[] list, string receiverName);

        [Static, Export("arraySizeForArrayNamed:")]
        int ArraySizeForArrayNamed(string arrayName);
        /* [Static, Export ("copyArrayNamed:withOffset:toArray:count:")] */
        /* int CopyArrayNamed (string arrayName, int offset, [unmapped: pointer: Pointer] destinationArray, int n); */
        /* [Static, Export ("copyArray:toArrayNamed:withOffset:count:")] */
        /* int CopyArray ([unmapped: pointer: Pointer] sourceArray, string arrayName, int offset, int n); */
        [Static, Export("sendNoteOn:pitch:velocity:")]
        int SendNoteOn(int channel, int pitch, int velocity);

        [Static, Export("sendControlChange:controller:value:")]
        int SendControlChange(int channel, int controller, int value);

        [Static, Export("sendProgramChange:value:")]
        int SendProgramChange(int channel, int value);

        [Static, Export("sendPitchBend:value:")]
        int SendPitchBend(int channel, int value);

        [Static, Export("sendAftertouch:value:")]
        int SendAftertouch(int channel, int value);

        [Static, Export("sendPolyAftertouch:pitch:value:")]
        int SendPolyAftertouch(int channel, int pitch, int value);

        [Static, Export("sendMidiByte:byte:")]
        int SendMidiByte(int port, int byte_);

        [Static, Export("sendSysex:byte:")]
        int SendSysex(int port, int byte_);

        [Static, Export("sendSysRealTime:byte:")]
        int SendSysRealTime(int port, int byte_);
    }

    [BaseType(typeof(NSObject))]
    public partial interface PdDispatcher : PdReceiverDelegate {
        [Export("addListener:forSource:")]
        int AddListener(NSObject listener, string source);

        [Export("removeListener:forSource:")]
        int RemoveListener(NSObject listener, string source);

        [Export("removeAllListeners")]
        void RemoveAllListeners();
    }

    [BaseType(typeof(NSObject))]
    public partial interface PdFile {
        [Export("dollarZero")]
        int DollarZero { get; }

        [Export("baseName", ArgumentSemantic.Copy)]
        string BaseName { get; }

        [Export("pathName", ArgumentSemantic.Copy)]
        string PathName { get; }

        [Static, Export("openFileNamed:path:")]
        NSObject OpenFileNamed(string baseName, string pathName);

        [Export("openNewInstance")]
        NSObject OpenNewInstance { get; }
        /* [Export ("isValid"), Verify ("ObjC method massaged into getter property", "/Users/peter/git/libpd/objc/PdFile.h", Line = 28)] */
        /* [unmapped: bool: Builtin] IsValid { get; } */
        [Export("closeFile")]
        void CloseFile();
    }

    [BaseType(typeof(NSObject))]
    public partial interface PdMidiDispatcher : PdMidiReceiverDelegate {
        [Export("addListener:forChannel:")]
        int AddListener(NSObject listener, int channel);

        [Export("removeListener:forChannel:")]
        int RemoveListener(NSObject listener, int channel);

        [Export("removeAllListeners")]
        void RemoveAllListeners();
    }
}
