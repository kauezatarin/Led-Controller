#include <EEPROM.h>

//define os pinos dos leds
//LED RGB 1
const int ledRed5 = 6;
const int ledGreen5 = 5;
const int ledBlue5 = 3;

//LED RGB 2
const int ledRed6 = 11;
const int ledGreen6 = 10;
const int ledBlue6 = 9;

//LED's single color
const int led1 = 2;
const int led2 = 7;
const int led3 = 8;
const int led4 = 12;

//velocidade da conexão
const int baud = 9600;

//armazena o estado do debug
bool debug = false; // true - ligado / false- desligado

//indica se a placa de conexão utiliza transistors PNP para controle dos LED's
bool PNP; // true - ligado / false- desligado -- o estado é assinalado no corpo do código

//indica quantos slots da EEPROM estão sendo utilizados para as configurações
const int mem_size = 14;

//variaveis de comando
byte led; //indica o led a cer modificado
byte red; //armazena o valor da intenciadade do vermelho no RGB
byte green; //armazena o valor da intenciadade do verde no RGB
byte blue; //armazena o valor da intencidade do azul no RGB
byte on; //indica se o LED de unica cor será ligado ou desligado

//======variaveis de memória=======

//LED RGB 1
byte red5; //armazena o valor da intenciadade do vermelho
byte green5; //armazena o valor da intenciadade do verde
byte blue5; //armazena o valor da intencidade do azul
byte led5_on; //indica se o LED está ligado ou desligado

//LED RGB 2
byte red6; //armazena o valor da intenciadade do vermelho
byte green6; //armazena o valor da intenciadade do verde
byte blue6; //armazena o valor da intencidade do azul
byte led6_on; //indica se o LED está ligado ou desligado

//LED's 1-4
byte led1_on; //armazena se o led1 está ligado
byte led2_on; //armazena se o led2 está ligado
byte led3_on; //armazena se o led3 está ligado
byte led4_on; //armazena se o led4 está ligado

//=================================

//funções secundarias
void EEPROM_Clear(); //apaga todos os dados da EEPROM
void EEPROM_Load(); //carrega as configurações da EEPROM
void EEPROM_Save(); //salva as configurações na EEPROM
void EEPROM_Print(); //printa os valores salvos na EEPROM
void Apply_Settings(); //aplica as configurações armazenadas na memória
void Led_test(); //Inicia o teste dos leds

// the setup function runs once when you press reset or power the board
void setup() {
  Serial.begin(baud);//inicia a comunicação serial

  //inicializa os pinos do LED RGB 1
  pinMode(ledRed5, OUTPUT);
  pinMode(ledGreen5, OUTPUT);
  pinMode(ledBlue5, OUTPUT);

  //inicializa os pinos do LED RGB 2
  pinMode(ledRed6, OUTPUT);
  pinMode(ledGreen6, OUTPUT);
  pinMode(ledBlue6, OUTPUT);

  //inicializa os pinos dos LED de unica cor
  pinMode(led1, OUTPUT);
  pinMode(led2, OUTPUT);
  pinMode(led3, OUTPUT);
  pinMode(led4, OUTPUT);

  delay(100);
  
  EEPROM_Load();
  
  delay(100);
}

// the loop function runs over and over again forever
void loop() {

  if (Serial.available()) //se a serial tiver algum dado a ser lido
  {
    led = Serial.parseInt();
    delay(10);

    if (led == 0) //verifica se foi recebido o comando de apagar os LED's
    {
  	  if(PNP == true)
  	  {
  		 //desliga o RGB 1
  		 digitalWrite(ledRed5, 255);
  		 digitalWrite(ledGreen5, 255);
  		 digitalWrite(ledBlue5, 255);
  
  		 //desliga o RGB 2
  		 digitalWrite(ledRed6, 255);
  		 digitalWrite(ledGreen6, 255);
  		 digitalWrite(ledBlue6, 255);
  		
  		 //desliga os leds 1-4
  		 digitalWrite(led1, HIGH);
  		 digitalWrite(led2, HIGH);
  		 digitalWrite(led3, HIGH);
  		 digitalWrite(led4, HIGH);
  	  }
  	  else
  	  {
  		 //desliga o RGB 1
  		 digitalWrite(ledRed5, LOW);
  		 digitalWrite(ledGreen5, LOW);
  		 digitalWrite(ledBlue5, LOW);
  
  		 //desliga o RGB 2
  		 digitalWrite(ledRed6, LOW);
  		 digitalWrite(ledGreen6, LOW);
  		 digitalWrite(ledBlue6, LOW);
  		
  		 //desliga os leds 1-4
  		 digitalWrite(led1, LOW);
  		 digitalWrite(led2, LOW);
  		 digitalWrite(led3, LOW);
  		 digitalWrite(led4, LOW); 
  	  }
	  
  	  led1_on = 0;
  	  led2_on = 0;
  	  led3_on = 0;
  	  led4_on = 0;
	    led5_on = 0;
	    led6_on = 0;
      
      if(debug == true)
        Serial.println("Leds apagados");
    }
	else if (led >= 1 && led <= 4) //se forem os leds de unica cor, executa a ação desejada
    {
      on = Serial.parseInt();

      if (on == 1) //liga o LED indicado
      {
        if (led == 1)
        {
          if(PNP == true) //Verifica a configuração PNP
			  digitalWrite(led1, LOW);
		  else
			  digitalWrite(led1, HIGH);
		      
			  led1_on = 1;
		  
          if(debug == true)
            Serial.println("LED 1 Ligado");
        }
        else if (led == 2)
        {
          if(PNP == true) //Verifica a configuração PNP
			  digitalWrite(led2, LOW);
		  else
			  digitalWrite(led2, HIGH);
		  
		      led2_on = 1;
		  
          if(debug == true)
            Serial.println("LED 2 Ligado");
        }
        else if (led == 3)
        {
          if(PNP == true) //Verifica a configuração PNP
			  digitalWrite(led3, LOW);
		  else
			  digitalWrite(led3, HIGH);
		  
		      led3_on = 1;
		  
          if(debug == true)
            Serial.println("LED 3 Ligado");
        }
        else if (led == 4)
        {
          if(PNP == true) //Verifica a configuração PNP
			  digitalWrite(led4, LOW);
		  else
			  digitalWrite(led4, HIGH);
		  
		      led4_on = 1;
		  
          if(debug == true)
            Serial.println("LED 4 Ligado");
        }
      }
      else//desliga o LED indicado
      {
        if (led == 1)
        {
          if(PNP == true) //Verifica a configuração PNP
			  digitalWrite(led1, HIGH);
		  else
			  digitalWrite(led1, LOW);
		  
		      led1_on = 0;
		  
          if(debug == true)
            Serial.println("LED 1 Desligado");
        }
        else if (led == 2)
        {
          if(PNP == true) //Verifica a configuração PNP
			  digitalWrite(led2, HIGH);
		  else
			  digitalWrite(led2, LOW);
		  
		      led2_on = 0;
		  
          if(debug == true)
            Serial.println("LED 1 Desligado");
        }
        else if (led == 3)
        {
          if(PNP == true) //Verifica a configuração PNP
			  digitalWrite(led3, HIGH);
		  else
			  digitalWrite(led3, LOW);
		  
		      led3_on = 0;
		  
          if(debug == true)
            Serial.println("LED 1 Desligado");
        }
        else if (led == 4)
        {
          if(PNP == true) //Verifica a configuração PNP
			  digitalWrite(led4, HIGH);
		  else
			  digitalWrite(led4, LOW);
		  
		      led4_on = 0;
		  
          if(debug == true)
            Serial.println("LED 1 Desligado");
        }
      }
    }
    else if (led >= 5 && led <= 6) // se for um dos leds RGB, executa a ação desejada
    {
      //le os valores referentes a cada cor do RGB para placa PNP
	  if(PNP == true)
	  {
  		red = 255 - Serial.parseInt();
  		green = 255 - Serial.parseInt();
  		blue = 255 - Serial.parseInt();
	  }
	  else //le os valores referentes a cada cor do RGB para placa não PNP
	  {
  		red = Serial.parseInt();
  		green = Serial.parseInt();
  		blue = Serial.parseInt();
	  }

      if (led == 5) //executa a ação no RGB 1
      {
        analogWrite(ledRed5, red);
        analogWrite(ledGreen5, green);
        analogWrite(ledBlue5, blue);
		        
    		if(red == 0 && green ==0 && blue == 0)//se desligado seta a variavel de controle para 0
        {
          led5_on = 0;
        }
        else//senão atualiza as variaveis de controle
        {
          red5 = red;
          green5 = green;
          blue5 = blue;
          led5_on = 1; //se ligado seta a variavel de controle para 1
        }
        
        if(debug == true)
          Serial.println("LED RGB 1 Alterado");
      }
      else if (led == 6)//executa a ação no RGB 2
      {
        analogWrite(ledRed6, red);
        analogWrite(ledGreen6, green);
        analogWrite(ledBlue6, blue);
		
    		if(red == 0 && green ==0 && blue == 0)//se desligado seta a variavel de controle para 0
        {
          led6_on = 0;
        }
        else//senão atualiza as variaveis de controle
        {
          red6 = red;
          green6 = green;
          blue6 = blue;
          led6_on = 1; //se ligado seta a variavel de controle para 1
        }

        if(debug == true)
          Serial.println("LED RGB 2 Alterado");
      }
    }
    else if (led == 100) //faz o Handshake
    {
      Serial.print(255);
    }
    else if (led == 101) // retorna a versão do firmware
    {
      Serial.print("Versao:");
      Serial.println("0.8 BETA");
      Serial.println("Firmware implementado por Kaue Zatarin.");
      Serial.println("Este codigo e de livre uso e nenhuma taxa devera ser cobrada por ele.");
    }
    else if(led == 102) //Recarrega configs da EEPROM
    {
      EEPROM_Load(); 
      if(debug == true)
        Serial.println("Config Reloaded");
    }
	else if(led == 103) //alterna o controle PNP e não PNP'
	{
		if(PNP == true)
			PNP = false;
		else
			PNP = true;
	}
    else  if (led == 110) //limpa a EEPROM
    {      
      EEPROM_Clear();
    }
    else if (led == 111) //envia os dados da EEPROM pela serial
    {
      EEPROM_Send();
    }
  	else if(led == 112) //Salva as configurações atuais na EEPROM
  	{
  		EEPROM_Save();
  	}
    else if(led == 113) //Printa os registros salvos na EEPROM
    {
      EEPROM_Print();
    }
	  else if(led == 200)//testa os leds
    {
		if(PNP == false)
			Led_test();
		else
			Led_test_PNP();
    }
    else if(led == 201)//alterna debug on/off
    {
      if(debug == true)
        debug = false;
      
      else
        debug = true;
    }
    if (debug == true) //verifica se o debug está ligado
    {
  		if(PNP == true)
  		{
  			Serial.print("input LED:");
  			Serial.println(led);
  			
  			Serial.print("input red:");
  			Serial.println(255 - red);
  			
  			Serial.print("input green:");
  			Serial.println(255 - green);
  			
  			Serial.print("input blue:");
  			Serial.println(255 - blue);
  		}
  		else
  		{
  			Serial.print("input LED:");
  			Serial.println(led);
  			
  			Serial.print("input red:");
  			Serial.println(red);
  			
  			Serial.print("input green:");
  			Serial.println(green);
  			
  			Serial.print("input blue:");
  			Serial.println(blue);
  		}
  			
  		Serial.print("input ON:");
  		Serial.println(on);
    }

    on = 0; //reseta a variavel on
  }
  
  delay(100);
}

//limpa todos os dados da EEPROM setando o valor '0'
void EEPROM_Clear() {

  for (int i = 0 ; i < EEPROM.length() ; i++)
  {
    EEPROM.write(i, 0);
  }

  delay(10);
  
  if(debug == true)
    Serial.println("EEPROM apagada");

  return;
}

//Carrega os dados gravados na EEPROM
void EEPROM_Load() {
	
	if(EEPROM.read(0) == 0)//se é a primeira inicialização
	{
		EEPROM_Clear();//limpa a EEPROM para evitar lixo de memória
		
		EEPROM.write(0, 1);//seta para ja inicializado
		EEPROM.write(13, 1);//seta para controle PNP por padrão
		
		//Controle de LED's
		if(EEPROM.read(13) == 1)
			PNP = true;
		else 
			PNP = false;
		
		//realiza o test dos leds
		if(PNP == false)
			Led_test();//inicia a sequencia de testes não PNP
		else
			Led_test_PNP();//inicia a sequencia de testes PNP
	}
	else//senão carrega e aplica as configurações salvas na memória
	{
		//LED's 1-4
		led1_on = EEPROM.read(1);//le o status salvo em memória para o LED 1
		led2_on = EEPROM.read(2);//le o status salvo em memória para o LED 2
		led3_on = EEPROM.read(3);//le o status salvo em memória para o LED 3
		led4_on = EEPROM.read(4);//le o status salvo em memória para o LED 4
		
		//LED's RGB'
		led5_on = EEPROM.read(5);//le o status salvo em memória para o LED 5
		red5 = EEPROM.read(6); //le o status salvo em memória para o vermelho
		green5 = EEPROM.read(7); //le o status salvo em memória para o verde
		blue5 = EEPROM.read(8); //le o status salvo em memória para o azul
				
		led6_on = EEPROM.read(9);//le o status salvo em memória para o LED 6
		red6 = EEPROM.read(10); //le o status salvo em memória para o vermelho
		green6 = EEPROM.read(11); //le o status salvo em memória para o verde
		blue6 = EEPROM.read(12); //le o status salvo em memória para o azul
		
		//Controle de LED's
		if(EEPROM.read(13) == 1)
			PNP = true;
		else 
			PNP = false;
		
		Apply_Settings();// aplica as configurações carregadas da memória		
	}
	
}

//Salva as configurações na memória seletivamente para prolongar a vida da EEPROM
void EEPROM_Save() {
	
	if(EEPROM.read(1) != led1_on)
		EEPROM.write(1, led1_on);
	
	if(EEPROM.read(2) != led2_on)	
		EEPROM.write(2, led2_on);
	
	if(EEPROM.read(3) != led3_on)
		EEPROM.write(3, led3_on);
	
	if(EEPROM.read(4) != led4_on)	
		EEPROM.write(4, led4_on);
	
	if(EEPROM.read(5) != led5_on)	
		EEPROM.write(5, led5_on);
	
	if(EEPROM.read(6) != red5)	
		EEPROM.write(6, red5);
	
	if(EEPROM.read(7) != green5)	
		EEPROM.write(7, green5);
	
	if(EEPROM.read(8) != blue5)
		EEPROM.write(8, blue5);
	
	if(EEPROM.read(9) != led6_on)	
		EEPROM.write(9, led6_on);
	
	if(EEPROM.read(10) != red6)	
		EEPROM.write(10, red6);
	
	if(EEPROM.read(11) != green6)	
		EEPROM.write(11, green6);
	
	if(EEPROM.read(12) != blue6)	
		EEPROM.write(12, blue6);
	
	if(EEPROM.read(13) == 1 && PNP == false)	
		EEPROM.write(13, 0);
	
	else if(EEPROM.read(13) == 0 && PNP == true)	
		EEPROM.write(13, 1);
		

  Serial.print("Salvo");
}

//Printa os valores salvos na EEPROM
void EEPROM_Print() {

  int i = 0;

  while(i<mem_size)
  {
    Serial.print(i);
    Serial.print(" ---> ");
    Serial.println(EEPROM.read(i));
    i++;
  }
}

//Envia as configurações da EEPROM pela serial
void EEPROM_Send() {

  int i;
  
  for(i=1; i<mem_size; i++)
  {
    Serial.print(EEPROM.read(i));
    Serial.print(";");
  }

}

//Aplica as configurações armazenadas na mamória RAM
void Apply_Settings() {
	if(PNP == true)
  {
    if(led1_on == 1)
      digitalWrite(led1, LOW);
    else
      digitalWrite(led1, HIGH);
      
    if(led2_on == 1)
      digitalWrite(led2, LOW);
    else
      digitalWrite(led2, HIGH);
      
    if(led3_on == 1)
      digitalWrite(led3, LOW);
    else
      digitalWrite(led3, HIGH);
      
    if(led4_on == 1)
      digitalWrite(led4, LOW);
    else
      digitalWrite(led4, HIGH);
      
    if(led5_on == 1)
    {
      analogWrite(ledRed5, red5);
      analogWrite(ledGreen5, green5);
      analogWrite(ledBlue5, blue5);
    }
    else
    {
      digitalWrite(ledRed5, 255);
      digitalWrite(ledGreen5, 255);
      digitalWrite(ledBlue5, 255);
    }
    if(led6_on == 1)
    {
      analogWrite(ledRed6, red6);
      analogWrite(ledGreen6, green6);
      analogWrite(ledBlue6, blue6);
    }
    else
    {
      digitalWrite(ledRed6, 255);
      digitalWrite(ledGreen6, 255);
      digitalWrite(ledBlue6, 255);
    }
  }
  else
  {
    if(led1_on == 1)
    digitalWrite(led1, HIGH);
    if(led2_on == 1)
      digitalWrite(led2, HIGH);
    if(led3_on == 1)
      digitalWrite(led3, HIGH);
    if(led4_on == 1)
      digitalWrite(led4, HIGH);
    if(led5_on == 1)
    {
      analogWrite(ledRed5, red5);
      analogWrite(ledGreen5, green5);
      analogWrite(ledBlue5, blue5);
    }
    if(led6_on == 1)
    {
      analogWrite(ledRed6, red6);
      analogWrite(ledGreen6, green6);
      analogWrite(ledBlue6, blue6);
    }
  }
	
	
}

//Inicia a Sequencia de teste dos leds placa não PNP tempo estimado de 10 segundos
void Led_test() {

  //desliga o RGB 1
  digitalWrite(ledRed5, LOW);
  digitalWrite(ledGreen5, LOW);
  digitalWrite(ledBlue5, LOW);
  
  //desliga o RGB 2
  digitalWrite(ledRed6, LOW);
  digitalWrite(ledGreen6, LOW);
  digitalWrite(ledBlue6, LOW);
      
  //desliga os leds 1-4
  digitalWrite(led1, LOW);
  digitalWrite(led2, LOW);
  digitalWrite(led3, LOW);
  digitalWrite(led4, LOW);
	
	//LED's  1-4
	digitalWrite(led1, HIGH);
	delay(1000);
	digitalWrite(led1, LOW);
	
	digitalWrite(led2, HIGH);
	delay(1000);
	digitalWrite(led2, LOW);
	
	digitalWrite(led3, HIGH);
	delay(1000);
	digitalWrite(led3, LOW);
	
	digitalWrite(led4, HIGH);
	delay(1000);
	digitalWrite(led4, LOW);
	
	//LED RGB 1
	analogWrite(ledRed5, 255);
	delay(1000);
	analogWrite(ledRed5, 0);
	
	analogWrite(ledGreen5, 255);
	delay(1000);
	analogWrite(ledGreen5, 0);
	
	analogWrite(ledBlue5, 255);
	delay(1000);
	analogWrite(ledBlue5, 0);
	
	//LED RGB 2
	analogWrite(ledRed6, 255);
	delay(1000);
	analogWrite(ledRed6, 0);
	
	analogWrite(ledGreen6, 255);
	delay(1000);
	analogWrite(ledGreen6, 0);
	
	analogWrite(ledBlue6, 255);
	delay(1000);
	analogWrite(ledBlue6, 0);
	
	//Retorna os LED's ao estado anterior ao teste
	if(led1_on == 1)
		digitalWrite(led1, HIGH);
	if(led2_on == 1)
		digitalWrite(led2, HIGH);
	if(led3_on == 1)
		digitalWrite(led3, HIGH);
	if(led4_on == 1)
		digitalWrite(led4, HIGH);
	if(led5_on == 1)
	{
		analogWrite(ledRed5, red5);
		analogWrite(ledGreen5, green5);
		analogWrite(ledBlue5, blue5);
	}
	if(led6_on == 1)
	{
		analogWrite(ledRed6, red6);
		analogWrite(ledGreen6, green6);
		analogWrite(ledBlue6, blue6);
	}
	
	delay(100);
}

//Inicia a Sequencia de teste dos leds placa PNP tempo estimado de 10 segundos
void Led_test_PNP() {

  //desliga o RGB 1
  digitalWrite(ledRed5, 255);
  digitalWrite(ledGreen5, 255);
  digitalWrite(ledBlue5, 255);
  
  //desliga o RGB 2
  digitalWrite(ledRed6, 255);
  digitalWrite(ledGreen6, 255);
  digitalWrite(ledBlue6, 255);
    
  //desliga os leds 1-4
  digitalWrite(led1, HIGH);
  digitalWrite(led2, HIGH);
  digitalWrite(led3, HIGH);
  digitalWrite(led4, HIGH);
	
	//LED's  1-4
	digitalWrite(led1, LOW);
	delay(1000);
	digitalWrite(led1, HIGH);
	
	digitalWrite(led2, LOW);
	delay(1000);
	digitalWrite(led2, HIGH);
	
	digitalWrite(led3, LOW);
	delay(1000);
	digitalWrite(led3, HIGH);
	
	digitalWrite(led4, LOW);
	delay(1000);
	digitalWrite(led4, HIGH);
	
	//LED RGB 1
	analogWrite(ledRed5, 0);
	delay(1000);
	analogWrite(ledRed5, 255);
	
	analogWrite(ledGreen5, 0);
	delay(1000);
	analogWrite(ledGreen5, 255);
	
	analogWrite(ledBlue5, 0);
	delay(1000);
	analogWrite(ledBlue5, 255);
	
	//LED RGB 2
	analogWrite(ledRed6, 0);
	delay(1000);
	analogWrite(ledRed6, 255);
	
	analogWrite(ledGreen6, 0);
	delay(1000);
	analogWrite(ledGreen6, 255);
	
	analogWrite(ledBlue6, 0);
	delay(1000);
	analogWrite(ledBlue6, 255);
	
	//Retorna os LED's ao estado anterior ao teste
	if(led1_on == 1)
		digitalWrite(led1, LOW);
	if(led2_on == 1)
		digitalWrite(led2, LOW);
	if(led3_on == 1)
		digitalWrite(led3, LOW);
	if(led4_on == 1)
		digitalWrite(led4, LOW);
	if(led5_on == 1)
	{
		analogWrite(ledRed5, red5);
		analogWrite(ledGreen5, green5);
		analogWrite(ledBlue5, blue5);
	}
	if(led6_on == 1)
	{
		analogWrite(ledRed6, red6);
		analogWrite(ledGreen6, green6);
		analogWrite(ledBlue6, blue6);
	}
	
	delay(100);
}
