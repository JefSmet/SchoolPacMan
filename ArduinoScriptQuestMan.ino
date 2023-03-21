// Declare global variables
char serialInputChar;
int potCurr = -1;
int potValue = -1;
bool pressedNine = false;

void setup() {
	Serial.begin(9600);
	
	pinMode(A0, INPUT);			// Potentio
	pinMode(9, INPUT_PULLUP); 	// Button
	pinMode(10, OUTPUT); 		// Red LED
	pinMode(12, OUTPUT); 		// Green LED
	
	digitalWrite(10, LOW);  //LED off
	digitalWrite(12, LOW);  //LED off
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
  
	potValue = analogRead(A0); 
	if (potCurr != potValue)
	{
		potCurr = potValue;
		Serial.println(potCurr);
		delay(50);
	}
	
	if (digitalRead(9) == LOW)
	{
		Serial.println('B');
		delay(50); //debounce
		while (digitalRead(9) == LOW);
		delay(50); //debounce
	}  
}
