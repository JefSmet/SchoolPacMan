// Declare global variables
char serialInputChar;
bool pressedEight = false;
bool pressedNine = false;

void setup() {
  Serial.begin(9600);
  pinMode(12, OUTPUT);
  pinMode(10, OUTPUT);
  pinMode(9, INPUT_PULLUP);
  pinMode(13, OUTPUT);
  digitalWrite(13, LOW);  //LED off
  pinMode(A0, INPUT);
}

void loop() {
  // Read serial input
  
  if (Serial.available()) {
    serialInputChar = Serial.read();
    if (serialInputChar == 'I') {
      digitalWrite(12, HIGH);
    }
    if (serialInputChar == 'O') {
      digitalWrite(12, LOW);
    }
    if (serialInputChar == 'A'){
      digitalWrite(10, HIGH);
    }
    if (serialInputChar == 'U'){
      digitalWrite(10, LOW);
    }
  }
  

  
  int potValue = analogRead(A0);  
  //potValue = map(potValue, 0 , 1023, 0, 10);
  Serial.println(potValue);
  delay(50);


/*
  // Send 'R' when button on pin 8 is pressed
  if (digitalRead(8) == LOW && !pressedEight) {
    Serial.write('R');
    delay(50);  // Debounce
    pressedEight = true;
  } else if (pressedEight && digitalRead(8) == HIGH) {
    pressedEight = false;
  }*/

  // Send 'L' when button on pin 9 is pressed
  if (digitalRead(9) == LOW && !pressedNine) {
    Serial.println('B');
    delay(50);  // Debounce
    pressedNine=true;
  } 
 else if (pressedNine && digitalRead(9) == HIGH){
   pressedNine=false;
   delay(50);
 }
  
}
