#include <IRremote.h> 

IRsend irsend;  
String CMD = "";

void setup()
{
  Serial.begin(9600);                   
  
}

void loop()
{
  while(Serial.available() > 0)
  {
      CMD += char(Serial.read());
      delay(2);
      Serial.print("test");
      
  }
  if(  CMD.length() > 0)
  {
    if(CMD == "Turn On Off")
    {
      Serial.print("test");
          irsend.sendSamsung(0xE0E040BF, 32);  //Trun on /off
          CMD = "";
    }
    else if(CMD == "Channel 1")
    {
          irsend.sendSamsung(0xE0E020DF, 32);  //Channel 1
          CMD = "";
    }
    else if(CMD == "Channel 2")
    {
          irsend.sendSamsung(0xE0E0A05F, 32);  //Channel 2
          CMD = "";
    }
    else if(CMD == "Sound Increase")
    {
          irsend.sendSamsung(0xE0E0E01F, 32);  //Sound +
          CMD = "";
    }
    else if(CMD ==  "Sound Decrease")
    {
          irsend.sendSamsung(0xE0E0D02F, 32);  //Sound -
          CMD = "";
    }
    else if(CMD == "Next Page")
    {
          irsend.sendSamsung(0xE0E048B7, 32);  //Page +
          CMD = "";
    }
    else if(CMD == "Previous Page")
    {
          irsend.sendSamsung(0xE0E008F7, 32);  //Page -
          CMD = "";
    }
    else
    {
          CMD = "";
    }
  }
}
