using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using Microsoft.Speech.Recognition;
using System.IO;
using Microsoft.Speech.AudioFormat;
using System.ComponentModel;

namespace _20120718KinectTV.Speech
{
    class SpeechManager : INotifyPropertyChanged
    {

        private SpeechRecognitionEngine _sre;
        private KinectAudioSource _source;

        public SpeechManager()
        {
            
        }

        public void StartSpeechRecognition(SpeechRecognitionEngine sre, KinectAudioSource source)
        {
            //source = CreateAudioSource();
            this._sre = sre;
            this._source = source;

            Func<RecognizerInfo, bool> matchingFunc = r =>
            {
                string value;
                r.AdditionalInfo.TryGetValue("Kinect", out value);
                return "True".Equals(value, StringComparison.InvariantCultureIgnoreCase)
                    && "en-US".Equals(r.Culture.Name, StringComparison.InvariantCultureIgnoreCase);
            };
            RecognizerInfo ri = SpeechRecognitionEngine.InstalledRecognizers().Where(matchingFunc).FirstOrDefault();

            this._sre = new SpeechRecognitionEngine(ri.Id);
            CreateGrammars(ri);
            this._sre.SpeechRecognized += sre_SpeechRecognized;
            this._sre.SpeechHypothesized += sre_SpeechHypothesized;
            this._sre.SpeechRecognitionRejected += sre_SpeechRecognitionRejected;

            Stream s = this._source.Start();
            this._sre.SetInputToAudioStream(s,
                                        new SpeechAudioFormatInfo(
                                            EncodingFormat.Pcm, 16000, 16, 1,
                                            32000, 2, null));
            this._sre.RecognizeAsync(RecognizeMode.Multiple);
        }

        

        private void CreateGrammars(RecognizerInfo ri)
        {
            var colors = new Choices();
            colors.Add("cyan");
            colors.Add("yellow");
            colors.Add("magenta");
            colors.Add("blue");
            colors.Add("green");
            colors.Add("red");

            var create = new Choices();
            create.Add("create");
            create.Add("put");

            var shapes = new Choices();
            shapes.Add("circle");
            shapes.Add("triangle");
            shapes.Add("square");
            shapes.Add("diamond");

            var gb = new GrammarBuilder();
            gb.Culture = ri.Culture;
            gb.Append(create);
            gb.AppendWildcard();
            gb.Append(colors);
            gb.Append(shapes);
            gb.Append("there");

            var g = new Grammar(gb);
            this._sre.LoadGrammar(g);

            var q = new GrammarBuilder();
            q.Append("quit application");
            var quit = new Grammar(q);

            //_sre.LoadGrammar(quit);
        }

       

        

        private void sre_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            HypothesizedText += " Rejected";
            Confidence = Math.Round(e.Result.Confidence, 2).ToString();
            
        }

        void sre_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            HypothesizedText = e.Result.Text;
        }

        private void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //Dispatcher.BeginInvoke(new Action<SpeechRecognizedEventArgs>(InterpretCommand), e);
        }

        public void CloseSpeech(SpeechRecognitionEngine sre)
        {
            sre.RecognizeAsyncCancel();
            sre.RecognizeAsyncStop();
            //_source.Dispose();
            sre.Dispose();
        }

        private string _hypothesizedText;
        public string HypothesizedText
        {
            get { return _hypothesizedText; }
            set
            {
                _hypothesizedText = value;
                OnPropertyChanged("HypothesizedText");
            }
        }

        private string _confidence;
        public string Confidence
        {
            get { return _confidence; }
            set
            {
                _confidence = value;
                OnPropertyChanged("Confidence");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        
    }
}
