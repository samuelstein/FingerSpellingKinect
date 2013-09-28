using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Diagnostics;


namespace FingerSpelling.Speech
{
    class SpeechSynthesizerHandler
    {
        private SpeechSynthesizer speaker;

        public SpeechSynthesizerHandler()
        {
            speaker = new SpeechSynthesizer();
            //In dem Fall unnötig, aber falls zB vorher OutputToWav eingestellt war
            speaker.SetOutputToDefaultAudioDevice();
            List<VoiceInfo> installedVoices = GetInstalledVoices();
            Debug.WriteLine(installedVoices.Count);
            foreach (VoiceInfo voice in installedVoices)
            {
                Debug.WriteLine("voice " + voice.Description);
            }

        }

        public void TextToSpeech(String text, int rate, int volume)
        {
            //Geschwindigkeit (-10 - 10)
            speaker.Rate = rate;
            //Lautstärke (0-100)
            speaker.Volume = volume;
            //Such passende Stimme zu angegebenen Argumenten
            speaker.SelectVoiceByHints(VoiceGender.Neutral, VoiceAge.Adult);
            //speaker.SelectVoice("LH Michael");
            //Text wird ausgegeben (abbrechen mit speaker.CancelAsync())
            speaker.SpeakAsync(text);
        }

        public void StopVoice()
        {
            speaker.SpeakAsyncCancelAll();
        }

        //zusätzliche Methode, kann manchmal nützlich sein
        private List<VoiceInfo> GetInstalledVoices()
        {
            var listOfVoiceInfo = from voice
                                  in speaker.GetInstalledVoices()
                                  select voice.VoiceInfo;

            return listOfVoiceInfo.ToList<VoiceInfo>();
        }
    }
}
