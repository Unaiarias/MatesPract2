int seg[] = {11, 10, 5, 6, 7, 12, 13}; // Pines para a b c d e f g
int nums[10][7] = {
  {1,1,1,1,1,1,0}, // 0
  {0,1,1,0,0,0,0}, // 1
  {1,1,0,1,1,0,1}, // 2
  {1,1,1,1,0,0,1}, // 3
};

int potX = A0;
int potY = A1;

int pinBoton = 8;    // Botón
int pinLedBoton = 9; // LED del botón

int x = 0, y = 0;

void setup() {
  // Configurar pines como salida
  for (int i = 0; i < 7; i++) {
    pinMode(seg[i], OUTPUT);
  }
  
  // Mostrar número 0
  mostrarNumero(3);
  
  pinMode(LED_BUILTIN, OUTPUT);
  
  pinMode(pinLedBoton, OUTPUT);     // LED externo
  pinMode(pinBoton, INPUT_PULLUP);  // Botón
  Serial.begin(9600);
}

void loop() {

  // Leer potenciómetros
  int lecturaX = analogRead(potX);
  int lecturaY = analogRead(potY);

  x = map(lecturaX, 0, 1023, 0, 15);
  y = map(lecturaY, 0, 1023, 0, 2);

  dibujar();

  delay(50);

  // Leer botón
  if (digitalRead(pinBoton) == LOW) {   // LOW = pulsado
    digitalWrite(pinLedBoton, HIGH);    // ENCENDER LED
    Serial.println("BOTON_PULSADO");
  } else {
    digitalWrite(pinLedBoton, LOW);     // APAGAR LED
  }
}

void dibujar() {
  Serial.print(x);
  Serial.print(",");
  Serial.println(y);
}

void mostrarNumero(int n) {
  for (int i = 0; i < 7; i++) {
    digitalWrite(seg[i], nums[n][i]);
  }
}
