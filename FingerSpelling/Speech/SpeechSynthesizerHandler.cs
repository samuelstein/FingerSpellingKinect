using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Diagnostics;


namespace FingerSpelling.Speech
{
    /// <summary> 
    /// Represents the speech synthesizer.</summary>
    class SpeechSynthesizerHandler
    {
        private SpeechSynthesizer speaker;

        /// <summary> 
        /// Constructor</summary>
        public SpeechSynthesizerHandler()
        {
            speaker = new SpeechSynthesizer();
            speaker.SetOutputToDefaultAudioDevice();
            List<VoiceInfo> installedVoices = GetInstalledVoices();
            Debug.WriteLine(installedVoices.Count);
            foreach (VoiceInfo voice in installedVoices)
            {
                Debug.WriteLine("voice " + voice.Description);
            }

        }

        /// <summary> 
        /// Translates the given string to voice with the desired voice.</summary>
        public void TextToSpeech(String text, int rate, int volume)
        {
            //speed (-10 - 10)
            speaker.Rate = rate;
            //volume (0-100)
            speaker.Volume = volume;
            //Get desired voice
            speaker.SelectVoiceByHints(VoiceGender.Neutral, VoiceAge.Adult);
            speaker.SpeakAsync(text);
        }

        /// <summary> 
        /// Stops all voices.</summary>
        public void StopVoice()
        {
            speaker.SpeakAsyncCancelAll();
        }

        /// <summary> 
        /// Get all voices installed on the system.</summary>
        private List<VoiceInfo> GetInstalledVoices()
        {
            var listOfVoiceInfo = from voice
                                  in speaker.GetInstalledVoices()
                                  select voice.VoiceInfo;

            return listOfVoiceInfo.ToList<VoiceInfo>();
        }
    }
}
