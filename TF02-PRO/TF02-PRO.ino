#include <SoftwareSerial.h>
const int HEADER = 0x59;
// Variables
int TF01_pix;
int dist, strength;
int a, b, c, d, e, f, check, i;
SoftwareSerial Serial1 (A4, A5);

void setup()
{
  // Set serial interfaces
  Serial.begin(115200);
  Serial1.begin(115200);
}

void loop()
{
  Serial.print("0\n");
  // Check if at least 9 bytes are available
  if (Serial1.available() >= 9)
  {
    Serial.print("1\n");
    // Check for first header byte
    if(Serial1.read() == HEADER)
    {
      Serial.print("2\n");
      // Check for second header byte
      if(Serial1.read() == HEADER)
      {
        Serial.print("3\n");
        // Read all 6 data bytes
        a = Serial1.read();
        b = Serial1.read();
        c = Serial1.read();
        d = Serial1.read();
        e = Serial1.read();
        f = Serial1.read();
        // Read checksum byte
        check = (a + b + c + d + e + f + HEADER + HEADER);
        // Compare lower 8 bytes of checksum
        if(Serial1.read() == (check & 0xff))
        {
          // Calculate distance
          dist = (a + (b * 256));
          // Calculate signal strength
          strength = (c + (d * 256));
          // Display results to USB serial port
          Serial.print("dist = ");
          Serial.print(dist);
          Serial.print('\t');
          Serial.print("strength = ");
          Serial.print(strength);
          Serial.print('\n');
        }
      }
    }
  }
}