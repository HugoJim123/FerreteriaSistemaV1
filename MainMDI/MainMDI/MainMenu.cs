using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using MantenimientoProductos;
using LoginFerreteria;
using System.Configuration;
using ModuloDeSeguridad;
using VentasCompras;
using VentasCompras.VENTAS;
using VentasCompras.CLIENTES;
using VentasCompras.DEVOLUCIONES;
using VentasCompras.COTIZACIONES;
using VentasCompras.BI.GENERAL;

namespace MainMDI
{
    public partial class MainMDI : Form
    {
        public MainMDI()
        {
            InitializeComponent();
            if(ConfigurationManager.AppSettings["PanelState"] == "0")
            {
                minimizedmenubarstate();
            }
            /*if (sesionState == false)
            {
                getDatos();
            }*/
            if(staticSesionState == false)
            {
                stateOfSesion();
            }
        }

        // Varables estaticas
        public static string nickname;
        public static string userImg_path;
        public static string cadenaPermisos = "";
        public static bool staticSesionState;

        // Variables
        public string[] nombrebtn = new string[] { "     INICIO", "     INVENTARIO", "     COMPRAS", "     VENTAS", "     DEVOLUCIONES", "     SEGURIDAD", "     CLIENTES" };
        //public bool sesionState = false;
        public string[] permisos = new string[8];
        public bool flag3 = false;
        public bool flag2 = false;
        public bool isClosingSession = false;
        bool firstIn = true;

        #region Dlls para poder hacer el movimiento del Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        Rectangle sizeGripRectangle;
        bool inSizeDrag = false;
        const int GRIP_SIZE = 15;

        int w = 0;
        int h = 0;
        #endregion
            
        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            SendMessage(this.Handle, 0x112, 0xf012, 0);
            w = this.Width;
            h = this.Height;
        }

        private void pictureBoxUserPhoto_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath figura = new GraphicsPath();
            int x, y, ancho, alto;
            ancho = pictureBoxUserPhoto.Width;
            alto = pictureBoxUserPhoto.Height;
            x = (pictureBoxUserPhoto.Width - ancho) / 2;
            y = (pictureBoxUserPhoto.Height - alto) / 2;
            figura.AddEllipse(new RectangleF(x, y, ancho, alto));
            pictureBoxUserPhoto.Region = new Region(figura);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if(staticSesionState == true)
            {
                ClaseSeguridadModulo csm = new ClaseSeguridadModulo();
                csm.registrarSalidaSesion();
            }
            this.Close();
        }   

        private void buttonMaximized_Click(object sender, EventArgs e)
        {
            bool flag = false;
            if(panelSeguimientoBoton.Location == buttonSecurity.Location)
            {
                flag = true;
            }
            if(this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else if(this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            if(flag) moverPanelSeguimiento(buttonSecurity);
        }

        private void buttonMinimazed_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        
        // Funciones Varias ---------------------------------------------------------------------
        
        void moverPanelSeguimiento(Button btn)
        {
            panelSeguimientoBoton.Location = new Point(0, btn.Location.Y);
        }

        void cargarPanel(Form man)
        {
            man.TopLevel = false;
            man.AutoScroll = true;
            this.panelContenedor.Controls.Clear();
            this.panelContenedor.Controls.Add(man);
            man.Show();
            man.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        void moverPanelSeguimientoSubmenu(Button btn, Panel pnl)
        {
            pnl.Location = new Point(btn.Location.X, btn.Location.Y+45);
        }

        void mostrarSubpaneles(Panel pnl)
        {
            panelSubMenuInventarios.Visible = false;
            panelSubMenuCompras.Visible = false;
            panelSubMenuVentas.Visible = false;
            panelSubMenuDevoluciones.Visible = false;
            panelSubMenuUsuarios.Visible = false;
            panelSubMenuClientes.Visible = false;
            panelSubMenuDashboard.Visible = false;
            pnl.Visible = true;
        }

        // Funciones Varias ---------------------------------------------------------------------
        // Funciones de Inicio de sesion ---------------------------------------------------------------------

        public void LoadForm()
        {

        }

        public void CargarPermisos1()
        {
            if (cadenaPermisos[0] == '1')
            {
                mostrarSubpaneles(panelSubMenuDashboard);
                moverPanelSeguimiento(buttonHome);
                moverPanelSeguimientoSubmenu(button10, panelSeguimientoDashboard);
                UI_GeneralGraph man = new UI_GeneralGraph();
                cargarPanel(man);
            }
            else
                   if (cadenaPermisos[1] == '1')
            {
                mostrarSubpaneles(panelSubMenuInventarios);
                moverPanelSeguimientoSubmenu(button1, panelSeguimientoSubmenuInv);
                moverPanelSeguimiento(buttonInventarios);
                string valorMenor = ConfigurationManager.AppSettings["ValorMenor"];
                string valorMayor = ConfigurationManager.AppSettings["ValorMayor"];
                MantenimientoProductos.UI_MantenimientoProductos man = new MantenimientoProductos.UI_MantenimientoProductos(panelContenedor, button3, panelSeguimientoSubmenuInv, valorMenor, valorMayor);
                cargarPanel(man);
            }
            else
                   if (cadenaPermisos[2] == '1')
            {
                moverPanelSeguimientoSubmenu(button8, panelSeguimientoSubmenuCom);
                mostrarSubpaneles(panelSubMenuCompras);
                moverPanelSeguimiento(buttonCompras);
                string valorMenor = ConfigurationManager.AppSettings["ValorMenor"];
                UI_ConsultaCompras man = new UI_ConsultaCompras(valorMenor, panelContenedor, button7, panelSeguimientoSubmenuCom);
                cargarPanel(man);
            }
            else
                   if (cadenaPermisos[3] == '1')
            {
                moverPanelSeguimientoSubmenu(button12, panelSeguimientoSubmenuVen);
                mostrarSubpaneles(panelSubMenuVentas);
                moverPanelSeguimiento(buttonVentas);
                string valorMenor = ConfigurationManager.AppSettings["ValorMenor"];
                UI_ConsultaVentas man = new UI_ConsultaVentas(valorMenor, panelContenedor, button11, panelSeguimientoSubmenuVen);
                cargarPanel(man);
            }
            else
                   if (cadenaPermisos[4] == '1')
            {
                moverPanelSeguimientoSubmenu(button16, panelSeguimientoSubmenuDev);
                mostrarSubpaneles(panelSubMenuDevoluciones);
                moverPanelSeguimiento(buttonDevoluciones);
                string valorMenor = ConfigurationManager.AppSettings["ValorMenor"];
                UI_ConsultaDevoluciones man = new UI_ConsultaDevoluciones(valorMenor, panelContenedor, button15, panelSeguimientoSubmenuDev);
                cargarPanel(man);
            }
            else
                   if (cadenaPermisos[5] == '1')
            {
                mostrarSubpaneles(panelSubMenuClientes);
                moverPanelSeguimientoSubmenu(button19, panelSeguimientoClientes);
                moverPanelSeguimiento(buttonClientes);
                UI_ConsultaClientes man = new UI_ConsultaClientes(panelContenedor, panelSeguimientoClientes, button18);
                cargarPanel(man);
            }
            else
                   if (cadenaPermisos[6] == '1')
            {
                moverPanelSeguimientoSubmenu(buttonUsuarios, panelSeguimientoSeguridad);
                mostrarSubpaneles(panelSubMenuUsuarios);
                moverPanelSeguimiento(buttonSecurity);
                ModuloDeSeguridad.UI_IngresoDeUsuarios man = new ModuloDeSeguridad.UI_IngresoDeUsuarios(0, "0");
                cargarPanel(man);
            }
        }

        public void getDatos()
        {
            UI_InicioDeSesion man = new UI_InicioDeSesion();
            staticSesionState = man.loginProcess();
            if (staticSesionState == true)
            {
                nickname = man.getNickname();
                userImg_path = man.getImg_Path();
                cadenaPermisos = man.getPermisos();
            }
            else
            {
                staticSesionState = false;
            }
            stateOfSesion();
            if (cadenaPermisos != "" && firstIn == true)
            {
                CargarPermisos1();
                firstIn = false;
            }
        }

        public void setCerrarSesion()
        {
            isClosingSession = true;
        }

        public void stateOfSesion()
        {
            if (staticSesionState == false)
            {
                UI_InicioDeSesion man = new UI_InicioDeSesion();
                cargarPanel(man);
                mostrarSubpaneles(panelSubMenuInventarios);
                panelSubMenuInventarios.Visible = false;
                panelMenu.Visible = false;
                panelUserProfile.Visible = false;
                //MessageBox.Show(man.Name);
            }
            else if (staticSesionState == true)
            {
                labelNickname.Text = nickname;
                if (userImg_path == "" || userImg_path == " ")
                {
                    pictureBoxUserPhoto.BackgroundImage = pictureBoxUserPhoto.ErrorImage;
                }
                else
                {
                    pictureBoxUserPhoto.BackgroundImage = Image.FromFile(userImg_path);
                }
                if (cadenaPermisos == "0000000")
                {
                    panelMenu.Visible = false;
                    panelSubMenuInventarios.Visible = false;
                    panelUserProfile.Visible = true;
                    mostrarSubpaneles(panelSubMenuInventarios);
                    panelSubMenuInventarios.Visible = false;
                    UI_SinPermisos man = new UI_SinPermisos();
                    cargarPanel(man);
                }
                else
                {
                    panelMenu.Visible = true;
                    panelUserProfile.Visible = true;
                }
                obtenerPermisos();
                /*if (cadenaPermisos[0] == '1')
                {
                    mostrarSubpaneles(panelSubMenuDashboard);
                    moverPanelSeguimiento(buttonHome);
                    moverPanelSeguimientoSubmenu(button10, panelSeguimientoDashboard);
                    UI_GeneralGraph man = new UI_GeneralGraph();
                    cargarPanel(man);
                }
                else
                if (cadenaPermisos[1] == '1')
                {
                    mostrarSubpaneles(panelSubMenuInventarios);
                    moverPanelSeguimientoSubmenu(button1, panelSeguimientoSubmenuInv);
                    moverPanelSeguimiento(buttonInventarios);
                    string valorMenor = ConfigurationManager.AppSettings["ValorMenor"];
                    string valorMayor = ConfigurationManager.AppSettings["ValorMayor"];
                    MantenimientoProductos.UI_MantenimientoProductos man = new MantenimientoProductos.UI_MantenimientoProductos(panelContenedor, button3, panelSeguimientoSubmenuInv, valorMenor, valorMayor);
                    cargarPanel(man);
                }
                else
                if (cadenaPermisos[2] == '1')
                {
                    moverPanelSeguimientoSubmenu(button8, panelSeguimientoSubmenuCom);
                    mostrarSubpaneles(panelSubMenuCompras);
                    moverPanelSeguimiento(buttonCompras);
                    string valorMenor = ConfigurationManager.AppSettings["ValorMenor"];
                    UI_ConsultaCompras man = new UI_ConsultaCompras(valorMenor, panelContenedor, button7, panelSeguimientoSubmenuCom);
                    cargarPanel(man);
                }
                else
                if (cadenaPermisos[3] == '1')
                {
                    moverPanelSeguimientoSubmenu(button12, panelSeguimientoSubmenuVen);
                    mostrarSubpaneles(panelSubMenuVentas);
                    moverPanelSeguimiento(buttonVentas);
                    string valorMenor = ConfigurationManager.AppSettings["ValorMenor"];
                    UI_ConsultaVentas man = new UI_ConsultaVentas(valorMenor, panelContenedor, button11, panelSeguimientoSubmenuVen);
                    cargarPanel(man);
                }
                else
                if (cadenaPermisos[4] == '1')
                {
                    moverPanelSeguimientoSubmenu(button16, panelSeguimientoSubmenuDev);
                    mostrarSubpaneles(panelSubMenuDevoluciones);
                    moverPanelSeguimiento(buttonDevoluciones);
                    string valorMenor = ConfigurationManager.AppSettings["ValorMenor"];
                    UI_ConsultaDevoluciones man = new UI_ConsultaDevoluciones(valorMenor, panelContenedor, button15, panelSeguimientoSubmenuDev);
                    cargarPanel(man);
                }
                else
                if (cadenaPermisos[5] == '1')
                {
                    mostrarSubpaneles(panelSubMenuClientes);
                    moverPanelSeguimientoSubmenu(button19, panelSeguimientoClientes);
                    moverPanelSeguimiento(buttonClientes);
                    UI_ConsultaClientes man = new UI_ConsultaClientes(panelContenedor, panelSeguimientoClientes, button18);
                    cargarPanel(man);
                }
                else
                if (cadenaPermisos[6] == '1')
                {
                    moverPanelSeguimientoSubmenu(buttonUsuarios, panelSeguimientoSeguridad);
                    mostrarSubpaneles(panelSubMenuUsuarios);
                    moverPanelSeguimiento(buttonSecurity);
                    ModuloDeSeguridad.UI_IngresoDeUsuarios man = new ModuloDeSeguridad.UI_IngresoDeUsuarios(0, "0");
                    cargarPanel(man);
                }*/

            }
            isClosingSession = false;
        }

        // Funciones de Inicio de sesion ---------------------------------------------------------------------
        // Funciones de Permisos ---------------------------------------------------------------------

        public void obtenerPermisos()
        {
            for(int i = 0; i < cadenaPermisos.Length; i++)
            {
                permisos[i] = cadenaPermisos[i].ToString();
            }
            if (permisos[6] == "1") buttonSecurity.Visible = true;
            else buttonSecurity.Visible = false;
            if (permisos[5] == "1") buttonClientes.Visible = true;
            else buttonClientes.Visible = false;
            if (permisos[4] == "1") buttonDevoluciones.Visible = true;
            else buttonDevoluciones.Visible = false;
            if (permisos[3] == "1") buttonVentas.Visible = true;
            else buttonVentas.Visible = false;
            if (permisos[2] == "1") buttonCompras.Visible = true;
            else buttonCompras.Visible = false;
            if (permisos[1] == "1") buttonInventarios.Visible = true;
            else buttonInventarios.Visible = false;
            if (permisos[0] == "1") buttonHome.Visible = true;
            else buttonHome.Visible = false;
        }

        // Funciones de Permisos ---------------------------------------------------------------------

        private void buttonHome_Click(object sender, EventArgs e)
        {
            mostrarSubpaneles(panelSubMenuDashboard);
            moverPanelSeguimiento(buttonHome);
            moverPanelSeguimientoSubmenu(button10, panelSeguimientoDashboard);
            UI_GeneralGraph man = new UI_GeneralGraph();
            cargarPanel(man);
        }

        private void buttonInventarios_Click(object sender, EventArgs e)
        {
            mostrarSubpaneles(panelSubMenuInventarios);
            moverPanelSeguimientoSubmenu(button1, panelSeguimientoSubmenuInv);
            moverPanelSeguimiento(buttonInventarios);
            string valorMenor = ConfigurationManager.AppSettings["ValorMenor"];
            string valorMayor = ConfigurationManager.AppSettings["ValorMayor"];
            MantenimientoProductos.UI_MantenimientoProductos man = new MantenimientoProductos.UI_MantenimientoProductos(panelContenedor, button3, panelSeguimientoSubmenuInv,valorMenor,valorMayor);
            cargarPanel(man);
        }

        private void buttonCompras_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(button8, panelSeguimientoSubmenuCom);
            mostrarSubpaneles(panelSubMenuCompras);
            moverPanelSeguimiento(buttonCompras);
            string valorMenor = ConfigurationManager.AppSettings["ValorMenor"];
            UI_ConsultaCompras man = new UI_ConsultaCompras(valorMenor, panelContenedor, button7, panelSeguimientoSubmenuCom);
            cargarPanel(man);
        }

        private void buttonVentas_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(button12, panelSeguimientoSubmenuVen);
            mostrarSubpaneles(panelSubMenuVentas);
            moverPanelSeguimiento(buttonVentas);
            string valorMenor = ConfigurationManager.AppSettings["ValorMenor"];
            UI_ConsultaVentas man = new UI_ConsultaVentas(valorMenor, panelContenedor, button11, panelSeguimientoSubmenuVen);
            cargarPanel(man);
        }

        private void buttonDevoluciones_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(button16, panelSeguimientoSubmenuDev);
            mostrarSubpaneles(panelSubMenuDevoluciones);
            moverPanelSeguimiento(buttonDevoluciones);
            string valorMenor = ConfigurationManager.AppSettings["ValorMenor"];
            UI_ConsultaDevoluciones man = new UI_ConsultaDevoluciones(valorMenor, panelContenedor, button15, panelSeguimientoSubmenuDev);
            cargarPanel(man);
        }

        void minimizedmenubarstate()
        {
            panelMenu.Width = 75;
            buttonMenuHide.Visible = false;
            buttonMenuShow.Visible = true;
            foreach (Control c in panelMenu.Controls)
            {
                if (c.GetType().ToString() == "System.Windows.Forms.Button")
                {
                    c.Text = "";
                }
            }
        }

        private void buttonMenuHide_Click(object sender, EventArgs e)
        {
            minimizedmenubarstate();
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["PanelState"].Value = "0"; // Escondido 1
            config.Save(ConfigurationSaveMode.Modified);
        }

        private void buttonMenuShow_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["PanelState"].Value = "1";// Mostrado 0
            config.Save(ConfigurationSaveMode.Modified);
            buttonMenuShow.Visible = false;
            buttonMenuHide.Visible = true;
            panelMenu.Width = 250;
            buttonHome.Text = nombrebtn[0];
            buttonInventarios.Text = nombrebtn[1];
            buttonCompras.Text = nombrebtn[2];
            buttonVentas.Text = nombrebtn[3];
            buttonDevoluciones.Text = nombrebtn[4];
            buttonClientes.Text = nombrebtn[6];
            buttonSecurity.Text = nombrebtn[5];
        }

        private void MainMDI_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string valorMenor = ConfigurationManager.AppSettings["ValorMenor"];
            string valorMayor = ConfigurationManager.AppSettings["ValorMayor"];
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSubmenuInv);
            MantenimientoProductos.UI_MantenimientoProductos man = new MantenimientoProductos.UI_MantenimientoProductos(panelContenedor, button3, panelSeguimientoSubmenuInv, valorMenor,valorMayor);
            cargarPanel(man);
        }

        private void panelSubMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSubmenuInv);
            MantenimientoProductos.UI_MovimientosInventario man = new MantenimientoProductos.UI_MovimientosInventario();
            cargarPanel(man);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSubmenuInv);
            MantenimientoProductos.UI_IngresoModifiacionProductosV2 man = new MantenimientoProductos.UI_IngresoModifiacionProductosV2(0, "");
            cargarPanel(man);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSubmenuInv);
        }

        private void buttonUserDetail_Click(object sender, EventArgs e)
        {
            UserPanelDisplay us = new UserPanelDisplay();
            us.StartPosition = FormStartPosition.Manual;
            Point pt = buttonUserDetail.PointToScreen(buttonUserDetail.Location);
            us.Left = pt.X - 400;
            us.Top = pt.Y + 73;
            us.ShowDialog();
            isClosingSession = us.getState();
            if (isClosingSession == true)
            {
                staticSesionState = false;
                stateOfSesion();
            }
        }


        // Compras -------------------------------------
        private void button8_Click(object sender, EventArgs e)
        {
            string valorMenor = ConfigurationManager.AppSettings["ValorMenor"];
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSubmenuCom);
            UI_ConsultaCompras man = new UI_ConsultaCompras(valorMenor, panelContenedor, button7, panelSeguimientoSubmenuCom);
            cargarPanel(man);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSubmenuCom);
            UI_IngresoComprasv2 man = new UI_IngresoComprasv2();
            cargarPanel(man);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSubmenuCom);
            Proveedores man = new Proveedores();
            cargarPanel(man);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSubmenuCom);
            UI_VistaProveedores man = new UI_VistaProveedores();
            cargarPanel(man);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            string valorMenor = ConfigurationManager.AppSettings["ValorMenor"];
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSubmenuCom);
            UI_ConsultaCotizacion man = new UI_ConsultaCotizacion(valorMenor, panelContenedor, button21, panelSeguimientoSubmenuCom);
            cargarPanel(man);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSubmenuCom);
            UI_IngresoDeCotizaciones man = new UI_IngresoDeCotizaciones();
            cargarPanel(man);
        }

        // Ventas

        private void button12_Click(object sender, EventArgs e)
        {
            string valorMenor = ConfigurationManager.AppSettings["ValorMenor"];
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSubmenuVen);
            UI_ConsultaVentas man = new UI_ConsultaVentas(valorMenor, panelContenedor, button11, panelSeguimientoSubmenuVen);
            cargarPanel(man);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSubmenuVen);
            UI_IngresoDeVentas man = new UI_IngresoDeVentas();
            cargarPanel(man);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSubmenuVen);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSubmenuVen);
        }

        // Devoluciones ..........................

        private void button16_Click(object sender, EventArgs e)
        {
            string valorMenor = ConfigurationManager.AppSettings["ValorMenor"];
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSubmenuDev);
            UI_ConsultaDevoluciones man = new UI_ConsultaDevoluciones(valorMenor, panelContenedor, button15, panelSeguimientoSubmenuDev);
            cargarPanel(man);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSubmenuDev);
            UI_IngresoDevoluciones man = new UI_IngresoDevoluciones();
            cargarPanel(man);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSubmenuDev);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSubmenuDev);
        }

        private void MainMDI_Enter(object sender, EventArgs e)
        {

        }

        private void MainMDI_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)    
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSubmenuInv);
            MantenimientoProductos.UI_OtrasConfiguraciones man = new MantenimientoProductos.UI_OtrasConfiguraciones();
            cargarPanel(man);
        }

        private void buttonSecB2_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSeguridad);
            UI_ConsultasUsuario man = new UI_ConsultasUsuario(panelContenedor, panelSeguimientoSeguridad, buttonUsuarios);
            cargarPanel(man);
        }

        private void buttonUsuarios_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSeguridad);
            ModuloDeSeguridad.UI_IngresoDeUsuarios man = new ModuloDeSeguridad.UI_IngresoDeUsuarios(0,"0");
            cargarPanel(man);
        }

        private void buttonSecv3_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSeguridad);
            //ModuloDeSeguridad.UI_CreacionDePerfiles man = new UI_CreacionDePerfiles();
            UI_ManejoPerfiles man = new UI_ManejoPerfiles(panelContenedor, buttonSecv4, panelSeguimientoSeguridad);
            cargarPanel(man);
        }

        private void buttonSecv4_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSeguridad);
            UI_IngresoModificacionPerfiles man = new UI_IngresoModificacionPerfiles();
            cargarPanel(man);
        }

        private void buttonSecurity_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(buttonUsuarios, panelSeguimientoSeguridad);
            mostrarSubpaneles(panelSubMenuUsuarios);
            moverPanelSeguimiento(buttonSecurity);
            ModuloDeSeguridad.UI_IngresoDeUsuarios man = new ModuloDeSeguridad.UI_IngresoDeUsuarios(0, "0");
            cargarPanel(man);
        }

        private void panelContenedor_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (isClosingSession == false)
            {
                getDatos();
            }
        }

        private void MainMDI_Activated(object sender, EventArgs e)
        {
        }

        private void buttonClientes_Click(object sender, EventArgs e)
        {
            mostrarSubpaneles(panelSubMenuClientes);
            moverPanelSeguimientoSubmenu(button19, panelSeguimientoClientes);
            moverPanelSeguimiento(buttonClientes);
            UI_ConsultaClientes man = new UI_ConsultaClientes(panelContenedor, panelSeguimientoClientes, button18);
            cargarPanel(man);
            /*moverPanelSeguimiento(buttonClientes);
            UI_IngresoModifiacionProductosV2 man = new UI_IngresoModifiacionProductosV2();
            cargarPanel(man);*/
        }

        private void button19_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoClientes);
            UI_ConsultaClientes man = new UI_ConsultaClientes(panelContenedor, panelSeguimientoClientes, button18);
            cargarPanel(man);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoClientes);
            UI_IngresoClientes man = new UI_IngresoClientes();
            cargarPanel(man);
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoDashboard);
            UI_GraficasVentas man = new UI_GraficasVentas();
            cargarPanel(man);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoDashboard);
            UI_GraficasCompras man = new UI_GraficasCompras();
            cargarPanel(man);
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoDashboard);
            UI_GraficasClientes man = new UI_GraficasClientes   ();
            cargarPanel(man);
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoSeguridad);
            UI_ConsultaBitacora man = new UI_ConsultaBitacora();
            cargarPanel(man);
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoDashboard);
            UI_GeneralGraph man = new UI_GeneralGraph();
            cargarPanel(man);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            moverPanelSeguimientoSubmenu(sender as Button, panelSeguimientoClientes);
            UI_PrecioCliente man = new UI_PrecioCliente();
            cargarPanel(man);
        }
    }
}
