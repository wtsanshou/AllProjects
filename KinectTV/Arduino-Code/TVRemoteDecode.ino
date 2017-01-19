/*
 * IRRemote 紅外線遙控教學
 *   範例 1.2: 顯示紅外線協定種類，如 NEC, Sony SIRC, Philips RC5, Philips RC6 等協定
 */
#include <IRremote.h>                    // 引用 IRRemote 函式庫


const int irReceiverPin = 6;             // 紅外線接收器 OUTPUT 訊號接在 pin 2
 
IRrecv irrecv(irReceiverPin);            // 定義 IRrecv 物件來接收紅外線訊號
decode_results results; // 解碼結果將放在 decode_results 結構的 result 變數裏
//int buttonState = 0;
//const int buttonPin = 4;
IRsend irsend;  

void setup()
{
  Serial.begin(9600);                     // 開啟 Serial port, 通訊速率為 9600 bps
  //pinMode(buttonPin, INPUT); 
  irrecv.enableIRIn();      // 啟動紅外線解碼
}

// 顯示紅外線協定種類
void showIRProtocol(decode_results *results) 
{
  Serial.print("Protocol: ");
  
  // 判斷紅外線協定種類
  switch(results->decode_type) {
   case NEC:
     Serial.print("NEC");
     break;
   case SONY:
     Serial.print("SONY");
     break;
   case RC5:
     Serial.print("RC5");
     break;
   case RC6:
     Serial.print("RC6");
     break;
   default:
     Serial.print("Unknown encoding");  
  }  

  // 把紅外線編碼印到 Serial port
  Serial.print(", irCode: ");            
  Serial.print(results->value, HEX);    // 紅外線編碼
  Serial.print(",  bits: ");           
  Serial.println(results->bits);        // 紅外線編碼位元數    
  //Serial.println(results->rawbuf);
  Serial.println(results->rawlen);
 
  for (int i = 0; i < results->rawlen; i++) {
    if ((i % 2) == 1) {
      Serial.print(results->rawbuf[i]*USECPERTICK, DEC);
    } 
    else {
      Serial.print(-(int)results->rawbuf[i]*USECPERTICK, DEC);
    }
    Serial.print(" ");
    if((i%10) == 0)
    {
      Serial.println("");
    }
  }
  Serial.println("");
}
void loop() 
{
  if (irrecv.decode(&results)) {         // 解碼成功，收到一組紅外線訊號
    showIRProtocol(&results);            // 顯示紅外線協定種類
    irrecv.resume();                     // 繼續收下一組紅外線訊號        
  }  
}


