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

// Guardamos la vida actual mostrada
int vidaActual = 3;

void setup() {
  for (int i = 0; i < 7; i++) {
    pinMode(seg[i], OUTPUT);
  }

  mostrarNumero(vidaActual);

  pinMode(LED_BUILTIN, OUTPUT);
  pinMode(pinLedBoton, OUTPUT);
  pinMode(pinBoton, INPUT_PULLUP);

  Serial.begin(9600);
}

void loop() {
  // ───────── POTENCIÓMETROS ─────────
  int lecturaX = analogRead(potX);
  int lecturaY = analogRead(potY);

  x = map(lecturaX, 0, 1023, 0, 15);
  y = map(lecturaY, 0, 1023, 0, 2);

  dibujar();
  delay(50);

  // ───────── BOTÓN ─────────
  if (digitalRead(pinBoton) == LOW) {
    digitalWrite(pinLedBoton, HIGH);
    Serial.println("BOTON_PULSADO");
  } else {
    digitalWrite(pinLedBoton, LOW);
  }

  // ───────── RECIBIR VIDAS DESDE UNITY ─────────
  while (Serial.available() > 0) {
    String linea = Serial.readStringUntil('\n');
    linea.trim(); // Quita espacios o \r

    if (linea.length() == 0) continue; // Ignorar líneas vacías

    // Verificar que TODOS los caracteres son dígitos
    bool esNumeroValido = true;
    for (int i = 0; i < linea.length(); i++) {
      if (!isDigit(linea[i])) {
        esNumeroValido = false;
        break;
      }
    }

    if (!esNumeroValido) continue; // Ignorar si hay basura

    int vidas = linea.toInt();
    if (vidas >= 0 && vidas <= 3) {
      vidaActual = vidas;
      mostrarNumero(vidaActual);
    }
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
