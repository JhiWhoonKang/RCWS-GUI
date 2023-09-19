#include <Servo.h>

Servo SG90_F;
Servo SG90_M;

int pos1 = 0;
int pos2 = 0;

void setup() {
  SG90_F.attach(9);
  SG90_M.attach(11);
  
  Serial.begin(9600);
}

void loop() {
  if (Serial.available()) {
    char cmd = Serial.read();

    // switch(cmd) {
    //   /* 배율 */
    //   case 'i': /* 배율 축소 */
    //     pos1 += 1;
    //     if(pos1 > 180) pos1 = 180;
    //     SG90_M.write(pos1);
    //     Serial.print("SG90_M position: ");
    //     Serial.println(pos1);
    //     break;

    //   case 'o': /* 배율 확대 */
    //     pos1 -= 1;
    //     if(pos1 < 0) pos1 = 0;
    //     SG90_M.write(pos1);
    //     Serial.print("SG90_M position: ");
    //     Serial.println(pos1);
    //     break;
    //   /* */

    //   /* 초점 */
    //   case 'e': /* 초점 확대 */
    //     pos2 += 1;
    //     if(pos2 > 180) pos2 = 180;
    //     SG90_F.write(pos2);
    //     Serial.print("SG90_F position: ");
    //     Serial.println(pos2);
    //     break;

    //   case 'r': /* 초점 축소 */
    //     pos2 -= 1;
    //     if(pos2 < 0) pos2 = 0;
    //     SG90_F.write(pos2);
    //     Serial.print("SG90_F position: ");
    //     Serial.println(pos2);
    //     break;
    //   /* */
    //   default:
    //     break;
    // }

    switch(cmd) {
      case 'A':
        SG90_M.write(0);
        SG90_F.write(180);
        break;
      case 'I':
        SG90_M.write(89);
        SG90_F.write(0);
        break;
      default:
        break;
    }
  }
  delay(15);
 }