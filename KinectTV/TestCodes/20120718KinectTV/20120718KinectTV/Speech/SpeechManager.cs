using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using Microsoft.Speech.Recognition;
using System.IO;
using Microsoft.Speech.AudioFormat;
using System.ComponentModel;
using System.Windows;
using System.Threading;
using System.Windows.Threading;
using System.IO.Ports;

namespace _20120718KinectTV.Speech
{
    class SpeechManager : INotifyPropertyChanged
    {

        private SpeechRecognitionEngine _sre;
        private KinectAudioSource _source;
        private Dispatcher _dispatcher;

        private SerialPort _serialPort = new SerialPort();
        private const string PORTNAME = "COM3";

        public event EventHandler QuitDetected;

        private int _guidencePage = 0;

        public SpeechManager()
        {
            //All names of Ports
            /*
            SerialPort _tempPort;
            String[] PortName = SerialPort.GetPortNames();

            foreach (string str in PortName)
            {
                try
                {
                    _tempPort = new SerialPort(str);
                    _tempPort.Open();
                    if (_tempPort.IsOpen)
                    {
                        Console.WriteLine(str);
                        _tempPort.Close();
                    }
                }
                catch (Exception e)
                { }
            }
             * */

            SpeechGuidenceColumn1 = "0";
            SpeechGuidenceVisibility1 = "Visible";

            SpeechGuidenceColumn2 = "1";
            SpeechGuidenceVisibility2 = "Hidden";

            SpeechGuidenceColumn3 = "1";
            SpeechGuidenceVisibility3 = "Hidden";

        }

        public void StartSpeechRecognition(KinectAudioSource source, Dispatcher dispatcher)
        {
            
            this._source = source;
            this._dispatcher = dispatcher;

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
            //turn on/off the TV
            var word0 = new Choices();
            word0.Add("turn");

            var word1= new Choices();
            word1.Add("on");
            word1.Add("off");
           
            var word3 = new Choices();
            word3.Add("TV");

            var gb = new GrammarBuilder();
            gb.Culture = ri.Culture;
            gb.Append(word0);
            gb.Append(word1);
            gb.AppendWildcard();
            gb.Append(word3);
            gb.Append("go");

            var g = new Grammar(gb);
            this._sre.LoadGrammar(g);


            //Channel
            var c0 = "channel";
            var c1 = new Choices();
            c1.Add("one");
            c1.Add("two");
            c1.Add("three");
            c1.Add("four");
            c1.Add("five");
            c1.Add("six");
            c1.Add("seven");
            c1.Add("eight");
            c1.Add("nine");
            c1.Add("ten");
            var channel = new GrammarBuilder();
            channel.Culture = ri.Culture;
            channel.Append(c0);
            channel.Append(c1);
            channel.AppendWildcard();
            channel.Append("go there");
            

            var c = new Grammar(channel);
            this._sre.LoadGrammar(c);

            //Change sound 
            var sc0 = new Choices();
            sc0.Add("increase");
            sc0.Add("decrease");

            var sc2 = new Choices();
            sc2.Add("sound");

            var sound = new GrammarBuilder();
            sound.Culture = ri.Culture;
            sound.Append(sc0);
            sound.AppendWildcard();
            sound.Append(sc2);
            sound.Append("go there");

            var cs = new Grammar(sound);
            this._sre.LoadGrammar(cs);

            //Next or previous page
            var p0 = new Choices();
            p0.Add("next");
            p0.Add("previous");
            var p1 = "channel";
            var page = new GrammarBuilder();
            page.Culture = ri.Culture;
            page.Append(p0);
            page.Append(p1);
            page.AppendWildcard();
            page.Append("go there");

            var p = new Grammar(page);
            this._sre.LoadGrammar(p);


            //Guidence pages
            var gp0 = new Choices();
            gp0.Add("next");
            gp0.Add("previous");
            var gp1 = "page";

            var guidence = new GrammarBuilder();
            guidence.Culture = ri.Culture;
            guidence.Append(gp0);
            guidence.Append(gp1);
            //guidence.AppendWildcard();
            guidence.Append("go there");

            var gp = new Grammar(guidence);
            this._sre.LoadGrammar(gp);


            //quit the application
            var s = new Choices();
            s.Add("quit");

            var q = new GrammarBuilder();
            q.Culture = ri.Culture;
            
            q.Append(s);
            q.Append("speech go there");
            var quit = new Grammar(q);

            this._sre.LoadGrammar(quit);
        }


        private void sre_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            HypothesizedText += " Rejected";
            Confidence = Math.Round(e.Result.Confidence, 2).ToString();
            
        }

        private void sre_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            HypothesizedText = e.Result.Text;
        }

         private void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            
            this._dispatcher.BeginInvoke(new Action<SpeechRecognizedEventArgs>(InterpretCommand), e);
        }

         private void InterpretCommand(SpeechRecognizedEventArgs e)
        {
            var result = e.Result;
            Confidence = Math.Round(result.Confidence, 2).ToString();
            if (result.Confidence < 95 && result.Words[0].Text == "quit" && result.Words[1].Text == "speech" && result.Words[2].Text == "go" && result.Words[3].Text == "there")
            {
                //this.Close();
                show = "quit....";
                if (QuitDetected != null)
                {
                    QuitDetected(this, new EventArgs());
                }
                return;  
            }
            var commandString = result.Words[0].Text;
            var orderString = result.Words[1].Text;
            var everString = result.Words[2].Text;
            var goalString = result.Words[3].Text;
            if (commandString == "turn" && (orderString == "on" || orderString == "off"))
            {
                if (goalString == "TV")
                {
                    
                    show = "turn on the TV ......";
                    serialSend("Turn On Off");
                }
            }
            else if (orderString == "channel")
            {
                if (commandString == "next")
                {
                    show = "next channel....";
                    serialSend("Next Page");
                }
                else if (commandString == "previous")
                {
                    show = "previous channel....";
                    serialSend("Previous Page");
                }
            }
            else if (orderString == "page")
            {
                if (commandString == "next")
                {
                    show = "next page....";
                    this._guidencePage = this._guidencePage + 1;
                }
                else if (commandString == "previous")
                {
                    show = "previous page....";

                    this._guidencePage = this._guidencePage - 1;
                    if (this._guidencePage < 0)
                    {
                        this._guidencePage = 2;
                    } 
                }

                var key = this._guidencePage % 3;
                switch (key)
                {
                    case 0:
                        SpeechGuidenceColumn2 = "1";
                        SpeechGuidenceVisibility2 = "Hidden";

                        SpeechGuidenceColumn3 = "1";
                        SpeechGuidenceVisibility3 = "Hidden";

                        SpeechGuidenceColumn1 = "0";
                        SpeechGuidenceVisibility1 = "Visible";

                        break;
                    case 1:
                        SpeechGuidenceColumn1 = "1";
                        SpeechGuidenceVisibility1 = "Hidden";

                        SpeechGuidenceColumn3 = "1";
                        SpeechGuidenceVisibility3 = "Hidden";

                        SpeechGuidenceColumn2 = "0";
                        SpeechGuidenceVisibility2 = "Visible";

                        break;
                    case 2:
                        SpeechGuidenceColumn1 = "1";
                        SpeechGuidenceVisibility1 = "Hidden";

                        SpeechGuidenceColumn2 = "1";
                        SpeechGuidenceVisibility2 = "Hidden";

                        SpeechGuidenceColumn3 = "0";
                        SpeechGuidenceVisibility3 = "Visible";

                        break;
                    default:
                        break;
                }
            }
            else if (commandString == "channel")
            {
                if (orderString == "one")
                {
                    show = "channel one....";
                    serialSend("Channel 1");
                }
                else if (orderString == "two")
                {
                    show = "channel two....";
                    serialSend("Channel 2");
                }
            }
            else if (everString == "sound")
            {
                //Console.WriteLine(everString);
                if (commandString == "increase")
                {
                    show = "increase the sound....";
                    serialSend("Sound Increase");
                }
                else if (commandString == "decrease")
                {
                    show = "decrease the sound....";
                    serialSend("Sound Decrease");
                }
            }
        }

         private void serialSend(String command)
         {
             try
             {
                 if (!_serialPort.IsOpen)
                 {
                     _serialPort.PortName = PORTNAME;
                     _serialPort.Open();
                 }
                 if (_serialPort.IsOpen)
                 {
                     _serialPort.Write(command);
                     Console.WriteLine(command);
                 }
             }
             catch(Exception e)
             {}
         }

        public void CloseSpeech()
        {
            if (this._sre != null)
            {
                this._sre.RecognizeAsyncCancel();
                this._sre.RecognizeAsyncStop();
                //this._sre.Dispose();

            }
            if (this._serialPort.IsOpen)
            {
                this._serialPort.Close();
            }
        }
        
        private string _show;
        public string show
        {
            get { return _show; }
            set
            {
                _show = value;
                OnPropertyChanged("show");
            }
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

        private string _speechGuidenceColumn1;
        public string SpeechGuidenceColumn1
        {
            get { return _speechGuidenceColumn1; }
            set
            {
                _speechGuidenceColumn1 = value;
                OnPropertyChanged("SpeechGuidenceColumn1");
            }
        }

        private string _speechGuidenceVisibility1;
        public string SpeechGuidenceVisibility1
        {
            get { return _speechGuidenceVisibility1; }
            set
            {
                _speechGuidenceVisibility1 = value;
                OnPropertyChanged("SpeechGuidenceVisibility1");
            }
        }

        private string _speechGuidenceColumn2;
        public string SpeechGuidenceColumn2
        {
            get { return _speechGuidenceColumn2; }
            set
            {
                _speechGuidenceColumn2 = value;
                OnPropertyChanged("SpeechGuidenceColumn2");
            }
        }

        private string _speechGuidenceVisibility2;
        public string SpeechGuidenceVisibility2
        {
            get { return _speechGuidenceVisibility2; }
            set
            {
                _speechGuidenceVisibility2 = value;
                OnPropertyChanged("SpeechGuidenceVisibility2");
            }
        }

        private string _speechGuidenceColumn3;
        public string SpeechGuidenceColumn3
        {
            get { return _speechGuidenceColumn3; }
            set
            {
                _speechGuidenceColumn3 = value;
                OnPropertyChanged("SpeechGuidenceColumn3");
            }
        }

        private string _speechGuidenceVisibility3;
        public string SpeechGuidenceVisibility3
        {
            get { return _speechGuidenceVisibility3; }
            set
            {
                _speechGuidenceVisibility3 = value;
                OnPropertyChanged("SpeechGuidenceVisibility3");
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
