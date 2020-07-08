using System;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSNR_o_meter
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.Run(new MainScreen());
        }
    }
}
