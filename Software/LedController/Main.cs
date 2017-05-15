using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;//necessário para pegar a versão do programa

namespace LedController
{
    public partial class Main : Form
    {
        DataHandler arduinoCom;
        Thread searchArduino;//http://www.macoratti.net/10/09/c_thd1.htm
        bool lastState = false;//armazena o ultimo estado da conexão (watcher)
        bool firstRun;
        Color RGB1;
        Color RGB2;
        Firmware_form firmware;

        public Main()
        {
            InitializeComponent();
            arduinoCom = new DataHandler(this);
            searchArduino = new Thread(threadConection);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Text = "Led Controller " + getAppVersion();

            insertLog("Lendo Configurações...");
            loadConfig();

            insertLog("Incializando comunicação serial...");
            arduinoCom.createConnection();//cria a porta serial para comunicação com o arduino

            insertLog("Procurando pelo controlador LED...");
            searchArduino.Start();//inicia a procura pelo arduino em uma thread separada
            
            insertLog("Watcher Inciado.");
            conectWatch.Enabled = true;//inicia o watcher da conexão
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)//encerra a conexão caso aberta
        {
            arduinoCom.closeConnection();
            searchArduino.Abort();
        }

        private string getAppVersion()//retorna a versão do programa
        {
            return string.Format("[versão {0}]", Assembly.GetExecutingAssembly().GetName().Version);
        }

        private void loadConfig()//carrega as configurações
        {
            cb_led1.Checked = Properties.Settings.Default.LED1;
            cb_led2.Checked = Properties.Settings.Default.LED2;
            cb_led3.Checked = Properties.Settings.Default.LED3;
            cb_led4.Checked = Properties.Settings.Default.LED4;

            cb_rgb1.Checked = Properties.Settings.Default.RGB1;
            cb_rgb2.Checked = Properties.Settings.Default.RGB2;

            RGB1 = Color.FromArgb(255, Properties.Settings.Default.R1, Properties.Settings.Default.G1, Properties.Settings.Default.B1);//carrega a ultima cor do RGB1
            RGB2 = Color.FromArgb(255, Properties.Settings.Default.R2, Properties.Settings.Default.G2, Properties.Settings.Default.B2);//carrega a ultima cor do RGB2

            firstRun = Properties.Settings.Default.firstRun;
        }

        public void insertLog(string evento)//insere um registro ao LOG
        {
            tb_log.AppendText(Environment.NewLine + DateTime.Now.ToString() + "  " + evento);            
        }

        private void threadConection()
        {
            arduinoCom.autoConnection(Properties.Settings.Default.COM, 9600);//inicia a procura e conexão automatica com o arduino
        }

        private void connectionWatcher(object sender, EventArgs e)//monitora o status da conexão com o arduino
        { 
            if(arduinoCom.isConnected() == true && lastState != true)//quando o arduino se connectar, reporta
            {
                insertLog("LED Controller Conectado! (" + arduinoCom.actualCOMPtor() + ")");
                lastState = arduinoCom.isConnected();//atualiza o ultimo estado do arduino
                conectWatch.Interval = 1000;//intervalo do watcher em ms
                Properties.Settings.Default.COM = arduinoCom.actualCOMPtor();//guarda a porta mais recente onde o controlador foi conectado
                Properties.Settings.Default.Save();

                if (firstRun == true)//se for a primeira inicialização sincroniza as configurãções com o arduino
                {
                    insertLog("Primeira inicialização detectada! Sincronizando dados...");

                    syncConfig(arduinoCom.getConfig());

                    firstRun = false;

                    Properties.Settings.Default.firstRun = false;
                    Properties.Settings.Default.Save();

                    insertLog("Sincronização finalizada!");
                }
            }
            else if(arduinoCom.isConnected() == false && lastState != false)//se o arduino se desconectar reporta
            {
                insertLog("LED Controller desconectado :(");
                lastState = arduinoCom.isConnected();//atualiza o ultimo estado do arduino
                conectWatch.Interval = 10000;//intervalo do watcher em ms
            }
            else if(arduinoCom.isConnected() == false && searchArduino.IsAlive == false)//realiza a pesquisa automatica caso o arduino não seja encontrado
            {
                searchArduino = new Thread(threadConection);
                searchArduino.Start();
            }
        }

        private bool charToBool(char element)
        {
            if (element == '0')
                return false;
            else if (element == '1')
                return true;
            else throw new System.ArgumentException("Parameter cannot be converted to bool.", "original");
        }

        private bool syncConfig(string controllerConf)
        {
            if (controllerConf == "")
                return false;

            string[] configs = controllerConf.Split(';');

            Properties.Settings.Default.LED1 = charToBool(Convert.ToChar(configs[0]));
            Properties.Settings.Default.LED2 = charToBool(Convert.ToChar(configs[1]));
            Properties.Settings.Default.LED3 = charToBool(Convert.ToChar(configs[2]));
            Properties.Settings.Default.LED4 = charToBool(Convert.ToChar(configs[3]));

            Properties.Settings.Default.RGB1 = charToBool(Convert.ToChar(configs[4]));
            Properties.Settings.Default.RGB2 = charToBool(Convert.ToChar(configs[8]));

            Properties.Settings.Default.R1 = Convert.ToByte(configs[5]);
            Properties.Settings.Default.G1 = Convert.ToByte(configs[6]);
            Properties.Settings.Default.B1 = Convert.ToByte(configs[7]);

            Properties.Settings.Default.R2 = Convert.ToByte(configs[9]);
            Properties.Settings.Default.G2 = Convert.ToByte(configs[10]);
            Properties.Settings.Default.B2 = Convert.ToByte(configs[11]);

            Properties.Settings.Default.Save();//salva as novas configurações

            loadConfig();

            return true;
        }

        private void bt_changeCollorRGB1_Click(object sender, EventArgs e)
        {
            colorDialog.Color = RGB1;//abre a janela com a ultima cor utilizada selecionada

            // Update the text box color if the user clicks OK 
            if (colorDialog.ShowDialog() == DialogResult.OK)
                RGB1 = colorDialog.Color;
            
        }

        private void bt_changeCollorRGB2_Click(object sender, EventArgs e)
        {
            colorDialog.Color = RGB2;//abre a janela com a ultima cor utilizada selecionada

            // Update the text box color if the user clicks OK 
            if (colorDialog.ShowDialog() == DialogResult.OK)
                RGB2 = colorDialog.Color;
        }

        private void bt_aplicar_Click(object sender, EventArgs e)
        {
            string command;

            if(arduinoCom.isConnected() == true)
            {
                Properties.Settings.Default.LED1 = cb_led1.Checked;
                Properties.Settings.Default.LED2 = cb_led2.Checked;
                Properties.Settings.Default.LED3 = cb_led3.Checked;
                Properties.Settings.Default.LED4 = cb_led4.Checked;

                Properties.Settings.Default.RGB1 = cb_rgb1.Checked;
                Properties.Settings.Default.RGB2 = cb_rgb2.Checked;

                Properties.Settings.Default.R1 = RGB1.R;
                Properties.Settings.Default.G1 = RGB1.G;
                Properties.Settings.Default.B1 = RGB1.B;

                Properties.Settings.Default.R2 = RGB2.R;
                Properties.Settings.Default.G2 = RGB2.G;
                Properties.Settings.Default.B2 = RGB2.B;

                Properties.Settings.Default.Save();//salva as novas configurações

                insertLog("Configurações Salvas!");

                insertLog("Aplicando configurações...");

                //LED's 1-4 ON/OFF
                command = "1," + Convert.ToByte(cb_led1.Checked) + ",2," + Convert.ToByte(cb_led2.Checked) + ",3," + Convert.ToByte(cb_led3.Checked) + ",4," + Convert.ToByte(cb_led4.Checked);

                //RGB cores e ON/OFF
                if (cb_rgb1.Checked == true)
                {
                    command += ",5," + RGB1.R + ","+ RGB1.G + "," + RGB1.B;
                }
                else
                {
                    command += ",5,0,0,0";
                }
                if (cb_rgb2.Checked == true)
                {
                    command +=",6," + RGB2.R + "," + RGB2.G + "," + RGB2.B;
                }
                else
                {
                    command += ",6,0,0,0";
                }
                
                arduinoCom.sendCommand(command +",112");//salva as configurações na EEP rom do arduino e aplica os comandos

                insertLog("Configurações aplicadas!");
            }
            else
                insertLog("Impossivel aplicar. Led Controller não encontrado :(");

        }

        private void bt_visualizar_Click(object sender, EventArgs e)
        {
            string command;

            if (arduinoCom.isConnected() == true)
            {
                insertLog("Testando configurações...");

                //LED's 1-4 ON/OFF
                command = "1," + Convert.ToByte(cb_led1.Checked) + ",2," + Convert.ToByte(cb_led2.Checked) + ",3," + Convert.ToByte(cb_led3.Checked) + ",4," + Convert.ToByte(cb_led4.Checked);

                //RGB cores e ON/OFF
                if (cb_rgb1.Checked == true)
                {
                    command += ",5," + RGB1.R + "," + RGB1.G + "," + RGB1.B;
                }
                else
                {
                    command += ",5,0,0,0";
                }
                if (cb_rgb2.Checked == true)
                {
                    command += ",6," + RGB2.R + "," + RGB2.G + "," + RGB2.B;
                }
                else
                {
                    command += ",6,0,0,0";
                }

                arduinoCom.sendCommand(command);//aplica os comandos

                insertLog("Teste aplicado! (Utilize o botão 'Aplicar' para salvar ou 'Cancelar' apra reverter)");
            }
            else
                insertLog("Impossivel aplicar. Led Controller não encontrado :(");
        }

        private void bt_cancelar_Click(object sender, EventArgs e)
        {
            if (arduinoCom.isConnected() == true)
            {
                loadConfig();
                arduinoCom.sendCommand("102");//pede para que o controlador retorne as configurações salvas na memoria
                insertLog("Configuração revertida!");
            }
            else
                insertLog("Impossivel reverter. Led Controller não encontrado :(");
        }

        private void bt_firmware_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Este software realiza o upload automatico para controladores baseados no Arduino Nano, caso esteja utilizando outro controlador, clique em 'Não'.", "Firmware Update", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                conectWatch.Enabled = false;
                arduinoCom.uploadFirmware();
                conectWatch.Enabled = true;
            }
            else if (dialogResult == DialogResult.No)
            {
                if (Application.OpenForms.OfType<Firmware_form>().Count() == 0)//verifica se ja existe uma aba aberta
                {
                    firmware = new Firmware_form();//instancia o formulario filho

                    //descobre a posição do form principal para centralizar o filho
                    int x = this.Left + (this.Width / 2) - (firmware.Width / 2);
                    int y = this.Top + (this.Height / 2) - (firmware.Height / 2);

                    firmware.Location = new Point(x, y);//seta a posição do formulario filho
                    firmware.Show();//exibe o formulario filho
                }
                else
                {
                    firmware.Focus();//caso a janela ja esteja aberta, foca na mesma
                }
            }            
        }

        private void bt_teste_Click(object sender, EventArgs e)
        {
            if (arduinoCom.isConnected() == true)
            {
                arduinoCom.sendCommand("200");
                insertLog("Teste iniciado! O teste ativará todas as portas intercaladamente.");
            }
            else
                insertLog("Impossivel testar. Led Controller não encontrado :(");
        }

        private void bt_sync_Click(object sender, EventArgs e)
        {
            if(arduinoCom.isConnected() == true)
            {                
                insertLog("Sincronização iniciada... (o programa pode parar de responder)");
                
                if (syncConfig(arduinoCom.getConfig()) == true)
                {
                    arduinoCom.sendCommand("102");
                    insertLog("Sincronização concluida!");
                }
                else
                {
                    insertLog("A sincronização falhou! Resposta não obtida.");
                    insertLog("Talvez o cotrolador esteja ocupado. Tente novamente em alguns segundos!");
                }
            }
            else
            {
                insertLog("Impossivel sincronizar. Controlador não encontrado :(");
            }
        }

    }
}
