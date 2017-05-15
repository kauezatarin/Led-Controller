using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace LedController
{
    public class DataHandler
    {
        private SerialPort serialPort = null;//porta serial
        private string RxString="";//string recebida pela serial
        private Main main;
        private bool isHandshaking = false;
        private bool ardcuinoConnected = false;
        private string actualCOM = "";
        Thread waitRx;

        public DataHandler(Main main)
        {
            this.main = main;
        }

        public bool createConnection()//cria a isntancia da porta serial
        {
            try
            {
                serialPort = new SerialPort();
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceived);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool openConnection(string portName, int baudRate)//inicia a conexão com o arduino
        {
            if(serialPort!=null && serialPort.IsOpen == false)//se a conexão estive fechada
            {
                try
                {
                    serialPort.PortName = portName;
                    serialPort.BaudRate = baudRate;

                    serialPort.Open();

                    Thread.Sleep(1000);//da um tempo para o arduino reiniciar caso ocorra

                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }

        public void closeConnection()//encerra a conexão (a instancia continua existindo)
        {
            if (serialPort != null && serialPort.IsOpen == true)  // se porta aberta
            {
                ardcuinoConnected = false;
                serialPort.Close();         //fecha a porta
            }
            else if(serialPort != null && serialPort.IsOpen == false)//se a porta estiver fachada, apenas assinala
                ardcuinoConnected = false;
        }

        public bool isConnected()//informa se o arduino está conectado
        {
            if(serialPort.IsOpen == false && ardcuinoConnected == true)//verifica a autenticidade da informação
            {
                ardcuinoConnected = false;

                return ardcuinoConnected;
            }
            else//se as informações baterem
            {
                return ardcuinoConnected;
            }
   
        }

        public void rebootController()//reinicia o arduino
        {
            serialPort.DtrEnable = true;
            Thread.Sleep(500);
            serialPort.DtrEnable = false;
        }

        public void reloadConfig()//carrega as configurações da memória e aplica
        {
                sendCommand("102");
        }

        public string getConfig()//le as configurações do controlador
        {
            RxString = "";//limpa a variavel

            sendCommand("111");//solicita as informações para o arduino

            waitRx = new Thread(isRxEmpty);

            waitRx.Start();

            waitRx.Join();//aguarda o arduino responder

            return RxString;
        }

        public string actualCOMPtor()//retorna a COM onde o arduino está
        {
            return actualCOM;
        }

        public void uploadFirmware()//faz o upload do firmware para o controlador
        {
            string installDir = Environment.CurrentDirectory;
            DialogResult dialogResult;
            
            if (isConnected() == false)
            {
                main.insertLog("Impossivel Atualizar. Controlador não encontrado :(");

                return;
            }

            // These files must be part of the installation.
            // They come from the Arduino installation directory arduino/hardware/tools/avr/bin
            if (File.Exists(installDir + "\\avr\\avrdude.exe") == false)
            {
                MessageBox.Show("avrdude tool not installed", "AVRUpdate error");
                return;
            }
            if (File.Exists(installDir + "\\avr\\avrdude.conf") == false)
            {
                MessageBox.Show("avrdude config file not installed", "AVRUpdate error");
                return;
            }
            if (File.Exists(installDir + "\\avr\\cygwin1.dll") == false)
            {
                MessageBox.Show("avrdude cygwin dll not installed", "AVRUpdate error");
                return;
            }
            if (File.Exists(installDir + "\\avr\\libusb0.dll") == false)
            {
                MessageBox.Show("avrdude usb dll not installed", "AVRUpdate error");
                return;
            }

            // This file is the new image to be uploaded to the Arduino board...
            if (File.Exists(installDir + "\\firmware\\Firmware.hex") == false)
            {
                MessageBox.Show("Firmware not installed", "Firmware Update error");
                return;
            }

            dialogResult = MessageBox.Show("Deseja iniciar a atualização?", "Firmware Update", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.No)
                return;

            main.insertLog("ATUALIZANDO FIRMWARE");

            main.insertLog("(NÃO DESLIGUE NEM REINICIE ATÉ QUE O PROCESSO SEJA CONCLUIDO)");

            closeConnection();

            string avrport = actualCOM; 
            string dir = installDir;
            dir.Replace("\\", "/");
            Process avrprog = new Process();
            StreamReader avrstdout, avrstderr;
            StreamWriter avrstdin;
            ProcessStartInfo psI = new ProcessStartInfo("cmd");


            psI.UseShellExecute = false;
            psI.RedirectStandardInput = true;
            psI.RedirectStandardOutput = true;
            psI.RedirectStandardError = true;
            psI.CreateNoWindow = true;
            avrprog.StartInfo = psI;
            avrprog.Start();
            avrstdin = avrprog.StandardInput;
            avrstdout = avrprog.StandardOutput;
            avrstderr = avrprog.StandardError;
            avrstdin.AutoFlush = true;
            //avrstdin.WriteLine(installDir + "\\avr\\avrdude.exe -Cavr/avrdude.conf -patmega328p -cstk500v1 -P" + avrport + " -b57600 -D -Uflash:w:" + dir + "/avr/AVRImage.hex:i");
            avrstdin.WriteLine("avr\\avrdude.exe -C avr/avrdude.conf -p m2560 -c wiring -P " + avrport + " -b 115200 -D -U flash:w:firmware/Firmware.hex:i");
            avrstdin.Close();
            main.insertLog(avrstdout.ReadToEnd());
            main.insertLog(avrstderr.ReadToEnd());
            main.insertLog("Finalizado! Aguarde seu controlador conectar novamente...");
        }

        public void sendCommand(string str)//envia os comandos pela serial
        {
            if(serialPort != null)//se a porta já estiver criada
            {
                if (serialPort.IsOpen == true)//porta está aberta
                {
                    serialPort.Write(str);  //envia o texto presente na variavel str
                }
                else
                {
                    isDisconnected(); //detecta que o dispositivo foi desconectado
                }
            }
        }

        public bool autoConnection(string lastConnected, int baudRate)//procura o arduino e conecta automaticamente com ele
        {
            string[] coms = getComAvailable();
            int lenght = SerialPort.GetPortNames().Length;

            if (lastConnected != "never")//verifica se a ultima porta utilizada ainda está na lista
            {
                for(int i=0;i<lenght;i++)//pesquisa o vetor
                {
                    if (coms[i] == lastConnected)//se encontrado verifica se é o arduino
                    {
                        if(openConnection(coms[i], baudRate) == true)//se a conexão for estabelecida
                        {
                            isHandshaking = true;
                            sendCommand("100");//envia o handshake

                            Thread.Sleep(2000);//dá 2 segundos para o arduio responder

                            if (RxString == "255")//verifica se o handshake funcionou, se 
                            {
                                isHandshaking = false;
                                ardcuinoConnected = true;
                                actualCOM = coms[i];
                                return true;
                            }
                            else
                            {
                                closeConnection();
                            }

                            isHandshaking = false;
                        }
                    }
                }
                        
            }
            foreach (string com in getComAvailable())//caso a verificação anterior falhe, tenta localizar o arduino novamente
            {
                if (openConnection(com, baudRate) == true)//se a conexão for estabelecida
                {
                    isHandshaking = true;
                    sendCommand("100");//envia o handshake

                    Thread.Sleep(2000);//dá 2 segundos para o arduio responder

                    if (RxString.Contains("255") == true)//verifica se o handshake funcionou
                    {
                        isHandshaking = false;
                        ardcuinoConnected = true;
                        actualCOM = com;
                        return true;
                    }
                    else
                    {
                        closeConnection();
                    }

                    isHandshaking = false;
                }
            }

            return false;
           
        }

        private void isDisconnected()//se o dispositivo for desconectado
        {
            try
            {
                serialPort.Close();
                ardcuinoConnected = false;
            }
            catch
            {
                return;
            }
        }

        private string[] getComAvailable()//retorna uma lista com todas as postas COM
        {
            return SerialPort.GetPortNames();
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)//serial port
        {
            RxString = serialPort.ReadExisting();              //le o dado disponível na serial

            if(isHandshaking == false)
                main.Invoke(new EventHandler(hadleReceivedData));   //chama outra thread para escrever o dado no text box
        }

        private void hadleReceivedData(object sender, EventArgs e)//trata os dados recebidos
        {
            //tratamento dos dados recebidos
        }

        private void isRxEmpty()
        {
            int i = 0;

            while(i<10)
            {
                if (RxString == "")
                {
                    i++;
                    Thread.Sleep(1000);
                }
                else
                    break;
            }
        }
    }
}
