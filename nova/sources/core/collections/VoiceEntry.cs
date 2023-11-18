    using System;
    
    namespace Nova;

    [Serializable]
    public class VoiceEntry
    {
        public readonly string voiceFileName;
        public readonly float voiceDelay;

        public VoiceEntry(string voiceFileName, float voiceDelay)
        {
            this.voiceFileName = voiceFileName;
            this.voiceDelay = voiceDelay;
        }
    }