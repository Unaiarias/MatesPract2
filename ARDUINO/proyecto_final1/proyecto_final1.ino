#include <LiquidCrystal.h>

byte sprite[8] = {
 B00100,
 B01110,
 B10101,
 B00100,
 B01110,
 B00100,
 B01010,
 B10001
};
int ledverde = 10;
int potX = A0;   // Potenciómetro para movimiento horizontal
int potY = A1;   // Potenciómetro para movimiento vertical

int x = 0, y = 0;

LiquidCrystal lcd (12, 11, 5, 4, 3, 2);

void setup() {
  lcd.begin(16, 2);
  lcd.createChar(0, sprite);
  pinMode(ledverde, OUTPUT);
  pinMode(9, OUTPUT);
  Serial.begin(9600);
}

void loop() {
  
  digitalWrite(ledverde, HIGH);

  // Leer potenciómetros
  int lecturaX = analogRead(potX);
  int lecturaY = analogRead(potY);

  // Escalar valores
  x = map(lecturaX, 0, 1023, 0, 15);
  y = map(lecturaY, 0, 1023, 0, 2);

  // Dibujar muñeco
  dibujar();
  
  delay(50); // movimiento suave
}

void dibujar() {
  Serial.print(x);
  Serial.print(",");
  Serial.println(y);
  lcd.clear();
  lcd.setCursor(x, y);
  lcd.write((byte)0);
}
