
namespace BienesSalida.Share.Models
{
    public class Paginado
    {
        public List<Producto>? registros { set; get; } = new();
        public int numeroRegistros { set; get; }
        public int numeroPaginas { set; get; }
        public int paginaActual { set; get; }
    }
}
