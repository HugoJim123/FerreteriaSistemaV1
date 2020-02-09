using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatosFerreteria;

namespace ModuloDeSeguridad
{
    public class ClaseSeguridadModulo
    {
        public void registrarSalidaSesion()
        {
            ClaseBitacoraFerreteria cpb = new ClaseBitacoraFerreteria();
            cpb.setBitacora("Cerrando Sesion");
        }
    }
}
